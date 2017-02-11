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
    public partial class TeethInfo : Form
    {
        private ProcessTeethInfo _proc;
        private int _clienId;
        private int _number;

        public TeethInfo(string number, int id)
        {
            InitializeComponent();
            _proc = new ProcessTeethInfo();
            _clienId = id;
            int.TryParse(number, out _number);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                _proc.SaveTeethInfo(new TeethInformation { ClientId = _clienId, Info = textBox1.Text, Teeth_number = _number });
                MessageBox.Show("Информация сохранена");
            }
        }

        private void TeethInfo_Load(object sender, EventArgs e)
        {
            textBox1.Text = _proc.LoadInformation(new TeethInformation { ClientId = _clienId, Teeth_number = _number });
        }
    }
}
