using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using ZedGraph;

namespace MultiQueueSimulation
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {


            SimulationSystem simulation_sys = new SimulationSystem();
            int num_servers = simulation_sys.NumberOfServers;
            //made reference to a GraphPane class
            GraphPane pane = zedGraphControl1.GraphPane;

            // set titles of graph , Xaxis and Yaxis
          //  pane.Title.Text = "Server Busy Time -- Server " + server_id;
            pane.XAxis.Title.Text = "Time";
            pane.YAxis.Title.Text = "Idle OR Not";

            // set points 
            PointPairList serverpairlist = new PointPairList();

            // draw graph
           // pane.AddBar("Server" + server_id, serverpairlist, Color.DarkBlue);

            // exchange Axis
            zedGraphControl1.AxisChange();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
