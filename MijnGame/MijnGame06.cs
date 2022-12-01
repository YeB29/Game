using System;

namespace MijnGame06
{
    interface Tekenbaar {
        void Teken();
    }
    abstract class Plaatsbaar : Tekenbaar {
        public int X { get; set; }
        public int Y { get; set; }
        public Plaatsbaar(char symbol = ' ') {
            this.Symbol = symbol;
        }
        public void ResetPositie() {
            X = 0;
            Y = 0;
        }
        public virtual void Teken() {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }
        public virtual char Symbol { get; }
    }
    class Muntje : Plaatsbaar {
        private bool knipper;
        public override char Symbol
        {
             get {
                if (knipper)
                    return 'O';
                else
                    return ' ';
             }
        }
        public override void Teken() {
            base.Teken();
            knipper = !knipper;
        }
    }
    class Speler : Plaatsbaar {
        public string Naam { get; set; }
        public int Punten { get; set; }
        public Speler() : base('*') { }
    }
    class Vijandje : Plaatsbaar {
        public Vijandje() : base('E') { }
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
