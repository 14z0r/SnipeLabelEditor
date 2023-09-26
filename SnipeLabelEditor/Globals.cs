using System.Net.Http.Headers;
using SnipeLabelEditor.Data;

namespace SnipeLabelEditor
{
    public class Globals
    {
        private static HttpClient _snipeIT;
        public static HttpClient SnipeIT
        {
            get
            {
                if (_snipeIT is null)
                {
                    _snipeIT = new HttpClient();
                    _snipeIT.BaseAddress = new Uri(Settings.APIBaseURL);
                    _snipeIT.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.APIBarerToken);
                }

                return _snipeIT;
            }
        }
        public static ApiSettings Settings 
        {
            get => ApiSettings.LoadSettings();
            set => Settings = value;
        }
        
        public static string ConfigBaseDirectory
        {
            get => Path.Combine(Environment.CurrentDirectory, "config");
        }
    }
}
