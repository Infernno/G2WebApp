using System;
using System.Diagnostics;
using Debug = G2WebApp.Core.Util.Debug;

namespace G2WebApp.Core.BackgroundTasks.Tasks
{
    public class StatsTask : Job
    {
        public override bool IsRepeatable => true;
        public override TimeSpan RepetitionIntervalTime => TimeSpan.FromMinutes(1);

        private PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time",
            Process.GetCurrentProcess().ProcessName);

        private PerformanceCounter memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

        private Process process = Process.GetCurrentProcess();

        public override void Action()
        {
            Debug.Log("Current CPU usage: {0} %", cpuCounter.NextValue().ToString("0.000"));
            Debug.Log("Current memory usage: {0} MB ({1} MB free)", process.WorkingSet64 / 1024 / 1024, memoryCounter.NextValue());
        }
    }
}