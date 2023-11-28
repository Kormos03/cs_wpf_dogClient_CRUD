﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;


namespace wpf_dogClient_gyakorlo
{
    public class dogService
    {
        HttpClient client = new HttpClient();
        List<Dog> dogs = new List<Dog>();
        public string url = "https://retoolapi.dev/SAsZIp/dogs";

        public List<Dog> GetDogs()
        {
           HttpResponseMessage response = client.GetAsync(url).Result;
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = response.Content.ReadAsStringAsync().Result;
                    dogs = JsonConvert.DeserializeObject<List<Dog>>(jsonContent)!;
                    return dogs;
                }
                else { MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase); return null; }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public bool PostDog(Dog dog)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dog);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dog added!");
                    return true;
                }
                else { MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase); return false; }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public bool deleteDog(Dog dog)
        {
            int id = dog.Id;
            try
            {
                HttpResponseMessage response = client.DeleteAsync(url + "/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dog deleted!");
                    return true;
                }
                else { Debug.WriteLine(dog.Id); MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase); return false; }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private const string ConnectionString = "http://localhost/phpmyadmin/index.php?route=/table/sql&db=doglogindb&table=doglogintable";

        public void InsertUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO doglogintable (username, password) VALUES (@username, @password)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
