using Throws;

namespace Frames
{
    public class Frame
    {
        protected FirstThrow firstThrow;
        protected AbstractThrow secondThrow;
        protected int score;

        public Frame(FirstThrow fThrow, AbstractThrow sThrow)
        {
            firstThrow = fThrow;

            if (!firstThrow.IsStrike())
            {
                secondThrow = sThrow;
                score = firstThrow.GetScore() + secondThrow.GetScore();
            }
            else
            {
                score = firstThrow.GetScore();
            }
        }

        public void SetScore(int score)
        {
            this.score = score;
        }

        public int GetScore()
        {
            return score;
        }

        public bool IsStrike()
        {
            return firstThrow.IsStrike();
        }

        public bool IsSplit()
        {
            return firstThrow.IsSplit();
        }

        public bool IsSpare()
        {
            if (!firstThrow.IsStrike() && ((firstThrow.GetScore() + secondThrow.GetScore()) == 10))
                return true;

            return false;
        }

        public void ResetScore(int add)
        {
            score += add;
        }

        public int GetScoreFirst()
        {
            return firstThrow.GetScore();
        }

        public int GetScoreSecond()
        {
            return secondThrow.GetScore();
        }
    }

    public class LastFrame : Frame
    {
        protected AbstractThrow thirdThrow;

        public LastFrame(FirstThrow fThrow, AbstractThrow sThrow) : base(fThrow, sThrow) { }

        public void SetThirdThrow(AbstractThrow tThrow)
        {
            thirdThrow = tThrow;

            score += thirdThrow.GetScore();
        }

        public void ResetSecondThrow(AbstractThrow sThrow)
        {
            secondThrow = sThrow;
            score += secondThrow.GetScore();
        }

        public bool IsSplitSecond()
        {
            return secondThrow.IsSplit();
        }

        public bool IsSplitThird()
        {
            return thirdThrow.IsSplit();
        }

        public bool IsStrikeSecond()
        {
            return secondThrow.IsStrike();
        }

        public bool IsStrikeThird()
        {
            return thirdThrow.IsStrike();
        }

        public int GetScoreThird()
        {
            return thirdThrow.GetScore();
        }

        public bool IsSpareThird()
        {
            if (!secondThrow.IsStrike() && ((secondThrow.GetScore() + thirdThrow.GetScore()) == 10))
                return true;

            return false;
        }
    }
}
