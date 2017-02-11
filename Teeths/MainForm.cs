using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Teeths.server;

namespace Teeths
{
    public partial class MainForm : Form
    {
        ProcessClient _proc;

        public MainForm()
        {
            InitializeComponent();
            _proc = new ProcessClient();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel53_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //show teeth's model
        {
            Form form2 = new JawModel((int)((clientlist.SelectedItem as ComboboxItem).Value));
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //add New Client
        {
            if (clientlist.Text == "Новый пациент")
            {
                Client _cl = new Client();
                _cl.Name = name.Text;
                _cl.Number = number.Text;
                _cl.Createdate = createdate.Value;
                _cl.Old = old.Text;
                //_cl.Sex = sex.SelectedIndex;
                _cl.Adress = adress.Text;
                _cl.Proffesion = profesion.Text;
                _cl.DiseaseInfo = diseaseInfo.Text;
                _cl.DiseaseNow = diseaseNow.Text;
                _cl.FirstDiagnos = firstdiagnos.Text;
                _proc.AddNewClient(_cl);
                this.ClearFields();
                MessageBox.Show("Новый пациент добавлен!");
                this.UpdateList();
            }
        }

        private void ClearFields()
        {
            name.Text = "";
            number.Text = "";
            old.Text = "";
            adress.Text = "";
            profesion.Text = "";
            diseaseInfo.Text = "";
            diseaseNow.Text = "";
            firstdiagnos.Text = "";

        }

        private void button2_Click(object sender, EventArgs e) //change Client
        {
            Client _cl = new Client();
            _cl.Name = name.Text;
            _cl.Number = number.Text;
            _cl.Createdate = createdate.Value;
            _cl.Old = old.Text;
            //_cl.Sex = sex.SelectedIndex;
            _cl.Adress = adress.Text;
            _cl.Proffesion = profesion.Text;
            _cl.DiseaseInfo = diseaseInfo.Text;
            _cl.DiseaseNow = diseaseNow.Text;
            _cl.FirstDiagnos = firstdiagnos.Text;
            _proc.UpdateclientInfo(_cl, (int)((clientlist.SelectedItem as ComboboxItem).Value));
            MessageBox.Show("Данные пациента изменены!");

        }

        private void button3_Click(object sender, EventArgs e) //delete client
        {
            _proc.DeleteClient((int)((clientlist.SelectedItem as ComboboxItem).Value));
            this.ClearFields();
            this.UpdateList();
            MessageBox.Show("Данные о пациенте удалены!");
        }

        private void clientlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clientlist.Text == "Новый пациент")
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                linkLabel53.Enabled = false;
                this.ClearFields();
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                linkLabel53.Enabled = true;

                Client _cl = new Client();
                _proc.getClientData(ref _cl, (int)((clientlist.SelectedItem as ComboboxItem).Value));

                name.Text = _cl.Name.TrimEnd();
                number.Text = _cl.Number.TrimEnd();
                createdate.Value = (DateTime)_cl.Createdate;
                old.Text = _cl.Old.TrimEnd();
                //_cl.Sex = sex.SelectedIndex;
                adress.Text = _cl.Adress.TrimEnd();
                profesion.Text = _cl.Proffesion.TrimEnd();
                diseaseInfo.Text = _cl.DiseaseInfo.TrimEnd();
                diseaseNow.Text = _cl.DiseaseNow.TrimEnd();
                firstdiagnos.Text = _cl.FirstDiagnos.TrimEnd();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.UpdateList();
        }

        private void UpdateList()
        {

            List<ClientsListData> clients = new List<ClientsListData>();
            _proc.update_clientList(ref clients);

            clientlist.Items.Clear();
            ComboboxItem ite = new ComboboxItem();
            ite.Text = "Новый пациент";
            ite.Value = -1;
            clientlist.Items.Add(ite);
            foreach (ClientsListData s in clients)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = s.Name.TrimEnd();
                item.Value = s.Id;
                clientlist.Items.Add(item);
                clientlist.SelectedIndex = 0;

            }
        }
    }
}
