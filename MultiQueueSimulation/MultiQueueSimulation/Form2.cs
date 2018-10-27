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
        SimulationSystem simulation_sys1 ;
        public Form2(SimulationSystem simulation_sys)
        {
            InitializeComponent();
            simulation_sys1 = simulation_sys;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //graph data idle or not
            int idle;

            
            int num_servers = simulation_sys1.NumberOfServers;

            
            for(int i=0; i<simulation_sys1.NumberOfServers; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "Server" + Convert.ToString(i + 1);
                ZedGraphControl zedgraph = new ZedGraphControl();
                zedgraph.ClientSize = tabControl1.Size;
                //made reference to a GraphPane class
                GraphPane pane = zedgraph.GraphPane;

                // set titles of graph , Xaxis and Yaxis
                pane.Title.Text = "Server Busy Time -- Server " + Convert.ToString(i + 1);
                pane.XAxis.Title.Text = "Time";
                pane.YAxis.Title.Text = "Idle OR Not";

                // set points 
                PointPairList serverpairlist = new PointPairList();
                for(int j=0 ;j<simulation_sys1.Servers[i].graphData.Count; j++ )
                {
                    if(simulation_sys1.Servers[i].graphData[j])
                        idle=1;
                    else
                        idle=0;
                    serverpairlist.Add(j, idle);
                }
                
                

                // draw graph
                pane.AddBar("Server" + Convert.ToString(i+1), serverpairlist, Color.DarkBlue);

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
