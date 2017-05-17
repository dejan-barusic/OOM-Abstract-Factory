using System;
using System.Collections.Generic;

namespace OOM_AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            TrgovinaSlatkiša malaTrgovina = new TrgovinaSlatkiša(new CrvenaTvornicaSlatkiša());
            TrgovinaSlatkiša velikaTrgovina = new TrgovinaSlatkiša(new LjubičastaTvornicaSlatkiša());
            List<IČokolada> mojeČokolade = new List<IČokolada>();
            List<IKeks> mojiKeksi = new List<IKeks>();

            try { mojeČokolade.AddRange(malaTrgovina.kupiČokolade(2)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }
            try { mojeČokolade.AddRange(velikaTrgovina.kupiČokolade(1)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }

            try { mojiKeksi.AddRange(malaTrgovina.kupiKekse(3)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }
            try { mojiKeksi.AddRange(velikaTrgovina.kupiKekse(2)); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("\n" + e.ParamName + "\n"); }

            Console.WriteLine("Sve moje čokolade:");
            foreach (IČokolada čokolada in mojeČokolade)
                Console.WriteLine("\nNaziv: " + čokolada.Naziv + "\nSastojci: " + čokolada.Sastojci);
            Console.WriteLine("\n\nSvi moji keksi:");
            foreach (IKeks keks in mojiKeksi)
                Console.WriteLine("\nNaziv: " + keks.Naziv + "\nTip: " + keks.Tip);

            Console.ReadKey();
        }
    }

    // "AbstractProductA"
    interface IČokolada
    {
        string Naziv { get; }
        string Sastojci { get; }
    }

    // "AbstractProductB"
    interface IKeks
    {
        string Naziv { get; }
        string Tip { get; }
    }

    // "ConcreteProductA1"
    class LjubičastaČokolada : IČokolada
    {
        public string Naziv
        {
            get { return "Ljubičasta čokolada"; }
        }
        public string Sastojci
        {
            get { return "Kakao 20%, šećer 40%, mlijeko u prahu 40%"; }
        }
    }

    // "ConcreteProductA2"
    class CrvenaČokolada : IČokolada
    {
        public string Naziv
        {
            get { return "Crvena čokolada"; }
        }
        public string Sastojci
        {
            get { return "Kakao 15%, šećer 45%, mlijeko u prahu 40%"; }
        }
    }

    // "ConcreteProductB1"
    class LjubičastiKeks : IKeks
    {
        public string Naziv
        {
            get { return "Ljubičasti keks"; }
        }
        public string Tip
        {
            get { return "čajni"; }
        }
    }

    // "ConcreteProductB2"
    class CrveniKeks : IKeks
    {
        public string Naziv
        {
            get { return "Crveni keks"; }
        }
        public string Tip
        {
            get { return "vafel"; }
        }
    }

    // "AbstractFactory"
    interface ITvornicaSlatkiša
    {
        IČokolada napraviČokoladu();
        IKeks napraviKeks();
    }

    // "ConcreteFactory1"
    class LjubičastaTvornicaSlatkiša : ITvornicaSlatkiša
    {
        public IČokolada napraviČokoladu()
        {
            return new LjubičastaČokolada();
        }

        public IKeks napraviKeks()
        {
            return new LjubičastiKeks();
        }
    }

    // "ConcreteFactory2"
    class CrvenaTvornicaSlatkiša : ITvornicaSlatkiša
    {
        public IČokolada napraviČokoladu()
        {
            return new CrvenaČokolada();
        }

        public IKeks napraviKeks()
        {
            return new CrveniKeks();
        }
    }


    // "Client"
    class TrgovinaSlatkiša
    {
        public TrgovinaSlatkiša(ITvornicaSlatkiša tvornica)
        {
            this.tvornica = tvornica;
            željenoStanjeZalihaČokolade = 10;
            zalihaČokolade = new Stack<IČokolada>();
            naručiČokoladeIzTvornice();
            željenoStanjeZalihaKeksa = 12;
            zalihaKeksa = new Stack<IKeks>();
            naručiKekseIzTvornice();
        }

        public IEnumerable<IČokolada> kupiČokolade(int količina) {
            if (količina > zalihaČokolade.Count)
                throw new ArgumentOutOfRangeException("Trgovina nema dovoljno čokolade!");
            Stack<IČokolada> artikli = new Stack<IČokolada>();
            for (int i = 0; i < količina; ++i)
                artikli.Push(zalihaČokolade.Pop());
            naručiČokoladeIzTvornice();
            return artikli;
        }

        public IEnumerable<IKeks> kupiKekse(int količina)
        {
            if (količina > zalihaKeksa.Count)
                throw new ArgumentOutOfRangeException("Trgovina nema dovoljno keksa!");
            Stack<IKeks> artikli = new Stack<IKeks>();
            for (int i = 0; i < količina; ++i)
                artikli.Push(zalihaKeksa.Pop());
            naručiKekseIzTvornice();
            return artikli;
        }

        private void naručiČokoladeIzTvornice()
        {
            while(željenoStanjeZalihaČokolade > zalihaČokolade.Count)
                zalihaČokolade.Push(tvornica.napraviČokoladu());
        }

        private void naručiKekseIzTvornice()
        {
            while (željenoStanjeZalihaKeksa > zalihaKeksa.Count)
                zalihaKeksa.Push(tvornica.napraviKeks());
        }

        private ITvornicaSlatkiša tvornica;
        private Stack<IČokolada> zalihaČokolade;
        private Stack<IKeks> zalihaKeksa;
        private int željenoStanjeZalihaČokolade, željenoStanjeZalihaKeksa;
    }
}
