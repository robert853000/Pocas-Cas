# Pocas-Cas
The program retrieves weather information
The program retrieves weather information from the following URL: https://api.open-meteo.com/v1/forecast?latitude=50.09&longitude=14.41&hourly=temperature_2m.

Using this URL, the program connects to the Open Meteo API to fetch the weather forecast data. It specifies the latitude and longitude coordinates to determine the location for which the weather data is requested. Additionally, the parameter "hourly=temperature_2m" is included to specifically retrieve the hourly temperature data at 2 meters above ground level.

Once the program successfully receives the response from the API, it parses the JSON data and extracts relevant information such as latitude, longitude, generation time, UTC offset, timezone, timezone abbreviation, elevation, and the hourly temperature data.

The extracted weather data is then displayed in the console, providing details about the location, generation time, time zone, elevation, and the temperature for each hour.

Overall, the program serves as a weather forecast application that utilizes the Open Meteo API to provide real-time temperature information for a specific location.
