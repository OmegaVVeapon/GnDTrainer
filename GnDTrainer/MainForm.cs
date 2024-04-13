using GnDTrainer.Properties;
using Memory;
using System.Diagnostics;

namespace GnDTrainer
{

    public partial class MainForm : Form
    {
        public Mem m = new();
        private readonly Dictionary<int, Bitmap> weapons = new();
        private readonly Dictionary<int, (string levelName, Bitmap levelImage, byte levelHex)> levels = new();

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

            // Initialize the images in the correct display order
            levels.Add(0, ("Second loop", Resources._2nd_Loop, 0x35));
            levels.Add(1, ("Level One", Resources.Level_1, 0x04));
            levels.Add(2, ("Level Two", Resources.Level_2, 0x07));
            levels.Add(3, ("Castle Boss", Resources.Level_2_Castle_Boss, 0x0A));
            levels.Add(4, ("Level Three - Water - 1st Stage", Resources.Level_3_Water_1st_stage, 0x0D));
            levels.Add(5, ("Level Three - Water - 2nd Stage", Resources.Level_3_Water_2nd_stage, 0x10));
            levels.Add(6, ("Level Three - Fire - 1st Stage", Resources.Level_3_Fire_1st_stage, 0x13));
            levels.Add(7, ("Level Three - Fire - Boss Fight", Resources.Level_3_Lava_Boss, 0x19));
            levels.Add(8, ("Level Four", Resources.Level_4, 0x1C));
            levels.Add(9, ("Level Five - 1st Stage", Resources.Level_5_1st_stage, 0x1F));
            levels.Add(10, ("Level Five - 2nd Stage", Resources.Level_5_2nd_stage, 0x24));
            levels.Add(11, ("Azazel Boss", Resources.Level_5_Azazel_Boss, 0x27));
            levels.Add(12, ("True Final Boss", Resources.True_Final_Boss, 0x29));
            levels.Add(13, ("Credits", Resources.Credits, 0x2D));
            levels.Add(14, ("Weak Ending", Resources.Weak_Ending, 0x31));
            levels.Add(15, ("Good Ending", Resources.Good_Ending, 0x2E));

        }

        bool ProcOpen = false;
        int processId;
        int currentWeapon;

        int selectedLevel = 0;

