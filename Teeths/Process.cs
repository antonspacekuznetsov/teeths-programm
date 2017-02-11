using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Teeths.server;

namespace Teeths
{

    class ProcessClient
    {


        public ProcessClient()
        {


        }

        public void update_clientList(ref List<ClientsListData> _data)
        {
             using (IController<Client> sql = new Controller<Client>())
                foreach (Client r in sql.GetAll())
                {
                    _data.Add(new ClientsListData {Id = r.Id, Name = r.Name });
                }
        }

        public void AddNewClient(Client _cl)
        {
            try
            {
                using (IController<Client> sql = new Controller<Client>())
                {
                    sql.Create(_cl);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void getClientData(ref Client _cl, int id)
        {
            try
            {
                using (IController<Client> sql = new Controller<Client>())
                {
                    _cl = sql.GetById(id);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void UpdateclientInfo(Client _cl, int id)
        {
            try
            {
                using (IController<Client> sql = new Controller<Client>())
                {
                   Client  cl = sql.GetById(id);

                   cl.Name = _cl.Name;
                   cl.Number = _cl.Number;
                   cl.Createdate = _cl.Createdate;
                   cl.Old = _cl.Old;
                   //_cl.Sex = sex.SelectedIndex;
                   cl.Adress = _cl.Adress;
                   cl.Proffesion = _cl.Proffesion;
                   cl.DiseaseInfo = _cl.DiseaseInfo;
                   cl.DiseaseNow = _cl.DiseaseNow;
                   cl.FirstDiagnos = _cl.FirstDiagnos;
                   sql.Update(cl);

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }

        }

        public void DeleteClient(int id)
        {
            try
            {
                using (IController<Client> sql = new Controller<Client>())
                {
                    sql.Delete(id);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }
    }

    class ProcessTeethInfo
    {
        public ProcessTeethInfo()
        {

        }

        public void SaveTeethInfo(TeethInformation _tf)
        {
            try
            {
                using (IController<TeethInformation> sql = new Controller<TeethInformation>())
                {
                    bool priznak = false;

                    foreach (TeethInformation tf in sql.GetAll())
                    {
                        if (tf.ClientId == _tf.ClientId && tf.Teeth_number == _tf.Teeth_number)
                        {
                            tf.Info = _tf.Info;
                            sql.Update(tf);
                            priznak = true;
                            break;
                        }
                    }

                    if (!priznak)
                    {
                        sql.Create(_tf);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public string LoadInformation(TeethInformation _tf)
        {
            string result = string.Empty;
            try
            {
                using (IController<TeethInformation> sql = new Controller<TeethInformation>())
                {
                    foreach (TeethInformation tf in sql.GetAll())
                    {
                        if (tf.ClientId == _tf.ClientId && tf.Teeth_number == _tf.Teeth_number)
                        {
                            result = tf.Info;
                            break;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }

            return result.TrimEnd();

        }

    }
}
