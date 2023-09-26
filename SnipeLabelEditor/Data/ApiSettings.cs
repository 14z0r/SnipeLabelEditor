using System.Text.Json;

namespace SnipeLabelEditor.Data
{
    public class ApiSettings
    {
        public string APIBaseURL { get; set; } = string.Empty;
        public string APIBarerToken { get; set; } = string.Empty;

        private static string filename = Path.Combine(Globals.ConfigBaseDirectory, "settings.json");

        /// <summary>
        /// Loads the settings.json file and converts the json to an APISettings-Object.
        /// </summary>
        /// <returns>APISetting with values form settings.json</returns>
        public static ApiSettings LoadSettings()
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();

                string x = JsonSerializer.Serialize(new ApiSettings());
                File.WriteAllText(filename, x);
            }

            string jsonstring = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<ApiSettings>(jsonstring);
        }

        /// <summary>
        /// Converts an APISettings-Objet to a json and writes the string into the setting.json.
        /// </summary>
        /// <param name="settings"></param>
        public static void SaveSettings(ApiSettings settings)
        {
            string jsonstring = JsonSerializer.Serialize(settings);
            File.WriteAllText(filename, jsonstring);
            LoadSettings();
        }
    }
}
