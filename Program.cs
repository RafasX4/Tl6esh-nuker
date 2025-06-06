// Coded / Dev By Tl6esh
// tL6esh Nuker - Discord Nuker Tool
// Copyright © 2025

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace tL6esh_Nuker
{
    class Program
    {
        
        private static string[] logo = new string[]
        {
            @"  $$\     $$\       $$$$$$\                      $$\             $$\   $$\           $$\                           ",
            @"  $$ |    $$ |     $$  __$$\                     $$ |            $$$\  $$ |          $$ |                          ",
            @"$$$$$$\   $$ |     $$ /  \__| $$$$$$\   $$$$$$$\ $$$$$$$\        $$$$\ $$ |$$\   $$\ $$ |  $$\  $$$$$$\   $$$$$$\  ",
            @"\_$$  _|  $$ |     $$$$$$$\  $$  __$$\ $$  _____|$$  __$$\       $$ $$\$$ |$$ |  $$ |$$ | $$  |$$  __$$\ $$  __$$\ ",
            @"  $$ |    $$ |     $$  __$$\ $$$$$$$$ |\$$$$$$\  $$ |  $$ |      $$ \$$$$ |$$ |  $$ |$$$$$$  / $$$$$$$$ |$$ |  \__|",
            @"  $$ |$$\ $$ |     $$ /  $$ |$$   ____| \____$$\ $$ |  $$ |      $$ |\$$$ |$$ |  $$ |$$  _$$<  $$   ____|$$ |      ",
            @"  \$$$$  |$$$$$$$$\ $$$$$$  |\$$$$$$$\ $$$$$$$  |$$ |  $$ |      $$ | \$$ |\$$$$$$  |$$ | \$$\ \$$$$$$$\ $$ |      ",
            @"   \____/ \________|\______/  \_______|\_______/ \__|  \__|      \__|  \__| \______/ \__|  \__| \_______|\__|      "
        };

        
        private static ConsoleColor defaultColor = ConsoleColor.White;
        private static ConsoleColor primaryColor = ConsoleColor.Red;
        private static ConsoleColor secondaryColor = ConsoleColor.DarkRed;
        private static ConsoleColor highlightColor = ConsoleColor.Red;

        
        private static string userToken = "";
        private static string userName = "";

        
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            /
            Console.Title = "tL6esh Nuker v1.0 | Discord | @Tl6esh";
            Console.OutputEncoding = Encoding.UTF8;
            
            
            InitializeEnvironment();
            
            
            Login();
            
            
            while (true)
            {
                DisplayMainMenu();
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        AccountNukerMenu();
                        break;
                    case "2":
                        ServerNukerMenu();
                        break;
                    case "3":
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default:
                        ColorWriteLine("خيار غير صالح. يرجى المحاولة مرة أخرى.", ConsoleColor.Red);
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        private static void InitializeEnvironment()
        {
            
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }

            
            string[] requiredFiles = { "tokens.txt", "data/channels.txt", "data/roles.txt", "data/members.txt" };
            foreach (string file in requiredFiles)
            {
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                }
            }

            
            if (!File.Exists("data/logins.json"))
            {
                var loginData = new { Login = "" };
                File.WriteAllText("data/logins.json", JsonConvert.SerializeObject(loginData, Formatting.Indented));
            }
        }

        private static void Login()
        {
            Console.Clear();
            DisplayLogo();

            string loginData = File.ReadAllText("data/logins.json");
            var loginJson = JsonConvert.DeserializeObject<dynamic>(loginData);

            if (string.IsNullOrEmpty((string)loginJson.Login))
            {
                ColorWrite("\n[", defaultColor);
                ColorWrite("#", highlightColor);
                ColorWriteLine("] تسجيل الدخول إلى tL6esh Nuker", defaultColor);

                ColorWrite("[", defaultColor);
                ColorWrite("#", highlightColor);
                ColorWrite("] أدخل اسم المستخدم: ", defaultColor);
                
                userName = Console.ReadLine();
                
                loginJson.Login = userName;
                File.WriteAllText("data/logins.json", JsonConvert.SerializeObject(loginJson, Formatting.Indented));

                ColorWrite("\n[", defaultColor);
                ColorWrite("#", highlightColor);
                ColorWrite("] تم تسجيل الدخول بنجاح كـ: [", defaultColor);
                ColorWrite(userName, highlightColor);
                ColorWriteLine("]", defaultColor);

                ColorWrite("[", defaultColor);
                ColorWrite(">", highlightColor);
                ColorWrite("] اضغط ENTER للمتابعة: ", defaultColor);
                Console.ReadLine();
            }
            else
            {
                userName = (string)loginJson.Login;
            }
        }

        private static void DisplayLogo()
        {
            Console.ForegroundColor = primaryColor;
            foreach (string line in logo)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("\n                                 Coded / Dev By Tl6esh\n");
            Console.ForegroundColor = defaultColor;
        }

        private static void DisplayMainMenu()
        {
            Console.Clear();
            DisplayLogo();

            ColorWriteLine("\nالقائمة الرئيسية:", defaultColor);
            ColorWrite("[", defaultColor);
            ColorWrite("1", highlightColor);
            ColorWriteLine("] Account Nuker", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("2", highlightColor);
            ColorWriteLine("] Server Nuker", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("3", highlightColor);
            ColorWriteLine("] خروج", defaultColor);
            
            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اختيارك: ", defaultColor);
        }

        private static void AccountNukerMenu()
        {
            Console.Clear();
            DisplayLogo();

            ColorWriteLine("\nAccount Nuker:", defaultColor);
            ColorWrite("[", defaultColor);
            ColorWrite("1", highlightColor);
            ColorWriteLine("] تدمير الحساب", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("2", highlightColor);
            ColorWriteLine("] حذف الأصدقاء", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("3", highlightColor);
            ColorWriteLine("] مغادرة السيرفرات", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("4", highlightColor);
            ColorWriteLine("] إنشاء سيرفرات", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("5", highlightColor);
            ColorWriteLine("] العودة", defaultColor);
            
            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اختيارك: ", defaultColor);
            
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    NukeAccount();
                    break;
                case "2":
                    RemoveFriends();
                    break;
                case "3":
                    LeaveServers();
                    break;
                case "4":
                    CreateServers();
                    break;
                case "5":
                    return;
                default:
                    ColorWriteLine("خيار غير صالح. يرجى المحاولة مرة أخرى.", ConsoleColor.Red);
                    Thread.Sleep(1500);
                    AccountNukerMenu();
                    break;
            }
        }

        private static void ServerNukerMenu()
        {
            Console.Clear();
            DisplayLogo();

            ColorWriteLine("\nServer Nuker:", defaultColor);
            ColorWrite("[", defaultColor);
            ColorWrite("1", highlightColor);
            ColorWriteLine("] معلومات السيرفر", defaultColor);
            
            ColorWrite("[", defaultColor);
            ColorWrite("2", highlightColor);
            ColorWriteLine("] العودة", defaultColor);
            
            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اختيارك: ", defaultColor);
            
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ServerLookup();
                    break;
                case "2":
                    return;
                default:
                    ColorWriteLine("خيار غير صالح. يرجى المحاولة مرة أخرى.", ConsoleColor.Red);
                    Thread.Sleep(1500);
                    ServerNukerMenu();
                    break;
            }
        }

        private static void NukeAccount()
        {
            Console.Clear();
            DisplayLogo();

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] توكن الحساب: ", defaultColor);
            string token = Console.ReadLine();

            ColorWrite("[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اسم السيرفر: ", defaultColor);
            string serverName = Console.ReadLine();

            ColorWrite("[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] محتوى الرسالة: ", defaultColor);
            string messageContent = Console.ReadLine();

            ColorWriteLine("\nجاري تدمير الحساب...", defaultColor);

            
            AccountNuker nuker = new AccountNuker();
            nuker.StartNuking(token, serverName, messageContent);

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اضغط ENTER للعودة: ", defaultColor);
            Console.ReadLine();
            AccountNukerMenu();
        }

        private static void RemoveFriends()
        {
            Console.Clear();
            DisplayLogo();

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] توكن الحساب: ", defaultColor);
            string token = Console.ReadLine();

            ColorWriteLine("\nجاري حذف الأصدقاء...", defaultColor);

            AccountNuker nuker = new AccountNuker();
            nuker.RemoveAllFriends(token);

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اضغط ENTER للعودة: ", defaultColor);
            Console.ReadLine();
            AccountNukerMenu();
        }

        private static void LeaveServers()
        {
            Console.Clear();
            DisplayLogo();

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] توكن الحساب: ", defaultColor);
            string token = Console.ReadLine();

            ColorWriteLine("\nجاري مغادرة السيرفرات...", defaultColor);

            AccountNuker nuker = new AccountNuker();
            nuker.LeaveAllServers(token);

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اضغط ENTER للعودة: ", defaultColor);
            Console.ReadLine();
            AccountNukerMenu();
        }

        private static void CreateServers()
        {
            Console.Clear();
            DisplayLogo();

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] توكن الحساب: ", defaultColor);
            string token = Console.ReadLine();

            ColorWrite("[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اسم السيرفر: ", defaultColor);
            string serverName = Console.ReadLine();

            ColorWrite("[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] عدد السيرفرات: ", defaultColor);
            int count = int.Parse(Console.ReadLine());

            ColorWriteLine("\nجاري إنشاء السيرفرات...", defaultColor);

            AccountNuker nuker = new AccountNuker();
            nuker.CreateServers(token, serverName, count);

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اضغط ENTER للعودة: ", defaultColor);
            Console.ReadLine();
            AccountNukerMenu();
        }

        private static void ServerLookup()
        {
            Console.Clear();
            DisplayLogo();

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] توكن الحساب: ", defaultColor);
            string token = Console.ReadLine();

            ColorWrite("[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] معرف السيرفر: ", defaultColor);
            string guildId = Console.ReadLine();

            ColorWriteLine("\nجاري البحث عن معلومات السيرفر...", defaultColor);

            ServerNuker nuker = new ServerNuker();
            nuker.LookupServer(token, guildId);

            ColorWrite("\n[", defaultColor);
            ColorWrite(">", highlightColor);
            ColorWrite("] اضغط ENTER للعودة: ", defaultColor);
            Console.ReadLine();
            ServerNukerMenu();
        }

        
        public static void ColorWrite(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }

        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}

nsole.Write(text);
            Console.ForegroundColor = originalColor;
        }

        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}

