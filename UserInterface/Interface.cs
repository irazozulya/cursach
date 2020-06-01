using System;
using System.Xml;
using System.Collections.Generic;
using BowlingLibrary;
using System.Xml.Serialization;
using System.IO;
using Throws;
using Frames;

namespace UserInterface
{
    public class Interface
    {
        protected Game game;

        public Interface(Game game)
        {
            this.game = game;
        }

        public Interface()
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

        public void Interact()
        {
            bool end = true;

            while(end)
            {
                Console.WriteLine("Choose what you want to do:\n1) Play the game" +
                                                             "\n2) Save the results into the file" +
                                                             "\n3) End");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine("\n");
                if (choice == "1")
                {
                    int[] pinsF1 = new int[] { };
                    Array.Sort(pinsF1);
                    int[] pinsS1 = pinsF1;
                    int[] pinsF2 = new int[] { 1, 3, 5 };
                    Array.Sort(pinsF2);
                    int[] pinsS2 = new int[] { 1 };
                    Array.Sort(pinsS2);
                    int[] pinsF3 = new int[] { 7, 10 };
                    Array.Sort(pinsF3);
                    int[] pinsS3 = new int[] { };
                    Array.Sort(pinsS3);
                    int[] pinsF4 = new int[] { 3, 4, 6, 7, 10, 1, 2, 5, 8, 9 };
                    Array.Sort(pinsF4);
                    int[] pinsS4 = new int[] { 3 };
                    Array.Sort(pinsS3);
                    int[] pinsF5 = new int[] { 3 };
                    Array.Sort(pinsF5);
                    int[] pinsS5 = new int[] { 3 };
                    Array.Sort(pinsS5);
                    int[] pinsF6 = new int[] { 1, 3, 5, 2 };
                    Array.Sort(pinsF6);
                    int[] pinsS6 = new int[] { };
                    Array.Sort(pinsS6);
                    FirstThrow ft1 = new FirstThrow(pinsF1);
                    SecondThrow st1 = new SecondThrow(pinsS1, ft1);
                    FirstThrow ft2 = new FirstThrow(pinsF2);
                    SecondThrow st2 = new SecondThrow(pinsS2, ft2);
                    FirstThrow ft3 = new FirstThrow(pinsF3);
                    SecondThrow st3 = new SecondThrow(pinsS3, ft3);
                    FirstThrow ft4 = new FirstThrow(pinsF4);
                    SecondThrow st4 = new SecondThrow(pinsS4, ft4);
                    FirstThrow ft5 = new FirstThrow(pinsF5);
                    SecondThrow st5 = new SecondThrow(pinsS5, ft5);
                    FirstThrow ft6 = new FirstThrow(pinsF6);
                    SecondThrow st6 = new SecondThrow(pinsS6, ft6);
                    Frame fr1 = new Frame(ft1, st1);
                    Frame fr2 = new Frame(ft1, st1);
                    Frame fr3 = new Frame(ft1, st1);
                    Frame fr4 = new Frame(ft1, st1);
                    Frame fr5 = new Frame(ft1, st1);
                    Frame fr6 = new Frame(ft1, st1);
                    Frame fr7 = new Frame(ft1, st1);
                    Frame fr8 = new Frame(ft1, st1);
                    Frame fr9 = new Frame(ft6, st6);
                    LastFrame lf = new LastFrame(ft1, st1);
                    Player pl = new Player("Zozulya");
                    pl.AddFrame(fr1);
                    pl.AddFrame(fr2);
                    pl.AddFrame(fr3);
                    pl.AddFrame(fr4);
                    pl.AddFrame(fr5);
                    pl.AddFrame(fr6);
                    pl.AddFrame(fr7);
                    pl.AddFrame(fr8);
                    pl.AddFrame(fr9);
                    pl.AddFrame(lf);
                    pl.SetThirdThrow(ft1, ft1);
                    game.AddPlayer(pl);
                    game.ShowTable();
                }
                else if (choice == "2")
                {
                    /*XmlWriter xmlWriter = XmlWriter.Create("players.xml");
                    List<Player> players = game.GetPlayerList();
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Players");
                    for (int i = 0; i < players.Count; i++)
                    {
                        xmlWriter.WriteStartElement("Player");
                        xmlWriter.WriteStartAttribute("id");
                        xmlWriter.WriteValue(players[i].GetId());
                        xmlWriter.WriteEndAttribute();
                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteValue(players[i].name);
                        xmlWriter.WriteEndAttribute();
                        xmlWriter.WriteStartAttribute("previousScore");
                        xmlWriter.WriteValue(players[i].GetTotalScore());
                        xmlWriter.WriteEndAttribute();
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Close();*/
                    List<Player> players = game.GetPlayerList();
                    foreach (Player pl in players)
                        pl.previousScore = pl.GetTotalScore();

                    XmlSerializer playersFormatter = new XmlSerializer(typeof(List<Player>), new XmlRootAttribute("Players"));

                    using (FileStream fs = new FileStream("players.xml", FileMode.OpenOrCreate))
                    {
                        playersFormatter.Serialize(fs, players);
                    }
                }
                else if (choice == "3")
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
            }
        }
    }
}
