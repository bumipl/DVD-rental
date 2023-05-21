using System;
using System.Collections.Generic;

namespace Wypozyczalnia
{
    // Interfejs do zarządzania wypożyczeniami
    public interface IWypozyczalnia
    {
        void Wypozycz(string tytul);
        void Zwroc(string tytul);
        void WyswietlDostepne();
    }

    // Klasa bazowa reprezentująca nośnik
    public abstract class Nosnik
    {
        public string Tytul { get; set; }
        public int IloscDostepna { get; set; }

        public Nosnik(string tytul, int iloscDostepna)
        {
            Tytul = tytul;
            IloscDostepna = iloscDostepna;
        }
    }

    // Klasa reprezentująca kasetę wideo
    public class Kasetka : Nosnik
    {
        public Kasetka(string tytul, int iloscDostepna) : base(tytul, iloscDostepna)
        {
        }
    }

    // Klasa reprezentująca płytę DVD
    public class PlytaDVD : Nosnik
    {
        public PlytaDVD(string tytul, int iloscDostepna) : base(tytul, iloscDostepna)
        {
        }
    }

    // Klasa reprezentująca wypożyczalnię
    public class Wypozyczalnia : IWypozyczalnia
    {
        private List<Nosnik> magazyn;
        private Dictionary<string, DateTime> wypozyczenia;

        public Wypozyczalnia()
        {
            magazyn = new List<Nosnik>();
            wypozyczenia = new Dictionary<string, DateTime>();
        }

        public void DodajNosnik(Nosnik nosnik)
        {
            magazyn.Add(nosnik);
        }

        public void Wypozycz(string tytul)
        {
            Nosnik nosnik = ZnajdzNosnik(tytul);

            if (nosnik != null && nosnik.IloscDostepna > 0)
            {
                Console.WriteLine("Wypożyczono: " + nosnik.Tytul);
                nosnik.IloscDostepna--;

                DateTime dataZwrotu = DateTime.Now.AddDays(7);
                wypozyczenia[tytul] = dataZwrotu;
            }
            else
            {
                Console.WriteLine("Przepraszamy, nie ma dostępnych egzemplarzy tego tytułu.");
            }
        }

        public void Zwroc(string tytul)
        {
            Nosnik nosnik = ZnajdzNosnik(tytul);

            if (nosnik != null)
            {
                if (wypozyczenia.ContainsKey(tytul))
                {
                    DateTime dataZwrotu = wypozyczenia[tytul];
                    DateTime dzisiaj = DateTime.Now;

                    double opłata = 0;
                    if (dzisiaj > dataZwrotu)
                    {
                        TimeSpan czasOpóźnienia = dzisiaj - dataZwrotu;
                        int dniOpóźnienia = czasOpóźnienia.Days;
                        opłata = dniOpóźnienia * 0.5;
                    }

                    Console.WriteLine("Zwrócono: " + nosnik.Tytul);
                    Console.WriteLine("Opłata za zwrot po terminie: " + opłata + " zł");

                    nosnik.IloscDostepna++;
                    wypozyczenia.Remove(tytul);
                }
                else
                {
                    Console.WriteLine("Nie znaleziono tytułu w wypożyczalni.");
                }
            }
        }

        public void WyswietlDostepne()
        {
            Console.WriteLine("Dostępne tytuły w wypożyczalni:");
            foreach (Nosnik nosnik in magazyn)
            {
                Console.WriteLine(nosnik.Tytul + " (" + nosnik.IloscDostepna + " dostępne)");
            }
        }

        private Nosnik ZnajdzNosnik(string tytul)
        {
            foreach (Nosnik nosnik in magazyn)
            {
                if (nosnik.Tytul.Equals(tytul, StringComparison.OrdinalIgnoreCase))
                {
                    return nosnik;
                }
            }
            return null;
        }

        public bool CzyFilmJestWBazie(string tytul)
        {
            return ZnajdzNosnik(tytul) != null;
        }

        public Nosnik ZnajdzFilmPropozycje()
        {
            // Baza filmów
            List<Nosnik> bazaFilmow = new List<Nosnik>
            {
                new Kasetka("Avengers: Endgame", 3),
                new Kasetka("The Shawshank Redemption", 1),
                new PlytaDVD("Inception", 2),
                new Kasetka("Pulp Fiction", 2),
                new PlytaDVD("The Dark Knight", 3),
                new Kasetka("Fight Club", 1)
            };

            // Losowe wybranie filmu z bazy
            Random random = new Random();
            int index = random.Next(bazaFilmow.Count);
            return bazaFilmow[index];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Wypozyczalnia wypozyczalnia = new Wypozyczalnia();

            // Dodanie kilku przykładowych nośników do wypożyczalni
            wypozyczalnia.DodajNosnik(new Kasetka("Avengers: Endgame", 3));
            wypozyczalnia.DodajNosnik(new Kasetka("The Shawshank Redemption", 1));
            wypozyczalnia.DodajNosnik(new PlytaDVD("Inception", 2));
            wypozyczalnia.DodajNosnik(new Kasetka("Pulp Fiction", 2));
            wypozyczalnia.DodajNosnik(new PlytaDVD("The Dark Knight", 3));
            wypozyczalnia.DodajNosnik(new Kasetka("Fight Club", 1));

            bool zakonczony = false;

            while (!zakonczony)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Wyświetl stan biblioteki");
                Console.WriteLine("2. Wypożycz film");
                Console.WriteLine("3. Zwrot filmu");
                Console.WriteLine("4. Zakończ program");
                Console.WriteLine("Wybierz opcję:");

                int wybor = Convert.ToInt32(Console.ReadLine());

                switch (wybor)
                {
                    case 1:
                        wypozyczalnia.WyswietlDostepne();
                        break;
                    case 2:
                        Console.WriteLine("Podaj tytuł filmu, który chcesz wypożyczyć:");
                        string tytulWypozyczenia = Console.ReadLine();
                        if (wypozyczalnia.CzyFilmJestWBazie(tytulWypozyczenia))
                        {
                            wypozyczalnia.Wypozycz(tytulWypozyczenia);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono podanego tytułu w bazie. Proponowany film: ");
                            Nosnik propozycja = wypozyczalnia.ZnajdzFilmPropozycje();
                            Console.WriteLine(propozycja.Tytul);
                            wypozyczalnia.Wypozycz(propozycja.Tytul);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Podaj tytuł filmu, który chcesz zwrócić:");
                        string tytulZwrotu = Console.ReadLine();
                        wypozyczalnia.Zwroc(tytulZwrotu);
                        break;
                    case 4:
                        zakonczony = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
