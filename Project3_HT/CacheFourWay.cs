using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2_HT
{
    public partial class CacheFourWay : Form
    {
        public CacheFourWay()
        {
            InitializeComponent();
        }

        public void UpdateFPMultRS()
        {
            List<Label> Labels = new List<Label>()
            {
                Set0_Entry0_valid, FPMultDestReg1, FPMultOp1_1, FPMultOp2_1,
                FPMultMnem2, FPMultDestReg2, FPMultOp1_2, FPMultOp2_2,
                FPMultMnem3, FPMultDestReg3, FPMultOp1_3, FPMultOp2_3
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach

            if (RSManager.FPMultRS[0] != null)
            {
                Labels[0].Text = RSManager.FPMultRS[0].mnemonic;
                Labels[1].Text = RSManager.FPMultRS[0].destR;
                Labels[2].Text = RSManager.FPMultRS[0].operand1;
                Labels[3].Text = RSManager.FPMultRS[0].operand2;
            }//end if
            if (RSManager.FPMultRS[1] != null)
            {
                Labels[4].Text = RSManager.FPMultRS[1].mnemonic;
                Labels[5].Text = RSManager.FPMultRS[1].destR;
                Labels[6].Text = RSManager.FPMultRS[1].operand1;
                Labels[7].Text = RSManager.FPMultRS[1].operand2;
            }//end if
            if (RSManager.FPMultRS[2] != null)
            {
                Labels[8].Text = RSManager.FPMultRS[2].mnemonic;
                Labels[9].Text = RSManager.FPMultRS[2].destR;
                Labels[10].Text = RSManager.FPMultRS[2].operand1;
                Labels[11].Text = RSManager.FPMultRS[2].operand2;
            }//end if
        }//end UpdateFPMultRS()

    }
}
