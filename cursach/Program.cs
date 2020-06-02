using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using BowlingLibrary;
using UserInterface;

namespace cursach
{
    class Program
    {
        static void Main(string[] args)
        {
            bool end = true;

            while (end)
            {
                Console.WriteLine("Choose what you want to do:\n1) Read a file" +
                                                             "\n2) Make a new game" +
                                                             "\n3) End");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine("\n");
                if (choice == "1")
                {
                    XmlSerializer playersFormatter = new XmlSerializer(typeof(List<Player>), new XmlRootAttribute("Players"));

                    List<Player> players;

                    using (FileStream fs = new FileStream("players.xml", FileMode.OpenOrCreate))
                    {
                        players = (List<Player>)playersFormatter.Deserialize(fs);
                    }

                    int maxId = Player.lastId;
                    foreach (Player pl in players)
                    {
                        if(maxId < pl.id)
                        {
                            maxId = pl.id;
                        }
                    }
                    Player.lastId = maxId;

                    Game gm = new Game();
                    foreach (Player pl in players)
                        gm.AddPlayer(pl);
                    Interface inter = new Interface(gm);
                    inter.Interact();
                }
                else if (choice == "2")
                {
                    Interface inter = new Interface();
                    inter.Interact();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Bye!");
                    end = false;
                }
            }
        }
    }
}
