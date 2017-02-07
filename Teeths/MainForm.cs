using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teeths
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel53_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //show teeth's model
        {
            Form form2 = new JawModel();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //add New Client
        {

        }

        private void button2_Click(object sender, EventArgs e) //change Client
        {

        }

        private void button3_Click(object sender, EventArgs e) //delete client
        {

        }
    }
}
