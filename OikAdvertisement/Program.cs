using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.VisualBasic;


namespace OikAdvertisement
{
    class Program
    {
        public static string StartZapros = "Подожди секунду";
        static public string AdsText;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Оик бот запущен");

            Console.Write("Введите текст рекламы:");
           AdsText =  Console.ReadLine();




            Console.WriteLine("Имя пользователя || Сообщение пользователя || Время отправки ");
            Console.ForegroundColor = ConsoleColor.White;


            var client = new TelegramBotClient("6031831916:AAHsrwBWnOc3-VXTfjhHkEnqCqfxu3E6DgE");
            client.StartReceiving(Update, Error);



            Console.ReadKey();
        
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;



            try
            {
                if (message != null)
                {
                    if (message.Text != null)
                    {
                        
                        Users(message.Chat.Id);
                        if (message.Chat.Title!=null)
                        {
                          await botClient.SendTextMessageAsync(-882338491, $" Кто: {message.Chat.Title} || Сообщение: {message.Text} ");
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(-882338491, $"  Кто: {message.Chat.FirstName} || Сообщение:{message.Text}  ");
                        }


                        Console.WriteLine($"{message.Chat.FirstName}   || {message.Text} || {DateAndTime.Now}");
                        if (message.Text.ToLower().Contains("/start") | message.Text.ToLower().Contains("cтарт"))
                        {
                           
                            await using Stream stream = System.IO.File.OpenRead(@"Resurses\TE9AqRe-AGU.jpg");
                            await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(stream, fileName: "TE9AqRe-AGU.jpg"));


                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{message.Chat.FirstName}👋, Привет. Этот бот предназначен для просмотра важной информации от колледжа ❤Оик❤.\nВы можете добавить меня в свою группу😜");

                            good();
                            ADS();
                        }


                        //Изминения 
                        if (message.Text.ToLower().Contains("изменения"))
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, StartZapros);

                            using (var client = new WebClient())
                            {
                                client.DownloadFile("https://oink.ru/uploads/lessons/izmeneniya.doc", @"Resurses\izmeneniya.doc");
                                await using Stream stream = System.IO.File.OpenRead(@"Resurses\izmeneniya.doc");
                                await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, fileName: "izmeneniya.doc"));
                                Console.WriteLine("Отправлено: ", message.Chat.FirstName);
                                good();
                                ADS();
                            }
                            
                        }
                        if (message.Text.ToLower().Contains("основное"))
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, StartZapros);

                            using (var client = new WebClient())
                            {
                                client.DownloadFile("https://oink.ru/uploads/raspisanie.xls", @"Resurses\OsnRaspisanie.xls");
                                await using Stream stream = System.IO.File.OpenRead(@"Resurses\OsnRaspisanie.xls");
                                await botClient.SendDocumentAsync(message.Chat.Id, new InputOnlineFile(stream, fileName: "OsnRaspisanie.xls"));
                                Console.WriteLine("Отправлено: ", message.Chat.FirstName);
                                good();
                                ADS();
                            }
                   
                        }

                        //Сайт
                        if (message.Text.ToLower().Contains("сайт"))
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"🌐Сайт: https://www.oink.ru/🌐\n✅Разработчик: https://vk.com/art.yango✅") ;
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Группа в вк: https://vk.com/new.oink");
                            Console.WriteLine("Отправлено: ", message.Chat.FirstName);
                            good();
                            ADS();

                        }

                        //Звонок
                        if (message.Text.ToLower().Contains("звонок"))
                        {
                            await botClient.SendTextMessageAsync(update.Message.Chat.Id, StartZapros);
                            await using Stream stream = System.IO.File.OpenRead(@"Resurses\Zvonki.jpg");
                            await botClient.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(stream, fileName: "Zvonki.jpg"));
                            Console.WriteLine("Отправлено: ", message.Chat.FirstName);
                            good();
                            ADS();

                        }




                        async void good()
                        {
                            {
                                string[][] strings = new[] {
                                   new[]{ "", "Изменения✍"},
                                   new[]{ "Основное📃", "Сайт💻"},
                                   new[]{ "Звонок🔔", ""}
                        };

                                ReplyKeyboardMarkup keyboardMarkup = strings;
                                keyboardMarkup.ResizeKeyboard = true;
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Обработано ", replyMarkup: keyboardMarkup);
                                return;
                            }
                        }



                        async void ADS()
                        {
                            Random Ads = new Random();
                            string FileAds = @"Resurses\ADS.jpg";
                            if (Ads.Next(0,5)==3)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($"Реклама: {update.Message.Chat.FirstName}");
                                Console.ForegroundColor = ConsoleColor.White;
                                if (System.IO.File.Exists(FileAds))
                                {
                                    await using Stream stream = System.IO.File.OpenRead(@"Resurses\ADS.jpg");
                                    await botClient.SendPhotoAsync(message.Chat.Id,new InputOnlineFile(stream, fileName: "ADS.jpg"),AdsText);
                                }
                                else
                                {
                                   await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Тут может быть твоя реклама 😉");
                                }
                            }
                        }


                        void Users(long idC)
                        {
                            
                            string path = @"Resurses\Users.txt";
                            bool its = false;
                            string[] lines = System.IO.File.ReadAllLines(path);
                            using (FileStream stream = new FileStream(path, FileMode.Append))
                            {
                                using (StreamWriter wr = new StreamWriter(stream))
                                {

                                    for (int i = 0; i < lines.Length; i++)
                                    {
                                        if (lines[i] == idC.ToString())
                                        {
                                            its = true;
                                        }

                                    }
                                    if (its != true)
                                        wr.WriteLine(idC.ToString());
                                }
                            }
                        }

                    }
                }
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        
            



        }



        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            
             throw new NotImplementedException();
            
           
        }



    }
}