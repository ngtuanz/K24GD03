using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Reflection;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public int Coins { get; set; }
    public bool IsActive { get; set; }
    public int VipLevel { get; set; }
    public string Region { get; set; }
    public string LastLogin { get; set; }

}

class Program
{
    static async Task Main(string[] args)
    {
        var url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/refs/heads/main/lab12_players.json";
        var httpClient = new HttpClient();
        var json = await httpClient.GetStringAsync(url);
        var players = JsonConvert.DeserializeObject<List<Player>>(json);

        for (int i = 0; i < players.Count; i++)
            players[i].Id = i + 1;

        var firebase = new FirebaseClient(
            "https://lab12z-default-rtdb.asia-southeast1.firebasedatabase.app/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("https://lab12z-default-rtdb.asia-southeast1.firebasedatabase.app/")
            });

        DateTime now = new DateTime(2025, 6, 30, 0, 0, 0, DateTimeKind.Utc);

        var inactivePlayers = players.Where(p =>
        {
            DateTime lastLogin = DateTime.Parse(p.LastLogin).ToUniversalTime();
            return !p.IsActive || (now - lastLogin).TotalDays > 5;
        }).ToList();

        Console.WriteLine("\n======= NGUOI CHOI KHONG HOAT DONG =======");

        for (int i = 0; i < inactivePlayers.Count; i++)
        {
            var p = inactivePlayers[i];

            DateTime parsedLogin = DateTime.Parse(p.LastLogin).ToUniversalTime();
            string formattedLogin = parsedLogin.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine($"[{i + 1}] Name: {p.Name}, IsActive: {p.IsActive}, LastLogin: {formattedLogin}");

            await firebase
                .Child("final_exam_bai1_inactive_players_method")
                .Child((i + 1).ToString())
                .PutAsync(new
                {
                    p.IsActive,
                    LastLogin = formattedLogin,
                    p.Name
                });
        }

        var lowLevelPlayers = players.Where(p => p.Level < 10).ToList();

        Console.WriteLine("\n====== NGUOI CHOI CAP THAP ======");

        for (int i = 0; i < lowLevelPlayers.Count; i++)
        {
            var p = lowLevelPlayers[i];
            Console.WriteLine($"[{i + 1}] Name: {p.Name}, Level: {p.Level}, Gold: {p.Gold}");

            await firebase
                .Child("final_exam_bai1_low_level_players_method")
                .Child((i + 1).ToString())
                .PutAsync(new
                {
                    p.Name,
                    p.Level,
                    CurrentGold = p.Gold
                });
        }

        var top3VIP = players
            .Where(p => p.VipLevel > 0 && p.Level > 0)
            .OrderByDescending(p => p.Level)
            .Take(3)
            .Select((p, index) =>
            {
                int bonus = index == 0 ? 2000 : index == 1 ? 1500 : 1000;
                return new
                {
                    p.Name,
                    p.Level,
                    p.VipLevel,
                    CurrentGold = p.Gold,
                    AwardedGold = bonus

                };
            }).ToList();
        Console.WriteLine("\n=== TOP 3 VIP NHAN THUONG ===");

        for (int i = 0; i < top3VIP.Count; i++)
        {
            var p = top3VIP[i];
            Console.WriteLine($"[{i + 1}] Name: {p.Name}, VIP: {p.VipLevel}, Level: {p.Level}, Gold: {p.CurrentGold}, Reward: {p.AwardedGold}");

            await firebase
                .Child("final_exam_bai2_top3_vip_awards_method")
                .Child((i + 1).ToString())
                .PutAsync(new
                {
                    p.Name,
                    p.VipLevel,
                    p.Level,
                    p.CurrentGold,
                    AwardedGoldAmount = p.AwardedGold
                });
        }

    }
}
