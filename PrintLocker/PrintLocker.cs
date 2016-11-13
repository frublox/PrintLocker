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
        public bool PrintingDisabled = true;

        private PrintLockerForm form;

        private List<string> queuesToBlock;
        private byte[] passwordHash;

        private Thread jobMonitor;
        private SHA256 mySHA256 = SHA256.Create();

        public PrintLocker(byte[] passwordHash, List<string> queuesToBlock, PrintLockerForm form)
        {
            this.form = form;
            this.passwordHash = passwordHash;
            this.queuesToBlock = queuesToBlock;

            jobMonitor = new Thread(new ThreadStart(monitorQueues));
            jobMonitor.IsBackground = true;
            jobMonitor.Start();
        }

        public bool CheckPassword(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = mySHA256.ComputeHash(bytes);

            return passwordHash.SequenceEqual(hash);
        }

        private void monitorQueues()
        {
            LocalPrintServer printServer = new LocalPrintServer();

            while (true)
            {
                pauseJobs(printServer);

                Thread.Sleep(100);
            }
        }

        private void pauseJobs(LocalPrintServer server)
        {
            PrintQueue queue;

            foreach (string queueName in queuesToBlock)
            {
                queue = new PrintQueue(server, queueName);

                pauseQueue(queue);
            }
        }

        /// <summary>
        /// Pauses all of the current user's jobs in the queue, provided printing is disabled
        /// </summary>
        /// <param name="queue"></param>
        private void pauseQueue(PrintQueue queue)
        {
            queue.Refresh();

            foreach (PrintSystemJobInfo job in queue.GetPrintJobInfoCollection())
            {
                job.Refresh();

                logJob(job.JobIdentifier, job.NumberOfPages, job.TimeJobSubmitted, job.Submitter);

                if (PrintingDisabled && !job.IsPaused && job.Submitter.Equals(Environment.UserName))
                {
                    job.Pause();
                    form.RestoreFromTray();
                }
                else
                {
                    job.Resume();
                }

                job.Commit();
            }

            queue.Commit();
        }

        public void ResumeLatestJob()
        {
            PrintSystemJobInfo latestJob = getLatestJob();

            if (latestJob != null)
            {
                latestJob.Resume();
                latestJob.Refresh();
            }
        }

        public void DeleteLatestJob()
        {
            PrintSystemJobInfo latestJob = getLatestJob();

            if (latestJob != null)
            {
                latestJob.Cancel();
                latestJob.Refresh();
            }
        }

        /// <summary>
        /// Gets the most recently submitted job
        /// </summary>
        /// <returns>The most recent job, or null if there are no jobs</returns>
        private PrintSystemJobInfo getLatestJob()
        {
            PrintQueue queue;
            DateTime latestJobTime = DateTime.Now;
            PrintSystemJobInfo latestJob = null;

            foreach (string queueName in queuesToBlock)
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

        public void ResumeJobs()
        {
            PrintQueue queue;

            foreach (string queueName in queuesToBlock)
            {
                queue = new PrintQueue(printServer, queueName);
                queue.Refresh();

                foreach (var job in queue.GetPrintJobInfoCollection())
                {
                    job.Refresh();
                    job.Resume();
                    job.Refresh();
                }
            }
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
