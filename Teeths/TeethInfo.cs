﻿using System;
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
    public partial class TeethInfo : Form
    {
        public TeethInfo(string number, int id)
        {
            InitializeComponent();
            textBox1.Text = number;
        }
    }
}
