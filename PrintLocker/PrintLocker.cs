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
        public bool PrintingDisabled { get; set; }

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

            PrintingDisabled = true;

            jobMonitor = new Thread(new ThreadStart(monitorQueues));
            jobMonitor.IsBackground = true;
            jobMonitor.Start();
        }

        public bool CheckPassword(string password)
        {
            byte[] inputPasswordHash = computeHash(password);

            return passwordHash.SequenceEqual(inputPasswordHash);
        }

        /// <summary>
        /// Computes the SHA256 hash of the given string.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private byte[] computeHash(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);

            return mySHA256.ComputeHash(bytes);
        }

        private void monitorQueues()
        {
            LocalPrintServer server = new LocalPrintServer();

            while (true)
            {
                pauseJobs(server);

                Thread.Sleep(2000);
            }
        }

        private void pauseJobs(LocalPrintServer server)
        {
            PrintQueue queue;

            foreach (string queueName in queuesToBlock)
            {
                queue = new PrintQueue(server, queueName);
                queue.Refresh();

                foreach (PrintSystemJobInfo job in queue.GetPrintJobInfoCollection())
                {
                    job.Refresh();

                    logJob(job.JobIdentifier, job.NumberOfPages, job.TimeJobSubmitted, job.Submitter);

                    if (PrintingDisabled && !job.IsPaused && job.Submitter.Equals(Environment.UserName))
                    {
                        // showWindow();
                        job.Pause();
                        job.Refresh();
                    }
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
