using Project3_HT;
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
    public partial class Controller : Form
    {
        public Controller()
        {
            InitializeComponent();
        }

        private void staticSim_Click(object sender, EventArgs e)
        {
            Tangents staticSim = new Tangents();
            staticSim.Show();
        }

        private void dynamicSim_Click(object sender, EventArgs e)
        {
            DynamicSim dynamicSim = new DynamicSim();
            dynamicSim.Show();
        }
    }
}
