using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_dogClient_gyakorlo
{
    public class User
    {

        public static List<User> users = new List<User>();
        public User(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
        }

        public string username { get; set; }
        public string password { get; set; }

        public string email { get; set; }

        public bool login(string username, string password)
        {
            Debug.WriteLine("Login method called!\n" + users.Count());
            foreach(User item in users)
            {
                Debug.WriteLine(item.username);
                if(item.username == username && item.password == password)
                {
                    MessageBox.Show("Login successful!");
                    return true;
                }
                else
                {
                    MessageBox.Show("The username or password not match!");
                    return false;
                }
            }
            Debug.WriteLine("Unexpectd error!");
               return false;
        }

        public bool register(User user)
        {
            if(users.Contains(user))
            {
                MessageBox.Show("This user already exists!");
                return false;
            }
            else if(string.IsNullOrWhiteSpace(user.username) || string.IsNullOrWhiteSpace(user.password) || string.IsNullOrWhiteSpace(user.email))
            {
                MessageBox.Show("Please fill all the fields!");
                return false;
            }
            else if(user.password.Length <= 8)
            {
                MessageBox.Show("Password must be at least 8 characters long!");
                return false;
            }
            else if(!user.email.Contains("@") || !user.email.Contains("."))
            {
                MessageBox.Show("Invalid email address!");
                return false;
            }
            else
            {
                users.Add(user);
                foreach (User item in users)
                {
                    Debug.WriteLine(item.username);
                }   

                MessageBox.Show("User added!");
                return true;
            }   
        }

    }
}
