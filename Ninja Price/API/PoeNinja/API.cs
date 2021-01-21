using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using SharpDX;

namespace Ninja_Price.API.PoeNinja
{
    public class Api
    {
        public static string DownloadFromUrl(string url)
        {
            try
            {
                using (var web = new WebClient())
                {
                    var json = web.DownloadString(url);
                    return json;
                }
            }
            catch (CookieException)
            {
                return "";
            }
        }

        public class Json
        {
            public static string ReadJson(string filePath)
            {
                return File.ReadAllText(filePath);
            }

            public static void SaveJson(string filePath, string data)
            {
                using (var file = File.CreateText(filePath))
                {
                    file.WriteLine(data);
                }
            }

            public static TSettingType LoadSettingFile<TSettingType>(string fileName)
            {
                return JsonConvert.DeserializeObject<TSettingType>(File.ReadAllText(fileName));
            }

            public static bool SaveSettingFile<TSettingType>(string fileName, TSettingType setting)
            {
                try
                {
                    File.WriteAllText(fileName, JsonConvert.SerializeObject(setting, Formatting.Indented));
                    if (Main.Main.Controller.Settings.Debug) { Main.Main.Controller.LogMessage($"{fileName} - Downloaded", 25, SharpDX.Color.Yellow); }
                    return true;
                }
                catch(Exception e)
                {
                    File.WriteAllText(fileName, e.StackTrace);
                    if (Main.Main.Controller.Settings.Debug) { Main.Main.Controller.LogError($"{fileName} - {e.StackTrace} Failed", 25); }

                }
                return false;
            }
        }
    }
}