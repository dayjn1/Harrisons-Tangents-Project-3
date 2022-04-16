// ---------------------------------------------------------------------------
// File name:                   Settings.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Course-Section:              CSCI-4717
// Creation Date:               04/04/2022
// ---------------------------------------------------------------------------

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
    /**
    * Class Name:       Settings
    * Class Purpose:    To allow the user some basic settings of how to run pipeline
    *
    * <hr>
    * Date created:     04/04/2022
    * @Janine Day
    */
    public partial class Settings : Form
    {
        /**
        * Method Name:    Settings()
        * Method Purpose: Auto-generated, initializes form
        *
        * <hr>
        * Date created: 04/04/2022
        * @Janine Day
        * <hr>
        */
        public Settings()
        {
            InitializeComponent();
        }//end Settings()

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        /**
        * Method Name:    ProgramTypeCB_SelectedIndexChanged(object, EventArgs)
        * Method Purpose: Sets the program type based on user selection
        *
        * <hr>
        * Date created: 04/04/2022
        * @Janine Day
        * <hr>
        */
        private void ProgramTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynamicSim.ProgramType = ProgramTypeCB.Text;
        }//end ProgramTypeCB_SelectedIndexChanged(object, EventArgs)


        /**
        * Method Name:    ResetButton_Click(object, EventArgs)
        * Method Purpose: Restarts the application
        *
        * <hr>
        * Date created: 04/04/2022
        * @Janine Day
        * <hr>
        */
        private void ResetButton_Click(object sender, EventArgs e)
        {
            DynamicSim.Reset();
        }//end ResetButton_Click(object, EventArgs)

        /**
        * Method Name:    cycleSpeed_ValueChanged(object, EventArgs)
        * Method Purpose: Sets the cyclespeed, in MS
        *
        * <hr>
        * Date created: 04/04/2022
        * @Janine Day
        * <hr>
        */
        private void cycleSpeed_ValueChanged(object sender, EventArgs e)
        {
            DynamicSim.cycleSpeed = (int)cycleSpeed.Value;
        }//end cycleSpeed_ValueChanged(object, EventArgs)
    }//end Settings
}//end Project3_HT
