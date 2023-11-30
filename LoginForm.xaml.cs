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
using System.Windows.Shapes;

namespace wpf_dogClient_gyakorlo
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        User user;
        dogService dogService = new dogService();
        public LoginForm()
        {
            InitializeComponent();
            User.users.Add(new User("admin", "admin", "admin@admin.com"));
            loginForm();
        }

        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            registForm();
        }

        private void registrateButton_Click(object sender, RoutedEventArgs e)
        {
            registrateButton.IsEnabled = false;

            user = new User(usernameTextBox.Text, passwordTextBox.Password, emailTextBox.Text);
            if (user.register(user))
            {
                loginForm();
                registrateButton.IsEnabled = true;
                return;
            }
            registrateButton.IsEnabled = true;
            return;
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            user = new User("", "", "");   
            if (user.login(usernameTextBox.Text,passwordTextBox.Password))
            {
                this.Visibility = Visibility.Hidden;
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            loginForm();
        }

        public void loginForm()
        {
            registrationButton.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Visible;
            registrateButton.Visibility = Visibility.Hidden;
            emailLabel.Visibility = Visibility.Hidden;
            emailTextBox.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            usernameTextBox.Text = "";
            passwordTextBox.Password = "";
            emailTextBox.Text = "";
        }

        public void registForm()
        {
            registrationButton.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Hidden;
            registrateButton.Visibility = Visibility.Visible;
            emailLabel.Visibility = Visibility.Visible;
            emailTextBox.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;
            usernameTextBox.Text = "";
            passwordTextBox.Password = "";
            emailTextBox.Text = "";
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            // Close all windows
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
        }

}
}

