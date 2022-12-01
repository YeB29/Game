using System;

namespace MijnGame03
{
    abstract class Poppetje {
        public int X;
        public int Y;
        public void Teken() {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol());
        }
        public abstract char Symbol();
    }
    class Speler : Poppetje {
        public string Naam;
        public int Punten;
        public override char Symbol()
        {
            return '*';
        }
    }
    class Vijandje : Poppetje {
        public override char Symbol()
        {
            return 'E';
        }
    }
    class Program
    {
        public static void TekenVeld()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("----------");
            for (int i = 0; i < 8; i++)
                Console.WriteLine("|        |");
            Console.WriteLine("----------");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welkom bij mijn game!");
            Speler s = new Speler();
            s.Naam = Console.ReadLine();
            s.Punten = 10;
            s.X = 4;
            s.Y = 1;
            TekenVeld();
            s.Teken();
            Console.ReadLine();
        }
    }
}
