using System;
using System.Collections.Generic;
using Throws;
using Frames;

namespace BowlingLibrary
{
    public class Game
    {
        protected int id;
        protected List<Player> players = new List<Player>();

        public Game(int id)
        {
            this.id = id;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        public void ShowTable()
        {
            Console.WriteLine("{0, -3}| {1, -20}| {2, -4}| {3, -4}| {4, -4}| {5, -4}| {6, -4}| {7, -4}| {8, -4}| {9, -4}| {10, -4}| {11, -9}| {12, -10}", "ID", "Player", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Total score");
            foreach (Player pl in players)
                pl.ShowScore();
        }
    }

    public class Player
    {
        protected int id;
        public string name;
        protected int previousScore;
        protected List<Frame> frames = new List<Frame>();
        protected LastFrame lastFrame;

        public Player(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Player(string name, int previousScore, int id)
        {
            this.name = name;
            this.previousScore = previousScore;
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        protected void ResetPrevious()
        {
            int size = frames.Count;
            if (size == 2)
            {
                if (frames[0].IsSpare())
                {
                    frames[0].ResetScore(frames[1].GetScoreFirst());
                }
            }
            else if (size == 3)
            {
                if (frames[1].IsSpare())
                {
                    frames[1].ResetScore(frames[2].GetScoreFirst());
                }
                else if (frames[0].IsStrike())
                {
                    if (frames[1].IsStrike())
                    {
                        frames[0].ResetScore(frames[2].GetScoreFirst());
                        frames[1].ResetScore(frames[2].GetScore());
                    }
                }
            }
            else if (size > 3)
            {
                if (frames[size - 2].IsSpare())
                {
                    frames[size - 2].ResetScore(frames[size - 1].GetScoreFirst());
                }
                else if (frames[size - 4].IsStrike() && frames[size - 3].IsStrike() && frames[size - 2].IsStrike())
                {
                    frames[size - 4].SetScore(30);
                    frames[size - 3].ResetScore(frames[size - 1].GetScoreFirst());
                    frames[size - 2].ResetScore(frames[size - 1].GetScore());
                }
                else if (frames[size - 3].IsStrike() && frames[size - 2].IsStrike())
                {
                    frames[size - 3].ResetScore(frames[size - 1].GetScoreFirst());
                    frames[size - 2].ResetScore(frames[size - 1].GetScore());
                }
                else if (frames[size - 2].IsStrike())
                {
                    frames[size - 2].ResetScore(frames[size - 1].GetScore());
                }
            }
        }

        public void AddFrame(Frame frame)
        {
            if (frames.Count < 9)
            {
                frames.Add(frame);
                ResetPrevious();
            }
        }

        public void AddFrame(LastFrame frame)
        {
            if (frames.Count == 9)
            {
                lastFrame = frame;

                if (frames[8].IsSpare())
                {
                    frames[8].ResetScore(lastFrame.GetScoreFirst());
                }
                else if (frames[6].IsStrike() && frames[7].IsStrike() && frames[8].IsStrike())
                {
                    frames[6].SetScore(30);
                    frames[7].ResetScore(lastFrame.GetScoreFirst());
                    frames[8].ResetScore(lastFrame.GetScore());
                }
                else if (frames[7].IsStrike() && frames[8].IsStrike())
                {
                    frames[7].ResetScore(lastFrame.GetScoreFirst());
                    frames[8].ResetScore(lastFrame.GetScore());
                }
                else if (frames[8].IsStrike())
                {
                    frames[8].ResetScore(lastFrame.GetScore());
                }
            }
        }

        public void SetThirdThrow(FirstThrow thThrow)
        {
            if (lastFrame.IsSpare())
            {
                lastFrame.SetThirdThrow(thThrow);
            }
        }

        public void SetThirdThrow(FirstThrow sThrow, AbstractThrow thThrow)
        {
            if (lastFrame.IsStrike())
            {
                lastFrame.ResetSecondThrow(sThrow);
                lastFrame.SetThirdThrow(thThrow);
            }
        }

        public void ShowScore()
        {
            int total = 0;
            Console.Write("{0, -3}| {1, -20}|", id, name);
            foreach(Frame fr in frames)
            {
                if (fr.IsSplit())
                {
                    if (fr.IsSpare())
                    {
                        Console.Write("({0})|/|", fr.GetScoreFirst());
                        total += fr.GetScore();
                    }
                    else
                    {
                        if (fr.GetScoreSecond() == 0)
                        {
                            Console.Write("({0})| -|", fr.GetScoreFirst());
                        }
                        else
                        {
                            Console.Write("({0})| {1}|", fr.GetScoreFirst(), fr.GetScoreSecond());
                        }
                        
                        total += fr.GetScore();
                    }
                }
                else if (fr.IsStrike())
                {
                    Console.Write(" X|  |");
                    total += fr.GetScore();
                }
                else if (fr.IsSpare())
                {
                    if (fr.GetScoreFirst() == 0)
                    {
                        Console.Write(" -| /|", fr.GetScoreFirst());
                    }
                    else
                    {
                        Console.Write(" {0}| /|", fr.GetScoreFirst());
                    }
                    total += fr.GetScore();
                }
                else
                {
                    if (fr.GetScoreFirst() == 0)
                    {
                        if (fr.GetScoreSecond() == 0)
                        {
                            Console.Write(" -| -|");
                        }
                        else
                        {
                            Console.Write(" -| {0}|", fr.GetScoreSecond());
                        }
                    }
                    else
                    {
                        if (fr.GetScoreSecond() == 0)
                        {
                            Console.Write(" {0}| -|", fr.GetScoreFirst());
                        }
                        else
                        {
                            Console.Write(" {0}| {1}|", fr.GetScoreFirst(), fr.GetScoreSecond());
                        }
                    }
                    total += fr.GetScore();
                }
            }
            for (int i = 0; i < 9 - frames.Count; i++)
            {
                Console.Write("  |  |");
            }

            if (frames.Count != 9)
            {
                Console.Write("   |  |   |");
            }
            else
            {
                if (lastFrame.IsSplit())
                {
                    if (lastFrame.IsSpare())
                    {
                        if(lastFrame.IsSplitThird())
                        {
                            Console.Write("({0})| /|({1})|", lastFrame.GetScoreFirst(), lastFrame.GetScoreThird());
                        }
                        else if (lastFrame.IsStrikeThird())
                        {
                            Console.Write("({0})| /| X |", lastFrame.GetScoreFirst());
                        }
                        else
                        {
                            if (lastFrame.GetScoreThird() == 0)
                            {
                                Console.Write("({0})| /| - |", lastFrame.GetScoreFirst());
                            }
                            else
                            {
                                Console.Write("({0})| /| {1} |", lastFrame.GetScoreFirst(), lastFrame.GetScoreThird());
                            }
                        }
                        total += lastFrame.GetScore();
                    }
                    else
                    {
                        if (lastFrame.GetScoreSecond() == 0)
                        {
                            Console.Write("({0})| -|   |", lastFrame.GetScoreFirst());
                        }
                        else
                        {
                            Console.Write("({0})| {1}|   |", lastFrame.GetScoreFirst(), lastFrame.GetScoreSecond());
                        }
                        total += lastFrame.GetScore();
                    }
                }
                else if (lastFrame.IsStrike())
                {
                    if (lastFrame.IsStrikeSecond())
                    {
                        if (lastFrame.IsSplitThird())
                        {
                            Console.Write(" X | X|({0})|", lastFrame.GetScoreThird());
                        }
                        else if (lastFrame.IsStrikeThird())
                        {
                            Console.Write(" X | X| X |");
                        }
                        else
                        {
                            if (lastFrame.GetScoreThird() == 0)
                            {
                                Console.Write(" X | X| - |");
                            }
                            else
                            {
                                Console.Write(" X | X| {0} |", lastFrame.GetScoreThird());
                            }
                        }
                    }
                    else
                    {
                        if (lastFrame.IsSplitSecond())
                        {
                            if (lastFrame.IsSpareThird())
                            {
                                Console.Write(" X |({0})| /|", lastFrame.GetScoreSecond());
                            }
                            else
                            {
                                if (lastFrame.GetScoreThird() == 0)
                                {
                                    Console.Write(" X |({0})| -|", lastFrame.GetScoreSecond());
                                }
                                else
                                {
                                    Console.Write(" X |({0})| {1}|", lastFrame.GetScoreSecond(), lastFrame.GetScoreThird());
                                }
                            }
                            Console.Write(" X |  |({0})|", lastFrame.GetScoreThird());
                        }
                        else
                        {
                            if (lastFrame.GetScoreSecond() == 0)
                            {
                                if (lastFrame.IsSpareThird())
                                {
                                    Console.Write(" X | -| / |");
                                }
                                else
                                {
                                    if (lastFrame.GetScoreThird() == 0)
                                    {
                                        Console.Write(" X | -| - |");
                                    }
                                    else
                                    {
                                        Console.Write(" X | -| {0} |", lastFrame.GetScoreThird());
                                    }
                                }
                            }
                            else
                            {
                                if (lastFrame.IsSpareThird())
                                {
                                    Console.Write(" X | {0}| / |", lastFrame.GetScoreSecond());
                                }
                                else
                                {
                                    if (lastFrame.GetScoreThird() == 0)
                                    {
                                        Console.Write(" X | {0}| - |", lastFrame.GetScoreSecond());
                                    }
                                    else
                                    {
                                        Console.Write(" X | {0}| {1} |", lastFrame.GetScoreSecond(), lastFrame.GetScoreThird());
                                    }
                                }
                            }
                        }
                    }
                    total += lastFrame.GetScore();
                }
                else if (lastFrame.IsSpare())
                {
                    if (lastFrame.IsSplitThird())
                    {
                        if (lastFrame.GetScoreFirst() == 0)
                        {
                            Console.Write(" - | /|({0})|", lastFrame.GetScoreThird());
                        }
                        else
                        {
                            Console.Write(" {0} | /|({1})|", lastFrame.GetScoreFirst(), lastFrame.GetScoreThird());
                        }
                    }
                    else if (lastFrame.IsStrikeThird())
                    {
                        if (lastFrame.GetScoreFirst() == 0)
                        {
                            Console.Write(" - | /| X |");
                        }
                        else
                        {
                            Console.Write(" {0} | /| X |", lastFrame.GetScoreFirst());
                        }
                    }
                    else
                    {
                        if (lastFrame.GetScoreFirst() == 0)
                        {
                            if (lastFrame.GetScoreThird() == 0)
                            {
                                Console.Write(" - | /| - |");
                            }
                            else
                            {
                                Console.Write(" - | /| {0} |", lastFrame.GetScoreThird());
                            }
                        }
                        else
                        {
                            if (lastFrame.GetScoreThird() == 0)
                            {
                                Console.Write(" {0} | /| - |", lastFrame.GetScoreFirst());
                            }
                            else
                            {
                                Console.Write(" {0} | /| {1} |", lastFrame.GetScoreFirst(), lastFrame.GetScoreThird());
                            }
                        }
                    }
                    total += lastFrame.GetScore();
                }
                else
                {
                    if (lastFrame.GetScoreFirst() == 0)
                    {
                        if (lastFrame.GetScoreSecond() == 0)
                        {
                            Console.Write(" - | -|   |");
                        }
                        else
                        {
                            Console.Write(" - | {0}|   |", lastFrame.GetScoreSecond());
                        }
                    }
                    else
                    {
                        if (lastFrame.GetScoreSecond() == 0)
                        {
                            Console.Write(" {0} | -|   |", lastFrame.GetScoreFirst());
                        }
                        else
                        {
                            Console.Write(" {0} | {1}|   |", lastFrame.GetScoreFirst(), lastFrame.GetScoreSecond());
                        }
                    }
                    total += lastFrame.GetScore();
                }
            }

            Console.WriteLine(" {0}", total);

            foreach (var i in frames)
                Console.Write(i.GetScore() + " // ");
            Console.WriteLine(lastFrame.GetScore());
        }
    }
}
