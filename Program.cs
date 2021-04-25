using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Ingrese el numero de jugadores:");
            int nJugadores = int.Parse(Console.ReadLine());
            Juego juego = new Juego(nJugadores);
            juego.Iniciar();
        }

    }
    public class MazoIngles
    {
        public List<Carta> cartas { get; set; }
        public int nCartas { get { return cartas.Count(); } }

        public MazoIngles()
        {
            cartas = new List<Carta>();
        }
        /// <summary>
        /// inicializa un mazo de cartas ingles
        /// </summary>
        /// <param name="doble">Indica si se requiere un mazo doble (rojo/azul)</param>
        /// <param name="usarJokers">Indica si se utilizaran jokes</param>
        public MazoIngles(bool doble, bool usarJokers)
        {
            crearMazoSimple(usarJokers, enumColorMazo.azul);
            //si se necesitan dos mazos
            if (doble)
            {
                crearMazoSimple(usarJokers, enumColorMazo.rojo);
            }


        }
        public void barajar()
        {

            Random rm = new Random();
            cartas = cartas.OrderBy(a => rm.NextDouble()).ToList();
        }
        public Mano repartirMano(int nCartasMano)
        {
            var manoJugador = new Mano() { cartas = this.cartas.Take(nCartasMano).ToList() };
            this.cartas.RemoveRange(0, nCartasMano);
            return manoJugador;
        }

        private void crearMazoSimple(bool usarJokers, enumColorMazo colorMazo)
        {
            cartas = cartas ?? new List<Carta>();
            foreach (string pinta in Enum.GetNames(typeof(enumPinta)).Where(a => a != "Joker"))
            {
                foreach (var nCarta in Enumerable.Range(1, 13))
                {

                    cartas.Add(new Carta((enumPinta)Enum.Parse(typeof(enumPinta), pinta), nCarta, colorMazo));
                }
            }
            if (usarJokers)
            {
                cartas.AddRange(new List<Carta>{
                        new Carta(enumPinta.Joker,enumColorCarta.color,colorMazo),
                        new Carta(enumPinta.Joker,enumColorCarta.negro,colorMazo)
                    });
            }
        }
    }
    public class Mano
    {

        public List<Carta> cartas { get; set; }
        public int puntajeMano
        {
            get { return cartas.Sum(a => a.valor); }
        }

    }

    public class Carta
    {
        public class PintaCarta
        {
            public enumPinta pinta { get; }
            public enumColorCarta colorCarta { get; }
            public PintaCarta(enumPinta pinta, enumColorCarta colorCarta)
            {
                this.pinta = pinta;
                this.colorCarta = colorCarta;

            }
        }

        public enumColorMazo colorMazo { get; }
        public int numero { get; }
        public int valor { get; }
        public string nombre { get; }
        public PintaCarta pinta { get; }
        public Carta(enumPinta pinta, enumColorCarta color, enumColorMazo colorMazo)
        {
            if (pinta != enumPinta.Joker)
                throw new Exception("Este metodo solo permite crear Jockers");
            this.colorMazo = colorMazo;
            this.numero = 0;
            this.pinta = new PintaCarta(pinta, color);
            this.valor = 30;
            this.nombre = "Joker";
        }

        public Carta(enumPinta pinta, int numero, enumColorMazo colorMazo)
        {
            if (pinta == enumPinta.Joker)
                throw new Exception("Jockers deben ser creados indicando su color");
            this.colorMazo = colorMazo;
            this.numero = numero;
            this.pinta = new PintaCarta(pinta, pinta == enumPinta.Corazon || pinta == enumPinta.Diamante ? enumColorCarta.color : enumColorCarta.negro);

            switch (pinta)
            {
                case enumPinta.Joker:
                    valor = 30;
                    nombre = "Joker";
                    break;
                default:
                    switch (numero)
                    {
                        case 11:
                            nombre = "J";
                            valor = 10;
                            break;
                        case 12:
                            nombre = "Q";
                            valor = 10;
                            break;
                        case 13:
                            nombre = "K";
                            valor = 10;
                            break;
                        case 1:
                            nombre = "A";
                            valor = 20;
                            break;
                        default:
                            nombre = numero.ToString();
                            valor = numero;
                            break;
                    }

                    break;
            }

        }
    }
    public enum enumPinta
    {
        Picas,
        Trebol,
        Diamante,
        Corazon,
        Joker
    }
    public enum enumColorCarta
    {
        negro,
        color
    }
    public enum enumColorMazo
    {
        azul,
        rojo
    }
    public class Jugador
    {

        public string nombre { get; set; }
        public Mano mano { get; set; }
        public int nJugador { get; }
        public Jugador(Mano mano, int nJugador)
        {
            Console.WriteLine($"Ingrese nombre del jugador {nJugador + 1}");

            this.nombre = Console.ReadLine();
            this.nJugador = nJugador;
            this.mano = mano;
        }
    }
    public class Juego
    {

        public MazoIngles mazo { get; set; }
        public MazoIngles pila { get; set; }
        public List<Jugador> jugadores { get; set; }

        public Juego(int nJugadores)
        {
            int nCartasMano = 12;
            //Instancio el mazo

            mazo = new MazoIngles(true, true);
            mazo.barajar();
            jugadores = new List<Jugador>();
            for (int i = 0; i < nJugadores; i++)
            {
                Console.WriteLine();

                Mano manoJugador = mazo.repartirMano(nCartasMano);
                Jugador jugador = new Jugador(manoJugador, i);
                jugadores.Add(jugador);

            }
            Console.Write($"\nEn la mesa: \n{mazo.nCartas} cartas");




            Console.ReadKey();


        }
        public void Iniciar()
        {
            pila = new MazoIngles();
            pila.cartas.Add(tomarCartaMazo());
            bool enJuego = true;
            while (enJuego)
            {
                foreach (Jugador jugador in jugadores.OrderBy(a => a.nJugador))
                {
                    Console.Clear();
                    MostrarMano(jugador);
                    Console.WriteLine($"En la Mesa:\n\t\tMazo:{mazo.nCartas} \tPila:{pila.cartas.Last().nombre} {pila.cartas.Last().pinta.pinta} ({pila.nCartas} cartas)");
                    
                    Console.WriteLine($"Que hacer?\n1)\tTomar carta de mazo\n2)\tTomar carta de pila");
                    Console.WriteLine("Seleccione una opcion (1,2)");

                    var opcion = int.Parse(Console.ReadLine());
                    switch (opcion)
                    {
                        case 1:
                            jugador.mano.cartas.Add(tomarCartaMazo());
                            break;
                        case 2:
                            jugador.mano.cartas.Add(tomarCartaPila());
                            break;
                    }

                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                    MostrarMano(jugador);

                    TerminarTurno(jugador);
                }
            }

            void MostrarMano(Jugador jugador)
            {
                Console.WriteLine($"Jugador {jugador.nJugador + 1} | {jugador.nombre}");
                Console.WriteLine($"\tMano:");
                jugador.mano.cartas.ForEach(a => Console.Write($"\t\t{jugador.mano.cartas.IndexOf(a) + 1 }._ {a.nombre} {a.pinta.pinta} {a.pinta.colorCarta}\n"));
                Console.WriteLine($"\tPuntaje:\n\t\t{jugador.mano.puntajeMano}");
            }

            void TerminarTurno(Jugador jugador)
            {
                Console.WriteLine($"Seleccione una Carta para descartar:");
                Console.WriteLine($"Ingrese el número({1}/{jugador.mano.cartas.Count()})");
                int intCartaSeleccionada = int.Parse(Console.ReadLine()) - 1;
                Carta cartaDescartada = jugador.mano.cartas[intCartaSeleccionada];
                jugador.mano.cartas.Remove(cartaDescartada);
                pila.cartas.Add(cartaDescartada);
                Console.Write("Presione una tecla para continuar...");
                Console.ReadLine();
            }
        }

        public Carta tomarCartaMazo()
        {
            Carta cartaRecogida = mazo.cartas.Last();
            mazo.cartas.Remove(cartaRecogida);
            Console.WriteLine($"La Carta recogida es un: {cartaRecogida.nombre} {cartaRecogida.pinta.pinta}");
            return cartaRecogida;
        }
        public Carta tomarCartaPila()
        {
            Carta cartaRecogida = pila.cartas.Last();
            Console.WriteLine($"La Carta recogida es un: {cartaRecogida.nombre} {cartaRecogida.pinta.pinta}");
            pila.cartas.Remove(cartaRecogida);
            return cartaRecogida;

        }
    }
}
