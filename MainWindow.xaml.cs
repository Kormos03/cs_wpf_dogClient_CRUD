﻿using System;
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

    public partial class MainWindow : Window
    {
        dogService dogService = new dogService();
        public static bool opened = false;
        public static bool closing = false;

        public MainWindow()
        {
            InitializeComponent();  
            mainTable.ItemsSource = dogService.GetDogs();
            if (closing == true) { this.Close(); return; }
            if(opened == false)
            {
                LoginForm loginForm = new LoginForm();
                opened = true;
                loginForm.ShowDialog();
            }
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

        private void modifyButton_Click(object sender, RoutedEventArgs e)
        {
            Dog dog = mainTable.SelectedItem as Dog;
            if (dog == null) { MessageBox.Show("Please select a dog!"); return; }
            DogForm dogForm = new DogForm(dog);
            dogForm.Closed += (_, _) =>
            {
                mainTable.ItemsSource = dogService.GetDogs();
            };
            dogForm.ShowDialog();

            mainTable.ItemsSource = dogService.GetDogs();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            // Close all windows
            closing = true;
        }
    }
}
