using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Args;
namespace TelegramBot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("1539030874:AAHUBnYfxOthcspi_NEzespNc-o18Yhcm28");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot id: {me.Id}. Bot Name {me.FirstName}" );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Console.ReadLine();
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {

                var text = e?.Message?.Text;
                if (text == null)
                    return;
                Console.WriteLine($"recieved text message'{text}' in chat '{e.Message.Chat.Id}'");

                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + text + "&units=metric&appid=a89085cd586bbb189a873abff0ec3455";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                    
                }

                double k = 1.33322;

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Temperature in {weatherResponse.Name}: {weatherResponse.Main.Temp}°C" + Environment.NewLine +
                          $"Temperature feels like {weatherResponse.Main.feels_like}°C" + Environment.NewLine +
                          $"Humidity in {weatherResponse.Name}: {weatherResponse.Main.Humidity}%" + Environment.NewLine +
                          $"Wind in { weatherResponse.Name}: {weatherResponse.Wind.WindDirection(weatherResponse.Wind.deg)} - {weatherResponse.Wind.speed} m/s" + Environment.NewLine +
                          $"Atmosphere pressure in {weatherResponse.Name}: {Convert.ToInt32(weatherResponse.Main.pressure/k)}" + Environment.NewLine +
                          $"Also in {weatherResponse.Name}: {weatherResponse.Weather[0].Main} and {weatherResponse.Weather[0].Description   }"
                    ).ConfigureAwait(false);
            }
            catch(Exception)    
            {
                var text = e?.Message?.Text;
                if (text == null)
                    return;
                await botClient.SendTextMessageAsync(
                   chatId: e.Message.Chat,
                   text: "Проверьте правильность написания названия города"
                   ).ConfigureAwait(false);
            }
        } 
    }
}
