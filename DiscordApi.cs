// Coded / Dev By Tl6esh
// tL6esh Nuker - Discord Nuker Tool
// Copyright © 2025

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace tL6esh_Nuker
{
    public class DiscordApi
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "https://discord.com/api/v9";

        
        public static Dictionary<string, string> GetHeaders(string token)
        {
            return new Dictionary<string, string>
            {
                { "Authorization", token },
                { "Content-Type", "application/json" },
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36" }
            };
        }

        
        public static async Task<JObject> GetAsync(string endpoint, string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}{endpoint}");
                
                foreach (var header in GetHeaders(token))
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return JObject.Parse(content);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {response.StatusCode} - {content}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        
        public static async Task<JObject> PostAsync(string endpoint, string token, object data)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}{endpoint}")
                {
                    Content = content
                };
                
                foreach (var header in GetHeaders(token))
                {
                    if (header.Key != "Content-Type") // Already set in StringContent
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return new JObject();
                    }
                    return JObject.Parse(responseContent);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {response.StatusCode} - {responseContent}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        
        public static async Task<bool> DeleteAsync(string endpoint, string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}{endpoint}");
                
                foreach (var header in GetHeaders(token))
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                var response = await client.SendAsync(request);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        
        public static async Task<JObject> PatchAsync(string endpoint, string token, object data)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{BaseUrl}{endpoint}")
                {
                    Content = content
                };
                
                foreach (var header in GetHeaders(token))
                {
                    if (header.Key != "Content-Type") // Already set in StringContent
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return new JObject();
                    }
                    return JObject.Parse(responseContent);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {response.StatusCode} - {responseContent}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }
    }
}

.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }
    }
}

