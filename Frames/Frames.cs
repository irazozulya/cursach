using Throws;

namespace Frames
{
    public class Frame // Клас основних фреймів
    {
        protected FirstThrow firstThrow; // Перший кидок
        protected AbstractThrow secondThrow; // Другий кидок
        protected int score; // Очки за фрейм

        public Frame(FirstThrow fThrow, AbstractThrow sThrow) // Конструктор
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

        public void SetScore(int score) // Встановлення результатів фрейму
        {
            this.score = score;
        }

        public int GetScore() // Повернення результатів фрейму
        {
            return score;
        }

        public bool IsStrike() // Перевірка чи є перший кидок фрейму страйком
        {
            return firstThrow.IsStrike();
        }

        public bool IsSplit() // Перевірка чи є перший кидок фрейму сплітом
        {
            return firstThrow.IsSplit();
        }

        public bool IsSpare() // Перевірка чи є фрейм спеаром
        {
            if (!firstThrow.IsStrike() && ((firstThrow.GetScore() + secondThrow.GetScore()) == 10))
                return true;

            return false;
        }

        public void ResetScore(int add) // Перезапис результатів фрейму
        {
            score += add;
        }

        public int GetScoreFirst() // Повернення результатів за перший кидок
        {
            return firstThrow.GetScore();
        }

        public int GetScoreSecond() // Повернення результатів за другий кидок
        {
            return secondThrow.GetScore();
        }
    }

    public class LastFrame : Frame // Клас для останніх фреймів
    {
        protected AbstractThrow thirdThrow; // Третій кидок

        public LastFrame(FirstThrow fThrow, AbstractThrow sThrow) : base(fThrow, sThrow) { } // Конструктор

        public void SetThirdThrow(AbstractThrow tThrow) // Запис третьго кидка
        {
            thirdThrow = tThrow;

            score += thirdThrow.GetScore();
        }

        public void ResetSecondThrow(AbstractThrow sThrow) // Перезапис другого кидка
        {
            secondThrow = sThrow;
            score += secondThrow.GetScore();
        }

        public bool IsSplitSecond() // Перевірка чи є другий кидок сплітом
        {
            return secondThrow.IsSplit();
        }

        public bool IsSplitThird() // Перевірка чи є третій кидок сплітом
        {
            return thirdThrow.IsSplit();
        }

        public bool IsStrikeSecond() // Перевірка чи є другий кидок страйком
        {
            return secondThrow.IsStrike();
        }

        public bool IsStrikeThird()  // Перевірка чи є третій кидок страйком
        {
            return thirdThrow.IsStrike();
        }

        public int GetScoreThird() // Повернення результатів за третій кидок
        {
            return thirdThrow.GetScore();
        }

        public bool IsSpareThird() // Перевірка чи є третій кидок спеаром
        {
            if (!secondThrow.IsStrike() && ((secondThrow.GetScore() + thirdThrow.GetScore()) == 10))
                return true;

            return false;
        }
    }
}
