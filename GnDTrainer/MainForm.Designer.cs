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
            ((System.ComponentModel.ISupportInitialize)livesUpDown).BeginInit();
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
            label2.Location = new Point(19, 68);
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
            livesUpDown.ValueChanged += livesUpDown_ValueChanged;
            // 
            // armorCheckBox
            // 
            armorCheckBox.AutoSize = true;
            armorCheckBox.Location = new Point(19, 106);
            armorCheckBox.Name = "armorCheckBox";
            armorCheckBox.Size = new Size(147, 19);
            armorCheckBox.TabIndex = 6;
            armorCheckBox.Text = "Always armor in chests";
            armorCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(armorCheckBox);
            Controls.Add(livesUpDown);
            Controls.Add(label2);
            Controls.Add(ProcOpenLabel);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Omega's GnD Trainer v0.0.1 x86";
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)livesUpDown).EndInit();
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
    }
}