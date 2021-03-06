﻿using System;
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
            this.FinishTime = 0;
            this.IdleTime = 0;
            this.FinishTime = 0;
            this.graphData = new List<bool>();
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
        public int IdleTime { get; set; }
        public int totalServiceTime { get; set; }

        public List<bool> graphData;

        // function to calculate IdleProbability,AverageServiceTime and Utilization
        public void calculate(int totalRunTime, int totalNumOfCus)
        {
            this.totalServiceTime = totalRunTime - IdleTime;

            this.IdleProbability = (decimal)IdleTime / totalRunTime;
            this.AverageServiceTime = 0;
            if (totalNumOfCus != 0)
            {
                this.AverageServiceTime = (decimal)totalServiceTime / totalNumOfCus;//must be total number of customers served 
            }
            this.Utilization = (decimal)totalServiceTime / totalRunTime;
        }

       
    }
}
