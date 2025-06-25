using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace SEMI_FINAL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\nguye\\OneDrive\\Tài liệu\\GitHub\\K24GD03\\serviceAccountKey.json.json")
            });
            var firebase = new FirebaseClient("https://semi-finalz-default-rtdb.asia-southeast1.firebasedatabase.app/");

            Player plTam = null;

            while (true)
            {
                Console.WriteLine(" ===== MENU QUAN LY PLAYER =====");
                Console.WriteLine("1. Nhap du lieu cua player");
                Console.WriteLine("2. Lay du lieu tu Firebase");
                Console.WriteLine("3. Cap nhat du lieu cua Player");
                Console.WriteLine("4. Xoa du lieu ");
                Console.WriteLine("5. Top 5 Player co Gold cao nhat");
                Console.WriteLine("6. Top 5 Player co Score cao nhat");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon: ");
                var chon = Console.ReadLine();

                if (chon == "1")
                {
                    plTam = new Player();
                    Console.Write("Nhap IdPlayer: ");
                    plTam.PlayerID = Console.ReadLine();
                    Console.Write("Nhap ten player: ");
                    plTam.Name = Console.ReadLine();
                    Console.Write("Nhap Gold: ");
                    plTam.Gold = int.Parse(Console.ReadLine());
                    Console.Write("Nhap Score: ");
                    plTam.Score = int.Parse(Console.ReadLine());

                    await firebase.Child("Player").Child(plTam.PlayerID).PutAsync(plTam);

                    Console.WriteLine("Da luu thong tin Player vao Firebase.");
                }

                
                else if (chon == "2")
                {
                    var danhSach = await firebase.Child("Player").OnceAsync<Player>();
                    Console.WriteLine("Danh sach sinh vien tu Firebase:");
                    foreach (var item in danhSach)
                    {
                        var sv = item.Object;
                        Console.WriteLine($"- {sv.PlayerID} | Name: {sv.Name} | Gold: {sv.Gold} | Score: {sv.Score}");
                    }
                }
                else if (chon == "3")
                {
                    Console.Write("Nhap IdPlayer can sua: ");
                    string playerid = Console.ReadLine();

                    Console.Write("Nhap Player moi: ");
                    string name = Console.ReadLine();
                    Console.Write("Nhap Gold moi: ");
                    int gold = int.Parse(Console.ReadLine());
                    Console.Write("Score moi: ");
                    int Score = int.Parse(Console.ReadLine());

                    var plMoi = new Player
                    {
                        PlayerID = playerid,
                        Name = name,
                        Gold = gold,
                        Score = Score
                    };

                    await firebase.Child("Player").Child(playerid).PutAsync(plMoi);
                    Console.WriteLine("Cap nhat thanh cong.");
                }
                else if (chon == "4")
                {
                    Console.Write("Nhap PlayerId can xoa: ");
                    string playerid = Console.ReadLine();

                    await firebase.Child("Player").Child(playerid).DeleteAsync();
                    Console.WriteLine("Da xoa Player.");
                }
                else if (chon == "5")
                {
                    var danhSach = await firebase.Child("Player").OnceAsync<Player>();
                    var top5 = danhSach
                        .Select(item => item.Object)
                        .OrderByDescending(p => p.Gold)
                        .Take(5)
                        .ToList();
                    Console.WriteLine("===== TOP GOLD =====");
                    Console.WriteLine("TOP 5 GOLD:");
                    for (int i = 0; i < top5.Count; i++)
                    {
                        var p = top5[i];
                        Console.WriteLine($"{i + 1}. {p.Name} - Gold: {p.Gold}");

                        await firebase.Child("TopGold").Child((i + 1).ToString()).PutAsync(new
                        {
                            Top = i + 1,
                            p.Name,
                            p.Gold
                        });
                    }

                    Console.WriteLine("Da luu TOP 5 Top gold.");
                }
                else if (chon == "6")
                {
                    var danhSach = await firebase.Child("Player").OnceAsync<Player>();
                    var top5 = danhSach
                        .Select(item => item.Object)
                        .OrderByDescending(p => p.Score)
                        .Take(5)
                        .ToList();
                    Console.WriteLine("====== TOP SCORE =====");
                    Console.WriteLine("TOP 5 SCORE:");
                    for (int i = 0; i < top5.Count; i++)
                    {
                        var p = top5[i];
                        Console.WriteLine($"{i + 1}. {p.Name} - Score: {p.Score}");

                        string key = $"p0{i + 1}";

                        await firebase.Child("TopScore").Child(key).PutAsync(new
                        {
                            index = i + 1,
                            name = p.Name,
                            score = p.Score,
                            gold = p.Gold,
                            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        });
                    }

                    Console.WriteLine("Da luu Top 5 vao Top Score.");
                }
                else if (chon == "0")
                {
                    Console.WriteLine("Tam biet!");
                    break;
                }
                else
                {
                    Console.WriteLine("Lua chon khong hop le.");
                }
            }
        }
    }
}
