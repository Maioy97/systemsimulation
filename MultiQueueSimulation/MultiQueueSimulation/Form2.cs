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
            //graph data idle or not
            int idle;

            SimulationSystem simulation_sys = new SimulationSystem();
            int num_servers = simulation_sys.NumberOfServers;

            
            for(int i=0; i<simulation_sys.NumberOfServers; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "Server" + (i + 1);
                ZedGraphControl zedgraph = new ZedGraphControl();
                zedgraph.ClientSize = tabControl1.Size;
                //made reference to a GraphPane class
                GraphPane pane = zedgraph.GraphPane;

                // set titles of graph , Xaxis and Yaxis
                pane.Title.Text = "Server Busy Time -- Server " + i+1;
                pane.XAxis.Title.Text = "Time";
                pane.YAxis.Title.Text = "Idle OR Not";

                // set points 
                PointPairList serverpairlist = new PointPairList();
                for(int j=0 ;j<simulation_sys.Servers[i].graphData.Count; j++ )
                {
                    if(simulation_sys.Servers[i].graphData[j])
                        idle=1;
                    else
                        idle=0;
                    serverpairlist.Add(j, idle);
                }
                
                

                // draw graph
                pane.AddBar("Server" + i+1, serverpairlist, Color.DarkBlue);

                // exchange Axis
                zedgraph.AxisChange();
                tp.Controls.Add(zedgraph);
                tabControl1.TabPages.Add(tp);
            }
        
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
