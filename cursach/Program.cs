using System;
using BowlingLibrary;
using Throws;
using Frames;

namespace cursach
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] pinsF1 = new int[] {};
            Array.Sort(pinsF1);
            int[] pinsS1 = pinsF1;
            int[] pinsF2 = new int[] { 1, 3, 5};
            Array.Sort(pinsF2);
            int[] pinsS2 = new int[] { 1};
            Array.Sort(pinsS2);
            int[] pinsF3 = new int[] { 7, 10 };
            Array.Sort(pinsF3);
            int[] pinsS3 = new int[] {};
            Array.Sort(pinsS3);
            int[] pinsF4 = new int[] {3, 4, 6, 7, 10, 1, 2, 5, 8, 9 };
            Array.Sort(pinsF4);
            int[] pinsS4 = new int[] { 3};
            Array.Sort(pinsS3);
            int[] pinsF5 = new int[] { 3 };
            Array.Sort(pinsF5);
            int[] pinsS5 = new int[] { 3 };
            Array.Sort(pinsS5);
            int[] pinsF6 = new int[] { 1, 3, 5, 2};
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
            Player pl = new Player("Ira", 12);
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
            Game gm = new Game(1);
            gm.AddPlayer(pl);
            gm.ShowTable();
        }
    }
}
