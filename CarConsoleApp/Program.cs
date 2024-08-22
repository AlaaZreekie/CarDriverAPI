using CarConsoleApp;
using System;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

// namespace CarConsoleApp;

Console.WriteLine("Welcome To CarAPI");
string Choice;
do
{

    Console.WriteLine("Enter 1 To LogIn.");
    Console.WriteLine("Enter 2 To Register.");
    Console.WriteLine("Enter  Exit To Exit.");
    Console.Write("Enter Your Choise : ");
    Choice = Console.ReadLine();
    Choice = Choice.ToLower();
    switch (Choice)
    {
        case ("1"):
            {
                string Email;
                string Password;
                Console.WriteLine("Enter Your Email And Password.");
                Console.Write("Enter Your Email : ");
                Email = Console.ReadLine();
                Console.Write("Enter The Password : ");
                Password = Console.ReadLine();
                var logIn = new { email = Email, password = Password };

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7140/api/User/");
                var Json = JsonSerializer.Serialize(logIn);
                var Content = new StringContent(Json, Encoding.UTF8, "application/json");

                var Response = client.PostAsync("LogIn", Content).GetAwaiter().GetResult();
                if (Response.IsSuccessStatusCode)
                {
                    var Token = await Response.Content.ReadFromJsonAsync<TokenDTO>();
                    Console.WriteLine(Token?.Token);
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }

                break;
            }
        case ("2"):
            {
                string Name;
                string Email;
                string Password;
                Console.WriteLine("Enter Your Email ,Name And Password.");
                Console.Write("Enter Your Name : ");
                Name = Console.ReadLine();
                Console.Write("Enter Your Email : ");
                Email = Console.ReadLine();
                Console.Write("Enter The Password : ");
                Password = Console.ReadLine();
                var register = new { name = Name, email = Email, password = Password };

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7140/api/User/");
                var Json = JsonSerializer.Serialize(register;
                var Content = new StringContent(Json, Encoding.UTF8, "application/json");

                var Response = client.PostAsync("Register", Content).Result;
                if (Response.IsSuccessStatusCode)
                {
                    /*var responseContent = Response.Content.ReadAsStringAsync().Result;
                    var Token = JsonSerializer.Deserialize<string>(responseContent);
                    Console.WriteLine(Token);*/
                    var Token = await Response.Content.ReadFromJsonAsync<TokenDTO>();
                    Console.WriteLine(Token?.Token);
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }
                break;
            }
        case ("exit"):
            {
                Console.WriteLine("---" + Choice + "ing---");
                Console.WriteLine("---End Of Program ---");
                break;
            }
        default:
            {
                Console.WriteLine("ERROR...Enter Valid Choice.");
                break;
            }
    }

} while (Choice != "exit");
