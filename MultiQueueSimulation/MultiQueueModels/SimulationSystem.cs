using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            this.Servers = new List<Server>();
            this.InterarrivalDistribution = new List<TimeDistribution>();
            this.PerformanceMeasures = new PerformanceMeasures();
            this.SimulationTable = new List<SimulationCase>();
        }

        ///////////// INPUTS ///////////// 
        public int NumberOfServers { get; set; }
        public int StoppingNumber { get; set; }
        public List<Server> Servers { get; set; }
        public List<TimeDistribution> InterarrivalDistribution { get; set; }
        public Enums.StoppingCriteria StoppingCriteria { get; set; }
        public Enums.SelectionMethod SelectionMethod { get; set; }

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }

        public void ServerSelection(int currentCaseIndex)
        {
            List<int> available_server_index = new List<int>();
            
            for (int i = 0; i < NumberOfServers; i++) //fill available_server_id
            {
                if (SimulationTable[currentCaseIndex].ArrivalTime <= Servers[i].FinishTime)
                {
                    available_server_index.Add(i);
                }
            }
            //get the servers that will finish first if all are working
            if (available_server_index.Count == 0)
            {
                int min_finish_time = Servers[0].FinishTime;
                available_server_index.Add(0);
                //search for minimum finish time
                for (int i = 0; i < NumberOfServers; i++) //fill available_server_id
                {
                    if (min_finish_time > Servers[i].FinishTime)//current server will finish earlier 
                    {
                        min_finish_time = Servers[i].FinishTime;
                        available_server_index.Clear();
                        available_server_index.Add(i);
                    }
                    else if (min_finish_time == Servers[i].FinishTime)
                    {
                        available_server_index.Add(i);
                    }
                }
            }

            //based on the selection methode chose a server from the available servers
            if (SelectionMethod == Enums.SelectionMethod.HighestPriority)
            {
                SimulationTable[currentCaseIndex].AssignedServer = Servers[available_server_index[0]];
            }
            else if (SelectionMethod == Enums.SelectionMethod.Random)
            {
                Random rand = new Random();
                int randomnumber = rand.Next(0, available_server_index.Count);
                SimulationTable[currentCaseIndex].AssignedServer = Servers[available_server_index[randomnumber]];
            }
            else if (SelectionMethod == Enums.SelectionMethod.LeastUtilization)
            {
                int max_idel_server_index = 0;
                //search for the server of max idel time (LeastUtilization)
                for (int i = 0; i < available_server_index.Count; i++) //fill available_server_id
                {
                    if (Servers[available_server_index[max_idel_server_index]].IdleTime < Servers[available_server_index[i]].IdleTime)
                    {
                        max_idel_server_index = i;
                    }
                }
                SimulationTable[currentCaseIndex].AssignedServer = Servers[available_server_index[max_idel_server_index]];
            }

        }

        List<TimeDistribution> generate_cumulative_range(List<int> time_col, List<decimal> probability_col)
        {
            int size = time_col.Count;

            //fill time column
            for (int i = 0; i < size; i++)
            {
                InterarrivalDistribution[i].Time = time_col[i];
                InterarrivalDistribution[i].Probability = probability_col[i];
            }
            //fill Cumulative column
            InterarrivalDistribution[0].CummProbability = probability_col[0];
            for (int i = 1; i < size; i++)
            {
                InterarrivalDistribution[i].CummProbability = InterarrivalDistribution[i - 1].CummProbability + probability_col[i];
            }
            //fill MinRange , MaxRange
            InterarrivalDistribution[0].MinRange = 1;
            InterarrivalDistribution[size - 1].MaxRange = 0;
            for (int i = 0; i < size - 1; i++)
            {
                InterarrivalDistribution[i].MaxRange = Convert.ToInt32(InterarrivalDistribution[i].CummProbability * 100);
            }
            for (int i = 1; i < size; i++)
            {
                InterarrivalDistribution[i].MinRange = InterarrivalDistribution[i - 1].MaxRange + 1;
            }
            return InterarrivalDistribution;
        }

    }
}
