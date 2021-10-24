using carioca.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class Jugador
    {
        private int Puntaje { get; set; }
        public string nombre { get; set; }
        public Mano mano { get; set; }
        public int nJugador { get; }
        public int puntaje { get => Puntaje; }
        public List<CartasMesa> CartasEnMesa { get => _cartasEnMesa; set => _cartasEnMesa = value; }

        List<CartasMesa> _cartasEnMesa;
        public Jugador(int nJugador)
        {
            Console.WriteLine($"Ingrese nombre del jugador {nJugador + 1}");

            this.nombre = Console.ReadLine();
            this.nJugador = nJugador;
            this.mano = new Mano(this);
        }
        public List<CartasMesa> SetCartasEnMesa(enumPartida tipoPartida)
        {
            if (null == CartasEnMesa)
            {
                switch (tipoPartida)
                {
                    case enumPartida.DosTrios:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Trio)
                    };
                        break;
                    case enumPartida.EscalaColor:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.EscalaColor)
                    };
                        break;
                    case enumPartida.UnTrioUnaEscala:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Escala)
                    };
                        break;
                    case enumPartida.DosEscalas:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Escala),
                        new CartasMesa(enumTipoGrupo.Escala),
                    };
                        break;
                    case enumPartida.TresTrios:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Trio)
                    };
                        break;
                    case enumPartida.DosTriosUnaEscala:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Escala),
                    };
                        break;
                    case enumPartida.UnTrioDosEscalas:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Escala),
                        new CartasMesa(enumTipoGrupo.Escala),
                    };
                        break;
                    case enumPartida.TresEscalas:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Escala),
                        new CartasMesa(enumTipoGrupo.Escala),
                        new CartasMesa(enumTipoGrupo.Escala),
                    };
                        break;
                    case enumPartida.CuatroTrios:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.Trio),
                        new CartasMesa(enumTipoGrupo.Trio)
                    };
                        break;
                    case enumPartida.EscalaSucia:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.EscalaSucia)
                    };
                        break;
                    case enumPartida.EscalaReal:
                        CartasEnMesa = new List<CartasMesa>() {
                        new CartasMesa(enumTipoGrupo.EscalaReal)
                    };
                        break;
                }
            }
            return CartasEnMesa;
        }
        public void VerCartasEnMesa()
        {
            for (int i = 0; i < CartasEnMesa.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {CartasEnMesa[i].tipoGrupo}");
                if (CartasEnMesa[i].Cartas == null)
                    Console.Write("Sin Cartas");
                else
                    switch (CartasEnMesa[i].tipoGrupo)
                    {
                        case enumTipoGrupo.Trio:
                            Console.Write($" de {CartasEnMesa[i].Cartas.FirstOrDefault().numero} ");
                            break;
                        case enumTipoGrupo.EscalaReal:
                        case enumTipoGrupo.Escala:
                            Console.Write($" de {CartasEnMesa[i].Cartas.FirstOrDefault().pinta.nombre} ");
                            break;
                        case enumTipoGrupo.EscalaColor:
                            Console.Write($" de {CartasEnMesa[i].Cartas.FirstOrDefault().pinta.colorCarta} ");
                            break;
                    }
            }

        }
        public void CalculaPuntajeFinal()
        {
            Puntaje += mano.puntajeMano;
        }
        public class Mano
        {
            private Jugador _jugador { get; set; }
            public List<Carta> cartas { get; set; }
            public int puntajeMano
            {
                get { return cartas.Sum(a => a.valor); }
            }
            public Mano(Jugador jugador)
            {
                _jugador = jugador;
            }
            public void mostrarMano()
            {
                Console.WriteLine($"Jugador {_jugador.nJugador + 1} | {_jugador.nombre}");
                Console.WriteLine($"\tMano:");
                _jugador.mano.cartas.ToList().ForEach(a => Console.WriteLine($"\t\t{this._jugador.mano.cartas.IndexOf(a) + 1}) {a.ImprimeCarta()}"));
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
                            {
                                Console.Write("(Trio listo!)");
                                SetGrupoMesa(gpCartas.ToList());
                            }
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
                            Console.Write($"\tEscala de :{gpCartas.Key}");
                            if (gpCartas.Count() > 2)
                            {
                                Console.Write("(Escala lista!)");
                                SetGrupoMesa(gpCartas.ToList());
                            }
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

            private void SetGrupoMesa(IList<Carta> gpCartas)
            {
                Console.WriteLine("Agregar a carta en mesa? (S/N)");
                string resp = Console.ReadLine();
                if (resp == "S")
                {
                    _jugador.VerCartasEnMesa();
                    Console.WriteLine("Indique a que grupo agregar");
                    int nGrupo = int.Parse(Console.ReadLine());
                    _jugador.CartasEnMesa[nGrupo - 1].AgragarCarta(gpCartas);
                }
            }
        }
    }
}
