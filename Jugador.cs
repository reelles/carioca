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
        public bool bajado
        {
            get
            {
                return CartasEnMesa.All(grupos => grupos.visible);
            }
        }
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
                Console.Write($"\t\t{i + 1}) {CartasEnMesa[i].tipoGrupo}");
                if (CartasEnMesa[i].Cartas == null || CartasEnMesa[i].Cartas.Count() == 0)
                    Console.WriteLine("=> Sin Cartas");
                else
                {
                    switch (CartasEnMesa[i].tipoGrupo)
                    {
                        case enumTipoGrupo.Trio:
                            Console.Write($" de {CartasEnMesa[i].Cartas.FirstOrDefault().numero}");
                            CartasEnMesa[i].Cartas.ForEach(carta => carta.ImprimeCarta());
                            break;
                        case enumTipoGrupo.EscalaReal:
                        case enumTipoGrupo.Escala:
                            Console.Write($" de {CartasEnMesa[i].Cartas.FirstOrDefault().pinta.nombre} ");
                            break;
                        case enumTipoGrupo.EscalaColor:
                            Console.Write($" de {CartasEnMesa[i].Cartas.FirstOrDefault().pinta.colorCarta} ");
                            break;
                    }
                    Console.WriteLine($"({CartasEnMesa[i].Cartas.Count()} cartas)");
                    CartasEnMesa[i].Cartas.ForEach(carta => Console.WriteLine($"\t\t\t{carta.ImprimeCarta()} "));
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
                get { return cartas.Sum(a => a.valor) + _jugador.CartasEnMesa.Where(a => !a.visible).Sum(a => a.Cartas.Sum(a => a.valor)); }
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
                Console.WriteLine($"\tEn Mesa (Bajado={_jugador.bajado}):");
                _jugador.VerCartasEnMesa();
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
                                var addedCarts = SetGrupoMesa(gpCartas.ToList()).ToList();
                                if (addedCarts != null)
                                    addedCarts.ForEach(a => cartas.Remove(a));
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
                                var addedCarts = SetGrupoMesa(gpCartas.ToList()).ToList();
                                if (addedCarts != null)
                                    addedCarts.ForEach(a => cartas.Remove(a));
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

            private IList<Carta> SetGrupoMesa(IList<Carta> gpCartas)
            {
                Console.WriteLine("\t\tAgregar a carta en mesa? (S/N)");
                string resp = Console.ReadLine().ToUpper();
                if (resp == "S")
                {
                    _jugador.VerCartasEnMesa();
                    Console.WriteLine("\t\tIndique a que grupo agregar");
                    int nGrupo = int.Parse(Console.ReadLine());
                    return _jugador.CartasEnMesa[nGrupo - 1].AgragarCarta(gpCartas);
                }
                else
                    return null;
            }
        }
    }
}
