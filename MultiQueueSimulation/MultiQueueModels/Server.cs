using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class Server
    {
        public Server()
        {
            this.TimeDistribution = new List<TimeDistribution>();
        }

        public int ID { get; set; }
        public decimal IdleProbability { get; set; }
        public decimal AverageServiceTime { get; set; }
        public decimal Utilization { get; set; }

        public List<TimeDistribution> TimeDistribution;

        //optional if needed use them
        public int FinishTime { get; set; }
        public int TotalWorkingTime { get; set; }

        // New Definitions 
        public int idelTime { get; set; }
        public int totalServiceTime { get; set; }

        // function to calculate IdleProbability,AverageServiceTime and Utilization
        public void calculate(int totalRunTime, int totalNumOfCus)
        {
            this.totalServiceTime = totalRunTime - idelTime;

            this.IdleProbability = idelTime / totalRunTime;
            this.AverageServiceTime = totalServiceTime / totalNumOfCus;
            this.Utilization = totalServiceTime / totalRunTime;
        }

        // function to generate graph NOt implemented
        public void generateGraph() { }
    }
}
