using GnDTrainer.Properties;
using Memory;
using System.Diagnostics;

namespace GnDTrainer
{
    public partial class MainForm : Form
    {
        public Mem m = new();

        Dictionary<int, Bitmap> weapons = new Dictionary<int, Bitmap>();

        public MainForm()
        {
            InitializeComponent();

            // Initialize the weapons in the correct rotation order
            weapons.Add(0, Resources.Lance);
            weapons.Add(1, Resources.Knife);
            weapons.Add(2, Resources.Torch);
            weapons.Add(3, Resources.Axe);
            weapons.Add(4, Resources.Cross);
            weapons.Add(5, Resources.Scythe);
            weapons.Add(6, Resources.Crossbow);
            weapons.Add(7, Resources.Disc);
            weapons.Add(8, Resources.Shield);
            weapons.Add(9, Resources.Fireball);

        }

        bool ProcOpen = false;
        int processId;
        int currentWeapon;

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

            //int score = m.ReadInt("base+000E24A0,-2");
            //System.Diagnostics.Debug.WriteLine("The score is: " + score);

            Thread.Sleep(1000);
            BGWorker.ReportProgress(0);
        }

        private void toggleElements(bool state)
        {
            armorCheckBox.AutoCheck = state;

            livesUpDown.Enabled = state;

            preWeapon.Enabled = state;
            nextWeapon.Enabled = state;
        }

        private void chestArmorCheck()
        {
            if (armorCheckBox.Checked)
            {
                // Chests give a guaranteed armor after 5 unopened chests.
                m.WriteMemory("base+0037E418,F4,00", "int", "6");
            }
        }

        private int getCurrentWeapon()
        {
            currentWeapon = m.ReadByte("base+0037E418,80,0");
            weaponPictureBox.Image = weapons[currentWeapon];
            return currentWeapon;

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
                ProcOpenLabel.ForeColor = System.Drawing.Color.White;
                toggleElements(true);

                chestArmorCheck();

                // Keep this updated to reflect the player's in-game weapon on the trainer
                getCurrentWeapon();
            }
            else
            {
                ProcOpenLabel.Text = "No game found!";
                ProcOpenLabel.ForeColor = System.Drawing.Color.Red;
                toggleElements(false);
            }
            BGWorker.RunWorkerAsync();
        }

        private void livesUpDown_ValueChanged(object sender, EventArgs e)
        {
            int desiredLivesInt = (int)livesUpDown.Value;
            string desiredLivesHex = desiredLivesInt.ToString("X");
            m.WriteMemory("base+000E24A0,2", "byte", desiredLivesHex);
        }


        private void preWeapon_Click(object sender, EventArgs e)
        {
            getCurrentWeapon();

            if (currentWeapon == 0)
            {
                weaponPictureBox.Image = Resources.Fireball;
                m.WriteMemory("base+0037E418,80,0", "int", "9");
            }
            else
            {
                weaponPictureBox.Image = weapons[currentWeapon - 1];
                m.WriteMemory("base+0037E418,80,0", "int", (currentWeapon - 1).ToString());
            }

        }

        private void nextWeapon_Click(object sender, EventArgs e)
        {
            getCurrentWeapon();

            if (currentWeapon == 9)
            {
                weaponPictureBox.Image = Resources.Lance;
                m.WriteMemory("base+0037E418,80,0", "int", "0");
            }
            else
            {
                weaponPictureBox.Image = weapons[currentWeapon + 1];
                m.WriteMemory("base+0037E418,80,0", "int", (currentWeapon + 1).ToString());
            }
        }

    }
}