namespace openWeather.Utils
{
    public class CommonFunctions
    {
        public static char ConjutorDeParametroDaUrl(string url)
        {
            return url.Contains("?") ? '&' : '?';
        }
    }
}
