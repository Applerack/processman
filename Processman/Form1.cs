using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Processman
{
    public partial class Form1 : Form
    {
        RunningApplicationNode current;
        ProcessManager processManager;
        RunningApplicationLinkedList runningApps;
        public Form1()
        {
            InitializeComponent();
            timer.Start();
            startup(); 


           

        }
        

        public void startup()
        {
            processManager = new ProcessManager();
            runningApps = processManager.GetRunningUserApplicationsInfo();
            current = runningApps.GetHead();
            startprint();
            findmaxusers();
            findminusers();
            printList(runningApps);
        }
        
        public void findmaxusers()
        {
            maximumcpulb.Text = processManager.findmaxCpuUsr(runningApps).Data.ProcessName;
            maxramlb.Text = processManager.findmaxRamUsr(runningApps).Data.ProcessName;

        }

        public void findminusers()
        {
            lowestcpulb.Text = processManager.findminCpuUsr(runningApps).Data.ProcessName;
            lowestramlb.Text = processManager.findminRamUsr(runningApps).Data.ProcessName;
            
        }

        private void startprint()
        {
            if (current != null)
            {
                lb1.Text = current.Data.ProcessName;
                lb2.Text = current.Data.MainWindowTitle;
                lb3.Text = current.Data.ProcessId.ToString();
                lb4.Text = current.Data.StartTime.ToString();
                lb5.Text = current.Data.TotalProcessTime.ToString();
                lb6.Text = current.Data.TotalUserTime.ToString();
                lb7.Text = current.Data.HandleCount.ToString();
                lb8.Text = current.Data.ThreadCount.ToString();
                cpuprogressbar.Value = (int)current.Data.CPUsagePercent;
                memoryprogressbar.Value = (int)current.Data.RAMUsagePercent;
                lb9.Text = current.Data.RAMUsageValue + " MB";
              
            }
        }


        private void printList(RunningApplicationLinkedList runningApps)
        {
            processlist.View = View.Details;
            processlist.GridLines = true;

            // Add columns to the ListView for each data property
            processlist.Columns.Add("Process Name", 100);
            processlist.Columns.Add("Main Window Title", 150);
            processlist.Columns.Add("Process ID", 80);
            processlist.Columns.Add("Start Time", 120);
            processlist.Columns.Add("Total Process Time", 150);
            processlist.Columns.Add("Total User Time", 150);
            processlist.Columns.Add("Handle Count", 80);
            processlist.Columns.Add("Thread Count", 80);
            processlist.Columns.Add("CPU Usage (%)", 100);
            processlist.Columns.Add("RAM Usage (%)", 100);

            RunningApplicationNode current = runningApps.GetHead();

            // Iterate through the linked list and populate the ListView
            while (current != null)
            {
                ListViewItem item = new ListViewItem(new[]
                {
            current.Data.ProcessName,
            current.Data.MainWindowTitle,
            current.Data.ProcessId.ToString(),
            current.Data.StartTime.ToString(),
            current.Data.TotalProcessTime.ToString(),
            current.Data.TotalUserTime.ToString(),
            current.Data.HandleCount.ToString(),
            current.Data.ThreadCount.ToString(),
            ((int)current.Data.CPUsagePercent).ToString(),
            ((int)current.Data.RAMUsagePercent).ToString()
        });

                processlist.Items.Add(item);

                current = current.Next;
            }
        }


        private void siticoneHtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            lb1.Text = "fuck u";
        }

        private void siticoneCustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void proceenamelb_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void siticoneHtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void siticoneHtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void siticoneButton1_Click_1(object sender, EventArgs e)
        {
            processlist.Items.Add("test");
        }

        private void siticoneHtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void siticoneGradientButton1_Click(object sender, EventArgs e)
        {
            string name = processManager.findmaxRamUsr(runningApps).Data.ProcessName.ToString();
            processManager.KillprocessByName(name);
            processManager.DeleteProcess(processManager.findmaxCpuUsr(runningApps), runningApps);
            startup();
            MessageBox.Show("Succesfully aborted process : " + name);
        }

        private void siticoneButton1_Click_2(object sender, EventArgs e)
        {

        

           



        }

        private void siticoneImageButton2_Click(object sender, EventArgs e)
        {
            nextbutton.Enabled = false;
       
           if(current != null)
            {
                if(current.Next != null)
                {
                    current = current.Next;
                    lb1.Text = current.Data.ProcessName;
                    lb2.Text = current.Data.MainWindowTitle;
                    lb3.Text = current.Data.ProcessId.ToString();
                    lb4.Text = current.Data.StartTime.ToString();
                    lb5.Text = current.Data.TotalProcessTime.ToString();
                    lb6.Text = current.Data.TotalUserTime.ToString();
                    lb7.Text = current.Data.HandleCount.ToString();
                    lb8.Text = current.Data.ThreadCount.ToString();
                    cpuprogressbar.Value = (int)current.Data.CPUsagePercent;
                    memoryprogressbar.Value = (int)current.Data.RAMUsagePercent;
                    lb9.Text = current.Data.RAMUsageValue + " MB";
                }
                else
                {
                    MessageBox.Show(" END OF DOUBLY LINKED LIST");

                }

            }

            else
            {
                MessageBox.Show(" END OF DOUBLY LINKED LIST");
            }

            nextbutton.Enabled = true;

          
        }

        private void siticoneImageButton1_Click(object sender, EventArgs e)
        {

            priviousbutton.Enabled = false;
            if (current != null)
            {
                if (current.Previous != null)
                {
                    current = current.Previous;
                    lb1.Text = current.Data.ProcessName;
                    lb2.Text = current.Data.MainWindowTitle;
                    lb3.Text = current.Data.ProcessId.ToString();
                    lb4.Text = current.Data.StartTime.ToString();
                    lb5.Text = current.Data.TotalProcessTime.ToString();
                    lb6.Text = current.Data.TotalUserTime.ToString();
                    lb7.Text = current.Data.HandleCount.ToString();
                    lb8.Text = current.Data.ThreadCount.ToString();
                    cpuprogressbar.Value = (int)current.Data.CPUsagePercent;
                    memoryprogressbar.Value = (int)current.Data.RAMUsagePercent;
                    lb9.Text = current.Data.RAMUsageValue + " MB";
                }
                else
                {
                    MessageBox.Show(" END OF DOUBLY LINKED LIST");

                }


            }

            else
            {
                MessageBox.Show(" END OF DOUBLY LINKED LIST");
            }
            priviousbutton.Enabled = true;

        }

     

        private void siticoneGradientButton6_Click(object sender, EventArgs e)
        {
            processManager.BubbleSortByRam(runningApps);
            MessageBox.Show("SUCCESFULLY SORTED BY RAM USAGE !", "info ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siticoneGradientButton7_Click(object sender, EventArgs e)
        {
            processManager.BubbleSortID(runningApps);
            MessageBox.Show("SUCCESFULLY SORTED BY PROCESS ID !", "info ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void siticoneGradientButton8_Click(object sender, EventArgs e)
        {
            processManager.BubbleSortBypu(runningApps);
            MessageBox.Show("SUCCESFULLY SORTED BY CPU USAGE !", "info ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siticoneGradientButton3_Click(object sender, EventArgs e)
        {
         
        }

        private void siticoneGradientButton2_Click(object sender, EventArgs e)
        {
            string name = processManager.findmaxCpuUsr(runningApps).Data.ProcessName.ToString();
            processManager.KillprocessByName(name);
            processManager.DeleteProcess(processManager.findmaxCpuUsr(runningApps), runningApps);
            startup();
            MessageBox.Show("Succesfully aborted process : " + name); 
            
        }

        private void siticoneImageButton3_Click(object sender, EventArgs e)
        {
            processManager.FullRefresh(runningApps);
            startup();
            MessageBox.Show("YOUR SYSTEM SUCCESFULLY BOOSTED !", "info");

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            progressmaincpu.Value = (int)fcpu;
            progressmainram.Value = (int)fram;
            chartx.Series["CPU"].Points.AddY(fcpu);
            chartx.Series["RAM"].Points.AddY(fram);



        }

        private void siticoneProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SEARCHtb_Click(object sender, EventArgs e)
        {

        }

        private void siticoneImageButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void siticoneImageButton2_Click_1(object sender, EventArgs e)
        {
            string search = searchbox.Text;
            RunningApplicationNode result = processManager.FindByName(search, runningApps);

            if (result != null)
            {

                lbx1.Text = result.Data.ProcessName;
                lbx2.Text = result.Data.MainWindowTitle;
                lbx3.Text = result.Data.ProcessId.ToString();
                lbx4.Text = result.Data.StartTime.ToString();
                lbx5.Text = result.Data.TotalProcessTime.ToString();
                lbx6.Text = result.Data.TotalUserTime.ToString();
                lbx7.Text = result.Data.HandleCount.ToString();
                lbx8.Text = result.Data.ThreadCount.ToString();
                cpuprogressbarx.Value = (int)result.Data.CPUsagePercent;
                memoryprogressbarx.Value = (int)result.Data.RAMUsagePercent;
                lbx9.Text = current.Data.RAMUsageValue + " MB";
            }
            else
            {
                MessageBox.Show("No result found for Search : " + search, "NOT FOUND", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
