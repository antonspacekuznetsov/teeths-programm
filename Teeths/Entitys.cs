using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace Teeths
{
    public class Client : INotifyPropertyChanged
    {
        private string number;
        private string createdate;
        private string name;
        private string old;
        private int sex;
        private string adress;
        private string profesion;
        private string firstdiagnos;
        private string diseaseInfo;
        private string diseaseNow;

        public int Id { get; set; }
        public string Number
        {
            get { return number; }

            set
            {
                number = value;
                OnPropertyChanged("Number");
            }

        }
        public string Createdate
        {
            get { return createdate; }

            set
            {
                createdate = value;
                OnPropertyChanged("Createdate");
            }

        }
        public string Name
        {
            get { return name; }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }

        }
        public string Old
        {
            get { return old; }

            set
            {
                old = value;
                OnPropertyChanged("Old");
            }

        }
        public int Sex
        {
            get { return sex; }

            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }

        }
        public string Adress
        {
            get { return adress; }

            set
            {
                adress = value;
                OnPropertyChanged("Adress");
            }

        }
        public string Profesion
        {
            get { return profesion; }

            set
            {
                profesion = value;
                OnPropertyChanged("Profesion");
            }

        }
        public string Firstdiagnos
        {
            get { return firstdiagnos; }

            set
            {
                firstdiagnos = value;
                OnPropertyChanged("Firstdiagnos");
            }

        }
        public string DiseaseInfo
        {
            get { return diseaseInfo; }

            set
            {
                diseaseInfo = value;
                OnPropertyChanged("DiseaseInfo");
            }

        }
        public string DiseaseNow
        {
            get { return diseaseNow; }

            set
            {
                diseaseNow = value;
                OnPropertyChanged("DiseaseNow");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}