using System;
using System.Collections.Generic;

namespace WypozyczalniaKasetIVideo
{
    class Program
    {
        static void Main(string[] args)
        {
            Wypozyczalnia wypozyczalnia = new Wypozyczalnia();
            wypozyczalnia.DodajKasete(new Kaseta("Harry Potter i Kamień Filozoficzny", "Chris Columbus", 2001, 2));
            wypozyczalnia.DodajKasete(new Kaseta("Król Lew", "Rob Minkoff", 1994, 1));
            wypozyczalnia.DodajDVD(new DVD("Gra o Tron", "David Benioff, D. B. Weiss", 2011, 1));
            wypozyczalnia.DodajDVD(new DVD("Breaking Bad", "Vince Gilligan", 2008, 3));
            wypozyczalnia.WypozyczKasete(1);
            wypozyczalnia.WypozyczDVD(2);
            wypozyczalnia.ZwrocKasete(1);
            wypozyczalnia.ZwrocDVD(2);
        }
    }

    interface IWypozyczalny
    {
        bool CzyWypozyczony();
        void Wypozycz();
        void Zwroc();
    }

    class Kaseta : IWypozyczalny
    {
        private string tytul;
        private string rezyser;
        private int rokProdukcji;
        private int iloscEgzemplarzy;
        private int iloscDostepnychEgzemplarzy;

        public Kaseta(string tytul, string rezyser, int rokProdukcji, int iloscEgzemplarzy)
        {
            this.tytul = tytul;
            this.rezyser = rezyser;
            this.rokProdukcji = rokProdukcji;
            this.iloscEgzemplarzy = iloscEgzemplarzy;
            this.iloscDostepnychEgzemplarzy = iloscEgzemplarzy;
        }

        public bool CzyWypozyczony()
        {
            return iloscDostepnychEgzemplarzy < iloscEgzemplarzy;
        }

        public void Wypozycz()
        {
            if (iloscDostepnychEgzemplarzy > 0)
            {
                iloscDostepnychEgzemplarzy--;
                Console.WriteLine($"Wypożyczono kasetę \"{tytul}\".");
            }
            else
            {
                Console.WriteLine($"Brak dostępnych egzemplarzy kasetki \"{tytul}\".");
            }
        }

        public void Zwroc()
        {
            if (iloscDostepnychEgzemplarzy < iloscEgzemplarzy)
            {
                iloscDostepnychEgzemplarzy++;
                Console.WriteLine($"Zwrócono kasetę \"{tytul}\".");
            }
            else
            {
                Console.WriteLine($"Wszystkie egzemplarze kasetki \"{tytul}\" są dostępne.");

            }

    public string Tytul { get => tytul; }
        public string Rezyser { get => rezyser; }
        public int RokProdukcji { get => rokProdukcji; }
        public int IloscEgzemplarzy { get => iloscEgzemplarzy; }
        public int IloscDostepnychEgzemplarzy { get => iloscDostepnychEgzemplarzy; }
    }

    class DVD : IWypozyczalny
    {
        private string tytul;
        private string tworcy;
        private int rokProdukcji;
        private int iloscEgzemplarzy;
        private int iloscDostepnychEgzemplarzy;

        public DVD(string tytul, string tworcy, int rokProdukcji, int iloscEgzemplarzy)
        {
            this.tytul = tytul;
            this.tworcy = tworcy;
            this.rokProdukcji = rokProdukcji;
            this.iloscEgzemplarzy = iloscEgzemplarzy;
            this.iloscDostepnychEgzemplarzy = iloscEgzemplarzy;
        }

        public bool CzyWypozyczony()
        {
            return iloscDostepnychEgzemplarzy < iloscEgzemplarzy;
        }

        public void Wypozycz()
        {
            if (iloscDostepnychEgzemplarzy > 0)
            {
                iloscDostepnychEgzemplarzy--;
                Console.WriteLine($"Wypożyczono DVD \"{tytul}\".");
            }
            else
            {
                Console.WriteLine($"Brak dostępnych egzemplarzy DVD \"{tytul}\".");
            }
        }

        public void Zwroc()
        {
            if (iloscDostepnychEgzemplarzy < iloscEgzemplarzy)
            {
                iloscDostepnychEgzemplarzy++;
                Console.WriteLine($"Zwrócono DVD \"{tytul}\".");
            }
            else
            {
                Console.WriteLine($"Wszystkie egzemplarze DVD \"{tytul}\" są dostępne.");
            }
        }

        public string Tytul { get => tytul; }
        public string Tworcy { get => tworcy; }
        public int RokProdukcji { get => rokProdukcji; }
        public int IloscEgzemplarzy { get => iloscEgzemplarzy; }
        public int IloscDostepnychEgzemplarzy { get => iloscDostepnychEgzemplarzy; }
    }

    class Wypozyczalnia
    {
        private List<IWypozyczalny> kolekcja = new List<IWypozyczalny>();

        public void DodajKasete(Kaseta kaseta)
        {
            kolekcja.Add(kaseta);
            Console.WriteLine($"Dodano kasety \"{kaseta.Tytul}\" do kolekcji.");
        }

        public void DodajDVD(DVD dvd)
        {
            kolekcja.Add(dvd);
            Console.WriteLine($"Dodano DVD \"{dvd.Tytul}\" do kolekcji.");
        }

