using Memory;
using System.Diagnostics;

namespace GnDTrainer
{
    public partial class MainForm : Form
    {
        public Mem m = new();

        public MainForm()
        {
            InitializeComponent();
        }

        bool ProcOpen = false;
        int processId;

        private void BGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            Process[] processes = Process.GetProcessesByName("Ghosts'nDemons");
            if (processes != null && processes.Length > 0)
            {
                // The game runs on the child process
                processId = processes.Last().Id;
                ProcOpen = m.OpenProcess(processId);
            }
            else
            {
                ProcOpen = false;
                Thread.Sleep(1000);
                return;
            }

            chestArmorCheck();

            //int score = m.ReadInt("base+000E24A0,-2");
            //System.Diagnostics.Debug.WriteLine("The score is: " + score);

            Thread.Sleep(1000);
            BGWorker.ReportProgress(0);
        }

        private void chestArmorCheck()
        {
            if (armorCheckBox.Checked)
            {
                // Chests give a guaranteed armor after 5 unopened chests.
                m.WriteMemory("base+0037E418,F4,00", "int", "6");
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void BGWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (ProcOpen)
            {
                ProcOpenLabel.Text = processId.ToString();
                ProcOpenLabel.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                ProcOpenLabel.Text = "No game found!";
                ProcOpenLabel.ForeColor = System.Drawing.Color.Red;
            }
            BGWorker.RunWorkerAsync();
        }

        private void livesUpDown_ValueChanged(object sender, EventArgs e)
        {
            int desiredLivesInt = (int)livesUpDown.Value;
            string desiredLivesHex = desiredLivesInt.ToString("X");
            m.WriteMemory("base+000E24A0,2", "byte", desiredLivesHex);
        }
    }
}