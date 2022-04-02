// ---------------------------------------------------------------------------
// File name:                   Controller.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Nataliya Chibizova
// Edited By:                   Nataliya Chibizova
// Course-Section:              CSCI-4717
// Creation Date:               03/27/2022
// ---------------------------------------------------------------------------
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