        public void WypozyczKasete(int index)
        {
            if (index >= 0 && index < kolekcja.Count)
            {
                if (kolekcja[index] is Kaseta)

            }
            {
                kolekcja[index].Wypozycz();
            }
            else
            {
                Console.WriteLine("Nie można wypożyczyć DVD. Wybrany element nie jest kasetą.");
            }


            else
            {
                Console.WriteLine($"Nie można wypożyczyć kasety. Niepoprawny indeks elementu. (0 - {kolekcja.Count - 1})");
            }

            public void WypiszStan()
            {
                Console.WriteLine("\nStan wypożyczalni:");
                foreach (IWypozyczalny element in kolekcja)
                {
                    Console.WriteLine($"Tytuł: {element.Tytul}, Rok produkcji: {element.RokProdukcji}, Ilość egzemplarzy: {element.IloscEgzemplarzy}, Ilość dostępnych egzemplarzy: {element.IloscDostepnychEgzemplarzy}");
                }
            }
        }

        private void ZwrocKasete(int index)
        {
            if (index >= 0 && index < kolekcja.Count)
            {
                if (kolekcja[index] is Kaseta)
                {
                    kolekcja[index].Zwroc();
                }
                else
                {
                    Console.WriteLine("Nie można zwrócić kasety. Wybrany element nie jest kasetą.");
                }
            }
            else
            {
                Console.WriteLine($"Nie można zwrócić kasety. Niepoprawny indeks elementu. (0 - {kolekcja.Count - 1})");
            }
        }

        private void WypozyczDVD(int index)
        {
            if (index >= 0 && index < kolekcja.Count)
            {
                if (kolekcja[index] is DVD)
                {
                    kolekcja[index].Wypozycz();
                }
                else
                {
                    Console.WriteLine("Nie można wypożyczyć kasety. Wybrany element nie jest DVD.");
                }
            }
            else
            {
                Console.WriteLine($"Nie można wypożyczyć DVD. Niepoprawny indeks elementu. (0 - {kolekcja.Count - 1})");
            }
        }

        private void ZwrocDVD(int index)
        {
            if (index >= 0 && index < kolekcja.Count)
            {
                if (kolekcja[index] is DVD)
                {
                    kolekcja[index].Zwroc();
                }
                else
                {
                    Console.WriteLine("Nie można zwrócić DVD. Wybrany element nie jest DVD.");
                }
            }
            else
            {
                Console.WriteLine($"Nie można zwrócić DVD. Niepoprawny indeks elementu. (0 - {kolekcja.Count - 1})");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Wypozyczalnia wypozyczalnia = new Wypozyczalnia();

                Kaseta kaseta1 = new Kaseta("Titanic", "James Cameron", 1997, 2);
                Kaseta kaseta2 = new Kaseta("Rocky", "John G. Avildsen", 1976, 3);
                DVD dvd1 = new DVD("Incepcja", "Christopher Nolan", 2010, 1);
                DVD dvd2 = new DVD("Matrix", "Lana Wachowski, Lilly Wachowski", 1999, 4);

                wypozyczalnia.Dodaj(kaseta1);
                wypozyczalnia.Dodaj(kaseta2);
                wypozyczalnia.Dodaj(dvd1);
                wypozyczalnia.Dodaj(dvd2);
                wypozyczalnia.WypiszStan();

                Console.WriteLine("\nWypożyczanie kaset i DVD:");
                wypozyczalnia.WypozyczKasete(0);
                wypozyczalnia.WypozyczKasete(1);
                wypozyczalnia.WypozyczDVD(2);
                wypozyczalnia.WypozyczDVD(3);

                wypozyczalnia.WypiszStan();

                Console.WriteLine("\nZwracanie kaset i DVD:");
                wypozyczalnia.ZwrocKasete(0);
                wypozyczalnia.ZwrocDVD(2);

                wypozyczalnia.WypiszStan();

                Console.ReadKey();
            }
        }
    }
}


/* Wynik działania programu:
Stan wypożyczalni:
Tytuł: Titanic, Rok produkcji: 1997, Ilość egzemplarzy: 2, Ilość dostępnych egzemplarzy: 2
Tytuł: Rocky, Rok produkcji: 1976, Ilość egzemplarzy: 3, Ilość dostępnych egzemplarzy: 3
Tytuł: Incepcja, Rok produkcji: 2010, Ilość egzemplarzy: 1, Ilość dostępnych egzemplarzy: 1
Tytuł: Matrix, Rok produkcji: 1999, Ilość egzemplarzy: 4, Ilość dostępnych egzemplarzy: 4

Wypożyczanie kaset i DVD:
Kaseta Titanic została wypożyczona.
Kaseta Rocky została wypożyczona.
DVD Incepcja zostało wypożyczone.
Nie można wypożyczyć DVD. Wybrany element nie jest DVD.

Stan wypożyczalni:
Tytuł: Titanic, Rok produkcji: 1997, Ilość egzemplarzy: 2, Ilość dostępnych egzemplarzy: 1
Tytuł: Rocky, Rok produkcji: 1976, Ilość egzemplarzy: 3, Ilość dostępnych egzemplarzy: 2
Tytuł: Incepcja, Rok produkcji: 2010, Ilość egzemplarzy: 1, Ilość dostępnych egzemplarzy: 0
Tytuł: Matrix, Rok produkcji: 1999, Ilość egzemplarzy: 4, Ilość dostępnych egzemplarzy: 4

Zwracanie kaset i DVD:
Kaseta Titanic została zwrócona.
DVD Incepcja zostało zwrócone.

Stan wypożyczalni:
Tytuł: Titanic, Rok produkcji: 1997, Ilość egzemplarzy: 2, Ilość dostępnych egzemplarzy: 2
Tytuł: Rocky, Rok produkcji: 1976, Ilość egzemplarzy: 3, Ilość dostępnych egzemplarzy: 3
Tytuł: Incepcja, Rok produkcji: 2010, Ilość egzemplarzy: 1, Ilość dostępnych egzemplarzy: 1
Tytuł: Matrix, Rok produkcji: 1999, Ilość egzemplarzy: 4, Ilość dostępnych egzemplarzy: 4
*/