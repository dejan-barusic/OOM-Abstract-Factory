using System;
using System.Collections.Generic;

namespace OOM_AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            TrgovinaČokolade malaTrgovina = new TrgovinaČokolade(new CrvenaTvornicaČokolade());
            TrgovinaČokolade velikaTrgovina = new TrgovinaČokolade(new LjubičastaTvornicaČokolade());
            List<IČokolada> mojeČokolade = new List<IČokolada>();

            try { mojeČokolade.AddRange(malaTrgovina.kupiMliječneČokolade(15)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }
            try { mojeČokolade.AddRange(malaTrgovina.kupiČokoladeSLješnjacima(2)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }
            try { mojeČokolade.AddRange(velikaTrgovina.kupiMliječneČokolade(1)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }
            try { mojeČokolade.AddRange(velikaTrgovina.kupiČokoladeSLješnjacima(4)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }

            Console.WriteLine("Sve moje čokolade:");
            foreach (IČokolada čokolada in mojeČokolade)
                Console.WriteLine("\nNaziv: " + čokolada.Naziv + "\nSastojci: " + čokolada.Sastojci);

            Console.ReadKey();
        }
    }


    interface IČokolada
    {
        string Naziv { get; }
        string Sastojci { get; }
    }


    class LjubičastaMliječnaČokolada : IČokolada
    {
        public LjubičastaMliječnaČokolada()
        {
            naziv = "Ljubičasta mliječna čokolada";
            sastojci = "Kakao 20%, šećer 40%, mlijeko u prahu 40%";
        }
        public string Naziv
        {
            get { return naziv; }
        }
        public string Sastojci
        {
            get { return sastojci; }
        }
        private string naziv, sastojci;
    }


    class LjubičastaČokoladaSLješnjacima : IČokolada
    {
        public LjubičastaČokoladaSLješnjacima()
        {
            naziv = "Ljubičasta čokolada s lješnjacima";
            sastojci = "Kakao 20%, šećer 40%, mlijeko u prahu 40%, dodatak: lješnjaci";
        }
        public string Naziv
        {
            get { return naziv; }
        }
        public string Sastojci
        {
            get { return sastojci; }
        }
        private string naziv, sastojci;
    }


    class CrvenaMliječnaČokolada : IČokolada
    {
        public CrvenaMliječnaČokolada()
        {
            naziv = "Crvena mliječna čokolada";
            sastojci = "Kakao 15%, šećer 45%, mlijeko u prahu 40%";
        }
        public string Naziv
        {
            get { return naziv; }
        }
        public string Sastojci
        {
            get { return sastojci; }
        }
        private string naziv, sastojci;
    }


    class CrvenaČokoladaSLješnjacima : IČokolada
    {
        public CrvenaČokoladaSLješnjacima()
        {
            naziv = "Crvena čokolada s lješnjacima";
            sastojci = "Kakao 15%, šećer 45%, mlijeko u prahu 40%, dodatak: lješnjaci";
        }
        public string Naziv
        {
            get { return naziv; }
        }
        public string Sastojci
        {
            get { return sastojci; }
        }
        private string naziv, sastojci;
    }


    interface ITvornicaČokolade
    {
        IČokolada napraviMliječnuČokoladu();
        IČokolada napraviČokoladuSLješnjacima();
    }

    class LjubičastaTvornicaČokolade : ITvornicaČokolade
    {
        public IČokolada napraviMliječnuČokoladu()
        {
            return new LjubičastaMliječnaČokolada();
        }

        public IČokolada napraviČokoladuSLješnjacima()
        {
            return new LjubičastaČokoladaSLješnjacima();
        }
    }


    class CrvenaTvornicaČokolade : ITvornicaČokolade
    {
        public IČokolada napraviMliječnuČokoladu()
        {
            return new CrvenaMliječnaČokolada();
        }

        public IČokolada napraviČokoladuSLješnjacima()
        {
            return new CrvenaČokoladaSLješnjacima();
        }
    }


    class TrgovinaČokolade
    {
        public TrgovinaČokolade(ITvornicaČokolade tvornica)
        {
            this.tvornica = tvornica;
            željenoStanjeZaliha = 10;
            zalihaMliječneČokolade = new Stack<IČokolada>();
            naručiMliječneČokolade();
            zalihaČokoladeSLješnjacima = new Stack<IČokolada>();
            naručiČokoladeSLješnjacima();
        }
        public IEnumerable<IČokolada> kupiMliječneČokolade(int količina) {
            if (količina > zalihaMliječneČokolade.Count)
                throw new ArgumentOutOfRangeException("Nema dovoljno mliječne čokolade!");
            Stack<IČokolada> artikli = new Stack<IČokolada>();
            for (int i = 0; i < količina; ++i)
                artikli.Push(zalihaMliječneČokolade.Pop());
            naručiMliječneČokolade();
            return artikli;
        }
        public IEnumerable<IČokolada> kupiČokoladeSLješnjacima(int količina)
        {
            if (količina > zalihaČokoladeSLješnjacima.Count)
                throw new ArgumentOutOfRangeException("Nema dovoljno čokolade s lješnjacima!");
            Stack<IČokolada> artikli = new Stack<IČokolada>();
            for (int i = 0; i < količina; ++i)
                artikli.Push(zalihaČokoladeSLješnjacima.Pop());
            naručiČokoladeSLješnjacima();
            return artikli;
        }
        private void naručiMliječneČokolade()
        {
            while(željenoStanjeZaliha > zalihaMliječneČokolade.Count)
                zalihaMliječneČokolade.Push(tvornica.napraviMliječnuČokoladu());
        }
        private void naručiČokoladeSLješnjacima()
        {
            while (željenoStanjeZaliha > zalihaČokoladeSLješnjacima.Count)
                zalihaČokoladeSLješnjacima.Push(tvornica.napraviČokoladuSLješnjacima());
        }
        private ITvornicaČokolade tvornica;
        private Stack<IČokolada> zalihaMliječneČokolade, zalihaČokoladeSLješnjacima;
        private int željenoStanjeZaliha;
    }
}
