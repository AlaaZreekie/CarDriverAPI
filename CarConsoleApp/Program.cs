using CarConsoleApp;
using System;
using System.Net.Http.Headers;
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
    bool doExit = false;
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
                    if( Token.Token != null || Token.Token != "") { doExit =await Program(Token); }
                    
                    Console.WriteLine(Token?.Token);
                }
                else
                {
                    Console.WriteLine("ERROR...Enter Valid Email And Data!");
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
                var Json = JsonSerializer.Serialize(register);
                var Content = new StringContent(Json, Encoding.UTF8, "application/json");

                var Response = await client.PostAsync("Register", Content);
                if (Response.IsSuccessStatusCode)
                {
                    /*var responseContent =await Response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<User>(responseContent);*/
                    var user = await Response.Content.ReadFromJsonAsync<User>();
                    Console.WriteLine(user?.Name + " ===== " + user?.Email);
                    Console.WriteLine("------------------------------------------");
                    /*var user = await Response.Content.ReadFromJsonAsync<User>();
                    Console.WriteLine(user?.Name);
                    Console.WriteLine(user?.Email);*/
                }
                else
                {
                    Console.WriteLine("ERROR...Enter Valid Email And Data!");
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

       
    } if(doExit) {Choice = "exit";}

} while (Choice != "exit");


