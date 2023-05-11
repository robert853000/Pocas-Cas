using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System;
    using System.Net;
    
using System.Net.Http.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace PočasíČas
{


    

    public class WeatherDataX
    {
        public string City { get; set; }
        public float Temperature { get; set; }
        public string Description { get; set; }
    }
    public class WeatherData
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float GenerationTimeMs { get; set; }
        public float UtcOffsetSeconds { get; set; }
        public string Timezone { get; set; }
        public string TimezoneAbbreviation { get; set; }
        public float Elevation { get; set; }
        public HourlyUnit[] HourlyUnits { get; set; }
        public HourlyData Hourly { get; set; }
    }

    public class HourlyUnit
    {
        public string Time { get; set; }
        public string Temperature_2m { get; set; }
    }

    public class HourlyData
    {
        public string[] Time { get; set; }
        public float[] Temperature_2m { get; set; }
    }


    public static class WeatherService
    {
        public static async Task<WeatherData> GetWeatherData(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    WeatherData weatherData = await client.GetFromJsonAsync<WeatherData>(url);
                    return weatherData;
                }
            }
            catch (Exception ex)
            {
                // Zde můžete implementovat zpracování chyby
                Console.WriteLine("Chyba při načítání dat: " + ex.Message);
                return null;
            }
        }
    }
}
