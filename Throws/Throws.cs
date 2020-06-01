namespace Throws
{
    public abstract class AbstractThrow
    {
        protected int score = 0;
        protected bool[] pins;

        public virtual int GetScore()
        {
            return score;
        }

        public virtual bool[] GetPins()
        {
            return pins;
        }

        public abstract bool IsSplit();
        public abstract bool IsStrike();
    }

    public class FirstThrow : AbstractThrow
    {

        public FirstThrow(int[] indexes)
        {
            pins = new bool[10] { false, false, false, false, false, false, false, false, false, false };

            foreach (int i in indexes)
            {
                pins[i - 1] = true;
            }

            score = 10 - indexes.Length;
        }

        public override bool IsSplit()
        {
            if (pins[0])
            {
                return false;
            }
            else
            {
                if ((pins[5] || pins[9]) && (pins[3] || pins[6]))
                {
                    if ((pins[3] && pins[4] && pins[5]) || (pins[7] && pins[8]) || (pins[1] && pins[2]))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsStrike()
        {
            if (score == 10)
                return true;

            return false;
        }
    }

    public class SecondThrow : AbstractThrow
    {
        public SecondThrow() { }

        public SecondThrow(int[] indexes, FirstThrow fThrow)
        {
            pins = fThrow.GetPins();

            foreach (int i in indexes)
            {
                pins[i - 1] = true;
            }

            score = 10 - indexes.Length - fThrow.GetScore();
        }

        public override bool IsSplit() { return false; }
        public override bool IsStrike() { return false; }
    }
}
