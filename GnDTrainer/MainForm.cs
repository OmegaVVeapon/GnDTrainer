using GnDTrainer.Properties;
using Memory;
using System.Diagnostics;
// Allows us to call Windows functions like VirtualFreeEx cleanly (https://www.nuget.org/packages/Microsoft.Windows.CsWin32)



namespace GnDTrainer
{

    public partial class MainForm : Form
    {
        public Mem m = new();
        private readonly Dictionary<int, Bitmap> weapons = new();

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
        int currentLevel;
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

        private int GetCurrentLevel()
        {
            currentLevel = m.ReadByte("base+37E67C");
            System.Diagnostics.Debug.WriteLine("The current level is: " + currentLevel);
            return currentLevel;
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

        public async void SetLevel(bool enable)
        {
            if (enable)
            {
                // AoB scan and store it in AoBScanResults. We specify our start and end address regions to decrease scan time.
                IEnumerable<long> AoBScanResults = await m.AoBScan(minAdressRange, maxAdressRange, "89 35 7C E6 77 00", false, true);

                levelaobscanadress = AoBScanResults.FirstOrDefault().ToString("X");

                System.Diagnostics.Debug.WriteLine("Our First Found Address is " + levelaobscanadress);

                System.Diagnostics.Debug.WriteLine("The base address is " + procBaseAddress.ToString("X"));



                byte[] levelLoadCode = {
                    //0x83, 0xFE, 0x35, // cmp esi, 0x35
                    //0x0F, 0x8D, 0x05, 0x00, 0x00, 0x00,  // jnl 00B0000E (This jmp will change each time...)
                    0xBE, 0x35, 0x00, 0x00, 0x00, // mov esi,00000035
                    0x53, // push ebx
                    0xBB, 0x00, 0x00, 0x40, 0x00, // mov ebx,Ghosts'nDemons.exe (Use procBaseAddress in Hex here instead...)
                    0x89, 0xB3, 0x7C, 0xE6, 0x37, 0x00, // mov [ebx+0037E67C],esi
                    0x5B // pop ebx        
                };

                codecavebase = m.CreateCodeCave(levelaobscanadress, levelLoadCode, 6, 4096);

                UIntPtr codecaveAllocAddress = UIntPtr.Add(codecavebase, levelLoadCode.Length);

                int newint = (int)codecaveAllocAddress - 6;

                System.Diagnostics.Debug.WriteLine("Code Cave Base: 0x" + codecavebase.ToString("X"));

                System.Diagnostics.Debug.WriteLine("Read Allocated Memory: 0x" + newint.ToString("X") + "\r\n" + codecaveAllocAddress);

            }
            else
            {
                m.WriteMemory(levelaobscanadress, "bytes", "0x89,0x35,0x7C,0xE6,0x77,0x00");

                Imps.VirtualFreeEx(m.mProc.Handle, codecavebase, (UIntPtr)0uL, 32768u);
            }

        }

        private void SecondLoopCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (secondLoopCheckbox.Checked)
            {
                SetLevel(true);
            }
            //else
            //{
            //    setLevel(false);
            //}
        }
    }
}