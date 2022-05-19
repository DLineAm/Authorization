using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PseudoDataBase db = new PseudoDataBase();
    }

    public class PseudoDataBase
    {
        public List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Name = "Abooba",
                Login = "123",
                Password = "123"
            },
            new User
            {
                Name = "Baobab",
                Login = "1",
                Password = "1"
            },
            new User
            {
                Name = "Bulba",
                Login = "2",
                Password = "2"
            },
        };
    }

    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
