using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;



namespace LAB11
{
    internal class Program
    {
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
            public DateTime LastLogin { get; set; }
        }
        public class RichPlayerResult
        {
            public string Name { get; set; }
            public int Gold { get; set; }
            public int Coins { get; set; }
        }
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            string url = "https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json";
            string json = await client.GetStringAsync(url);

            List<Player> players = JsonConvert.DeserializeObject<List<Player>>(json);

            Console.WriteLine($"Tong so nguoi choi: {players.Count}");

            var richPlayers = players
                .Where(p => p.Gold > 1000 && p.Coins > 100)
                .OrderByDescending(p => p.Gold)
                .Select(p => new RichPlayerResult
                {
                    Name = p.Name,
                    Gold = p.Gold,
                    Coins = p.Coins
                })
                .ToList();

            Console.WriteLine("==== Danh sách Rich Players ====");
            foreach (var p in richPlayers)
            {
                Console.WriteLine($"Ten: {p.Name}, Gold: {p.Gold}, Coins: {p.Coins}");
            }
            var firebaseClinet = new FirebaseClient("https://lab11zza-default-rtdb.asia-southeast1.firebasedatabase.app/");
            await firebaseClinet
                .Child("quiz_bai1_richPlayers")
                .PutAsync(richPlayers);

            int totalVipPlayers = players.Count(p => p.VipLevel > 0);
            Console.WriteLine($"Tong so nguoi choi VIP: {totalVipPlayers}");

            var vipByRegion = players
                .Where(p => p.VipLevel > 0)
                .GroupBy(p => p.Region)
                .Select(g => new
                {
                    Region = g.Key,
                    Count = g.Count()
                }).ToList();

            Console.WriteLine("=== So nguoi choi VIP trong khu vuc ===");
            foreach (var r in vipByRegion)
            {
                Console.WriteLine($"- Khu vuc: {r.Region}, So nguoi choi VIP: {r.Count}");
            }

            DateTime now = new DateTime(2025, 06, 30, 0, 0, 0);
            var recentVipPlayers = players
                .Where(p => p.VipLevel > 0 && (now - p.LastLogin).TotalDays <= 2)
                .Select(p => new
                {
                    p.Name,
                    p.VipLevel,
                    p.LastLogin
                }).ToList();

            Console.WriteLine("=== NGuoi choi VIP dang nhap gan day ===");
            foreach (var p in recentVipPlayers)
            {
                Console.WriteLine($"Ten: {p.Name}, VIP Level: {p.VipLevel}, Last Login: {p.LastLogin}");
            }

            await firebaseClinet
                .Child("quiz_bai2_recentVipPlayers")
                .PutAsync(recentVipPlayers);
        }
    }
}