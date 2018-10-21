using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MultiQueueModels
{
    public class SimulationCase
    {
        public SimulationCase()
        {
            this.AssignedServer = new Server();
            this.TimeInQueue = 0;
        }

        public int CustomerNumber { get; set; }
        public int RandomInterArrival { get; set; }
        public int InterArrival { get; set; }
        public int ArrivalTime { get; set; }
        public int RandomService { get; set; }
        public int ServiceTime { get; set; }
        public Server AssignedServer { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int TimeInQueue { get; set; }

        //-----------------------------------
        
        //choice 1 : interarrival
        //choice 2 : servicetime
        //givin the distro table and a choice it gives a service time/interarrival time  
        void get_time(List<TimeDistribution> distro_table,int choice) 
        {
            Random rand=new Random();
            int randomnumber = rand.Next(1, 101);
            int time = 0;
            for (int i = 0; i < distro_table.Count(); i++)
            {
                if (randomnumber <= distro_table[i].MaxRange && randomnumber >= distro_table[i].MinRange)
                { time = distro_table[i].Time; }
            }
            if (choice == 1)
            {
                RandomInterArrival=randomnumber;
                InterArrival = time;
            }
            if (choice == 2)
            {
                RandomService = randomnumber;
                ServiceTime = time;
            }
        }
        public void fill_arrival_values(SimulationCase prev_case,List<TimeDistribution> distro_table) 
        {
            this.CustomerNumber = prev_case.CustomerNumber + 1;
            get_time(distro_table, 1);//RandomInterArrival&InterArrival filled
            this.ArrivalTime = prev_case.ArrivalTime + this.InterArrival;
        }
        public void fill_service_values()
        {
            get_time(this.AssignedServer.TimeDistribution, 2);//RandomService&ServiceTime filled
            //StartTime&EndTime
            if (ArrivalTime > AssignedServer.FinishTime)
            {
                StartTime = ArrivalTime;
                this.TimeInQueue = 0;
            }
            else
            {
                StartTime = AssignedServer.FinishTime;
                this.TimeInQueue = StartTime - ArrivalTime;
            }
            this.EndTime = StartTime + ServiceTime;
            AssignedServer.FinishTime = EndTime;
        }
        /*public void fill_values(SimulationCase prev_case, List<TimeDistribution> distro_table)
        {
            this.CustomerNumber = prev_case.CustomerNumber + 1;
            get_time(distro_table, 1);//RandomInterArrival&InterArrival filled
            this.ArrivalTime = prev_case.ArrivalTime + this.InterArrival;
            get_time(distro_table, 2);//RandomService&ServiceTime filled
            //StartTime&EndTime
            if (ArrivalTime > AssignedServer.FinishTime)
            {
                StartTime = ArrivalTime;
                this.TimeInQueue = 0;
            }
            else
            {
                StartTime = AssignedServer.FinishTime;
                this.TimeInQueue = StartTime - ArrivalTime;
            }
            this.EndTime = StartTime + ServiceTime;
            AssignedServer.FinishTime = EndTime;
        }*/
    }

}
