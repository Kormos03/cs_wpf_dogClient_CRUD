using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace wpf_dogClient_gyakorlo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //Make a database for login validation and then, every doctor can add their own dogs to the database.
    //Make a login form, where the doctor can login and see every dog to prevent unaccepted access.
    //The default login is the admin:admin, who can see every dog.
    //Make a form, where the doctor can edit the dog's data.
    //Make the frontend of the application.

    public partial class MainWindow : Window
    {
        dogService dogService = new dogService();
        public MainWindow()
        {
            InitializeComponent();  
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            mainTable.ItemsSource = dogService.GetDogs();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DogForm dogForm = new DogForm();
            dogForm.ShowDialog();
            mainTable.ItemsSource = dogService.GetDogs();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(mainTable.SelectedItem != null)
            {
                MessageBoxResult areusure = MessageBox.Show("Are you sure you want to delete this dog?", "Delete", MessageBoxButton.YesNo); 
                if (areusure == MessageBoxResult.No) return;
                else
                {
                    dogService.deleteDog((Dog)mainTable.SelectedItem);
                    mainTable.ItemsSource = dogService.GetDogs();
                    return;
                }
            }
            MessageBox.Show("Please select a dog!");
            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(mainTable.ItemsSource);
                view.Filter = o =>
                {
                    if (string.IsNullOrEmpty(searchBox.Text)) return true;
                    if (o is Dog item)
                    {
                        return item.Name.Contains(searchBox.Text);
                    }
                    return false;
                };
            
        }
    }
}
