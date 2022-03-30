using System;
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
    public partial class DynamicSim : Form
    {
        public int cycleSpeed = 500;
        public DynamicSim()
        {
            InitializeComponent();
        }

        private void cycleSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            cycleSpeed = (int)cycleSpeedNUD.Value;
        }
    }
}
