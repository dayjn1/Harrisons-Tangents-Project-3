// ---------------------------------------------------------------------------
// File name:                   Form1.Designer.cs
// Project name:                Project 2 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Automatically Generated
// Course-Section:              CSCI-4717
// Creation Date:               02/17/2022
// ---------------------------------------------------------------------------


namespace Project3_HT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tangents));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.cycleLabel = new System.Windows.Forms.Label();
            this.cycledescrLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DHLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.FetchDR = new System.Windows.Forms.Label();
            this.FetchOp1 = new System.Windows.Forms.Label();
            this.FetchOp2 = new System.Windows.Forms.Label();
            this.DecodeDR = new System.Windows.Forms.Label();
            this.DecodeOp1 = new System.Windows.Forms.Label();
            this.DecodeOp2 = new System.Windows.Forms.Label();
            this.ExecuteDR = new System.Windows.Forms.Label();
            this.ExecuteOp1 = new System.Windows.Forms.Label();
            this.ExecuteOp2 = new System.Windows.Forms.Label();
            this.MemoryDR = new System.Windows.Forms.Label();
            this.MemoryOp1 = new System.Windows.Forms.Label();
            this.MemoryOp2 = new System.Windows.Forms.Label();
            this.RegisterDR = new System.Windows.Forms.Label();
            this.RegisterOp1 = new System.Windows.Forms.Label();
            this.RegisterOp2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SHLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1061, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // FetchBox
            // 
            this.FetchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FetchBox.Location = new System.Drawing.Point(247, 278);
            this.FetchBox.Name = "FetchBox";
            this.FetchBox.Size = new System.Drawing.Size(100, 29);
            this.FetchBox.TabIndex = 1;
            // 
            // DecodeBox
            // 
            this.DecodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecodeBox.Location = new System.Drawing.Point(399, 278);
            this.DecodeBox.Name = "DecodeBox";
            this.DecodeBox.Size = new System.Drawing.Size(100, 29);
            this.DecodeBox.TabIndex = 2;
            // 
            // ExecuteBox
            // 
            this.ExecuteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteBox.Location = new System.Drawing.Point(556, 278);
            this.ExecuteBox.Name = "ExecuteBox";
            this.ExecuteBox.Size = new System.Drawing.Size(100, 29);
            this.ExecuteBox.TabIndex = 3;
            // 
            // MemoryBox
            // 
            this.MemoryBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryBox.Location = new System.Drawing.Point(704, 278);
            this.MemoryBox.Name = "MemoryBox";
            this.MemoryBox.Size = new System.Drawing.Size(100, 29);
            this.MemoryBox.TabIndex = 4;
            // 
            // RegisterBox
            // 
            this.RegisterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterBox.Location = new System.Drawing.Point(877, 278);
            this.RegisterBox.Name = "RegisterBox";
            this.RegisterBox.Size = new System.Drawing.Size(100, 29);
            this.RegisterBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Stencil", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(254, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fetch ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Stencil", 13.8F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(404, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Decode ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Stencil", 13.8F);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(552, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Execute ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Stencil", 13.8F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(712, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 44);
            this.label4.TabIndex = 9;
            this.label4.Text = "Access\r\nMemory";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Stencil", 13.8F);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(860, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 44);
            this.label5.TabIndex = 10;
            this.label5.Text = "Write to\r\nRegister File";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.Silver;
            this.StartButton.Font = new System.Drawing.Font("Stencil", 13.8F);
            this.StartButton.Location = new System.Drawing.Point(42, 85);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(140, 47);
            this.StartButton.TabIndex = 11;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // cycleLabel
            // 
            this.cycleLabel.AutoSize = true;
            this.cycleLabel.BackColor = System.Drawing.Color.Transparent;
            this.cycleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cycleLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cycleLabel.Location = new System.Drawing.Point(958, 54);
            this.cycleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cycleLabel.Name = "cycleLabel";
            this.cycleLabel.Size = new System.Drawing.Size(19, 20);
            this.cycleLabel.TabIndex = 14;
            this.cycleLabel.Text = "0";
            // 
            // cycledescrLabel
            // 
            this.cycledescrLabel.AutoSize = true;
            this.cycledescrLabel.BackColor = System.Drawing.Color.Transparent;
            this.cycledescrLabel.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cycledescrLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cycledescrLabel.Location = new System.Drawing.Point(882, 54);
            this.cycledescrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cycledescrLabel.Name = "cycledescrLabel";
            this.cycledescrLabel.Size = new System.Drawing.Size(60, 19);
            this.cycledescrLabel.TabIndex = 15;
            this.cycledescrLabel.Text = "Cycle:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(861, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 38);
            this.label6.TabIndex = 16;
            this.label6.Text = "Data\r\nHazards:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // DHLabel
            // 
            this.DHLabel.AutoSize = true;
            this.DHLabel.BackColor = System.Drawing.Color.Transparent;
            this.DHLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DHLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DHLabel.Location = new System.Drawing.Point(958, 112);
            this.DHLabel.Name = "DHLabel";
            this.DHLabel.Size = new System.Drawing.Size(19, 20);
            this.DHLabel.TabIndex = 17;
            this.DHLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Stencil", 13.8F);
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(12, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(603, 22);
            this.label8.TabIndex = 18;
            this.label8.Text = "To get started, go to File and select open to get your file!";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(38, 348);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(195, 19);
            this.label9.TabIndex = 19;
            this.label9.Text = "Destination Register:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(128, 384);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "Operand 1:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(128, 418);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 19);
            this.label11.TabIndex = 21;
            this.label11.Text = "Operand 2:";
            // 
            // FetchDR
            // 
            this.FetchDR.AutoSize = true;
            this.FetchDR.BackColor = System.Drawing.Color.Transparent;
            this.FetchDR.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FetchDR.Location = new System.Drawing.Point(265, 348);
            this.FetchDR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FetchDR.Name = "FetchDR";
            this.FetchDR.Size = new System.Drawing.Size(0, 19);
            this.FetchDR.TabIndex = 22;
            // 
            // FetchOp1
            // 
            this.FetchOp1.AutoSize = true;
            this.FetchOp1.BackColor = System.Drawing.Color.Transparent;
            this.FetchOp1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FetchOp1.Location = new System.Drawing.Point(265, 384);
            this.FetchOp1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FetchOp1.Name = "FetchOp1";
            this.FetchOp1.Size = new System.Drawing.Size(0, 19);
            this.FetchOp1.TabIndex = 23;
            // 
            // FetchOp2
            // 
            this.FetchOp2.AutoSize = true;
            this.FetchOp2.BackColor = System.Drawing.Color.Transparent;
            this.FetchOp2.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FetchOp2.Location = new System.Drawing.Point(265, 418);
            this.FetchOp2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FetchOp2.Name = "FetchOp2";
            this.FetchOp2.Size = new System.Drawing.Size(0, 19);
            this.FetchOp2.TabIndex = 24;
            // 
            // DecodeDR
            // 
            this.DecodeDR.AutoSize = true;
            this.DecodeDR.BackColor = System.Drawing.Color.Transparent;
            this.DecodeDR.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecodeDR.Location = new System.Drawing.Point(419, 348);
            this.DecodeDR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DecodeDR.Name = "DecodeDR";
            this.DecodeDR.Size = new System.Drawing.Size(0, 19);
            this.DecodeDR.TabIndex = 25;
            // 
            // DecodeOp1
            // 
            this.DecodeOp1.AutoSize = true;
            this.DecodeOp1.BackColor = System.Drawing.Color.Transparent;
            this.DecodeOp1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecodeOp1.Location = new System.Drawing.Point(419, 384);
            this.DecodeOp1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DecodeOp1.Name = "DecodeOp1";
            this.DecodeOp1.Size = new System.Drawing.Size(0, 19);
            this.DecodeOp1.TabIndex = 26;
            // 
            // DecodeOp2
            // 
            this.DecodeOp2.AutoSize = true;
            this.DecodeOp2.BackColor = System.Drawing.Color.Transparent;
            this.DecodeOp2.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecodeOp2.Location = new System.Drawing.Point(419, 418);
            this.DecodeOp2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DecodeOp2.Name = "DecodeOp2";
            this.DecodeOp2.Size = new System.Drawing.Size(0, 19);
            this.DecodeOp2.TabIndex = 27;
            // 
            // ExecuteDR
            // 
            this.ExecuteDR.AutoSize = true;
            this.ExecuteDR.BackColor = System.Drawing.Color.Transparent;
            this.ExecuteDR.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteDR.Location = new System.Drawing.Point(572, 348);
            this.ExecuteDR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ExecuteDR.Name = "ExecuteDR";
            this.ExecuteDR.Size = new System.Drawing.Size(0, 19);
            this.ExecuteDR.TabIndex = 28;
            // 
            // ExecuteOp1
            // 
            this.ExecuteOp1.AutoSize = true;
            this.ExecuteOp1.BackColor = System.Drawing.Color.Transparent;
            this.ExecuteOp1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteOp1.Location = new System.Drawing.Point(572, 384);
            this.ExecuteOp1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ExecuteOp1.Name = "ExecuteOp1";
            this.ExecuteOp1.Size = new System.Drawing.Size(0, 19);
            this.ExecuteOp1.TabIndex = 29;
            // 
            // ExecuteOp2
            // 
            this.ExecuteOp2.AutoSize = true;
            this.ExecuteOp2.BackColor = System.Drawing.Color.Transparent;
            this.ExecuteOp2.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteOp2.Location = new System.Drawing.Point(572, 418);
            this.ExecuteOp2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ExecuteOp2.Name = "ExecuteOp2";
            this.ExecuteOp2.Size = new System.Drawing.Size(0, 19);
            this.ExecuteOp2.TabIndex = 30;
            // 
            // MemoryDR
            // 
            this.MemoryDR.AutoSize = true;
            this.MemoryDR.BackColor = System.Drawing.Color.Transparent;
            this.MemoryDR.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryDR.Location = new System.Drawing.Point(729, 348);
            this.MemoryDR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MemoryDR.Name = "MemoryDR";
            this.MemoryDR.Size = new System.Drawing.Size(0, 19);
            this.MemoryDR.TabIndex = 31;
            // 
            // MemoryOp1
            // 
            this.MemoryOp1.AutoSize = true;
            this.MemoryOp1.BackColor = System.Drawing.Color.Transparent;
            this.MemoryOp1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryOp1.Location = new System.Drawing.Point(729, 384);
            this.MemoryOp1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MemoryOp1.Name = "MemoryOp1";
            this.MemoryOp1.Size = new System.Drawing.Size(0, 19);
            this.MemoryOp1.TabIndex = 32;
            // 
            // MemoryOp2
            // 
            this.MemoryOp2.AutoSize = true;
            this.MemoryOp2.BackColor = System.Drawing.Color.Transparent;
            this.MemoryOp2.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryOp2.Location = new System.Drawing.Point(729, 418);
            this.MemoryOp2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MemoryOp2.Name = "MemoryOp2";
            this.MemoryOp2.Size = new System.Drawing.Size(0, 19);
            this.MemoryOp2.TabIndex = 33;
            // 
            // RegisterDR
            // 
            this.RegisterDR.AutoSize = true;
            this.RegisterDR.BackColor = System.Drawing.Color.Transparent;
            this.RegisterDR.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterDR.Location = new System.Drawing.Point(899, 348);
            this.RegisterDR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RegisterDR.Name = "RegisterDR";
            this.RegisterDR.Size = new System.Drawing.Size(0, 19);
            this.RegisterDR.TabIndex = 34;
            // 
            // RegisterOp1
            // 
            this.RegisterOp1.AutoSize = true;
            this.RegisterOp1.BackColor = System.Drawing.Color.Transparent;
            this.RegisterOp1.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterOp1.Location = new System.Drawing.Point(899, 384);
            this.RegisterOp1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RegisterOp1.Name = "RegisterOp1";
            this.RegisterOp1.Size = new System.Drawing.Size(0, 19);
            this.RegisterOp1.TabIndex = 35;
            // 
            // RegisterOp2
            // 
            this.RegisterOp2.AutoSize = true;
            this.RegisterOp2.BackColor = System.Drawing.Color.Transparent;
            this.RegisterOp2.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterOp2.Location = new System.Drawing.Point(899, 418);
            this.RegisterOp2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RegisterOp2.Name = "RegisterOp2";
            this.RegisterOp2.Size = new System.Drawing.Size(0, 19);
            this.RegisterOp2.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Stencil", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(836, 145);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 38);
            this.label12.TabIndex = 37;
            this.label12.Text = "Structural\r\nHazards:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // SHLabel
            // 
            this.SHLabel.AutoSize = true;
            this.SHLabel.BackColor = System.Drawing.Color.Transparent;
            this.SHLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SHLabel.Location = new System.Drawing.Point(958, 163);
            this.SHLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SHLabel.Name = "SHLabel";
            this.SHLabel.Size = new System.Drawing.Size(19, 20);
            this.SHLabel.TabIndex = 38;
            this.SHLabel.Text = "0";
            // 
            // Tangents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1061, 476);
            this.Controls.Add(this.SHLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.RegisterOp2);
            this.Controls.Add(this.RegisterOp1);
            this.Controls.Add(this.RegisterDR);
            this.Controls.Add(this.MemoryOp2);
            this.Controls.Add(this.MemoryOp1);
            this.Controls.Add(this.MemoryDR);
            this.Controls.Add(this.ExecuteOp2);
            this.Controls.Add(this.ExecuteOp1);
            this.Controls.Add(this.ExecuteDR);
            this.Controls.Add(this.DecodeOp2);
            this.Controls.Add(this.DecodeOp1);
            this.Controls.Add(this.DecodeDR);
            this.Controls.Add(this.FetchOp2);
            this.Controls.Add(this.FetchOp1);
            this.Controls.Add(this.FetchDR);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DHLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cycledescrLabel);
            this.Controls.Add(this.cycleLabel);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Tangents";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Label cycleLabel;
        private System.Windows.Forms.Label cycledescrLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label DHLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label FetchDR;
        private System.Windows.Forms.Label FetchOp1;
        private System.Windows.Forms.Label FetchOp2;
        private System.Windows.Forms.Label DecodeDR;
        private System.Windows.Forms.Label DecodeOp1;
        private System.Windows.Forms.Label DecodeOp2;
        private System.Windows.Forms.Label ExecuteDR;
        private System.Windows.Forms.Label ExecuteOp1;
        private System.Windows.Forms.Label ExecuteOp2;
        private System.Windows.Forms.Label MemoryDR;
        private System.Windows.Forms.Label MemoryOp1;
        private System.Windows.Forms.Label MemoryOp2;
        private System.Windows.Forms.Label RegisterDR;
        private System.Windows.Forms.Label RegisterOp1;
        private System.Windows.Forms.Label RegisterOp2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label SHLabel;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
    }
}

