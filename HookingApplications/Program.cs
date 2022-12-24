using System.Diagnostics;
using DiscordRPC;

// Code by J0nathan550

public class Program
{
    private static DiscordRpcClient client = new DiscordRpcClient("application_id_here");
    private static Timestamps time = Timestamps.Now;
    private static Stopwatch stopWatch = new Stopwatch();
    private static bool inGame = false;
    private static int lastGame = -1;
    private static bool runApplication = true;

    private struct Games
    {
        public string gameName;
        public string stateRPC;
        public string image;
    }

    private static List<Games> games = new()
    {
        new() { gameName = "dota2", stateRPC = "Playing Dota 2", image = "yourimageinapplication" },
        new() { gameName = "csgo", stateRPC = "Playing CS:GO", image = "yourimageinapplication"},
        new() { gameName = "notepad", stateRPC = "Writting notes in notepad", image = "yourimageinapplication"  },
    };

    public static void Init()
    {
        client.Initialize();
        stopWatch.Start();
    }

    public static string ReturnTime()
    {
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        string time = elapsedTime;
        return time;
    }

    public static void Main(string[] args)
    {
        Init();
        while (runApplication)
        {
            Console.Title = $"HookingApplication - RPC working: {ReturnTime()}";
            if (inGame)
            {
                Process[] hookProcess = Process.GetProcessesByName(games[lastGame].gameName);
                if (hookProcess.Length == 0)
                {
                    client.SetPresence(new RichPresence()
                    {
                        Details = "What are you forget in my profile?",
                        State = "Chilling",
                        Assets = new Assets()
                        {
                            LargeImageKey = "yourimageinapplication",
                            LargeImageText = "Image",
                        },
                        Timestamps = time
                    });
                    inGame = false;
                    lastGame = -1;
                }
                else
                {
                    client.SetPresence(new RichPresence()
                    {
                        Details = "What are you forget in my profile?",
                        State = games[lastGame].stateRPC,
                        Assets = new Assets()
                        {
                            LargeImageKey = games[lastGame].image,
                            LargeImageText = "Image",
                        },
                        Timestamps = time
                    });
                }
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                for (int i = 0; i < games.Count; i++)
                {
                    Process[] hookProcess = Process.GetProcessesByName(games[i].gameName);
                    if (hookProcess.Length == 0)
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "What are you forget in my profile?",
                            State = "Chilling",
                            Assets = new Assets()
                            {
                                LargeImageKey = "zxc",
                                LargeImageText = "Image",
                            },
                            Timestamps = time
                        });
                    }
                    else
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "What are you forget in my profile?",
                            State = games[i].stateRPC,
                            Assets = new Assets()
                            {
                                LargeImageKey = games[i].image,
                                LargeImageText = "Image",
                            },
                            Timestamps = time
                        });
                        inGame = true;
                        lastGame = i;
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}