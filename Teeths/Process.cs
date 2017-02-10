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

    class Process
    {


        public Process()
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
    }
}
