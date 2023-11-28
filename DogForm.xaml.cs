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
using System.Windows.Shapes;

namespace wpf_dogClient_gyakorlo
{
    //Dependencies: extended WPF toolkit (NuGet), Newtonsoft.Json (NuGet), System.Data.SqlClient (NuGet)
    /// <summary>
    /// Interaction logic for DogForm.xaml
    /// </summary>
    public partial class DogForm : Window
    {
        dogService dogService = new dogService();
        Dog dog;
        public DogForm()
        {
            InitializeComponent();
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        public Dog validateDog(Dog dog)     //Well, this is not actually validation, but it's a start
        {
            if (String.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Name cannot be empty!");
            }
            else if (String.IsNullOrEmpty(ageTextBox.Text) || (!float.TryParse(ageTextBox.Text, out float age) || age < 0.1 || age > 30))
            {
                MessageBox.Show("The age has to be a number between 0.1 and 30 ");
            }
            else if (String.IsNullOrEmpty(breedTextBox.Text))
            {
                MessageBox.Show("Breed cannot be empty!");
            }
           
            else if (sizeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Size cannot be empty!");
            }
            else if (neuteredComboBox.SelectedItem == null)
            {
                MessageBox.Show("The neutered area must be not empty!");
            }
            else if (vaccinatedComboBox.SelectedItem == null)
            {
                MessageBox.Show("The vaccinated area must be not empty!");
            }
            else if (genderComboBox.SelectedItem == null)
            {
                MessageBox.Show("The gender must be male or female!");
            }
            else if (String.IsNullOrEmpty(diagnosisTextBox.Text))
            {
                MessageBox.Show("The diagnosis cannot be empty!");
            }
            else if (String.IsNullOrEmpty(medicationTextBox.Text))
            {
                MessageBox.Show("The medication cannot be empty!");
            }
            else
            {
                if (visitedPicker.Value == null)
                {
                    dog.VisitedOn = DateTime.Now;
                }
                else { dog.VisitedOn = (DateTime)visitedPicker.Value!; }
                dog.Name = nameTextBox.Text;
                dog.Breed = breedTextBox.Text;
                dog.Neutered = neuteredComboBox.Text == "Yes" ? true : false;
                dog.Age = age;
                dog.Size = (string)sizeComboBox.Text;
                dog.Medication = medicationTextBox.Text;
                dog.Diagnosis = diagnosisTextBox.Text;
                dog.Vaccinated = vaccinatedComboBox.Text == "Yes" ? true : false;
                if(Gender.TryParse(genderComboBox.Text, out Gender value))
                {
                    dog.Gender = value;
                }
                else { Debug.WriteLine("The gender must be male of female bruh"); }
                Debug.WriteLine(ToString());
                return dog;
            }
            return null;

        }

        public void dogFormSend_Click(object sender, RoutedEventArgs e)
        {
            dog = new Dog();
            validateDog(dog);
            dogService.PostDog(dog);
            this.Close();
        }
        public override string ToString()
        {
            return $"{dog.Name}, {dog.Size}, {dog.Breed}, {dog.Medication}, {dog.Vaccinated}, {dog.Gender}, {dog.Diagnosis}, {dog.Age}, {dog.Neutered}, {dog.VisitedOn}";
        }
    }
}
