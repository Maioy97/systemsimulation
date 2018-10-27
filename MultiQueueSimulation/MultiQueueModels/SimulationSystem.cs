using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            this.waiting_Costumers_count = new List<int>();
            rand = new Random();
        }

        ///////////// INPUTS ///////////// 
        public int NumberOfServers { get; set; }
        public int StoppingNumber { get; set; }
        public List<Server> Servers { get; set; }
        public List<TimeDistribution> InterarrivalDistribution { get; set; }
        public Enums.StoppingCriteria StoppingCriteria { get; set; }
        public Enums.SelectionMethod SelectionMethod { get; set; }
        public int finishtime;
        public List<int> waiting_Costumers_count;
        Random rand;
        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }
        

        public void ServerSelection(int currentCaseIndex)
        {
            List<int> available_server_index = new List<int>();
            bool wait=false;
            for (int i = 0; i < NumberOfServers; i++) //fill available_server_id
            {
                if (SimulationTable[currentCaseIndex].ArrivalTime >= Servers[i].FinishTime)
                {
                    available_server_index.Add(i);
                }
            }
            //get the servers that will finish first if all are working
            if (available_server_index.Count == 0)
            {
                wait=true;
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
                if (!wait)
                {
                    Servers[available_server_index[0]].IdleTime += SimulationTable[currentCaseIndex].ArrivalTime - Servers[available_server_index[0]].FinishTime; 
                }
                else
                {
                    for (int i = SimulationTable[currentCaseIndex].ArrivalTime; i < SimulationTable[currentCaseIndex].AssignedServer.FinishTime; i++)
                    {
                        if (i >= (waiting_Costumers_count.Count()))
                        {
                            waiting_Costumers_count.Add(1);
                        }
                        else
                            waiting_Costumers_count[i]++;
                    }
                }
            }
            else if (SelectionMethod == Enums.SelectionMethod.Random)
            {
                int randomnumber = rand.Next(0, available_server_index.Count);
                System.Threading.Thread.Sleep(randomnumber);
                SimulationTable[currentCaseIndex].AssignedServer = Servers[available_server_index[randomnumber]];
                if (!wait)
                {
                    Servers[available_server_index[0]].IdleTime += SimulationTable[currentCaseIndex].ArrivalTime - Servers[available_server_index[0]].FinishTime;
                }
                else
                {
                    for (int i = SimulationTable[currentCaseIndex].ArrivalTime; i < SimulationTable[currentCaseIndex].AssignedServer.FinishTime; i++)
                    {
                        if (i >= (waiting_Costumers_count.Count()))
                        {
                            waiting_Costumers_count.Add(1);
                        }
                        else
                            waiting_Costumers_count[i]++;
                    }
                }
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
                if (!wait)
                {
                    Servers[available_server_index[0]].IdleTime += SimulationTable[currentCaseIndex].ArrivalTime - Servers[available_server_index[0]].FinishTime;
                }
                else
                {
                    for (int i = SimulationTable[currentCaseIndex].ArrivalTime; i < SimulationTable[currentCaseIndex].AssignedServer.FinishTime; i++)
                    {
                        if (i >= (waiting_Costumers_count.Count()))
                        {
                            waiting_Costumers_count.Add(1);
                        }
                        else
                            waiting_Costumers_count[i]++;
                    }
                }
            }

        }

        void generate_cumulative_range(List<TimeDistribution> dist)
        {
            int size = dist.Count;

            //fill Cumulative column
            dist[0].CummProbability = dist[0].Probability;
            for (int i = 1; i < size; i++)
            {
                dist[i].CummProbability = dist[i - 1].CummProbability + dist[i].Probability;
            }
            //fill MinRange , MaxRange
            dist[0].MinRange = 1;
            dist[size - 1].MaxRange = 0;
            for (int i = 0; i < size; i++)
            {
                dist[i].MaxRange = Convert.ToInt32(dist[i].CummProbability * 100);
            }
            dist[0].range = Convert.ToString(dist[0].MinRange) + " - " + Convert.ToString(dist[0].MaxRange);
            for (int i = 1; i < size; i++)
            {
                dist[i].MinRange = dist[i - 1].MaxRange + 1;
                dist[i].range = Convert.ToString(dist[i].MinRange) + " - " + Convert.ToString(dist[i].MaxRange);
                
            }
        }

        public void ReadInput(string filepath)
        {
            string str;
            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader SR = new StreamReader(fs);
            //    char s = (char)SR.Read();
            while (SR.Peek() != -1)
            {
                str = SR.ReadLine();
                if (str == "NumberOfServers")
                {
                    NumberOfServers = int.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "StoppingNumber")
                {
                    StoppingNumber = int.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "StoppingCriteria")
                {
                    StoppingCriteria = (Enums.StoppingCriteria)(int.Parse(SR.ReadLine()));
                    SR.ReadLine();
                    continue;
                }
                else if (str == "SelectionMethod")
                {
                    SelectionMethod = (Enums.SelectionMethod)(int.Parse(SR.ReadLine()));
                    SR.ReadLine();
                    continue;
                }
                else if (str == "InterarrivalDistribution")
                {
                    str = SR.ReadLine();
                    while (str != "")
                    {
                        string[] substrings = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        TimeDistribution TD = new TimeDistribution();
                        TD.Time = int.Parse(substrings[0]);
                        TD.Probability = decimal.Parse(substrings[1]);
                        str = SR.ReadLine();
                        InterarrivalDistribution.Add(TD);
                    }
                    generate_cumulative_range(InterarrivalDistribution);
                    continue;
                }
                else
                {
                    for (int i = 0; i < NumberOfServers; i++)
                    {
                        Servers.Add(new Server());
                        Servers[i].ID = i + 1;
                        str = SR.ReadLine();
                        while (str != "" && str != null)
                        {
                            string[] substrings = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            TimeDistribution TD = new TimeDistribution();
                            TD.Time = int.Parse(substrings[0]);
                            TD.Probability = decimal.Parse(substrings[1]);
                            str = SR.ReadLine();
                            Servers[i].TimeDistribution.Add(TD);
                        }
                        str = SR.ReadLine();
                        continue;
                    }
                }
            }


        }

        public void calculate_total_measures()
        {
            int waitingcustumer = 0, totalwaitingtime = 0;
            for (int i = 0; i <SimulationTable.Count; i++)
            {
                if (SimulationTable[i].TimeInQueue != 0)
                {
                    waitingcustumer++;
                    totalwaitingtime += SimulationTable[i].TimeInQueue;
                }
            }
            int max=0;
            if (waiting_Costumers_count.Count >0)
              max = waiting_Costumers_count.Max();
            PerformanceMeasures.calculateMeasures(totalwaitingtime, waitingcustumer,SimulationTable.Count,max);
        }

        public void StartSimulation(string filepath)
        {
            int maxEndtime = 0;
            ReadInput(filepath);
            generate_cumulative_range(InterarrivalDistribution);
            for (int i = 0; i < NumberOfServers; i++)
            {
                generate_cumulative_range(Servers[i].TimeDistribution);
            }
            //first customer
            SimulationTable.Add(new SimulationCase());
            SimulationTable[0].CustomerNumber = 1;
            SimulationTable[0].ArrivalTime = 0;
            SimulationTable[0].RandomInterArrival = 1;
            ServerSelection(0);
            SimulationTable[0].fill_service_values();
            if (maxEndtime < SimulationTable[0].EndTime)
                maxEndtime = SimulationTable[0].EndTime;
            if (StoppingCriteria == Enums.StoppingCriteria.NumberOfCustomers)
            {
                for (int i = 1; i < StoppingNumber; i++)
                {
                    SimulationTable.Add(new SimulationCase());
                    SimulationTable[i].fill_arrival_values(SimulationTable[i - 1], InterarrivalDistribution);
                    ServerSelection(i);
                    SimulationTable[i].fill_service_values();
                    if (maxEndtime < SimulationTable[i].EndTime)
                        maxEndtime = SimulationTable[i].EndTime;
                }
                finishtime = maxEndtime;
            }
            else
            {
                int i=0;
                while(SimulationTable[i].StartTime < StoppingNumber)//change to endtime <= if this is required
                {
                    i++;
                    SimulationTable.Add(new SimulationCase());
                    SimulationTable[i].fill_arrival_values(SimulationTable[i - 1], InterarrivalDistribution);
                    ServerSelection(i);
                    SimulationTable[i].fill_service_values();
                }
                if (SimulationTable[i].EndTime >StoppingNumber)
                {
                    if (SimulationTable[i].TimeInQueue > 0)
                    {
                        waiting_Costumers_count.RemoveRange(SimulationTable[i].ArrivalTime, waiting_Costumers_count.Count - SimulationTable[i].ArrivalTime);
                    }
                    SimulationTable[i].AssignedServer.FinishTime -= SimulationTable[i].ServiceTime;
                    SimulationTable.RemoveAt(i);
                }
                finishtime = SimulationTable[SimulationTable.Count-1].EndTime;
            }

            //servers calculation
            for (int i = 0; i < NumberOfServers; i++)
            {
                int numofCustomersperserver = 0;
                for (int j = 0; j < SimulationTable.Count; j++)
                {
                    if (SimulationTable[j].AssignedServer.ID == Servers[i].ID)
                    {
                        numofCustomersperserver++;
                    }
                }
                Servers[i].IdleTime += finishtime - Servers[i].FinishTime;
                Servers[i].calculate(finishtime, numofCustomersperserver);
            }

            calculate_total_measures();
        }
    }
}
