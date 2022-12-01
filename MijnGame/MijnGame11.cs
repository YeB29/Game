using System;
using System.Collections.Generic;

namespace MijnGame11
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
        public int Size { get; set; } = 8;
        public void Teken()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("----------");
            for (int i = 0; i < Size; i++)
                Console.WriteLine("|        |");
            Console.WriteLine("----------");
        }
    }
    class Level : Tekenbaar
    {
        private Veld veld = new Veld();
        public List<Vijandje> Vijandjes { get; set; }
        public string Naam { get; set; }
        public int? Moeilijkheid { get; set; }
        public void Teken()
        {
            veld.Teken();
            Console.SetCursorPosition(0, veld.Size);
            Console.WriteLine(Naam ?? "Naamloos level");
            
            // OPTIE 1:
            Console.Write(Vijandjes?.Count ?? 0);
            // OPTIE 2:
            if (Vijandjes == null)
                Console.Write(0);
            else
                Console.Write(Vijandjes.Count);
            
            Console.WriteLine(" Vijandjes");
            if (Moeilijkheid != null)
                Console.WriteLine("Moeilijkheidsgraad: " + Moeilijkheid);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welkom bij mijn game!");
            var s = new Speler() { Punten = 10 };
            s.Naam = Console.ReadLine();
            if (s.Naam == "admin") {
                // doe admin dingen ...
            }
            s.Positie.X = 4;
            s.Positie.Y = 1;
            var level = new Level() { Vijandjes = new List<Vijandje>() { 
                new Vijandje() { Positie = new Coordinaat(1, 3) }, 
                new Vijandje() { Positie = new Coordinaat(2, 2) } 
            }};
            level.Teken();
            s.Teken();
            Console.ReadLine();
        }
    }
}
