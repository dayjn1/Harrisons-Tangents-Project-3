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
    public partial class CacheFourWay : Form
    {
        public CacheFourWay()
        {
            InitializeComponent();
        }

        public void InitForm()
        {
            List<Label> Labels = new List<Label>()
            {
                AddressTag, AddressIndex, AddressOffset,
                 Set0_Entry0_valid, Set0_Entry0_tag, Set0_Entry0_index, Set0_Entry0_data,
                Set0_Entry1_valid, Set0_Entry1_tag, Set0_Entry1_index, Set0_Entry1_data,
                Set0_Entry2_valid, Set0_Entry2_tag, Set0_Entry2_index, Set0_Entry2_data,
                Set0_Entry3_valid, Set0_Entry3_tag, Set0_Entry3_index, Set0_Entry3_data,
                 Set1_Entry0_valid, Set1_Entry0_tag, Set1_Entry0_index, Set1_Entry0_data,
                Set1_Entry1_valid, Set1_Entry1_tag, Set1_Entry1_index, Set1_Entry1_data,
                Set1_Entry2_valid, Set1_Entry2_tag, Set1_Entry2_index, Set1_Entry2_data,
                Set1_Entry3_valid, Set1_Entry3_tag, Set1_Entry3_index, Set1_Entry3_data,
                 Set2_Entry0_valid, Set2_Entry0_tag, Set2_Entry0_index, Set2_Entry0_data,
                Set2_Entry1_valid, Set2_Entry1_tag, Set2_Entry1_index, Set2_Entry1_data,
                Set2_Entry2_valid, Set2_Entry2_tag, Set2_Entry2_index, Set2_Entry2_data,
                Set2_Entry3_valid, Set2_Entry3_tag, Set2_Entry3_index, Set2_Entry3_data,
                Set3_Entry0_valid, Set3_Entry0_tag, Set3_Entry0_index, Set3_Entry0_data,
                Set3_Entry1_valid, Set3_Entry1_tag, Set3_Entry1_index, Set3_Entry1_data,
                Set3_Entry2_valid, Set3_Entry2_tag, Set3_Entry2_index, Set3_Entry2_data,
                Set3_Entry3_valid, Set3_Entry3_tag, Set3_Entry3_index, Set3_Entry3_data

            };
            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }
        }

        public void UpdateEntry(int[] position)
        {
            switch (position[0])
            {
                case 0:
                    {
                        UpdateSet0(Cache.CacheArray);
                        break;
                    }
                case 1:
                    {
                        UpdateSet1(Cache.CacheArray);
                        break;
                    }
                case 2:
                    {
                        UpdateSet2(Cache.CacheArray);
                        break;
                    }
                case 3:
                    {
                        UpdateSet3(Cache.CacheArray);
                        break;
                    }
                default:
                    break;
            }
        }

        public void UpdateAddressLabel(Instruction instruction)
        {

            List<Label> Labels = new List<Label>()
            {
                AddressTag, AddressIndex, AddressOffset
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }

            CacheEntry temp = MemUnit.AddressToLookUp(instruction);
            Labels[0].Text = "Instr: " + instruction.Mnemonic + ", Tag: " + temp.tag.ToString();
            Labels[1].Text = "Index: " + temp.index.ToString();
            Labels[2].Text = temp.offset.ToString();
            //Labels[0].Text = temp.tag.ToString();            
            //Labels[1].Text = temp.index.ToString();
            //Labels[2].Text = temp.offset.ToString();
            Update();
        }
      
        public void UpdateSet0(CacheEntry[,] cache)
        {
            List<Label> Labels = new List<Label>()
            {
                Set0_Entry0_valid, Set0_Entry0_tag, Set0_Entry0_index, Set0_Entry0_data,
                Set0_Entry1_valid, Set0_Entry1_tag, Set0_Entry1_index, Set0_Entry1_data,
                Set0_Entry2_valid, Set0_Entry2_tag, Set0_Entry2_index, Set0_Entry2_data,
                Set0_Entry3_valid, Set0_Entry3_tag, Set0_Entry3_index, Set0_Entry3_data
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }

            if (cache[0, 0].empty != false)
            {
                Labels[0].Text = cache[0, 0].valid.ToString();
                Labels[1].Text = cache[0, 0].tag.ToString();
                Labels[2].Text = cache[0, 0].index.ToString();
                Labels[3].Text = cache[0, 0].data.ToString();
            }



            if (cache[0, 1].empty != false)
            {
                Labels[4].Text = cache[0, 1].valid.ToString();
                Labels[5].Text = cache[0, 1].tag.ToString();
                Labels[6].Text = cache[0, 1].index.ToString();
                Labels[7].Text = cache[0, 1].data.ToString(); 
            }

            if (cache[0, 2].empty != false)
            {
                Labels[8].Text = cache[0, 2].valid.ToString();
                Labels[9].Text = cache[0, 2].tag.ToString();
                Labels[10].Text = cache[0, 2].index.ToString();
                Labels[11].Text = cache[0, 2].data.ToString(); 
            }

            if (cache[0, 3].empty != false)
            {
                Labels[12].Text = cache[0, 3].valid.ToString();
                Labels[13].Text = cache[0, 3].tag.ToString();
                Labels[14].Text = cache[0, 3].index.ToString();
                Labels[15].Text = cache[0, 3].data.ToString(); 
            }
            Update();
        }//end UpdateSet0()

        public void UpdateSet1(CacheEntry[,] cache)
        {
            List<Label> Labels = new List<Label>()
            {
                Set1_Entry0_valid, Set1_Entry0_tag, Set1_Entry0_index, Set1_Entry0_data,
                Set1_Entry1_valid, Set1_Entry1_tag, Set1_Entry1_index, Set1_Entry1_data,
                Set1_Entry2_valid, Set1_Entry2_tag, Set1_Entry2_index, Set1_Entry2_data,
                Set1_Entry3_valid, Set1_Entry3_tag, Set1_Entry3_index, Set1_Entry3_data
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }

            if (cache[1, 0].empty != false)
            {
                Labels[0].Text = cache[1, 0].valid.ToString();
                Labels[1].Text = cache[1, 0].tag.ToString();
                Labels[2].Text = cache[1, 0].index.ToString();
                Labels[3].Text = cache[1, 0].data.ToString();
            }



            if (cache[1, 1].empty != false)
            {
                Labels[4].Text = cache[1, 1].valid.ToString();
                Labels[5].Text = cache[1, 1].tag.ToString();
                Labels[6].Text = cache[1, 1].index.ToString();
                Labels[7].Text = cache[1, 1].data.ToString();
            }

            if (cache[1, 2].empty != false)
            {
                Labels[8].Text = cache[1, 2].valid.ToString();
                Labels[9].Text = cache[1, 2].tag.ToString();
                Labels[10].Text = cache[1, 2].index.ToString();
                Labels[11].Text = cache[1, 2].data.ToString();
            }

            if (cache[0, 3].empty != false)
            {
                Labels[12].Text = cache[1, 3].valid.ToString();
                Labels[13].Text = cache[1, 3].tag.ToString();
                Labels[14].Text = cache[1, 3].index.ToString();
                Labels[15].Text = cache[1, 3].data.ToString();
            }
            Update();
        }//end UpdateSet1()

        public void UpdateSet2(CacheEntry[,] cache)
        {
            List<Label> Labels = new List<Label>()
            {
                Set2_Entry0_valid, Set2_Entry0_tag, Set2_Entry0_index, Set2_Entry0_data,
                Set2_Entry1_valid, Set2_Entry1_tag, Set2_Entry1_index, Set2_Entry1_data,
                Set2_Entry2_valid, Set2_Entry2_tag, Set2_Entry2_index, Set2_Entry2_data,
                Set2_Entry3_valid, Set2_Entry3_tag, Set2_Entry3_index, Set2_Entry3_data
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }

            if (cache[2, 0].empty != false)
            {
                Labels[0].Text = cache[2, 0].valid.ToString();
                Labels[1].Text = cache[2, 0].tag.ToString();
                Labels[2].Text = cache[2, 0].index.ToString();
                Labels[3].Text = cache[2, 0].data.ToString();
            }



            if (cache[2, 1].empty != false)
            {
                Labels[4].Text = cache[2, 1].valid.ToString();
                Labels[5].Text = cache[2, 1].tag.ToString();
                Labels[6].Text = cache[2, 1].index.ToString();
                Labels[7].Text = cache[2, 1].data.ToString();
            }

            if (cache[2, 2].empty != false)
            {
                Labels[8].Text = cache[2, 2].valid.ToString();
                Labels[9].Text = cache[2, 2].tag.ToString();
                Labels[10].Text = cache[2, 2].index.ToString();
                Labels[11].Text = cache[2, 2].data.ToString();
            }

            if (cache[2, 3].empty != false)
            {
                Labels[12].Text = cache[2, 3].valid.ToString();
                Labels[13].Text = cache[2, 3].tag.ToString();
                Labels[14].Text = cache[2, 3].index.ToString();
                Labels[15].Text = cache[2, 3].data.ToString();
            }
            Update();
        }//end UpdateSet2()

        public void UpdateSet3(CacheEntry[,] cache)
        {
            List<Label> Labels = new List<Label>()
            {
                Set3_Entry0_valid, Set3_Entry0_tag, Set3_Entry0_index, Set3_Entry0_data,
                Set3_Entry1_valid, Set3_Entry1_tag, Set3_Entry1_index, Set3_Entry1_data,
                Set3_Entry2_valid, Set3_Entry2_tag, Set3_Entry2_index, Set3_Entry2_data,
                Set3_Entry3_valid, Set3_Entry3_tag, Set3_Entry3_index, Set3_Entry3_data
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }

            if (cache[3, 0].empty != false)
            {
                Labels[0].Text = cache[3, 0].valid.ToString();
                Labels[1].Text = cache[3, 0].tag.ToString();
                Labels[2].Text = cache[3, 0].index.ToString();
                Labels[3].Text = cache[3, 0].data.ToString();
            }

            if (cache[3, 1].empty != false)
            {
                Labels[4].Text = cache[3, 1].valid.ToString();
                Labels[5].Text = cache[3, 1].tag.ToString();
                Labels[6].Text = cache[3, 1].index.ToString();
                Labels[7].Text = cache[3, 1].data.ToString();
            }

            if (cache[3, 2].empty != false)
            {
                Labels[8].Text = cache[3, 2].valid.ToString();
                Labels[9].Text = cache[3, 2].tag.ToString();
                Labels[10].Text = cache[3, 2].index.ToString();
                Labels[11].Text = cache[3, 2].data.ToString();
            }

            if (cache[3, 3].empty != false)
            {
                Labels[12].Text = cache[3, 3].valid.ToString();
                Labels[13].Text = cache[3, 3].tag.ToString();
                Labels[14].Text = cache[3, 3].index.ToString();
                Labels[15].Text = cache[3, 3].data.ToString();
            }
            Update();
        }//end UpdateSet3()

        public void UpdateHit()
        {
            hitTextBox.BackColor = Color.Green;
            Update();
        }

        public void UpdateCompMiss()
        {
            CompMiss.BackColor = Color.Red;
            Update();
        }
        public void UpdateCapacityMiss()
        {
            CapMiss.BackColor = Color.Red;
            Update();
        }

        public void UpdateConflictMiss()
        {
            ConflictMiss.BackColor = Color.Red;
            Update();
        }

        private void okayBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        public void ResetTextBoxBG()
        {
            ConflictMiss.BackColor = Color.White;
            CapMiss.BackColor = Color.White;
            CompMiss.BackColor = Color.White;
            hitTextBox.BackColor = Color.White;
        }
    }




}

