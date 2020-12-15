using OpenWeatherAPI;
using System;
using System.Threading.Tasks;
using System.Windows;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    public class OpenWeatherService : ITemperatureService
    {
        OpenWeatherProcessor owp;

        public OpenWeatherService(string apiKey)
        {
            owp = OpenWeatherProcessor.Instance;
            owp.ApiKey = apiKey;
        }
        
        public async Task<TemperatureModel> GetTempAsync()
        {
            var temp = await owp.GetCurrentWeatherAsync();
            var result = new TemperatureModel();

            if(temp != null)
            {
                 result = new TemperatureModel
                {
                    City = temp.Name,
                    DateTime = DateTime.UnixEpoch.AddSeconds(temp.DateTime).ToLocalTime(),
                    Temperature = temp.Main.Temperature
                };

                return result;

            }
            else
            {
                MessageBox.Show("API KEY OR CITY NULL :(  ");
            }

            return result;

        }

        public void SetLocation(string location)
        {
            owp.City = location;
        }

        public void SetApiKey(string apikey)
        {
            owp.ApiKey = apikey;
        }
    }
}
