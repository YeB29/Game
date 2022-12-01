using System;
using System.Collections.Generic;

namespace MijnGame14
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
        private static Random random = new Random();
        public static Coordinaat Willekeurig(int size) {
            return new Coordinaat(random.Next(size), random.Next(size));
        }
    }
    class NegatiefTekenenException : Exception { }
    static class Tekener {
        public static void SchrijfOp(Coordinaat Positie, string Text) {
            if (Positie.X < 0 || Positie.Y < 0)
                throw new NegatiefTekenenException();
            Console.SetCursorPosition(Positie.X, Positie.Y);
            Console.WriteLine(Text);
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
            Tekener.SchrijfOp(Positie, "" + Symbol);
        }
        public virtual char Symbol { get; }
    }
    class Muntje : Plaatsbaar {
        private bool knipper;
        public override char Symbol
        {
             get {
                return 'O';
             }
        }
        public override void Teken() {
            base.Teken();
            knipper = !knipper;
        }
    }
    class Snake : Tekenbaar {
        public string Naam { get; set; }
        public List<Coordinaat> staart = new List<Coordinaat>() { new Coordinaat(5,5) };
        // laatste element in de staart = uiteinde van de staart
        // eerste element in de staart = waar je bent
        public int Richting = 0; // 0 = naar rechts, 1 = naar links; 2 = naar boven; 3 = naar beneden;
        public void Toets(char keyChar)
        {
            switch (keyChar) {
                case 'd': Richting = 0; break;
                case 'w': Richting = 2; break;
                case 's': Richting = 3; break;
                case 'a': Richting = 1; break;
            }
        }

        private bool muntjegepakt = false;

        public void Beweeg()
        {
            if (Richting == 0) staart.Insert(0, staart[0] + new Coordinaat(1, 0));
            if (Richting == 1) staart.Insert(0, staart[0] + new Coordinaat(-1, 0));
            if (Richting == 2) staart.Insert(0, staart[0] + new Coordinaat(0, -1));
            if (Richting == 3) staart.Insert(0, staart[0] + new Coordinaat(0, 1));
            if (!muntjegepakt) {
                staart.RemoveAt(staart.Count - 1);
            }
            muntjegepakt = false;
        }

        public void Langer()
        {
            muntjegepakt = true;
        }

        public void Teken()
        {
            foreach (var s in staart)
            {
                Tekener.SchrijfOp(s, ".");
            }
        }
    }
    class Veld : Tekenbaar
    {
        public int Size { get; set; } = 15;
        public void Teken()
        {
            Tekener.SchrijfOp(new Coordinaat(0, 0), new string('-', Size + 2));
            for (int i = 1; i < Size + 1; i++)
                Tekener.SchrijfOp(new Coordinaat(0, i), "|" + new string(' ', Size) + "|");
            Tekener.SchrijfOp(new Coordinaat(0, Size + 1), new string('-', Size + 2));
        }
    }
    class Level : Tekenbaar
    {
        public Snake Snake = new Snake();
        public Veld Veld = new Veld();
        public string Naam { get; set; }
        private Muntje muntje;
        public Muntje Muntje { get {
            return muntje;
        } set {
            muntje = value;
            Console.Beep();
        }}
        public void Teken()
        {
            Veld.Teken();
            Snake.Teken();
            Muntje.Teken();
            Tekener.SchrijfOp(new Coordinaat(0, Veld.Size + 3), "Lengte van de staart: " + Snake.staart.Count);
        }
        public void NieuwMuntje()
        {
            Muntje = new Muntje() { Positie = Coordinaat.Willekeurig(Veld.Size) }; 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var level = new Level();
            level.NieuwMuntje();
            level.Teken();
            var key = Console.ReadKey();
            while (key.KeyChar != 'q') {
                level.Snake.Toets(key.KeyChar);
                while (!Console.KeyAvailable) {
                    level.Teken();
                    level.Snake.Beweeg();
                    if (level.Snake.staart[0].Equals(level.Muntje.Positie)) {
                        level.NieuwMuntje();
                        level.Snake.Langer();
                    }
                    System.Threading.Thread.Sleep(1000 - level.Snake.staart.Count * 100);
                }
                key = Console.ReadKey();
            }
        }
    }
}
