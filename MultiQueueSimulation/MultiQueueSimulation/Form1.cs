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
            button2.Enabled = false;
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
            string testcase = Constants.FileNames.TestCase3;
            simulation_sys.StartSimulation(testcase);
            num_servers.Text = simulation_sys.NumberOfServers.ToString();
            stop_criteria.Text = simulation_sys.StoppingCriteria.ToString();
            stop_num.Text = simulation_sys.StoppingNumber.ToString();
            select_method.Text = simulation_sys.SelectionMethod.ToString();

            //----------------- fill the tab

            //fill tab[0] with Inter arrival distribution table
            TabPage time_dist_tp = new TabPage();
            time_dist_tp.Size = tabControl1.Size;
            time_dist_tp.Text = "Inter Arrival Distribution";
            DataGridView time_dist_grid = new DataGridView();
            time_dist_grid.DataSource = simulation_sys.InterarrivalDistribution;
            time_dist_tp.Controls.Add(time_dist_grid);
            tabControl1.TabPages.Add(time_dist_tp);
            time_dist_grid.Columns.Remove("MinRange");
            time_dist_grid.Columns.Remove("MaxRange");
            time_dist_grid.Size = time_dist_tp.Size;

            //fill the remaining tabs with Service time distribution 
            for (int i = 0; i < simulation_sys.NumberOfServers;i++ )
            {
                TabPage server_dist_tp = new TabPage();
                server_dist_tp.Size = tabControl1.Size;
                server_dist_tp.Text = "Server" + (i + 1);
                DataGridView server_dist_grid = new DataGridView();
                server_dist_grid.DataSource = simulation_sys.Servers[i].TimeDistribution;
                server_dist_tp.Controls.Add(server_dist_grid);
                tabControl1.TabPages.Add(server_dist_tp);
                server_dist_grid.Columns.Remove("MinRange");
                server_dist_grid.Columns.Remove("MaxRange");
                server_dist_grid.Size = server_dist_tp.Size;

                //Show Performance Measures for each server
                TabPage server_PM_tp = new TabPage();
                server_PM_tp.Size = tabControl2.Size;
                server_PM_tp.Text = "Server" + (i + 1);
                Label Idle_Pro = new Label();
                Idle_Pro.Text = "IdleProbability";
                Label Idle_Pro_val = new Label();
                Idle_Pro_val.Text = simulation_sys.Servers[i].IdleProbability.ToString();
                Label utilization = new Label();
                utilization.Text = "Utilization";
                Label utilization_val = new Label();
                utilization_val.Text = simulation_sys.Servers[i].Utilization.ToString();
                Label averageServiceTime = new Label();
                averageServiceTime.Text = "AverageServiceTime";
                Label averageServiceTime_val = new Label();
                averageServiceTime_val.Text = simulation_sys.Servers[i].AverageServiceTime.ToString();
                TableLayoutPanel table_panel = new TableLayoutPanel();
                table_panel.ColumnCount = 2;
                table_panel.RowCount = 3;
                table_panel.Controls.Add(Idle_Pro);
                table_panel.Controls.Add(Idle_Pro_val);
                table_panel.Controls.Add(utilization);
                table_panel.Controls.Add(utilization_val);
                table_panel.Controls.Add(averageServiceTime);
                table_panel.Controls.Add(averageServiceTime_val);
                server_PM_tp.Controls.Add(table_panel);
                tabControl2.TabPages.Add(server_PM_tp);
            }

            //Show Performance Measures
            label13.Text = simulation_sys.PerformanceMeasures.AverageWaitingTime.ToString();
            label12.Text = simulation_sys.PerformanceMeasures.MaxQueueLength.ToString();
            label11.Text = simulation_sys.PerformanceMeasures.WaitingProbability.ToString();

            


            
            //Show OUTPUT!!!
            dataGridView1.DataSource = simulation_sys.SimulationTable;
            dataGridView1.Columns.RemoveAt(6);//to remove the assigned server object
            MessageBox.Show(testcase+"\n"+TestingManager.Test(simulation_sys, testcase));
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // close this form and move to graph form
            Form2 form2 = new Form2(simulation_sys);
            form2.Show();
        }

        private void stop_criteria_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

      
    }
}
