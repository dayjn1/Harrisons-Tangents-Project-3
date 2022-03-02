// ---------------------------------------------------------------------------
// File name:                   Form1.Designer.cs
// Project name:                Project 2 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Automatically Generated
// Course-Section:              CSCI-4717
// Creation Date:               02/17/2022
// ---------------------------------------------------------------------------


namespace Project2_HT
{
    partial class Tangents
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FetchBox = new System.Windows.Forms.TextBox();
            this.DecodeBox = new System.Windows.Forms.TextBox();
            this.ExecuteBox = new System.Windows.Forms.TextBox();
            this.MemoryBox = new System.Windows.Forms.TextBox();
            this.RegisterBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1137, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // FetchBox
            // 
            this.FetchBox.Location = new System.Drawing.Point(80, 226);
            this.FetchBox.Margin = new System.Windows.Forms.Padding(4);
            this.FetchBox.Name = "FetchBox";
            this.FetchBox.Size = new System.Drawing.Size(132, 22);
            this.FetchBox.TabIndex = 1;
            // 
            // DecodeBox
            // 
            this.DecodeBox.Location = new System.Drawing.Point(296, 226);
            this.DecodeBox.Margin = new System.Windows.Forms.Padding(4);
            this.DecodeBox.Name = "DecodeBox";
            this.DecodeBox.Size = new System.Drawing.Size(132, 22);
            this.DecodeBox.TabIndex = 2;
            // 
            // ExecuteBox
            // 
            this.ExecuteBox.Location = new System.Drawing.Point(493, 226);
            this.ExecuteBox.Margin = new System.Windows.Forms.Padding(4);
            this.ExecuteBox.Name = "ExecuteBox";
            this.ExecuteBox.Size = new System.Drawing.Size(132, 22);
            this.ExecuteBox.TabIndex = 3;
            // 
            // MemoryBox
            // 
            this.MemoryBox.Location = new System.Drawing.Point(692, 226);
            this.MemoryBox.Margin = new System.Windows.Forms.Padding(4);
            this.MemoryBox.Name = "MemoryBox";
            this.MemoryBox.Size = new System.Drawing.Size(132, 22);
            this.MemoryBox.TabIndex = 4;
            // 
            // RegisterBox
            // 
            this.RegisterBox.Location = new System.Drawing.Point(881, 226);
            this.RegisterBox.Margin = new System.Windows.Forms.Padding(4);
            this.RegisterBox.Name = "RegisterBox";
            this.RegisterBox.Size = new System.Drawing.Size(132, 22);
            this.RegisterBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 182);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fetch Instruction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Decode Instruction";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(493, 183);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Execute Instruction";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(692, 182);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Access Memory";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(877, 182);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Write to Register File";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(215, 69);
            this.StartButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(100, 28);
            this.StartButton.TabIndex = 11;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(509, 69);
            this.NextButton.Margin = new System.Windows.Forms.Padding(4);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(100, 28);
            this.NextButton.TabIndex = 12;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(805, 69);
            this.ContinueButton.Margin = new System.Windows.Forms.Padding(4);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(100, 28);
            this.ContinueButton.TabIndex = 13;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            // 
            // Tangents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 352);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RegisterBox);
            this.Controls.Add(this.MemoryBox);
            this.Controls.Add(this.ExecuteBox);
            this.Controls.Add(this.DecodeBox);
            this.Controls.Add(this.FetchBox);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Tangents";
            this.Text = "Pipeline Simulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox FetchBox;
        private System.Windows.Forms.TextBox DecodeBox;
        private System.Windows.Forms.TextBox ExecuteBox;
        private System.Windows.Forms.TextBox MemoryBox;
        private System.Windows.Forms.TextBox RegisterBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button ContinueButton;
    }
}

