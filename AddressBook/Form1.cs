using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddressBook
{
    public partial class Form1 : Form
    {
        //global variables
        private string path = "AddressBook.txt";
        private int index = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            readFile();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(txtName.Text + "," + txtStreetNo.Text + "," + txtStreetName.Text + "," + txtCity.Text + "," + txtProvince.Text);
            sw.Close();
            listBox1.Items.Clear();
            readFile();

            //clear text field and set focue back on the first text box
            clearTextSetFocus();

        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            //delete existing file
            File.Delete(path);
            StreamWriter sw = new StreamWriter(path);
            for(int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i == listBox1.SelectedIndex)
                {
                    sw.WriteLine(txtName.Text + "," + txtStreetNo.Text + "," + txtStreetName.Text + "," + txtCity.Text + "," + txtProvince.Text);
                }
                else
                {
                    sw.WriteLine(listBox1.Items[i].ToString());
                }
            }
            sw.Close();
            listBox1.Items.Clear();
            readFile();
            clearTextSetFocus();
            setControlState("i");
            
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            File.Delete(path);
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i != listBox1.SelectedIndex)
                {
                    sw.WriteLine(listBox1.Items[i].ToString());
                }
                
            }
            sw.Close();
            listBox1.Items.Clear();
            readFile();
            clearTextSetFocus();
            setControlState("i");
        }

        private void readFile()
        {
            StreamReader sr = new StreamReader(path);
            string record = sr.ReadLine();
            while (record != null)
            {
                //display the code in listbox
                listBox1.Items.Add(record);
                record = sr.ReadLine();
            }
            sr.Close();
        }

        private void clearTextSetFocus()
        {
            txtName.Clear();
            txtStreetNo.Clear();
            txtStreetName.Clear();
            txtCity.Clear();
            txtProvince.Clear();

            txtName.Focus();
        }

        private void setControlState(string state)
        {
            if (state.Equals("u/d"))
            {
                cmdInsert.Enabled = false;
                cmdUpdate.Enabled = true;
                cmdDelete.Enabled = true;
            }
            else if (state.Equals("i"))
            {
                cmdInsert.Enabled = true;
                cmdUpdate.Enabled = false;
                cmdDelete.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = listBox1.SelectedIndex;
            if (index > -1)
            {
                char[] delim = { ',' };
                string[] record = listBox1.SelectedItem.ToString().Split(delim);
                txtName.Text = record[0];
                txtStreetNo.Text = record[1];
                txtStreetName.Text = record[2];
                txtCity.Text = record[3];
                txtProvince.Text = record[4];
                setControlState("u/d");
            }
           
        }
    }
}
