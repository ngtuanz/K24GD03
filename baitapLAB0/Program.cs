using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace FirebaseConsole
{
    internal class Program
    {
        public class Student
        {
            public string Id {  get; set; }
            public string HoTen { get; set; }
            public string MSSV { get; set; }
            public string Email { get; set; }
            public string Lop { get; set; }

            public double Score { get; set; }
        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\nguye\\OneDrive\\Tài liệu\\GitHub\\K24GD03\\serviceAccountKey.json.json")
            });

            var firebase = new FirebaseClient("https://baitapla04-default-rtdb.asia-southeast1.firebasedatabase.app/");

            Student svTam = null;

            while (true)
            {
                Console.WriteLine("\n=== MENU QUAN LY SINH VIEN ===");
                Console.WriteLine("1. Nhap du lieu sinh vien");
                Console.WriteLine("2. Ghi du lieu vao Firebase");
                Console.WriteLine("3. Lay du lieu tu Firebase");
                Console.WriteLine("4. Cap nhat du lieu");
                Console.WriteLine("5. Xoa du lieu");
                Console.WriteLine("6. Top 5 nguoi co diem cao nhat");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon: ");
                var chon = Console.ReadLine();

                if (chon == "1")
                {
                    svTam = new Student();
                    Console.Write("Ho ten: ");
                    svTam.HoTen = Console.ReadLine();
                    Console.Write("MSSV: ");
                    svTam.MSSV = Console.ReadLine();
                    Console.Write("Email: ");
                    svTam.Email = Console.ReadLine();
                    Console.Write("Lop: ");
                    svTam.Lop = Console.ReadLine();
                    Console.Write("Score: ");
                    svTam.Score = double.Parse(Console.ReadLine());
                    Console.WriteLine("Da luu tam thong tin sinh vien.");
                }
                else if (chon == "2")
                {
                    if (svTam == null)
                    {
                        Console.WriteLine("Chua co du lieu sinh vien de ghi.");
                        continue;
                    }

                    await firebase.Child("sinhviens").Child(svTam.MSSV).PutAsync(svTam);
                    Console.WriteLine("Da ghi du lieu len Firebase.");
                }
                else if (chon == "3")
                {
                    var danhSach = await firebase.Child("sinhviens").OnceAsync<Student>();
                    Console.WriteLine("Danh sach sinh vien tu Firebase:");
                    foreach (var item in danhSach)
                    {
                        var sv = item.Object;
                        Console.WriteLine($"- {sv.HoTen} | MSSV: {sv.MSSV} | Email: {sv.Email} | Lop: {sv.Lop} | Score: {sv.Score}");
                    }
                }
                else if (chon == "4")
                {
                    Console.Write("Nhap MSSV can cap nhat: ");
                    string mssv = Console.ReadLine();

                    Console.Write("Ho ten moi: ");
                    string hoTen = Console.ReadLine();
                    Console.Write("Email moi: ");
                    string email = Console.ReadLine();
                    Console.Write("Lop moi: ");
                    string lop = Console.ReadLine();
                    Console.Write("Score moi: ");
                    double Score = double.Parse(Console.ReadLine());

                    var svMoi = new Student
                    {
                        MSSV = mssv,
                        HoTen = hoTen,
                        Email = email,
                        Lop = lop,
                        Score = Score
                    };

                    await firebase.Child("sinhviens").Child(mssv).PutAsync(svMoi);
                    Console.WriteLine("Cap nhat thanh cong.");
                }
                else if (chon == "5")
                {
                    Console.Write("Nhap MSSV can xoa: ");
                    string mssv = Console.ReadLine();

                    await firebase.Child("sinhviens").Child(mssv).DeleteAsync();
                    Console.WriteLine("Da xoa sinh vien.");
                }
                else if (chon == "6")
                {
                    var myStudents = await firebase.Child("sinhviens").OnceAsync<Student>();

                    var topStudents = myStudents
                        .Select(s => new Student
                        {
                            Id = s.Key,
                            HoTen = s.Object.HoTen,
                            MSSV = s.Object.MSSV,
                            Email = s.Object.Email,
                            Lop = s.Object.Lop,
                            Score = s.Object.Score
                        })
                        .OrderByDescending(s => s.Score)
                        .Take(5)
                        .ToList();

                    Console.WriteLine("Top 5 sinh vien diem cao nhat:");
                    foreach (var sv in topStudents)
                    {
                        Console.WriteLine($"- {sv.HoTen} | MSSV: {sv.MSSV} | Lop: {sv.Lop} | Score: {sv.Score}");
                    }
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