        string levelaobscanadress = string.Empty;
        IntPtr procBaseAddress = IntPtr.Zero;
        long minAdressRange = 0;
        long maxAdressRange = 0;
        UIntPtr codecavebase;

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        private void BGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            Process[] processes = Process.GetProcessesByName("Ghosts'nDemons");
            if (processes != null && processes.Length > 0)
            {
                // The game runs on the child process
                processId = processes.Last().Id;
                procBaseAddress = Process.GetProcessById(processId).MainModule.BaseAddress;
                minAdressRange = (long)Process.GetProcessById(processId).MainModule.BaseAddress;
                maxAdressRange = minAdressRange + (long)Process.GetProcessById(processId).MainModule.ModuleMemorySize;


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

        private void ToggleElements(bool state)
        {
            armorCheckBox.AutoCheck = state;

            livesUpDown.Enabled = state;

            preWeapon.Enabled = state;
            nextWeapon.Enabled = state;

            nextLevel.Enabled = state;
            preLevel.Enabled = state;
            level_button.Enabled = state;

            if (state)
            {
                level_button.Text = "Set Level!";
                level_button.FlatStyle = FlatStyle.Standard;
                level_button.FlatAppearance.BorderColor = Control.DefaultBackColor;
                level_button.FlatAppearance.MouseOverBackColor = Control.DefaultBackColor;
                level_button.FlatAppearance.MouseDownBackColor = Control.DefaultBackColor;
            }
            else
            {
                level_button.Text = "Disabled...";
                level_button.FlatStyle = FlatStyle.Flat;
                level_button.FlatAppearance.BorderColor = BackColor;
                level_button.FlatAppearance.MouseOverBackColor = BackColor;
                level_button.FlatAppearance.MouseDownBackColor = BackColor;
            }
            
        }

        private void ChestArmorCheck()
        {
            if (armorCheckBox.Checked)
            {
                // Chests give a guaranteed armor after 5 unopened chests.
                m.WriteMemory("base+0037E418,F4,00", "int", "6");
            }
        }

        private int GetCurrentWeapon()
        {
            currentWeapon = m.ReadByte("base+0037E418,80,0");
            weaponPictureBox.Image = weapons[currentWeapon];
            return currentWeapon;
        }

        private void GetCurrentLevel()
        {
            int currentLevel = m.ReadInt("base+37E67C");
            System.Diagnostics.Debug.WriteLine("The current level is: " + currentLevel);
            System.Diagnostics.Debug.WriteLine("The selected level is: " + levels[selectedLevel].levelHex);

            if (levels[selectedLevel].levelHex == currentLevel)
            {
                System.Diagnostics.Debug.WriteLine("Unsetting the code cave");
                SetLevel(false);
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
                ProcOpenLabel.ForeColor = System.Drawing.Color.White;
                ToggleElements(true);

                ChestArmorCheck();

                // Keep this updated to reflect the player's in-game weapon on the trainer
                GetCurrentWeapon();

                // Need to know what level we are at to know when to unset the level selection code cave
                GetCurrentLevel();
            }
            else
            {
                ProcOpenLabel.Text = "No game found!";
                ProcOpenLabel.ForeColor = System.Drawing.Color.Red;
                ToggleElements(false);
            }
            BGWorker.RunWorkerAsync();
        }

        private void LivesUpDown_ValueChanged(object sender, EventArgs e)
        {
            int desiredLivesInt = (int)livesUpDown.Value;
            string desiredLivesHex = desiredLivesInt.ToString("X");
            m.WriteMemory("base+000E24A0,2", "byte", desiredLivesHex);
        }


        private void PreWeapon_Click(object sender, EventArgs e)
        {
            GetCurrentWeapon();

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

        private void NextWeapon_Click(object sender, EventArgs e)
        {
            GetCurrentWeapon();

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

        public async void SetLevel(bool enable, byte levelHex = 0xFF)
        {
            if (enable)
            {
                // AoB scan and store it in AoBScanResults. We specify our start and end address regions to decrease scan time.
                IEnumerable<long> AoBScanResults = await m.AoBScan(minAdressRange, maxAdressRange, "89 35 7C E6 77 00", false, true);

                levelaobscanadress = AoBScanResults.FirstOrDefault().ToString("X");

                //System.Diagnostics.Debug.WriteLine("Our First Found Address is " + levelaobscanadress)
                //System.Diagnostics.Debug.WriteLine("The base address is " + procBaseAddress.ToString("X"));

                byte[] levelLoadCode = {
                    0xBE, levelHex, 0x00, 0x00, 0x00, // mov esi,00000035 - This is where the level value is stored
                    0x53, // push ebx
                    0xBB, 0x00, 0x00, 0x40, 0x00, // mov ebx,Ghosts'nDemons.exe (Use procBaseAddress in Hex here instead...)
                    0x89, 0xB3, 0x7C, 0xE6, 0x37, 0x00, // mov [ebx+0037E67C],esi
                    0x5B // pop ebx        
                };

                codecavebase = m.CreateCodeCave(levelaobscanadress, levelLoadCode, 6, 4096);

                UIntPtr codecaveAllocAddress = UIntPtr.Add(codecavebase, levelLoadCode.Length);

                // Set the player's armor to nothing (Arthur in trunks) since we need to player to die to load the next level. Makes it faster...
                m.WriteMemory("base+004BDAC8,54", "int", "5");

                //int newint = (int)codecaveAllocAddress - 6;
                //System.Diagnostics.Debug.WriteLine("Code Cave Base: 0x" + codecavebase.ToString("X"))
                //System.Diagnostics.Debug.WriteLine("Read Allocated Memory: 0x" + newint.ToString("X") + "\r\n" + codecaveAllocAddress);

            }
            else
            {
                m.WriteMemory(levelaobscanadress, "bytes", "0x89,0x35,0x7C,0xE6,0x77,0x00");

                Imps.VirtualFreeEx(m.mProc.Handle, codecavebase, (UIntPtr)0uL, 32768u);
            }

        }

        private void preLevel_Click(object sender, EventArgs e)
        {
            selectedLevel--;
            if (selectedLevel == -1)
            {
                selectedLevel = levels.Count - 1;
            }

            levelPictureBox.Image = levels[selectedLevel].levelImage;
            level_label.Text = levels[selectedLevel].levelName;
        }

        private void nextLevel_Click(object sender, EventArgs e)
        {

            selectedLevel++;
            if (selectedLevel == levels.Count)
            {
                selectedLevel = 0;
            }
            levelPictureBox.Image = levels[selectedLevel].levelImage;
            level_label.Text = levels[selectedLevel].levelName;
        }

        private void level_button_Click(object sender, EventArgs e)
        {
            SetLevel(true, levels[selectedLevel].levelHex);

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.GnD_sound);
            player.Play();
        }

    }
}