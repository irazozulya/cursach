using System;
using System.Collections.Generic;
using Throws;
using Frames;

namespace BowlingLibrary
{
    public class Game
    {
        protected List<Player> players = new List<Player>();

        public Game() { }

        public List<Player> GetPlayerList()
        {
            return players;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(int playerID)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].id == playerID)
                {
                    players.Remove(players[i]);
                }
            }
        }

        public string GetName(int playerID)
        {
            return players[playerID].name;
        }

        public void AddFrame(int playerID, Frame frame)
        {
            players[playerID].AddFrame(frame);
        }

        public void AddLastFrame(int playerID, LastFrame frame)
        {
            players[playerID].AddFrame(frame);
        }

        public bool IsStrikeLastFrame(int playerID)
        {
            return players[playerID].IsStrikeLastFrame();
        }

        public bool IsStrikeLastFrameSecond(int playerID)
        {
            return players[playerID].IsStrikeLastFrameSecond();
        }

        public bool IsSpareLastFrame(int playerID)
        {
            return players[playerID].IsSpareLastFrame();
        }

        public void ResetSecondThrowLastFrame(int playerID, AbstractThrow tThrow)
        {
            players[playerID].ResetSecondThrow(tThrow);
        }

        public void SetThirdThrowLastFrame(int playerID, AbstractThrow tThrow)
        {
            players[playerID].SetThirdThrow(tThrow);
        }

        public void ShowPlayerList()
        {
            Console.WriteLine("{0, -3}| {1, -20}|", "ID", "Player");
            foreach (Player pl in players)
                Console.WriteLine("{0, -3}| {1, -20}|", pl.id, pl.name);
        }

        public void ShowTable()
        {
            Console.WriteLine("{0, -3}| {1, -20}| {2, -4}| {3, -4}| {4, -4}| {5, -4}| {6, -4}| {7, -4}| {8, -4}| {9, -4}| {10, -4}| {11, -9}| {12, -10}| {13, -14}| {14, -11}", "ID", "Player", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Game score", "Previous score", "Total score");
            foreach (Player pl in players)
                pl.ShowScore();
        }
    }

    public class Player
    {
        public static int lastId = 0;
        public int id;
        public string name;
        public int previousScore;
        protected int currentScore = 0;
        protected List<Frame> frames = new List<Frame>();
        protected LastFrame lastFrame;
        protected bool isLast = false;

        public Player() { }

        public Player(string name)
        {
            this.name = name;
            lastId++;
            id = lastId;
            previousScore = 0;
        }

        public Player(string name, int previousScore, int id)
        {
            this.name = name;
            this.previousScore = previousScore;
            if (lastId <= id)
            {
                lastId = id;
            }
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        public int GetTotalScore()
        {
            return previousScore + currentScore;
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

        public bool IsStrikeLastFrame()
        {
            return lastFrame.IsStrike();
        }

        public bool IsStrikeLastFrameSecond()
        {
            return lastFrame.IsStrikeSecond();
        }

        public bool IsSpareLastFrame()
        {
            return lastFrame.IsSpare();
        }

        public void ResetSecondThrow(AbstractThrow tThrow)
        {
            lastFrame.ResetSecondThrow(tThrow);
            if (lastFrame.IsStrikeSecond())
            {
                frames[8].ResetScore(lastFrame.GetScoreSecond());
            }
        }

        public void SetThirdThrow(AbstractThrow tThrow)
        {
            lastFrame.SetThirdThrow(tThrow);
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
                isLast = true;

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

            if (frames.Count < 10 && !isLast)
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
            currentScore = total;
            Console.WriteLine(" {0, -10}| {1, -14}| {2, -11}", total, previousScore, total + previousScore);
        }
    }
}
