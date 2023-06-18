﻿using SnipeLabelEditor.Models;
using System.Text.Json;

namespace SnipeLabelEditor
{
    public class APISettings
    {
        public string APIBaseURL { get; set; } = string.Empty;
        public string APIBarerToken { get; set; } = string.Empty;

        private static string filename = Path.Combine(Environment.CurrentDirectory, "settings.json");

        public static APISettings LoadSettings()
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();

                string x = JsonSerializer.Serialize(new APISettings());
                File.WriteAllText(filename, x);
            }

            string jsonstring = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<APISettings>(jsonstring);
        }

        public static void SaveSettings()
        {
            string jsonstring = JsonSerializer.Serialize(Globals.Settings);
            File.WriteAllText(filename, jsonstring);
        }
    }
}
