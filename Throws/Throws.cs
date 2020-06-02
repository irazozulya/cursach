namespace Throws
{
    public abstract class AbstractThrow // Абстрактний клас кидка
    {
        protected int score = 0; // Очки за кидок
        protected bool[] pins = new bool[10] { false, false, false, false, false, false, false, false, false, false }; // Масив кегель, що залишилися після кидка

        public virtual int GetScore() // Повернення результатів кидка
        {
            return score;
        }

        public virtual bool[] GetPins() // Повернення масиву кегель, що залишилися після кидка
        {
            return pins;
        }

        public abstract bool IsSplit(); // Перевірка чи є кидок сплітом
        public abstract bool IsStrike(); // Перевірка чи є кидок страйком
    }

    public class FirstThrow : AbstractThrow // Клас першого кидка
    {
        public FirstThrow(int[] indexes) // Конструктор
        {
            int count = 0;

            foreach (int i in indexes)
            {
                if (i > 0 && i < 11)
                {
                    if (pins[i - 1] == false)
                    {
                        pins[i - 1] = true;
                        count++;
                    }
                }
            }

            score = 10 - count;
        }

        public override bool IsSplit() // Перевірка чи є кидок сплітом
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

        public override bool IsStrike() // Перевірка чи є кидок страйком
        {
            if (score == 10)
                return true;

            return false;
        }
    }

    public class SecondThrow : AbstractThrow // Клас другого кидка
    {
        public SecondThrow(int[] indexes, FirstThrow fThrow) // Конструктор
        {
            bool [] fPins = fThrow.GetPins();

            int count = 0;

            foreach (int i in indexes)
            {
                if (i > 0 && i < 11)
                {
                    if (fPins[i - 1] == true)
                    {
                        pins[i - 1] = true;
                        count++;
                    }
                }
            }

            score = 10 - count - fThrow.GetScore();
        }

        public override bool IsSplit() { return false; } // Перевірка чи є кидок сплітом
        public override bool IsStrike() { return false; } // Перевірка чи є кидок страйком
    }
}
