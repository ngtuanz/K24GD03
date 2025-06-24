using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace LAB04
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("FireShap installed successfully!");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\nguye\\OneDrive\\Tài liệu\\GitHub\\K24GD03\\serviceAccountKey.json.json")
            });

            Console.WriteLine("FIsebase Admin SDK đã khởi tạo thành công");
            await AddTestData();
            await ReadTestData();
            await UpdateTestData();
            await ReadTestData();
            await DeleteTestData();
        }

        private static string FirebaseDB_URL = "https://lemon-a-default-rtdb.asia-southeast1.firebasedatabase.app/";
        public static async Task AddTestData()
        {
            var firebase = new FirebaseClient("https://lemon-a-default-rtdb.asia-southeast1.firebasedatabase.app/");
            var testData = new
            {
                Message = "hello firebase",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await firebase.Child("test").PutAsync(testData);
            Console.WriteLine("dữ liệu được thêm vào Firebase");
        }

        public static async Task ReadTestData()
        {
            var firebase = new FirebaseClient("https://lemon-a-default-rtdb.asia-southeast1.firebasedatabase.app/");

            var testData = await firebase.Child("test").OnceSingleAsync<dynamic>();
            Console.WriteLine($"Message : {testData.Message}");

        }

        public static async Task UpdateTestData()
        {
            var firebase = new FirebaseClient("https://lemon-a-default-rtdb.asia-southeast1.firebasedatabase.app/");
            var testData = new
            {
                Message = "Update messger",
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };
            await firebase.Child("test").PutAsync(testData);
            Console.WriteLine("dữ liệu được thêm vào Firebase");
        }

        public static async Task DeleteTestData()
        {
            var firebase = new FirebaseClient("https://lemon-a-default-rtdb.asia-southeast1.firebasedatabase.app/");

            await firebase.Child("test").DeleteAsync();
            Console.WriteLine("dữ liệu bị xóa khỏi Firebase");
        }
    }
}