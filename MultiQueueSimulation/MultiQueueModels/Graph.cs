using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;


namespace MultiQueueModels
{
    class Graph
    {
        
        private ZedGraph.ZedGraphControl zedGraphControl1;
        public GraphPane pane { get; set; }
        public void build_graph(int server_id)
        {
            //made reference to a GraphPane class
            pane = zedGraphControl1.GraphPane;
            // set titles of graph , Xaxis and Yaxis
            pane.Title.Text = "Server Busy Time -- Server " + server_id;
            pane.XAxis.Title.Text = "Time";
            pane.YAxis.Title.Text = "Idle OR Not";

            // set points 
            PointPairList serverpairlist = new PointPairList();

            // draw graph
            pane.AddBar("Server" + server_id, serverpairlist, Color.DarkBlue);

            // exchange Axis
            zedGraphControl1.AxisChange();

           
        }

        
    }
}
