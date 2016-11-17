using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Printing;
using System.Threading;
using System.IO;

namespace PrintLocker
{
    class PrintLocker
    {
        private bool printingDisabled = true;

        private PrintLockerForm form;

        private List<string> queueNamesToBlock;
        private LocalPrintServer printServer = new LocalPrintServer();

        private byte[] passwordHash;
        private SHA256 mySHA256 = SHA256.Create();

        /// <summary>
        /// Spawns new thread that will begin monitoring all blocked printer queues
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <param name="queueNamesToBlock"></param>
        /// <param name="form">A reference to the WinForm gui</param>
        public PrintLocker(byte[] passwordHash, List<string> queueNamesToBlock, PrintLockerForm form)
        {
            this.form = form;
            this.passwordHash = passwordHash;
            this.queueNamesToBlock = queueNamesToBlock;

            Thread queueMonitor = new Thread(() => monitorQueues());
            queueMonitor.IsBackground = true;
            queueMonitor.Start();
        }

        /// <summary>
        /// Compares the input password's hash to the configured password's hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = mySHA256.ComputeHash(bytes);

            return passwordHash.SequenceEqual(hash);
        }

        /// <summary>
        /// Temporarily allows the most recent print job to go through
        /// </summary>
        public void AllowPrintJob()
        {
            printingDisabled = false;
            resumeLastJob();

            Thread.Sleep(500);

            printingDisabled = true;
        }

        private void monitorQueues()
        {
            PrintQueue queue;

            while (true)
            {
                if (printingDisabled)
                {
                    foreach (string queueName in queueNamesToBlock)
                    {
                        queue = new PrintQueue(printServer, queueName);
                        pauseAllJobs(queue);
                    }
                }

                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Pauses all of the current user's jobs in the queue, provided printing is disabled
        /// </summary>
        /// <param name="queue"></param>
        private void pauseAllJobs(PrintQueue queue)
        {
            queue.Refresh();

            foreach (PrintSystemJobInfo job in queue.GetPrintJobInfoCollection())
            {
                job.Refresh();

                logJob(job.JobIdentifier, job.NumberOfPages, job.TimeJobSubmitted, job.Submitter);
                pauseJob(job);
            }

            queue.Commit();
        }

        private void pauseJob(PrintSystemJobInfo job)
        {
            if (!job.IsPaused && job.Submitter.Equals(Environment.UserName))
            {
                job.Pause();
                form.RestoreFromTray();
            }
        }

        private void resumeAllJobs(PrintQueue queue)
        {
            queue.Refresh();

            foreach (var job in queue.GetPrintJobInfoCollection())
            {
                job.Refresh();
                job.Resume();
            }

            queue.Commit();
        }

        private void resumeLastJob()
        {
            PrintSystemJobInfo lastJob = getLastJob();

            if (lastJob != null)
            {
                lastJob.Resume();
                lastJob.Refresh();
            }
        }

        public void DeleteLastJob()
        {
            PrintSystemJobInfo lastJob = getLastJob();

            if (lastJob != null)
            {
                lastJob.Cancel();
                lastJob.Refresh();
            }
        }

        /// <summary>
        /// Gets the most recently submitted job
        /// </summary>
        /// <returns>The most recent job, or null if there are no jobs</returns>
        private PrintSystemJobInfo getLastJob()
        {
            DateTime latestJobTime = DateTime.Now;
            PrintSystemJobInfo latestJob = null;

            PrintQueue queue;
            foreach (string queueName in queueNamesToBlock)
            {
                queue = new PrintQueue(printServer, queueName);
                queue.Refresh();

                foreach (var job in queue.GetPrintJobInfoCollection())
                {
                    job.Refresh();

                    var time = job.TimeJobSubmitted;

                    if (time > latestJobTime)
                    {
                        latestJobTime = time;
                        latestJob = job;
                    }
                }
            }

            return latestJob;
        }

        private void logJob(int jobId, int numPages, DateTime timestamp, string user)
        {
            string contents = File.ReadAllText(Prefs.LogFilepath);

            if (!contents.Contains("ID#: " + jobId.ToString()))
            {
                string logLine = timestamp.ToShortDateString() + " " + timestamp.ToLongTimeString() +
                    ", ID#: " + jobId.ToString() + ", User: " + user + ", Pages: " + numPages.ToString() + "\n";

                File.AppendAllText(Prefs.LogFilepath, logLine);
            }
        }
    }
}
