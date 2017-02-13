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


        public ProcessClient()  { }

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

        public void DeleteAllTeethInfo(int id)
        {
            try
            {
                using (IController<TeethInformation> sql = new Controller<TeethInformation>())
                {
                    foreach (TeethInformation tf in sql.GetAll())
                    {
                        if (tf.ClientId == id)
                        {
                            sql.Delete(tf.Id);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }



        public void AddTableTeeth(Everytooth _t)
        {
            try
            {
                using (IController<Everytooth> sql = new Controller<Everytooth>())
                {
                    sql.Create(_t);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void load_teethtable(ref List<Everytooth> _t, int id)
        {
            try
            {
                using (IController<Everytooth> sql = new Controller<Everytooth>())
                {
                    foreach (Everytooth t in sql.GetAll())
                    {
                        if (t.ClientId == id)
                            _t.Add(t);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void DeleteAllTableTeeth(int id)
        {
            try
            {
                using (IController<Everytooth> sql = new Controller<Everytooth>())
                {
                    foreach (Everytooth tf in sql.GetAll())
                    {
                        if (tf.ClientId == id)
                        {
                            sql.Delete(tf.Id);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }



        public void AddGeneralData(DataView _dv)
        {
            try
            {
                using (IController<DataView> sql = new Controller<DataView>())
                {
                    bool priznak = false;

                    foreach (DataView d in sql.GetAll())
                    {
                        if (d.ClientId == _dv.ClientId)
                        {
                            d.DataOuterView = _dv.DataOuterView;
                            d.DataXray = _dv.DataXray;
                            d.Descriptionbite = _dv.Descriptionbite;
                            d.Descriptionmucous = _dv.Descriptionmucous;
                            sql.Update(d);
                            priznak = true;
                            break;
                        }
                    }
                    if (!priznak)
                        sql.Create(_dv);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void DeleteAllGeneralData(int id)
        {
            try
            {
                using (IController<DataView> sql = new Controller<DataView>())
                {
                    foreach (DataView tf in sql.GetAll())
                    {
                        if (tf.ClientId == id)
                        {
                            sql.Delete(tf.Id);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void loadGeneralData(ref DataView _dv, int id)
        {
            try
            {
                using (IController<DataView> sql = new Controller<DataView>())
                {
                    foreach (DataView t in sql.GetAll())
                    {
                        if (t.ClientId == id)
                        {
                            _dv = t;
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void addGeneralPart(GeneralPart _gp)
        {
            try
            {
                using (IController<GeneralPart> sql = new Controller<GeneralPart>())
                {
                    bool priznak = false;
                    foreach (GeneralPart gp in sql.GetAll())
                    {
                        if (gp.ClientId == _gp.ClientId)
                        {
                            gp.cureFeatures = _gp.cureFeatures;
                            gp.curePlan = _gp.curePlan;
                            gp.signConsulation = _gp.signConsulation;
                            gp.viewPlan = _gp.viewPlan;

                            sql.Update(gp);

                            priznak = true;
                        }
                    }

                    if (!priznak)
                    {
                        sql.Create(_gp);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }

        }

        public void loadGeneralPart(ref GeneralPart gp, int id)
        {
            try
            {
                using (IController<GeneralPart> sql = new Controller<GeneralPart>())
                {
                    foreach (GeneralPart t in sql.GetAll())
                    {
                        if (t.ClientId == id)
                        {
                            gp = t;
                            break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message);
            }
        }

        public void deleteAllGeneralPart(int id)
        {
            try
            {
                using (IController<GeneralPart> sql = new Controller<GeneralPart>())
                {
                    foreach (GeneralPart t in sql.GetAll())
                    {
                        if (t.ClientId == id)
                        {
                            sql.Delete(t.Id);

                        }
                    }
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