async Task<bool> Program(TokenDTO token)
{
    Console.WriteLine("\n \n");
   string choice;
   do
    {
        Console.WriteLine("-------Enter 1 To Show All Cars.");
        Console.WriteLine("-------Enter 2 To Create Car.");
        Console.WriteLine("-------Enter 3 To Update Car.");
        Console.WriteLine("-------Enter 4 To Delete Car\n");

        Console.WriteLine("-------Enter 5 To Show All Drivers");
        Console.WriteLine("-------Enter 6 To Create Driver");        
        Console.WriteLine("-------Enter 7 To Update Driver");
        Console.WriteLine("-------Enter 8 To Delete Driver\n");

        Console.WriteLine("-------Enter 9 To Show all leases");
        Console.WriteLine("-------Enter 10 To Create Leas\n");

        Console.WriteLine("-------Enter Exit To Exit\n");
        Console.Write("-------Enter Your Choise : ");
        choice = Console.ReadLine();
        choice.ToLower();
        bool doExit = false;
        switch (choice)
        {
            case ("5"):
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Driver/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Response = await client.GetAsync("GetAllDrivers");
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<List<DriverDTO>>();
                        if (user == null || user.Count == 0) { Console.WriteLine("There are no cars..."); break; }
                        foreach (DriverDTO driver in user)
                        {

                            Console.WriteLine();
                            Console.WriteLine($"Id : {driver.Id} , Name : {driver.Name}");
                            Console.Write("Cars : [ ");
                            foreach (int  carId in driver.Cars)
                            {
                                Console.Write($"{carId} , ");
                            }
                            Console.Write("]");
                            Console.WriteLine("");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid  Data!");
                    }
                    break;
                }
            case ("1"):
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Car/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Response = await client.GetAsync("GetCars");
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<List<CarDTO>>();
                        if (user == null || user.Count == 0) { Console.WriteLine("There are no cars..."); break; }
                        foreach (CarDTO car in user)
                        {

                            Console.WriteLine();
                            Console.WriteLine($"Id : {car.Id} , Type : {car.Type} , Color : {car.Color} , Number Of Doors : {car.DoorsNum}");
                            Console.Write("Drivers : [ ");
                            foreach (int driverId in car.Drivers)
                            {
                                Console.Write($"{driverId} , ");
                            }
                            Console.Write("]");
                            Console.WriteLine("");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid  Data!");
                    }
                    break;
                }
            case ("2"):
                {
                    string color;
                    string type;
                    int doorsNum;
                    Console.WriteLine("Enterthe color ,type And number of doors.");
                    Console.Write("Enter color : ");
                    color = Console.ReadLine();
                    Console.Write("Enter type : ");
                    type = Console.ReadLine();
                    
                    do
                    {
                        Console.Write("Enter the  number of doors : ");
                        doorsNum =Convert.ToInt32( Console.ReadLine());
                        if(doorsNum == 2) { break; }
                        if(doorsNum == 4) { break; }
                    } while (doorsNum != 4 || doorsNum != 2);
                    var car = new { Color = color,Type = type ,DoorsNum = doorsNum };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Car/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Json = JsonSerializer.Serialize(car);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.PostAsync("CreateCar", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<CarDTO>();
                        Console.WriteLine(user?.Id + "  " + user?.Color +"  "+ user.Type + "  "+ user.DoorsNum + "  Is Created");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid  Data!");
                    }
                    break;
                }
            case ("6"):
                {
                    string name;
                    Console.Write("Enter name : ");
                    name = Console.ReadLine();
                    var driver = new { Name = name };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Driver/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Json = JsonSerializer.Serialize(driver);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.PostAsync("CreateDriver", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<DriverDTO>();
                        Console.WriteLine(user?.Id + "  " + user?.Name+ "  Is Created");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid  Data!");
                    }
                    break;
                }
            case ("3"):
                {
                    int id;
                    string color;
                    string type;
                    int doorsNum;
                    Console.WriteLine("Enterthe id, color ,type And number of doors.");
                    Console.Write("Enter id : ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter color : ");
                    color = Console.ReadLine();
                    Console.Write("Enter type : ");
                    type = Console.ReadLine();

                    do
                    {
                        Console.Write("Enter the  number of doors : ");
                        doorsNum = Convert.ToInt32(Console.ReadLine());
                        if (doorsNum == 2) { break; }
                        if (doorsNum == 4) { break; }
                    } while (doorsNum != 4 || doorsNum != 2);
                    var car = new {Id = id, Color = color, Type = type, DoorsNum = doorsNum };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Car/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Json = JsonSerializer.Serialize(car);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.PutAsync("UpdateCar", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<CarDTO>();
                        Console.WriteLine(user?.Id + "  " + user?.Color + "  " + user.Type + "  " + user.DoorsNum + "  Is Updated");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid And Data!");
                    }
                    break;
                }
            case ("7"):
                {
                    int id;
                    string name;
                    Console.WriteLine("Enterthe id and name.");
                    Console.Write("Enter id : ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter name : ");
                    name = Console.ReadLine();
                    var driver = new { Id = id,Name = name };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Driver/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Json = JsonSerializer.Serialize(driver);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.PutAsync("UpdateDriver", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<DriverDTO>();
                        Console.WriteLine(user?.Id + "  " + user?.Name + "  Is Updated");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid And Data!");
                    }
                    break;
                }
            case ("4"):
                {
                    int id;
                    Console.Write("Enter id : ");
                    id = Convert.ToInt32(Console.ReadLine());                    
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Car/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    CancellationToken T = new CancellationToken();
                    var Json = JsonSerializer.Serialize(id);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.DeleteAsync($"DeleteCar/{id}");
                    if (Response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(Response.Content.ToString() + "  Is Deleted");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid Email And Data!  " + Response.Content.ToString());
                    }
                    break;
                }
            case ("8"):
                {
                    int id;
                    Console.Write("Enter id : ");
                    id = Convert.ToInt32(Console.ReadLine());
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Driver/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    CancellationToken T = new CancellationToken();
                    var Json = JsonSerializer.Serialize(id);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.DeleteAsync($"DeleteDriver/{id}");
                    if (Response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(Response.Content.ToString() + "  Is Deleted");
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid Email And Data!  " + Response.Content.ToString());
                    }
                    break;
                }
            case ("exit"):
                {
                    Console.WriteLine("---" + Choice + "ing---");
                    Console.WriteLine("---End Of Program ---");
                    break;
                }
            case ("9"):
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Leas/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Response = await client.GetAsync("GetLeas");
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<List<LeasDTO>>();
                        if (user == null || user.Count == 0) { Console.WriteLine("There are no cars..."); break; }
                        foreach (LeasDTO leas in user)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"CarId : {leas.CarId} , Type : {leas.CarName} , DriverId : {leas.DriverId} ,DriverName : {leas.DriverName}, StartDate : {leas.StartDate}, EndDate : {leas.EndDate}" );                            
                            Console.WriteLine("");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid  Data!");
                    }
                    break;
                }
            case ("10"):
                {
                    int carId;
                    int driverId;
                    int sDay, sMonth, sYear;
                    int eDay, eMonth, eYear;
                    DateOnly startDate;
                    DateOnly endDate;
                    Console.WriteLine("Enter the car id, driver id, startdate and endDate.");
                    Console.Write("Enter car id : ");
                    carId =Convert.ToInt32( Console.ReadLine());
                    Console.Write("Enter driver id : ");
                    driverId = Convert.ToInt32(Console.ReadLine());


                    do
                    {
                        Console.Write("Enter Satrt day : ");
                        sDay = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Satrt month : ");
                        sMonth = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Satrt year : ");
                        sYear = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter End day : ");
                        eDay = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter End month : ");
                        eMonth = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter End year : ");
                        eYear = Convert.ToInt32(Console.ReadLine());
                        startDate = new DateOnly(sYear, sMonth, sDay);
                        endDate = new DateOnly(eYear, eMonth, eDay);
                        Console.WriteLine("\n\n");

                    } while (startDate >= endDate);

                    var leas = new { CarId = carId, DriverId = driverId, StartDate = startDate, EndDate = endDate };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7140/api/Leas/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                    var Json = JsonSerializer.Serialize(leas);
                    var Content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var Response = await client.PostAsync("CreateLeas", Content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var user = await Response.Content.ReadFromJsonAsync<LeasDTO>();
                        Console.WriteLine($"CarId : {user.CarId} , Type : {user.CarName} , DriverId : {user.DriverId} ,DriverName : {user.DriverName}, StartDate : {user.StartDate}, EndDate : {user.EndDate}");

                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    else
                    {
                        Console.WriteLine("ERROR...Enter Valid  Data!");
                    }
                    break;
                }
            default:
                {
                    Console.WriteLine("ERROR...Enter Valid Choice.");
                    break;
                }


        }
        if (doExit) { choice = "exit"; }

    } while (choice != "exit");




    return true;
}




/*var user = await Response.Content.ReadFromJsonAsync<User>();
Console.WriteLine(user?.Name);
Console.WriteLine(user?.Email);*/





/*var responseContent =await Response.Content.ReadAsStringAsync();
var user = JsonSerializer.Deserialize<User>(responseContent);*/