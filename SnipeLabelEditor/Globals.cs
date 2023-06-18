using System.Net.Http.Headers;

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

        public static APISettings Settings 
        {
            get => APISettings.LoadSettings();
            set => Settings = value;
        }
    }
}
