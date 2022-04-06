using System;
using Project3_HT;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_HT
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void ProgramTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynamicSim.ProgramType = ProgramTypeCB.Text;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DynamicSim.Reset();
        }

        private void cycleSpeed_ValueChanged(object sender, EventArgs e)
        {
            DynamicSim.cycleSpeed = (int)cycleSpeed.Value;
        }
    }
}
