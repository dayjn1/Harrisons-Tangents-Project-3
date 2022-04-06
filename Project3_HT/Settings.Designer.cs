
namespace Project3_HT
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.cycleSpeed = new System.Windows.Forms.NumericUpDown();
            this.ProgramTypeLabel = new System.Windows.Forms.Label();
            this.ProgramTypeCB = new System.Windows.Forms.ComboBox();
            this.ResetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cycleSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(74, 81);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(156, 17);
            this.SpeedLabel.TabIndex = 0;
            this.SpeedLabel.Text = "Program Speed (in MS)";
            // 
            // cycleSpeed
            // 
            this.cycleSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cycleSpeed.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.cycleSpeed.Location = new System.Drawing.Point(260, 73);
            this.cycleSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.cycleSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cycleSpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.cycleSpeed.Name = "cycleSpeed";
            this.cycleSpeed.Size = new System.Drawing.Size(88, 29);
            this.cycleSpeed.TabIndex = 9;
            this.cycleSpeed.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.cycleSpeed.ValueChanged += new System.EventHandler(this.cycleSpeed_ValueChanged);
            // 
            // ProgramTypeLabel
            // 
            this.ProgramTypeLabel.AutoSize = true;
            this.ProgramTypeLabel.Location = new System.Drawing.Point(77, 144);
            this.ProgramTypeLabel.Name = "ProgramTypeLabel";
            this.ProgramTypeLabel.Size = new System.Drawing.Size(98, 17);
            this.ProgramTypeLabel.TabIndex = 10;
            this.ProgramTypeLabel.Text = "Program Type";
            // 
            // ProgramTypeCB
            // 
            this.ProgramTypeCB.FormattingEnabled = true;
            this.ProgramTypeCB.Items.AddRange(new object[] {
            "Step by Step",
            "Continuous"});
            this.ProgramTypeCB.Location = new System.Drawing.Point(260, 144);
            this.ProgramTypeCB.Name = "ProgramTypeCB";
            this.ProgramTypeCB.Size = new System.Drawing.Size(121, 24);
            this.ProgramTypeCB.TabIndex = 11;
            this.ProgramTypeCB.SelectedIndexChanged += new System.EventHandler(this.ProgramTypeCB_SelectedIndexChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(80, 221);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 12;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 281);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ProgramTypeCB);
            this.Controls.Add(this.ProgramTypeLabel);
            this.Controls.Add(this.cycleSpeed);
            this.Controls.Add(this.SpeedLabel);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cycleSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.NumericUpDown cycleSpeed;
        private System.Windows.Forms.Label ProgramTypeLabel;
        private System.Windows.Forms.ComboBox ProgramTypeCB;
        private System.Windows.Forms.Button ResetButton;
    }
}