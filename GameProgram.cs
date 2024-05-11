namespace PostDiscordEra
{
    class TextColours
    {
        public void RedText(string Text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Text);
            Console.ResetColor();
        }
        public void GreenText(string Text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Text);
            Console.ResetColor();
        }
        public void BlueText(string Text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Text);
            Console.ResetColor();
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

    }
    class MixedEmotions
    {
        static PlayerInfo player = new PlayerInfo("Bobby");
        static TextColours colouredText = new TextColours();

        static void Main()
        {
            Console.WriteLine("Welcome to M.F.S! Here we do only the finest reporting!\nWere so happy to finally meet you!\nJust to confirm some details\nPlease enter your name");
            player.SetName(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(player.GetName() + " is a great name! How old are you?");
            player.setAge(Convert.ToInt32(Console.ReadLine()));
            Console.Clear();
            if (player.GetAge() <= 18)
            {
                Console.WriteLine("Certainly a young one eh!");
            }
            else
            {
                Console.WriteLine("You're a bit older than I expected!");
            }
            colouredText.RedText("Loading data...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();    
            colouredText.GreenText("Data loaded successfully!");
        }
    }
}