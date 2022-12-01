using System;

namespace MijnGame05
{
    interface Tekenbaar {
        void Teken();
    }
    abstract class Plaatsbaar : Tekenbaar {
        public int X;
        public int Y;
        public virtual void Teken() {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol());
        }
        public abstract char Symbol();
    }
    class Muntje : Plaatsbaar {
        private bool knipper;
        public override char Symbol()
        {
            return 'O';
        }
        public override void Teken() {
            if (knipper)
                base.Teken();
            knipper = !knipper;
        }
    }
    class Speler : Plaatsbaar {
        public string Naam;
        public int Punten;
        public override char Symbol()
        {
            return '*';
        }
    }
    class Vijandje : Plaatsbaar {
        public override char Symbol()
        {
            return 'E';
        }
    }
    class Veld : Tekenbaar
    {
        public void Teken()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("----------");
            for (int i = 0; i < 8; i++)
                Console.WriteLine("|        |");
            Console.WriteLine("----------");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welkom bij mijn game!");
            Speler s = new Speler();
            s.Naam = Console.ReadLine();
            s.Punten = 10;
            s.X = 4;
            s.Y = 1;
            Veld veld = new Veld();
            veld.Teken();
            s.Teken();
            Console.ReadLine();
        }
    }
}
