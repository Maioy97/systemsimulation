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

        public void calculate(int totalWaitingTime, int totalWaitingNumOFCus, int totalNumOFCus)
        {
            this.AverageWaitingTime = totalWaitingTime / totalNumOFCus;
            this.WaitingProbability = totalWaitingNumOFCus / totalNumOFCus;
        }
    }
}
