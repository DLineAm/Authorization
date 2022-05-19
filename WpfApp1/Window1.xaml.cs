using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using WpfApp1.Annotations;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        private User _user;

        public Window1(User user)
        {
            User = user;
            InitializeComponent();
        }

        public User User
        {
            get => _user;
            set
            { 
                _user = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
