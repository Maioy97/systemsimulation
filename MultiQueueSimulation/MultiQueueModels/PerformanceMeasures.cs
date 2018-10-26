using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class PerformanceMeasures
    {
        public decimal AverageWaitingTime { get; set; }
        public int MaxQueueLength { get; set; }
        public decimal WaitingProbability { get; set; }

        public void calculateMeasures(int totalWaitingTime, int totalNumOFWaitedCus, int totalNumOFCus, int MaxQueue)
        {
            this.AverageWaitingTime = (decimal)totalWaitingTime / totalNumOFCus;
            this.WaitingProbability = (decimal)totalNumOFWaitedCus / totalNumOFCus;
            this.MaxQueueLength = MaxQueue;
        }
    }
}
