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
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class Form1 : Form
    {
        SimulationSystem simulation_sys = new SimulationSystem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // fill inputs
            
            simulation_sys.StartSimulation("TestCase1.txt");
            num_servers.Text = simulation_sys.NumberOfServers.ToString();
            stop_criteria.Text = simulation_sys.StoppingCriteria.ToString();
            stop_num.Text = simulation_sys.StoppingNumber.ToString();
            select_method.Text = simulation_sys.SelectionMethod.ToString();

            //----------------- fill the tab

            //fill tab[0] with Inter arrival distribution table
            TabPage time_dist_tp = new TabPage();
            time_dist_tp.Text = "Inter Arrival Distribution";
            DataGridView time_dist_grid = new DataGridView();
            time_dist_grid.DataSource = simulation_sys.InterarrivalDistribution;
            time_dist_tp.Controls.Add(time_dist_grid);
            tabControl1.TabPages.Add(time_dist_tp);

            //fill the remaining tabs with Service time distribution 
            for (int i = 0; i < simulation_sys.NumberOfServers;i++ )
            {
                TabPage server_dist_tp = new TabPage();
                server_dist_tp.Text = "Server" + (i + 1);
                DataGridView server_dist_grid = new DataGridView();
                server_dist_grid.DataSource = simulation_sys.Servers[i].TimeDistribution;
                server_dist_tp.Controls.Add(server_dist_grid);
                tabControl1.TabPages.Add(server_dist_tp);
            }

            
            
            //Show OUTPUT!!!
            dataGridView1.DataSource = simulation_sys.SimulationTable;
            dataGridView1.Columns.RemoveAt(6);//to remove the assigned server object


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // close this form and move to graph form
            Form2 form2 = new Form2(simulation_sys);
            form2.Show();
        }

      
    }
}
