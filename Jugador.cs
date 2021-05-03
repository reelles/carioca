using carioca.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class Jugador
    {
        private int _puntaje { get; set; }

        public string nombre { get; set; }
        public Mano mano { get; set; }
        public int nJugador { get; }
        public int puntaje { get => _puntaje; }
        public List<CartasMesa> cartasEnMesa { get; set; }
        public Jugador(int nJugador)
        {
            Console.WriteLine($"Ingrese nombre del jugador {nJugador + 1}");

            this.nombre = Console.ReadLine();
            this.nJugador = nJugador;
            this.mano = new Mano(this);
        }
        public void calculaPuntajeFinal()
        {
            _puntaje = +mano.puntajeMano;
        }
        public class Mano
        {
            private Jugador _jugador { get; set; }

            public Mano(Jugador jugador)
            {
                _jugador = jugador;
            }
            public List<Carta> cartas { get; set; }
            public int puntajeMano
            {
                get { return cartas.Sum(a => a.valor); }
            }
            public void mostrarMano()
            {
                Console.WriteLine($"Jugador {_jugador.nJugador + 1} | {_jugador.nombre}");
                Console.WriteLine($"\tMano:");
                _jugador.mano.cartas.ToList().ForEach(a =>Console.WriteLine($"\t\t{this._jugador.mano.cartas.IndexOf(a)+1}) {a.ImprimeCarta()}"));

                Console.WriteLine($"\tPuntaje:\n\t\t{_jugador.mano.puntajeMano}");

            }

            public void ordenarCartas(IPartida partida)
            {

                switch (partida.tipoPartida)
                {
                    case enumPartida.DosTrios:
                    case enumPartida.CuatroTrios:
                    case enumPartida.TresTrios:
                        cartas = cartas.OrderBy(a => a.numero).ThenByDescending(a => a.valor).ToList();
                        Console.WriteLine("Posibles Trios");
                        foreach (var gpCartas in cartas.GroupBy(a => a.numero).Where(a => a.Count() >= 2).OrderByDescending(a => a.Count()))
                        {
                            Console.Write($"\tTrio de :{gpCartas.Key}");
                            if (gpCartas.Count() > 2)
                                Console.Write("(Trio listo!)");
                            Console.WriteLine();
                            gpCartas.ToList().ForEach(a => Console.WriteLine($"\t\t{gpCartas.ToList().IndexOf(a) + 1}) {a.ImprimeCarta()}"));
                        }

                        break;
                    case enumPartida.DosEscalas:
                    case enumPartida.TresEscalas:
                        cartas = cartas.OrderBy(a => a.pinta).ThenBy(a => a.nombre).ToList();
                        Console.WriteLine("Posibles Escalas");

                        foreach (var gpCartas in cartas.GroupBy(a => a.pinta).Where(a => a.Count() >= 2).OrderByDescending(a => a.Count()))
                        {
                            Console.Write($"\tTrio de :{gpCartas.Key}");
                            if (gpCartas.Count() > 2)
                                Console.Write("(Trio listo!)");
                            Console.WriteLine();
                            gpCartas.ToList().ForEach(a => Console.WriteLine($"\t\t{gpCartas.ToList().IndexOf(a) + 1}) {a.ImprimeCarta()}"));
                        }


                        break;
                    case enumPartida.UnTrioUnaEscala:
                    case enumPartida.DosTriosUnaEscala:
                        break;
                    case enumPartida.UnTrioDosEscalas:



                        break;
                    case enumPartida.EscalaColor:
                        cartas = cartas.OrderBy(a => a.numero).OrderBy(a => a.pinta.colorCarta).ToList();
                        var max = cartas.GroupBy(a => a.pinta.colorCarta).OrderByDescending(a => a.Count()).First();

                        Console.WriteLine($"Posible Escala de {max.Key}s con {max.Count()} cartas.");
                        max.ToList().ForEach(a => Console.WriteLine($"\t{max.ToList().IndexOf(a) + 1}) {a.ImprimeCarta()}"));

                        break;
                    case enumPartida.EscalaSucia:
                        break;
                    case enumPartida.EscalaReal:
                        cartas = cartas.OrderBy(a => a.pinta).ThenBy(a => a.numero).ToList();
                        break;
                }
            }
        }
    }
}
