// Coded / Dev By Tl6esh
// tL6esh Nuker - Discord Nuker Tool
// Copyright © 2025

using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace tL6esh_Nuker
{
    public class ServerNuker
    {
        private static ConsoleColor defaultColor = ConsoleColor.White;
        private static ConsoleColor highlightColor = ConsoleColor.Red;
        private static ConsoleColor successColor = ConsoleColor.Green;

        
        public void LookupServer(string token, string guildId)
        {
            Task.Run(async () =>
            {
                try
                {
                   
                    var guildResponse = await DiscordApi.GetAsync($"/guilds/{guildId}?with_counts=true", token);
                    
                    if (guildResponse == null)
                    {
                        ColorWrite("[", defaultColor);
                        ColorWrite("!", ConsoleColor.Red);
                        ColorWriteLine("] فشل في الحصول على معلومات السيرفر.", ConsoleColor.Red);
                        return;
                    }
                    
                    
                    string ownerId = guildResponse["owner_id"].ToString();
                    var ownerResponse = await DiscordApi.GetAsync($"/guilds/{guildId}/members/{ownerId}", token);
                    
                    if (ownerResponse == null)
                    {
                        ColorWrite("[", defaultColor);
                        ColorWrite("!", ConsoleColor.Red);
                        ColorWriteLine("] فشل في الحصول على معلومات مالك السيرفر.", ConsoleColor.Red);
                        return;
                    }

                    
                    ColorWriteLine("\n####### معلومات السيرفر #######", successColor);
                    Console.WriteLine();
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("الاسم", highlightColor);
                    ColorWrite("]      $:   ", defaultColor);
                    ColorWriteLine(guildResponse["name"].ToString(), defaultColor);
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("المعرف", highlightColor);
                    ColorWrite("]        $:   ", defaultColor);
                    ColorWriteLine(guildResponse["id"].ToString(), defaultColor);
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("المالك", highlightColor);
                    ColorWrite("]     $:   ", defaultColor);
                    ColorWriteLine($"{ownerResponse["user"]["username"]}#{ownerResponse["user"]["discriminator"]}", defaultColor);
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("معرف المالك", highlightColor);
                    ColorWrite("]  $:   ", defaultColor);
                    ColorWriteLine(ownerId, defaultColor);
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("الأعضاء", highlightColor);
                    ColorWrite("]   $:   ", defaultColor);
                    ColorWriteLine(guildResponse["approximate_member_count"].ToString(), defaultColor);
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("المنطقة", highlightColor);
                    ColorWrite("]    $:   ", defaultColor);
                    ColorWriteLine(guildResponse["region"]?.ToString() ?? "unknown", defaultColor);
                    
                    ColorWrite("[", defaultColor);
                    ColorWrite("رابط الأيقونة", highlightColor);
                    ColorWrite("]  $:   ", defaultColor);
                    
                    if (guildResponse["icon"] != null)
                    {
                        ColorWriteLine($"https://cdn.discordapp.com/icons/{guildId}/{guildResponse["icon"]}.webp?size=256", defaultColor);
                    }
                    else
                    {
                        ColorWriteLine("لا توجد أيقونة", defaultColor);
                    }
                }
                catch (Exception ex)
                {
                    ColorWrite("[", defaultColor);
                    ColorWrite("!", ConsoleColor.Red);
                    ColorWrite("] خطأ: ", defaultColor);
                    ColorWriteLine(ex.Message, ConsoleColor.Red);
                }
            }).Wait();
        }

        
        private void ColorWrite(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }

        private void ColorWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}

        Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}

