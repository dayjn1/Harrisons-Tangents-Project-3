
namespace Project3_HT
{
    partial class Controller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Controller));
            this.label1 = new System.Windows.Forms.Label();
            this.staticSim = new System.Windows.Forms.Button();
            this.dynamicSim = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(544, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Choose the Simulation To Run";
            // 
            // staticSim
            // 
            this.staticSim.BackColor = System.Drawing.Color.Transparent;
            this.staticSim.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staticSim.Location = new System.Drawing.Point(33, 104);
            this.staticSim.Name = "staticSim";
            this.staticSim.Size = new System.Drawing.Size(186, 46);
            this.staticSim.TabIndex = 1;
            this.staticSim.Text = "Static";
            this.staticSim.UseVisualStyleBackColor = false;
            this.staticSim.Click += new System.EventHandler(this.staticSim_Click);
            // 
            // dynamicSim
            // 
            this.dynamicSim.BackColor = System.Drawing.Color.Transparent;
            this.dynamicSim.Font = new System.Drawing.Font("Stencil", 20.25F);
            this.dynamicSim.Location = new System.Drawing.Point(382, 104);
            this.dynamicSim.Name = "dynamicSim";
            this.dynamicSim.Size = new System.Drawing.Size(189, 46);
            this.dynamicSim.TabIndex = 2;
            this.dynamicSim.Text = "Dynamic";
            this.dynamicSim.UseVisualStyleBackColor = false;
            this.dynamicSim.Click += new System.EventHandler(this.dynamicSim_Click);
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(599, 181);
            this.Controls.Add(this.dynamicSim);
            this.Controls.Add(this.staticSim);
            this.Controls.Add(this.label1);
            this.Name = "Controller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button staticSim;
        private System.Windows.Forms.Button dynamicSim;
    }
}