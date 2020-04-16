using SecureLogin.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecureLogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            // Call API To check if input was correct and user is logged in.
            // TODO LoginHandler should be dependency injected instead
            LoginHandler logHandler = new LoginHandler();
            var response = logHandler.ValidateInput(UsernameField.Text, PasswordFieldHidden.Password);
            if(response.Result == true)
            {
                MessageBox.Show("YOU ARE NOW LOGGED IN");
            }
            else
            {
                MessageBox.Show("Something went wrong, please try again");
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Calls API to create a user with input from the fields ( mainly for test) 
            // TODO UserHandler should be dependency injected instead
            UserHandler userHandler = new UserHandler();
            userHandler.CreateUser(UsernameField.Text, PasswordFieldHidden.Password);
        }
    }
}
