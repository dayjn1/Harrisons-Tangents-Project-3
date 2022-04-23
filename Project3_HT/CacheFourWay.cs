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

namespace Project2_HT
{
    public partial class CacheFourWay : Form
    {
        public CacheFourWay()
        {
            InitializeComponent();
        }

        public void UpdateSet0(CacheEntry[,] cache)
        {
            List<Label> Labels = new List<Label>()
            {
                Set0_Entry0_valid, Set0_Entry0_tag, Set0_Entry0_index, Set0_Entry0_data,
                Set0_Entry1_valid, Set0_Entry1_tag, Set0_Entry1_index, Set1_Entry1_data,
                Set0_Entry2_valid, Set0_Entry2_tag, Set1_Entry2_index, Set1_Entry2_data,
                Set0_Entry3_valid, Set0_Entry3_tag, Set1_Entry3_index, Set1_Entry3_data
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach  \/

            if (cache[0, 0].empty.Equals(true))
            {
                Labels[0].Text = cache[0, 0].valid.ToString();
                Labels[1].Text = cache[0, 0].tag.ToString();
                Labels[2].Text = cache[0, 0].index.ToString();
                Labels[3].Text = cache[0, 3].data.ToString();
            }



            if (cache[0, 1].empty.Equals(true))
            {
                Labels[4].Text = cache[0, 1].valid.ToString();
                Labels[5].Text = cache[0, 1].tag.ToString();
                Labels[6].Text = cache[0, 1].index.ToString();
                Labels[7].Text = cache[0, 1].data.ToString(); 
            }

            if (cache[0, 2].empty.Equals(true))
            {
                Labels[8].Text = cache[0, 2].valid.ToString();
                Labels[9].Text = cache[0, 2].tag.ToString();
                Labels[10].Text = cache[0, 2].index.ToString();
                Labels[11].Text = cache[0, 2].data.ToString(); 
            }

            if (cache[0, 3].empty.Equals(true))
            {
                Labels[12].Text = cache[0, 3].valid.ToString();
                Labels[13].Text = cache[0, 3].tag.ToString();
                Labels[14].Text = cache[0, 3].index.ToString();
                Labels[15].Text = cache[0, 3].data.ToString(); 
            }
        }//end if
    }//end UpdateFPMultRS()

   }

