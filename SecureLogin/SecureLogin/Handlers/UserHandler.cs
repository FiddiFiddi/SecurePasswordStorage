using Newtonsoft.Json;
using SecureLogin.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace SecureLogin.Logic
{
    public class UserHandler
    {
        // Sends post to web api with user information to create user
        public async void CreateUser(string username, string password)
        {
            UserViewModel newUser = new UserViewModel()
            {
                Password = password,
                Username = username
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44302/");

                HttpResponseMessage response = new HttpResponseMessage();

                var json = JsonConvert.SerializeObject(newUser);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var reponse = await client.PostAsync("api/auth/create/user", data);

                if (response.IsSuccessStatusCode)
                {
                    // Be happy
                    Debug.WriteLine("Created User");
                }
            }
        }
    }
}
