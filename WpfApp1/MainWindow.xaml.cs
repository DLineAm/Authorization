using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using WpfApp1.Annotations;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _generatedCaptcha;
        private string _enteredCaptcha;
        private Visibility _captchaVisibility = Visibility.Collapsed;
        private bool _isInputEnabled = true;

        private DispatcherTimer _lockTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(15)
        };

        public MainWindow()
        {
            InitializeComponent();
            _lockTimer.Tick += delegate { IsInputEnabled = true; _lockTimer.Stop();  };
        }

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set
            { 
                _password = value;
                OnPropertyChanged();
            }
        }

        public string GeneratedCaptcha
        {
            get => _generatedCaptcha;
            set
            {
                _generatedCaptcha = value;
                OnPropertyChanged();
            }
        }

        public Visibility CaptchaVisibility
        {
            get => _captchaVisibility;
            set
            {
                _captchaVisibility = value;
                OnPropertyChanged();
            }
        }

        public string EnteredCaptcha
        {
            get => _enteredCaptcha;
            set
            {
                _enteredCaptcha = value;
                OnPropertyChanged();
            }
        }

        public bool IsInputEnabled
        {
            get => _isInputEnabled;
            set
            {
                _isInputEnabled = value;
                OnPropertyChanged();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login) ||
                string.IsNullOrWhiteSpace(Password) ||
                CaptchaVisibility == Visibility.Visible && string.IsNullOrWhiteSpace(EnteredCaptcha) )
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка");
                return;
            }

            if (CaptchaVisibility == Visibility.Visible &&
                EnteredCaptcha != GeneratedCaptcha)
            {
                MessageBox.Show("Введенная каптча не совпадает со сгенерированной, повторите попытку через 15 сек", "Ошибка");

                IsInputEnabled = false;
                _lockTimer.Start();

                GeneratedCaptcha = GenerateCaptcha();

                return;
            }

            var user = App.db.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (user == null)
            {
                MessageBox.Show("Неверное имя пользователя и/или пароль", "Ошибка");

                CaptchaVisibility = Visibility.Visible;
                GeneratedCaptcha = GenerateCaptcha();

                return;
            }

            var window = new Window1(user);
            window.Show();
            this.Close();
        }

        private string GenerateCaptcha()
        {
            var symbols = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            var result = "";
            var random = new Random();
            for (int i = 0; i < 4; i++)
            {
                result += symbols[random.Next(symbols.Length)];
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CaptchaRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            GeneratedCaptcha = GenerateCaptcha();
        }
    }
}
