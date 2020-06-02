using System;
using System.Collections.Generic;
using BowlingLibrary;
using System.Xml.Serialization;
using System.IO;
using Throws;
using Frames;

namespace UserInterface
{
    public class Interface // Клас інтерфейсу
    {
        protected Game game;

        public Interface(Game game) // Конструктор для партії із файлу
        {
            this.game = game;
        }

        public Interface() // Конструктор для нової партії
        {
            game = new Game();

            Console.WriteLine("How many players would you like to add?");
            Console.Write("Your choice: ");
            int numb = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");
            for (int i = 0; i < numb; i++)
            {
                Console.WriteLine("Enter info about the player in this format:\nZozulya");
                string player = Console.ReadLine();
                game.AddPlayer(new Player(player));
            }
        }

        protected int[] Read() // Читання номерів кегель, що залишилися після кидка
        {
            string temp = Console.ReadLine();
            int[] indexes;
            if (temp.Length == 0)
            {
                indexes = new int[] { };
            }
            else
            {
                string[] pins = temp.Split(" ");
                indexes = new int[pins.Length];
                for (int h = 0; h < pins.Length; h++)
                {
                    Int32.TryParse(pins[h], out indexes[h]);
                }   
                Array.Sort(indexes);
            }
            return indexes;
        }

