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
using Teeths.QRCoder;

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
                _cl.Sex = (sex.Text == "Мужской" ? (byte)1 : (byte)0);
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
            _cl.Sex = (sex.Text == "Мужской" ? (byte)1 : (byte)0);
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
            _proc.deleteAllGeneralPart((int)((clientlist.SelectedItem as ComboboxItem).Value));
            _proc.DeleteAllGeneralData((int)((clientlist.SelectedItem as ComboboxItem).Value));
            _proc.DeleteAllTeethInfo((int)((clientlist.SelectedItem as ComboboxItem).Value));
            _proc.DeleteAllTableTeeth((int)((clientlist.SelectedItem as ComboboxItem).Value));
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
                tabPage3.Enabled = false;
                tabPage2.Enabled = false;
                this.ClearFields();
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                linkLabel53.Enabled = true;
                tabPage3.Enabled = true;
                tabPage2.Enabled = true;

                Client _cl = new Client();
                _proc.getClientData(ref _cl, (int)((clientlist.SelectedItem as ComboboxItem).Value));

                name.Text = _cl.Name.TrimEnd();
                number.Text = _cl.Number.TrimEnd();
                createdate.Value = (DateTime)_cl.Createdate;
                old.Text = _cl.Old.TrimEnd();
                adress.Text = _cl.Adress.TrimEnd();
                profesion.Text = _cl.Proffesion.TrimEnd();
                diseaseInfo.Text = _cl.DiseaseInfo.TrimEnd();
                diseaseNow.Text = _cl.DiseaseNow.TrimEnd();
                firstdiagnos.Text = _cl.FirstDiagnos.TrimEnd();
                if (_cl.Sex == 1)
                    sex.Text = "Мужской";
                if (_cl.Sex == 0)
                    sex.Text = "Женский";
                this.load_dataview();
                this.loadGenerlPart();
                this.load_teethtable(); //загружаем табилицу состояния зубов
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
            }
            clientlist.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                int ind = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(ind);
            }
            catch (Exception ex)
            {

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            _proc.DeleteAllTableTeeth((int)(clientlist.SelectedItem as ComboboxItem).Value);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Everytooth tooths = new Everytooth();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    switch (cell.ColumnIndex)
                    {
                        case 0:
                            if (cell.Value != null)
                                 tooths.Teeth_number = cell.Value.ToString();
                            break;
                        case 1:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.O = 1;
                                else
                                    tooths.O = 0;
                            }
                            else
                                tooths.O = 0;
                            break;

                        case 2:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.R = 1;
                                else
                                    tooths.R = 0;
                            }
                            else
                                tooths.R = 0;
                            break;

                        case 3:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.C = 1;
                                else
                                    tooths.C = 0;
                            }
                            else
                                tooths.C = 0;
                            break;

                        case 4:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.P = 1;
                                else
                                    tooths.P = 0;
                            }
                            else
                                tooths.P = 0;
                            break;

                        case 5:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.Pt = 1;
                                else
                                    tooths.Pt = 0;
                            }
                            else
                                tooths.Pt = 0;
                            break;

                        case 6:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.Pi = 1;
                                else
                                    tooths.Pi = 0;
                            }
                            else
                                tooths.Pi = 0;
                            break;

                        case 7:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.A = 1;
                                else
                                    tooths.A = 0;
                            }
                            else
                                tooths.A = 0;
                            break;
                        case 8:
                            if (cell.Value != null)
                            tooths.Movement = cell.Value.ToString();
                            break;
                        case 9:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.K = 1;
                                else
                                    tooths.K = 0;
                            }
                            else
                                tooths.K = 0;
                            break;
                        case 10:
                            if (cell.Value != null)
                            {
                                if ((bool)cell.Value)
                                    tooths.I = 1;
                                else
                                    tooths.I = 0;
                            }
                            else
                                tooths.I = 0;
                            break;
                        case 11:
                            if (cell.Value != null)
                            tooths.X = cell.Value.ToString();
                            break;
                        case 12:
                            if (cell.Value != null)
                                tooths.Y = cell.Value.ToString();
                            break;
                        case 13:
                            if (cell.Value != null)
                                tooths.Z = cell.Value.ToString();
                            break;
                    }
                }

                tooths.ClientId = (int)(clientlist.SelectedItem as ComboboxItem).Value;
                _proc.AddTableTeeth(tooths);
            }
            MessageBox.Show("Данные сохранены");
            
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void load_teethtable()
        {
            List<Everytooth> _t = new List<Everytooth>();

            _proc.load_teethtable(ref  _t, ((int)(clientlist.SelectedItem as ComboboxItem).Value));//загружаем список состояния зубов выбраного пициента

            
            int counter = 0;
            dataGridView1.Rows.Clear();

            for (int i = 0; i < _t.Count - 1; i++)
            {
                dataGridView1.Rows.Add();
            }
                foreach (Everytooth t in _t)
                {

                    dataGridView1.Rows[counter].Cells[0].Value = t.Teeth_number;
                    dataGridView1.Rows[counter].Cells[1].Value = (t.O == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[2].Value = (t.R == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[3].Value = (t.C == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[4].Value = (t.P == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[5].Value = (t.Pt == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[6].Value = (t.Pi == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[7].Value = (t.A == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[8].Value = t.Movement;
                    dataGridView1.Rows[counter].Cells[9].Value = (t.K == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[10].Value = (t.I == 1 ? true : false);
                    dataGridView1.Rows[counter].Cells[11].Value = t.X;
                    dataGridView1.Rows[counter].Cells[12].Value = t.Y;
                    dataGridView1.Rows[counter].Cells[13].Value = t.Z;
                    counter++;
                }

                if (_t.Count  == 0)
                {
                    this.fillTable();
                }


        }

        private void load_dataview()
        {
            DataView dv = new DataView();
            _proc.loadGeneralData(ref dv, ((int)(clientlist.SelectedItem as ComboboxItem).Value));
            if (dv == null)
                return;
            textBox2.Text = dv.DataOuterView == null ? "" : dv.DataOuterView.TrimEnd();
            textBox3.Text = dv.Descriptionbite == null ? "" : dv.Descriptionbite.TrimEnd();
            textBox4.Text = dv.Descriptionmucous == null ? "" : dv.Descriptionmucous.TrimEnd();
            textBox5.Text = dv.DataXray == null ? "" : dv.DataXray.TrimEnd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView();
            dv.DataOuterView = textBox2.Text;
            dv.Descriptionbite = textBox3.Text;
            dv.Descriptionmucous = textBox4.Text;
            dv.DataXray = textBox5.Text;
            dv.ClientId = (int)(clientlist.SelectedItem as ComboboxItem).Value;
            _proc.AddGeneralData(dv);
            MessageBox.Show("Данные сохранены");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GeneralPart gp = new GeneralPart();
            gp.ClientId = (int)(clientlist.SelectedItem as ComboboxItem).Value;
            gp.viewPlan = textBox9.Text;
            gp.curePlan = textBox8.Text;
            gp.cureFeatures = textBox7.Text;
            gp.signConsulation = textBox6.Text;
            gp.terms = textBox10.Text;

            _proc.addGeneralPart(gp);

            MessageBox.Show("Данные сохранены");
        }

        private void loadGenerlPart()
        {
            GeneralPart gp = new GeneralPart();
            _proc.loadGeneralPart(ref gp, ((int)(clientlist.SelectedItem as ComboboxItem).Value));

            textBox10.Text = gp.terms == null ? "" : gp.terms.TrimEnd();
            textBox9.Text = gp.viewPlan == null ? "" : gp.viewPlan.TrimEnd();
            textBox8.Text = gp.curePlan == null ? "" : gp.curePlan.TrimEnd();
            textBox7.Text = gp.cureFeatures == null ? "" : gp.cureFeatures.TrimEnd();
            textBox6.Text = gp.signConsulation == null ? "" : gp.signConsulation.TrimEnd();
        }

        private void fillTable()
        {
            int x =1, y = 0;
            for (int i = 0; i < 52; i++)
                dataGridView1.Rows.Add();


            for (int i = 0; i < 52; i++)
            {
                y++;
                dataGridView1.Rows[i].Cells[0].Value = x.ToString() + y.ToString();

                if (x == 4 && y == 8)
                {
                    x = 5;
                    y = 0;
                }

                if (x > 4 && y == 5)
                {
                    x++;
                    y = 0;
                }

                if (y == 8)
                {
                    y = 0;
                    x++;
                }


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            QRCoding qr = new QRCoding();
            qr.GenerateQr("Hello world");
        }

        private void InputTeethInfo(string number)
        {
            tabControl2.SelectTab(0);
            dataGridView1.ClearSelection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().TrimEnd() == number)
                    {
                        dataGridView1.Rows[i].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                        break;
                    }
            }
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel52_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel51_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel50_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel49_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel48_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel47_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel46_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel45_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel44_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel43_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel32_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel31_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel30_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel29_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel28_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel27_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel26_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel25_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel24_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel23_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel22_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel21_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel20_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel19_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel18_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel17_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel33_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel34_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel35_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel36_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel37_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel38_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel39_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel40_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel41_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }

        private void linkLabel42_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = (LinkLabel)sender;
            this.InputTeethInfo(lnk.Text);
        }
    }
}
