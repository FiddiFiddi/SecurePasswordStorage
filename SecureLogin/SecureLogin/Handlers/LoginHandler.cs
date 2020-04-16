using Newtonsoft.Json;
using SecureLogin.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecureLogin.Logic
{
    public class LoginHandler
    {
        // Validates if the entered credentials is correct and user is allowed
        public async Task<bool> ValidateInput(string username, string password)
        {
            UserViewModel newUser = new UserViewModel()
            {
                Password = password,
                Username = username
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44302/");
                    HttpResponseMessage response = new HttpResponseMessage();

                    var json = JsonConvert.SerializeObject(newUser);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    response = await client.PostAsync("api/auth/login", data);
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            // Logs user in
                            return true;
                            // Could return a token to keep session up for future project
                        case System.Net.HttpStatusCode.BadRequest:
                            return false;
                            // something went wrong
                        case System.Net.HttpStatusCode.Unauthorized:
                            // User is not allowed!
                            return false;
                        default:
                            // Something went COMPLETELY wrong
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
