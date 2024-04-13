using System.Windows.Forms;

namespace GnDTrainer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BGWorker = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            ProcOpenLabel = new Label();
            label2 = new Label();
            livesUpDown = new NumericUpDown();
            armorCheckBox = new CheckBox();
            preWeapon = new Button();
            nextWeapon = new Button();
            weaponPictureBox = new PictureBox();
            levelPictureBox = new PictureBox();
            preLevel = new Button();
            nextLevel = new Button();
            level_button = new Button();
            level_label = new Label();
            ((System.ComponentModel.ISupportInitialize)livesUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)weaponPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)levelPictureBox).BeginInit();
            SuspendLayout();
            // 
            // BGWorker
            // 
            BGWorker.WorkerReportsProgress = true;
            BGWorker.DoWork += BGWorker_DoWork;
            BGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Process:";
            // 
            // ProcOpenLabel
            // 
            ProcOpenLabel.AutoSize = true;
            ProcOpenLabel.ForeColor = Color.Red;
            ProcOpenLabel.Location = new Point(61, 18);
            ProcOpenLabel.Name = "ProcOpenLabel";
            ProcOpenLabel.Size = new Size(94, 15);
            ProcOpenLabel.TabIndex = 1;
            ProcOpenLabel.Text = "No game found!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 68);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 4;
            label2.Text = "Lives:";
            // 
            // livesUpDown
            // 
            livesUpDown.Location = new Point(61, 66);
            livesUpDown.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            livesUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            livesUpDown.Name = "livesUpDown";
            livesUpDown.Size = new Size(64, 23);
            livesUpDown.TabIndex = 5;
            livesUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            livesUpDown.ValueChanged += LivesUpDown_ValueChanged;
            // 
            // armorCheckBox
            // 
            armorCheckBox.AutoSize = true;
            armorCheckBox.ForeColor = Color.White;
            armorCheckBox.Location = new Point(12, 106);
            armorCheckBox.Name = "armorCheckBox";
            armorCheckBox.Size = new Size(147, 19);
            armorCheckBox.TabIndex = 6;
            armorCheckBox.Text = "Always armor in chests";
            armorCheckBox.UseVisualStyleBackColor = true;
            // 
            // preWeapon
            // 
            preWeapon.BackColor = Color.Black;
            preWeapon.BackgroundImage = Properties.Resources.left_arrow;
            preWeapon.BackgroundImageLayout = ImageLayout.Stretch;
            preWeapon.FlatStyle = FlatStyle.Flat;
            preWeapon.ForeColor = Color.Black;
            preWeapon.ImageAlign = ContentAlignment.TopCenter;
            preWeapon.Location = new Point(461, 2);
            preWeapon.Margin = new Padding(0);
            preWeapon.Name = "preWeapon";
            preWeapon.Size = new Size(63, 47);
            preWeapon.TabIndex = 7;
            preWeapon.UseVisualStyleBackColor = false;
            preWeapon.Click += PreWeapon_Click;
            // 
            // nextWeapon
            // 
            nextWeapon.BackColor = Color.Black;
            nextWeapon.BackgroundImage = Properties.Resources.right_arrow;
            nextWeapon.BackgroundImageLayout = ImageLayout.Stretch;
            nextWeapon.FlatStyle = FlatStyle.Flat;
            nextWeapon.Location = new Point(518, 2);
            nextWeapon.Name = "nextWeapon";
            nextWeapon.Size = new Size(66, 47);
            nextWeapon.TabIndex = 8;
            nextWeapon.UseVisualStyleBackColor = false;
            nextWeapon.Click += NextWeapon_Click;
            // 
            // weaponPictureBox
            // 
            weaponPictureBox.Enabled = false;
            weaponPictureBox.Image = Properties.Resources.Torch;
            weaponPictureBox.Location = new Point(461, 52);
            weaponPictureBox.Name = "weaponPictureBox";
            weaponPictureBox.Size = new Size(123, 107);
            weaponPictureBox.TabIndex = 9;
            weaponPictureBox.TabStop = false;
            // 
            // levelPictureBox
            // 
            levelPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            levelPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            levelPictureBox.Image = Properties.Resources._2nd_Loop;
            levelPictureBox.InitialImage = null;
            levelPictureBox.Location = new Point(143, 248);
            levelPictureBox.Name = "levelPictureBox";
            levelPictureBox.Size = new Size(300, 166);
            levelPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            levelPictureBox.TabIndex = 11;
            levelPictureBox.TabStop = false;
            // 
            // preLevel
            // 
            preLevel.BackColor = Color.Black;
            preLevel.BackgroundImage = Properties.Resources.left_arrow;
            preLevel.BackgroundImageLayout = ImageLayout.Stretch;
            preLevel.FlatStyle = FlatStyle.Flat;
            preLevel.ForeColor = Color.Black;
            preLevel.ImageAlign = ContentAlignment.TopCenter;
            preLevel.Location = new Point(77, 309);
            preLevel.Margin = new Padding(0);
            preLevel.Name = "preLevel";
            preLevel.Size = new Size(63, 47);
            preLevel.TabIndex = 12;
            preLevel.UseVisualStyleBackColor = false;
            preLevel.Click += preLevel_Click;
            // 
            // nextLevel
            // 
            nextLevel.BackColor = Color.Black;
            nextLevel.BackgroundImage = Properties.Resources.right_arrow;
            nextLevel.BackgroundImageLayout = ImageLayout.Stretch;
            nextLevel.FlatStyle = FlatStyle.Flat;
            nextLevel.Location = new Point(449, 309);
            nextLevel.Name = "nextLevel";
            nextLevel.Size = new Size(66, 47);
            nextLevel.TabIndex = 13;
            nextLevel.UseVisualStyleBackColor = false;
            nextLevel.Click += nextLevel_Click;
            // 
            // level_button
            // 
            level_button.Enabled = false;
            level_button.FlatStyle = FlatStyle.Flat;
            level_button.Location = new Point(259, 219);
            level_button.Name = "level_button";
            level_button.Size = new Size(75, 23);
            level_button.TabIndex = 14;
            level_button.Text = "Disabled...";
            level_button.UseVisualStyleBackColor = true;
            level_button.Click += level_button_Click;
            // 
            // level_label
            // 
            level_label.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            level_label.ForeColor = Color.White;
            level_label.Location = new Point(72, 417);
            level_label.Name = "level_label";
            level_label.Size = new Size(443, 22);
            level_label.TabIndex = 15;
            level_label.Text = "Second Loop";
            level_label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(596, 450);
            Controls.Add(level_label);
            Controls.Add(level_button);
            Controls.Add(nextLevel);
            Controls.Add(preLevel);
            Controls.Add(levelPictureBox);
            Controls.Add(weaponPictureBox);
            Controls.Add(nextWeapon);
            Controls.Add(preWeapon);
            Controls.Add(armorCheckBox);
            Controls.Add(livesUpDown);
            Controls.Add(label2);
            Controls.Add(ProcOpenLabel);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Omega's GnD Trainer v0.0.1 x86";
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)livesUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)weaponPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)levelPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker BGWorker;
        private Label label1;
        private Label ProcOpenLabel;
        private Label label2;
        private NumericUpDown livesUpDown;
        private CheckBox armorCheckBox;
        private Button preWeapon;
        private Button nextWeapon;
        private PictureBox weaponPictureBox;
        private PictureBox levelPictureBox;
        private Button preLevel;
        private Button nextLevel;
        private Button level_button;
        private Label level_label;
    }
}