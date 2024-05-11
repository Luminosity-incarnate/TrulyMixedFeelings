using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PostDiscordEra
{
    class TextColours
    {
        // I used something known as ANSI code for the colour
        public string RedText(string text)
        {
            return $"\u001b[31m{text}\u001b[0m";
        }
        public string GreenText(string text)
        {
            return $"\u001b[32m{text}\u001b[0m";
        }
        public string BlueText(string text)
        {
            return $"\u001b[34m{text}\u001b[0m";
        }

    }
    class PlayerInfo
    {
        private string ID;
        private string Name;
        private int Age;

        public PlayerInfo(string _ID)
        {
            ID = _ID;
        }

        public string GetName()
        {
            return Name;
        }
        public int GetAge()
        {
            return Age;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void setAge(int age)
        {
            Age = age;
        }

    }
    class WeatherReport
    {
        public static string city;
        public static string conditions;
        public static double temp;
        public static double tempmax;
        public static double humidity;
        static List<String> cities = new List<String>() { "London", "York", "edinburgh", "Nottingham", "Leeds", "Liverpool" };


        public static async Task GatherWeatherInfo()
        {
            string url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{cities[RandomNumberGenerator.GetInt32(0, cities.Count)]}/today?unitGroup=metric&elements=datetime%2Ctempmax%2Ctemp%2Chumidity%2Cconditions&include=current&key=G7TGLWQ8CVQZUBQUR3SS38X42&contentType=json";
            // Call the amazing function that will have a looksie at the JSON data
            var urlData = await ParseJsonFromUrl(url);
            if (urlData != default)
            {
                // Spara variablerna with the values from the JSON code
                tempmax = urlData.Item1;
                temp = urlData.Item2;
                humidity = urlData.Item3;
                conditions = urlData.Item4;
                city = urlData.Item5;
            }
        }

        // async för att vi ska kunna använda await, fyra items seing as that's what we're in need of
        static async Task<(double, double, double, string, string)> ParseJsonFromUrl(string url)
        {
            try
            {
                // Skapa en ny httpclient and then send the request using url
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    // This just looks if the response stämmer med sanningen
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        JsonDocument doc = JsonDocument.Parse(jsonString);

                        // Yoink the values from the JSON code
                        double tempmax = doc.RootElement.GetProperty("days")[0].GetProperty("tempmax").GetDouble();
                        double temp = doc.RootElement.GetProperty("days")[0].GetProperty("temp").GetDouble();
                        double humidity = doc.RootElement.GetProperty("days")[0].GetProperty("humidity").GetDouble();
                        string conditions = doc.RootElement.GetProperty("days")[0].GetProperty("conditions").GetString();
                        string city = doc.RootElement.GetProperty("address").GetString();
                        return (tempmax, temp, humidity, conditions, city);
                    }
                    // Some error handling
                    else
                    {
                        Console.WriteLine($"No stuff yoinked. Status code: {response.StatusCode}");
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Big time failure. Exception: {ex.Message}");
                return default;
            }
        }
    }
    class MixedEmotions
    {
        static void TypeWriter(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay); // Delay between each character
            }
        }


        static PlayerInfo player = new PlayerInfo("Bobby");
        static TextColours colouredText = new TextColours();
        static WeatherReport weatherReport = new WeatherReport();
        static void Main()
        {
            TypeWriter($"Welcome to {colouredText.GreenText("M.F.S")}\nHere we do only the finest {colouredText.RedText("reporting")}!\nWere so happy to finally meet you!\nJust to confirm some details\nPlease enter your {colouredText.GreenText("name")}: ", 20);
            player.SetName(Console.ReadLine());
            Console.Clear();
            TypeWriter(colouredText.GreenText(player.GetName()) + " is a great name!\nPlease enter your age: ", 45);
            player.setAge(Convert.ToInt32(Console.ReadLine()));
            Console.Clear();
            if (player.GetAge() <= 18)
            {
                TypeWriter("Certainly a young one eh!", 45);
                System.Threading.Thread.Sleep(400);
            }
            else
            {
                TypeWriter("You're a bit older than I expected!", 45);
                System.Threading.Thread.Sleep(400);
            }
            Console.Clear();
            Console.WriteLine(colouredText.RedText("Loading data..."));
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine(colouredText.GreenText("Data loaded successfully!"));
            System.Threading.Thread.Sleep(1000);
            WeatherMiniGame();
        }

        static void WeatherMiniGame()
        {
            Console.Clear();
            WeatherReport.GatherWeatherInfo().Wait();
            TypeWriter($"Alright {player.GetName()}, are you ready to begin?\n", 30);
            if (Console.ReadLine() != "no")
            {
                Console.Clear();
                TypeWriter("Alright, let's get started!", 40);
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                TypeWriter("Well really that's to bad!\nRemember, we have a schedule to keep to, so you got five till start", 40);
                System.Threading.Thread.Sleep(5000);
            }
            Console.Clear();
            TypeWriter($"Today were taking a look at {WeatherReport.city}!", 40);
            System.Threading.Thread.Sleep(1000);
            switch (WeatherReport.city.ToLower())
            {
                case "london":
                    TypeWriter("\nFamously the city of the most pompeus royals!", 50);
                    break;
                case "york":
                    TypeWriter("\nThe city with a more famous younger brother...", 50);
                    break;
                case "edinburgh":
                    TypeWriter("\nOne of, if not the, worst city in the UK", 50);
                    break;
                case "nottingham":
                    TypeWriter("\nFamously the sherif of this city loved Robin Hood", 50);
                    break;
                case "leeds":
                    TypeWriter("\nA city which leeds a good life!", 50);
                    break;
                case "liverpool":
                    TypeWriter("\nThe city with the best team in the Premier League!", 50);
                    break;
                default:
                    TypeWriter("Sorry your program broke!", 1);
                    Main();
                    break;
            }
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            TypeWriter("Now let's take a look at the weather!\n", 30);
            if (WeatherReport.temp > 20 && WeatherReport.temp < 30)
            {
                TypeWriter($"It's a hot one today!\nAround {WeatherReport.temp}° outside today", 30);
            }
            else if (WeatherReport.temp > 30)
            {
                TypeWriter($"We are in heaven\nCurrently {WeatherReport.temp}°", 30);
            }
            else if (WeatherReport.temp < 10 && WeatherReport.temp > 0)
            {
                TypeWriter($"It's freezing!\nCurrently the temperature is at {WeatherReport.temp}°", 30);
            }
            else if (WeatherReport.temp < 10 && WeatherReport.temp > 0)
            {
                TypeWriter($"It's a cold one today!\nWith a low temperature of {WeatherReport.temp}°", 30);
            }
            else
            {
                TypeWriter($"It's a mild one today!\nWith the temperature currently resting at {WeatherReport.temp}°", 30);
            }
        }
    }
}