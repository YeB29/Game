using System;

namespace MijnGame09
{
    interface Tekenbaar {
        void Teken();
    }
    struct Coordinaat {
        public int X { get; set; }
        public int Y { get; set; }
        public Coordinaat(int X, int Y) {
            this.X = X;
            this.Y = Y;
        }
        public static Coordinaat operator +(Coordinaat c1, Coordinaat c2) {
            return new Coordinaat(c1.X + c2.X, c1.Y + c2.Y);
        }
    }
    abstract class Plaatsbaar : Tekenbaar {
        public Coordinaat Positie;
        public Plaatsbaar(char symbol = ' ') {
            this.Symbol = symbol;
        }
        public void ResetPositie() {
            Positie = new Coordinaat(0, 0);
        }
        public virtual void Teken() {
            Console.SetCursorPosition(Positie.X, Positie.Y);
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
        public static bool operator >(Speler sp1, Speler sp2) {
            return sp1.Punten > sp2.Punten;
        }
        public static bool operator <(Speler sp1, Speler sp2) {
            return sp1.Punten < sp2.Punten;
        }
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
            var s = new Speler();
            s.Naam = Console.ReadLine();
            if (s.Naam == "admin") {
                // doe admin dingen ...
            }
            s.Punten = 10;
            s.Positie.X = 4;
            s.Positie.Y = 1;
            var veld = new Veld();
            veld.Teken();
            s.Teken();
            Console.ReadLine();
        }
    }
}
