using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Globalization;

namespace PočasíČas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            PrepareGUI();

            LoadWeatherData();

        }
        public static void PrintWeatherData(WeatherData weatherData)
        {
            if (weatherData != null)
            {
                Console.WriteLine("Latitude: " + weatherData.Latitude);
                Console.WriteLine("Longitude: " + weatherData.Longitude);
                Console.WriteLine("Generation Time (ms): " + weatherData.GenerationTimeMs);
                Console.WriteLine("UTC Offset (seconds): " + weatherData.UtcOffsetSeconds);
                Console.WriteLine("Timezone: " + weatherData.Timezone);
                Console.WriteLine("Timezone Abbreviation: " + weatherData.TimezoneAbbreviation);
                Console.WriteLine("Elevation: " + weatherData.Elevation);

                Console.WriteLine("Hourly Units:");
                if (weatherData.HourlyUnits != null)
                {
                    foreach (HourlyUnit unit in weatherData.HourlyUnits)
                    {
                        Console.WriteLine("Time: " + unit.Time);
                        Console.WriteLine("Temperature at 2m: " + unit.Temperature_2m);
                    }
                }

                Console.WriteLine("Hourly Data:");
                if (weatherData.Hourly != null)
                {
                    for (int i = 0; i < weatherData.Hourly.Time.Length; i++)
                    {
                        //Console.WriteLine("Time: " + weatherData.Hourly.Time[i]);
                        string dateTimeString = weatherData.Hourly.Time[i];// "2023-05-11T19:00";
                        string format = "yyyy-MM-ddTHH:mm";
                        DateTime dateTime;

                        if (DateTime.TryParseExact(dateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        {
                            //Console.WriteLine("Parsed DateTime: " + dateTime);
                            string temperature = weatherData.Hourly.Temperature_2m[i].ToString();
                            string message = "Temperature for " + dateTime.ToString("dd MMM yyyy, HH:mm") + " is " + temperature;
                            Console.WriteLine(message);

                        }
                        else
                        {
                            Console.WriteLine("Failed to parse DateTime.");
                        }

                        //Console.WriteLine("Temperature at 2m: " + weatherData.Hourly.Temperature_2m[i]);
                    }
                }
            }
            else
            {
                Console.WriteLine("WeatherData is null.");
            }
            /*
            if (weatherData != null)
            {
                Type weatherDataType = typeof(WeatherData);
                PropertyInfo[] properties = weatherDataType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(weatherData);
                    string propertyName = property.Name;
                    Console.WriteLine(propertyName + ": " + value);
                }
            }
            else
            {
                Console.WriteLine("WeatherData je null.");
            }*/
        }
        private async Task LoadWeatherData()
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=50.09&longitude=14.41&hourly=temperature_2m"; // Nahraďte URL příslušnou adresou API pro počasí

            WeatherData weatherData = await WeatherService.GetWeatherData(url);

            if (weatherData != null)
            {
                // Použijte načtená data o počasí
                //Console.WriteLine("Město: " + weatherData.City);
                //Console.WriteLine("Teplota: " + weatherData.Temperature);
                //Console.WriteLine("Popis: " + weatherData.Description);
            }


            PrintWeatherData(weatherData);


        }

        private void PrepareGUI()
        {
            TextBox textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            Content = textBox;

            Console.SetOut(new TextBoxStreamWriter(textBox));
            Console.WriteLine("Vítejte v aplikaci!");

            // Sledování změn v obsahu TextBoxu a automatické posunutí na konec
            textBox.TextChanged += (sender, e) =>
            {
                textBox.ScrollToEnd();
            };
        }

        // Třída pro přesměrování výstupu z konzole do TextBoxu
        public class TextBoxStreamWriter : TextWriter
        {
            private TextBox textBox;

            public TextBoxStreamWriter(TextBox textBox)
            {
                this.textBox = textBox;
            }

            public override void Write(char value)
            {
                textBox.Dispatcher.Invoke(() => textBox.AppendText(value.ToString()));
            }

            public override Encoding Encoding => Encoding.UTF8;
        }



    }
}
