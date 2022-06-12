namespace EzPing.Core.Networking.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.ServiceProcess;

    public static class BitsController
    {
        [Nullable((byte) 1)]
        private static readonly ServiceController controller = new ServiceController("BITS");
        private static bool Stat = false;

        public static bool isRunning() => 
            (controller.Status == ServiceControllerStatus.Running) || (controller.Status == ServiceControllerStatus.StartPending);

        public static void SetStat(bool stat)
        {
            Stat = stat;
            if (Stat)
            {
                Start();
            }
            else
            {
                Stop();
            }
            Console.WriteLine("set to :" + Stat.ToString());
        }

        public static void Start()
        {
            try
            {
                if (!isRunning())
                {
                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 15));
                    Stat = true;
                    Process[] processesByName = Process.GetProcessesByName("svchost");
                    for (int i = 0; i < processesByName.Length; i++)
                    {
                        processesByName[i].PriorityClass = ProcessPriorityClass.RealTime;
                    }
                }
            }
            catch (Exception exception)
            {
                string[] contents = new string[] { exception.Message };
                File.AppendAllLines("./debug", contents);
            }
        }

        public static void Stop()
        {
            try
            {
                if (isRunning())
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 15));
                    Stat = false;
                }
            }
            catch (Exception exception)
            {
                string[] contents = new string[] { exception.Message };
                File.AppendAllLines("./debug", contents);
            }
        }
    }
}

