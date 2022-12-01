using System;

namespace MijnGame01
{
    class Speler {
        public string Naam;
        public int Punten;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welkom bij mijn game!");
            Speler s = new Speler();
            s.Naam = Console.ReadLine();
            s.Punten = 10;
            // ...
        }
    }
}
