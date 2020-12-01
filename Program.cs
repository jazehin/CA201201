using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA201201 //London2012.txt, london.pdf
{
    struct Sportag
    {
        public string Nev;
        public Dictionary<DateTime, int> Dontok;

        public Sportag(string sor)
        {
            var t = sor.Split(';');

            Nev = t[0];
            Dontok = new Dictionary<DateTime, int>();

            var datum = new DateTime(2012, 07, 28);

            for (int i = 1; i < t.Length; i++)
            {
                Dontok.Add(datum, int.Parse(t[i]));
                datum = datum.AddDays(1);
            }
        }
    }

    struct Sportag2
    {
        public string Nev;
        public List<int> Dontok;

        public Sportag2(string sor)
        {
            var t = sor.Split(';');

            Nev = t[0];
            Dontok = new List<int>();

            for (int i = 1; i < t.Length; i++)
            {
                Dontok.Add(int.Parse(t[i]));
            }
        }
    }
    class Program
    {
        static List<Sportag> sportagak = new List<Sportag>();
        static List<Sportag2> sportagak2 = new List<Sportag2>();
        static void Main()
        {
            F1(false);
            F2(false);
            F3(false);
            F4(false);
            F5(false);
            F6(false);
            Console.ReadKey(true);
        }

        #region sajat_megoldas
        private static void F6(bool v)
        {
            Console.WriteLine("6. feladat:");

            var start = new DateTime(2012, 07, 28);
            var datum = new DateTime(2012, 07, 29);

            int index = datum.Subtract(start).Days;

            int sum = 0;
            for (int i = 0; i < sportagak2.Count; i++)
            {
                sum += sportagak2[i].Dontok[index];
            }

            Console.WriteLine($"\t{datum.Day}.-án/én {sum}db döntő volt.");
        }
        private static void F5(bool v)
        {
            Console.WriteLine("5. feladat:");

            int sum = 0;
            foreach (var s in sportagak2)
            {
                foreach (var n in s.Dontok)
                {
                    sum += n;
                }
            }

            Console.WriteLine($"\t{sum}db aranyérmet osztottak ki az olimpián.");
        }
        private static void F4(bool v)
        {
            Console.WriteLine("4. feladat:");

            Dictionary<DateTime, int> dontokSzama = new Dictionary<DateTime, int>();

            DateTime start = new DateTime(2012, 07, 28);

            foreach (var s in sportagak2)
            {
                for (int i = 0; i < s.Dontok.Count; i++)
                {
                    var datum = start.AddDays(i);
                    if (!dontokSzama.ContainsKey(datum)) dontokSzama.Add(datum, s.Dontok[i]);
                    else dontokSzama[datum] += s.Dontok[i];
                }
            }

            DateTime maxi = dontokSzama.First().Key;
            foreach (var d in dontokSzama)
            {
                if (dontokSzama[maxi] < dontokSzama[d.Key]) maxi = d.Key;
            }

            Console.WriteLine($"\tA legtöbb döntő ({dontokSzama[maxi]}db) {maxi.Day}.-án/én volt.");
            //nekem ez így egy kicsit könnyebb volt :)
        }
        private static void F3(bool v)
        {
            Console.WriteLine("3. feladat:");
            int i = 0;
            while (sportagak2[i].Nev != "Úszás") i++;

            int sum = 0;
            foreach (var n in sportagak2[i].Dontok)
            {
                sum += n;
            }
            Console.WriteLine($"\tAranyérmek száma úszásban: {sum}db");
        }
        private static void F2(bool v)
        {
            Console.WriteLine("2. feladat:");
            int i = 0;
            while (sportagak2[i].Nev != "Atlétika") i++;

            int c = 0;
            foreach (var n in sportagak2[i].Dontok)
            {
                if (n > 0) c++;
            }
            Console.WriteLine($"\tDöntős napok száma atlétika sportágban: {c}db");
        }
        private static void F1(bool v)
        {
            var sr = new StreamReader(@"..\..\Res\London2012.txt");

            while (!sr.EndOfStream)
            {
                sportagak2.Add(new Sportag2(sr.ReadLine()));
            }

            sr.Close();
        }
        #endregion

        #region orai_megoldas
        private static void F6()
        {
            Console.WriteLine("6. feladat:");
            var datum = new DateTime(2012, 07, 29);

            int sum = 0;

            foreach (var s in sportagak)
            {
                sum += s.Dontok[datum];
            }

            Console.WriteLine($"\t{datum.Day}.-án/én {sum}db döntő volt.");
        }
        private static void F5()
        {
            Console.WriteLine("5. feladat:");

            int sum = 0;
            foreach (var s in sportagak)
            {
                foreach (var n in s.Dontok.Values)
                {
                    sum += n;
                }
            }

            Console.WriteLine($"\t{sum}db aranyérmet osztottak ki az olimpián.");
        }
        private static void F4()
        {
            Console.WriteLine("4. feladat:");

            var dpn = new Dictionary<DateTime, int>();

            for (int i = 1; i < sportagak.Count; i++)
            {
                foreach (var kvp in sportagak[i].Dontok)
                {
                    if (!dpn.Keys.Contains(kvp.Key)) dpn.Add(kvp.Key, kvp.Value);
                    else dpn[kvp.Key] += kvp.Value;
                }
            }

            var maxKvp = dpn.First();

            foreach (var kvp in dpn)
            {
                if (kvp.Value > maxKvp.Value) maxKvp = kvp;
            }

            Console.WriteLine($"\tA legtöbb döntő ({maxKvp.Value}db) {maxKvp.Key.Day}.-án/én volt.");
        }
        private static void F3()
        {
            Console.WriteLine("3. feladat:");
            int i = 0;
            while (sportagak[i].Nev != "Úszás") i++;

            int sum = 0;
            foreach (var n in sportagak[i].Dontok.Values)
            {
                sum += n;
            }
            Console.WriteLine($"\tAranyérmek száma úszásban: {sum}db");
        }
        private static void F2()
        {
            Console.WriteLine("2. feladat:");
            int i = 0;
            while (sportagak[i].Nev != "Atlétika") i++;

            int c = 0;
            foreach (var n in sportagak[i].Dontok.Values)
            {
                if (n > 0) c++;
            }
            Console.WriteLine($"\tDöntős napok száma atlétika sportágban: {c}db");
        }
        private static void F1()
        {
            var sr = new StreamReader(@"..\..\Res\London2012.txt");

            while (!sr.EndOfStream)
            {
                sportagak.Add(new Sportag(sr.ReadLine()));
            }

            sr.Close();
        }
        #endregion
    }
}
