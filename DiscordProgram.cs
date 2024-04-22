using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DSharpPlus;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.PortableExecutable;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.IO;

namespace MixedFeelingsBot
{
    class Program
    {

        static async Task Main(string[] args)
        {
            // Get the list of names to hang!
            List<string> playerInfo = new List<string>();

            // Get amount of users as of right absolute now in dat daaatabasee
            int Count = UserAmount();

            // Define dat pattern for the regi fingi
            string pattern = @"\(([^)]+)\)";

            // Easy peasy lemon squeezy discord bot
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "MTIzMDkxNzk4NzEyOTc1MzY2MA.GHaiTF.tpR4Mn7ULunvQDZRfrGPxiX8w01Fj7aEbw2AdQ",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("game"))
                {
                    // Find the X on the map. Define our SQL vessel. Finally define what the crew will be querying for.
                    string ConString = @"Data Source=Broder_Jakob\SQLEXPRESS;Initial Catalog=MixedfeelingsData;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";
                    SqlConnection con = new SqlConnection(ConString);
                    string querystring = "select * from playerInformation;";


                    // Open dat connection and execcute our query
                    con.Open();
                    SqlCommand cmd = new SqlCommand(querystring, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (playerInfo.Contains(reader["player_id"] + " " + reader["name"]))
                        {
                            continue;
                        }
                        else
                        {
                            playerInfo.Add(reader["player_id"] + " " + reader["name"]);
                        }
                    }
                    await e.Message.RespondAsync((string.Join(Environment.NewLine, playerInfo)));
                    reader.Close();
                    con.Close();
                    playerInfo.Clear();
                }

                
                if (e.Message.Content.ToLower().StartsWith("add"))
                {
                    // Set the new destination lads!
                    string ConString = @"Data Source=Broder_Jakob\SQLEXPRESS;Initial Catalog=MixedfeelingsData;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";
                    SqlConnection con = new SqlConnection(ConString);

                    // Do a little converting to the name of jesus lord and savior
                    string input = Convert.ToString(e.Message.Author);
                    Match actualAuthor = Regex.Match(input, pattern);
                    string actualActualAuthor = actualAuthor.Groups[1].Value;
                    actualActualAuthor = actualActualAuthor.Substring(0, actualActualAuthor.Count());

                    // Actually query the database
                    string querystring = $"INSERT INTO playerInformation(player_id, name) VALUES ({Count+1}, '{actualActualAuthor}');";
                    
                    //  Set sail for the horizon!
                    con.Open();
                    SqlCommand cmd = new SqlCommand(querystring, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        playerInfo.Add(reader["player_id"] + " " + reader["name"]);
                    }
                    con.Close();
                    reader.Close();
                } 
            };

            int UserAmount()
            {
                // Find the amount of kids on the ship
                string ConString = @"Data Source=Broder_Jakob\SQLEXPRESS;Initial Catalog=MixedfeelingsData;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";
                SqlConnection con = new SqlConnection(ConString);
                string querystring = "SELECT MAX(player_id) AS max_player_id FROM playerInformation;";
                int count = 0;

                // Open dat connection and execcute our query
                con.Open();
                SqlCommand cmd = new SqlCommand(querystring, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader["max_player_id"]);
                }
                return count;
            }

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