        public void Interact() // Взаємодія із користувачем
        {
            bool played = false;
            bool end = true;

            while(end)
            {
                Console.WriteLine("Choose what you want to do:\n1) Play the game" +
                                                             "\n2) Add a player" +
                                                             "\n3) Remove a player" +
                                                             "\n4) Show the game table" +
                                                             "\n5) Save the results into the file" +
                                                             "\n6) End");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine("\n");
                if (choice == "1")// Гра
                {
                    if (!played)
                    {
                        game.ShowTable();
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < game.GetPlayerList().Count; j++)
                            {
                                Console.WriteLine("\nEnter info about the numbers of pins, that left after the FIRST throw of {0}, in this format:\n1 2 3 4 5 6 7 8 9 10", game.GetName(j)); ;
                                int[] indexes1 = Read();
                                FirstThrow ft = new FirstThrow(indexes1);
                                Console.WriteLine("\nEnter info about the numbers of pins, that left after the SECOND throw of {0}, in this format:\n1 2 3 4 5 6", game.GetName(j));
                                int[] indexes2 = Read();
                                SecondThrow st = new SecondThrow(indexes2, ft);
                                Frame fr = new Frame(ft, st);
                                game.AddFrame(j, fr);
                                game.ShowTable();
                            }
                        }

                        for (int j = 0; j < game.GetPlayerList().Count; j++)
                        {
                            Console.WriteLine("\nEnter info about the numbers of pins, that left after the FIRST throw of {0}, in this format:\n1 2 3 4 5 6 7 8 9 10", game.GetName(j));
                            int[] indexes1 = Read();
                            FirstThrow ft = new FirstThrow(indexes1);
                            Console.WriteLine("\nEnter info about the numbers of pins, that left after the SECOND throw of {0}, in this format:\n1 2 3 4 5 6", game.GetName(j));
                            int[] indexes2 = Read();
                            SecondThrow st = new SecondThrow(indexes2, ft);
                            LastFrame fr = new LastFrame(ft, st);
                            game.AddLastFrame(j, fr);
                            if (game.IsStrikeLastFrame(j))
                            {
                                Console.WriteLine("\nEnter info about the numbers of pins, that left after the SECOND throw of {0}, in this format:\n1 2 3 4 5 6 7 8 9 10", game.GetName(j));
                                indexes2 = Read();
                                FirstThrow stL = new FirstThrow(indexes2);
                                game.ResetSecondThrowLastFrame(j, stL);
                                if (game.IsStrikeLastFrameSecond(j))
                                {
                                    Console.WriteLine("\nEnter info about the numbers of pins, that left after the THIRD throw of {0}, in this format:\n1 2 3 4 5 6", game.GetName(j));
                                    int[] indexes3 = Read();
                                    FirstThrow thT = new FirstThrow(indexes3);
                                    game.SetThirdThrowLastFrame(j, thT);
                                }
                                else
                                {
                                    Console.WriteLine("\nEnter info about the numbers of pins, that left after the THIRD throw of {0}, in this format:\n1 2 3 4 5 6", game.GetName(j));
                                    int[] indexes3 = Read();
                                    SecondThrow thT = new SecondThrow(indexes3, stL);
                                    game.SetThirdThrowLastFrame(j, thT);
                                }
                            }
                            else if (game.IsSpareLastFrame(j))
                            {
                                Console.WriteLine("\nEnter info about the numbers of pins, that left after the THIRD throw, in this format of {0}, in this format:\n1 2 3 4 5 6", game.GetName(j));
                                int[] indexes3 = Read();
                                FirstThrow thT = new FirstThrow(indexes3);
                                game.SetThirdThrowLastFrame(j, thT);
                            }
                            game.ShowTable();
                        }
                        played = true;
                    }
                    else
                    {
                        Console.WriteLine("You've already played this game! To start the new one:\n     1) Save the results\n     2) End the game\n     3) Read a file\n");
                        game.ShowTable();
                    }
                }
                else if (choice == "2") // Додавання нового гравця
                {
                    Console.WriteLine("Enter info about the player in this format:\nZozulya");
                    try
                    {
                        string player = Console.ReadLine();
                        game.AddPlayer(new Player(player));
                    }
                    catch (PlayerException ex)
                    {
                        CatchExcepts(ex);
                    }
                    finally
                    {
                        Console.WriteLine("\n");
                        game.ShowPlayerList();
                    }
                }
                else if (choice == "3") // Видалення гравця
                {
                    try
                    {
                        game.ShowPlayerList();
                        Console.WriteLine("\nEnter the player's id in this format:\n3");
                        game.RemovePlayer(Convert.ToInt32(Console.ReadLine()));
                    }
                    catch (FormatException ex)
                    {
                        CatchExcepts(ex);
                    }
                    finally
                    {
                        Console.WriteLine("\n");
                        game.ShowPlayerList();
                    }
                }
                else if (choice == "4") // Вивід таблиці результатів партії
                {
                    game.ShowTable();
                }
                else if (choice == "5") // Запис результатів партії у файл players.xml
                {
                    List<Player> players = game.GetPlayerList();
                    foreach (Player pl in players)
                        pl.previousScore = pl.GetTotalScore();

                    XmlSerializer playersFormatter = new XmlSerializer(typeof(List<Player>), new XmlRootAttribute("Players"));

                    using (FileStream fs = new FileStream("players.xml", FileMode.OpenOrCreate))
                    {
                        playersFormatter.Serialize(fs, players);
                    }
                }
                else if (choice == "6") // Вихід
                {
                    Console.WriteLine("Do you want to exit without saving?");
                    Console.Write("Press y/n: ");
                    string yes = Console.ReadLine();
                    Console.WriteLine("\n");
                    if (yes == "y" || yes == "Y")
                    {
                        end = false;
                    }
                }
                Console.WriteLine("\n\n");
            }
        }

        public void CatchExcepts(Exception ex) // Запис виключення до exceptions.xml
        {
            List<Except> excepts;

            XmlSerializer exceptsFormatter = new XmlSerializer(typeof(List<Except>), new XmlRootAttribute("Exceptions"));

            using (FileStream fs = new FileStream("exceptions.xml", FileMode.OpenOrCreate)) // Читання exceptions.xml
            {
                excepts = (List<Except>)exceptsFormatter.Deserialize(fs);
            }

            excepts.Add(new Except(ex.Message, Convert.ToString(ex.TargetSite), ex.StackTrace));

            using (FileStream fs = new FileStream("exceptions.xml", FileMode.OpenOrCreate)) // Запис exceptions.xml
            {
                exceptsFormatter.Serialize(fs, excepts);
            }
        }
    }

    public class Except // Клас виключень
    {
        public string Message;
        public string TargetSite;
        public string StackTrace;

        public Except() { }

        public Except(string Message, string TargetSite, string StackTrace)
        {
            this.Message = Message;
            this.TargetSite = TargetSite;
            this.StackTrace = StackTrace;
        }
    }
}
