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
            SimulationSystem simulation_sys = new SimulationSystem();
            simulation_sys.ReadInput("TestCase1.txt");
            num_servers.Text = simulation_sys.NumberOfServers.ToString();
            stop_criteria.Text = simulation_sys.StoppingCriteria.ToString();
            stop_num.Text = simulation_sys.StoppingNumber.ToString();
            select_method.Text = simulation_sys.SelectionMethod.ToString();

            TabPage time_dist_tp = new TabPage();
            DataGridView time_dist_grid = new DataGridView();
            time_dist_grid.DataSource = simulation_sys.InterarrivalDistribution;
            time_dist_tp.Controls.Add(time_dist_grid);

            for (int i = 0; i < simulation_sys.NumberOfServers;i++ )
            {
                TabPage server_dist_tp = new TabPage();
                DataGridView server_dist_grid = new DataGridView();
                server_dist_grid.DataSource = simulation_sys.Servers[i].TimeDistribution;
                server_dist_tp.Controls.Add(server_dist_grid);
            }

                dataGridView1.DataSource = simulation_sys.SimulationTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }

      
    }
}
