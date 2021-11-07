using carioca.Partidas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class Juego
    {

        public MazoIngles mazo { get; set; }
        public MazoIngles pila { get; set; }
        public List<Jugador> jugadores { get; set; }
        public List<Partidas.IPartida> partidas { get; set; }
        public Juego(int nJugadores)
        {
            partidas = new List<IPartida>();

            foreach (string partida in Enum.GetNames(typeof(enumPartida)))
            {
                enumPartida tPartida = (enumPartida)Enum.Parse(typeof(enumPartida), partida);

                partidas.Add(Partida.ObtenerPartida(tPartida));

            }
            jugadores = new List<Jugador>();
            for (int i = 0; i < nJugadores; i++)
            {
                Console.WriteLine();
                Jugador jugador = new Jugador(i);
                jugadores.Add(jugador);

            }


        }
        public void Iniciar(IPartida partida)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            mazo = new MazoIngles(true, true);
            mazo.barajar();
            Console.WriteLine("Repartiendo manos...");
            jugadores.ForEach(jugador =>
            {
                jugador.mano.cartas = mazo.repartirMano(partida.nCartasMano);
                jugador.SetCartasEnMesa(partida.tipoPartida);
            });
            pila = new MazoIngles();
            pila.cartas.Add(tomarCartaMazo());
            Console.Clear();
            Console.WriteLine($"Partida: {partida.tipoPartida}");
            Console.WriteLine($"\t{partida.descripcion}");

            Console.WriteLine($"Puntajes actuales:");
            Console.WriteLine($"#\t|\tNombre\t|\tPuntaje");
            jugadores.OrderBy(a => a.puntaje).ThenBy(a => a.nJugador).ToList().ForEach(a => Console.WriteLine($"{a.nJugador + 1}\t|\t{a.nombre}\t|\t{a.puntaje}"));

            Console.Write("Presione una tecla para continuar...");
            Console.ReadLine();
            bool enJuego = true;
            while (enJuego)
            {
                foreach (Jugador jugador in jugadores.OrderBy(a => a.nJugador))
                {
                    Console.Clear();
                    //Inicia el Turno
                    jugador.mano.mostrarMano();
                    Console.WriteLine($"Partida: {partida.tipoPartida}");

                    //se termina el mazo, se conserva la primera de la pila
                    if (mazo.nCartas == 0)
                    {
                        Carta cartaPila = tomarCartaPila();
                        pila.cartas.Reverse();
                        mazo = pila;
                        pila = new MazoIngles();
                        pila.cartas.Add(cartaPila);
                    }
                    Console.WriteLine($"En la Mesa:\n\t\tMazo:{mazo.nCartas} \tPila:{pila.cartas.Last().nombre} ({pila.nCartas} cartas)");
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
                    jugador.mano.mostrarMano();
                    AccionesDelTurno(jugador);
                }
            }
            jugadores.ForEach(a => a.CalculaPuntajeFinal());

            void AccionesDelTurno(Jugador jugador)
            {
                var tieneJoker = jugador.mano.cartas.Any(cartas => cartas.pinta.nombre == enumPinta.Joker);
                var acciones = 7;
                Console.WriteLine("Acciones Del Turno");
                Console.WriteLine("1) Ver Mano");
                Console.WriteLine("2) Ver Cartas de Jugadores");
                Console.WriteLine("3) Ordenar mis cartas");
                if (!jugador.bajado)
                    Console.WriteLine("4) Bajarse");
                Console.WriteLine("5) Bajar a oponentes");
                Console.WriteLine("6) Informacion de partida");
                Console.WriteLine("7) Terminar Turno");
                if (tieneJoker) {
                    Console.WriteLine("8) Uscar Joker");
                    acciones++;
                }
                Console.Write($"Seleccione la accion a realizar(1/{acciones}):");
                int opcion = 0;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.Clear();
                    Console.WriteLine("Opcion invalida, debe ingresar un numero.");
                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                    jugador.mano.mostrarMano();
                    AccionesDelTurno(jugador);
                }
                Console.Clear();
                switch (opcion)
                {
                    case 1: //Ver mano jugador
                        jugador.mano.mostrarMano();
                        AccionesDelTurno(jugador);
                        break;
                    case 2://Ver cartas de jugadores

                        AccionesDelTurno(jugador);
                        break;
                    case 3://Ordenar Cartas del jugador

                        jugador.mano.ordenarCartas(partida);
                        AccionesDelTurno(jugador);
                        break;
                    case 4://Bajarse
                        

                        AccionesDelTurno(jugador);
                        break;
                    case 5://Bajar al oponente

                        AccionesDelTurno(jugador);
                        break;
                    case 6://Infromacion de partida
                        Console.WriteLine(partida.descripcion);

                        jugador.mano.mostrarMano();

                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        AccionesDelTurno(jugador);
                        break;

                    case 7://Terminar Turno
                        TerminarTurno(jugador);
                        break;
                    case 8://Usar JOKER
                        UsarJoker(jugador);


                        TerminarTurno(jugador);
                        break;
                    default:
                        Console.WriteLine($"Opcion invalida, debe ingresar una opcion del 1 al 7.");
                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        jugador.mano.mostrarMano();
                        AccionesDelTurno(jugador);
                        break;


                }

            }

            void TerminarTurno(Jugador jugador)
            {
                if (jugador.mano.cartas.Count == 0)
                {
                    enJuego = false;
                    Console.WriteLine($"Fin de la partida, {jugador.nombre} es el ganador!");

                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadLine();

                }
                else
                {
                    jugador.mano.mostrarMano();
                    Console.WriteLine($"Seleccione una Carta para descartar:");
                    Console.Write($"Ingrese el número({1}/{jugador.mano.cartas.Count()}):");

                    int intCartaSeleccionada = 0;

                    if (!int.TryParse(Console.ReadLine(), out intCartaSeleccionada))
                    {
                        Console.WriteLine($"Opcion invalida, debe ingresar una opcion del 1/{jugador.mano.cartas.Count()}.");
                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        TerminarTurno(jugador);
                    }
                    intCartaSeleccionada--;
                    if (intCartaSeleccionada >= 0 && intCartaSeleccionada < jugador.mano.cartas.Count)
                    {

                        Carta cartaDescartada = jugador.mano.cartas[intCartaSeleccionada];

                        jugador.mano.cartas.Remove(cartaDescartada);
                        pila.cartas.Add(cartaDescartada);
                        Console.WriteLine($"La Carta descartada es un: {cartaDescartada.ImprimeCarta()}");
                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine($"Opcion invalida, debe ingresar una opcion del 1/{jugador.mano.cartas.Count()}.");
                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        TerminarTurno(jugador);
                    }
                }


            }
        }

        private void UsarJoker(Jugador jugador)
        {
            Console.Clear();

            Console.WriteLine("Seleccione el Joker a utilizar:");
            var jokersJugador = jugador.mano.cartas.Where(carta => carta.pinta.nombre == enumPinta.Joker).ToList();
            jokersJugador.ForEach(a => Console.WriteLine($"\t\t{jokersJugador.IndexOf(a) + 1}) {a.ImprimeCarta()}"));
            int intCartaSeleccionada = 0;
            if (!int.TryParse(Console.ReadLine(), out intCartaSeleccionada))
            {
                Console.WriteLine($"Opcion invalida, debe ingresar una opcion del 1/{jokersJugador.Count()}.");
                Console.Write("Presione una tecla para continuar...");
                Console.ReadKey();
                UsarJoker(jugador);
            }
            else if (intCartaSeleccionada > 0 && intCartaSeleccionada < jokersJugador.Count())
            {
                //TODO: Implementar uso de JOKERS


            }
            else {
                Console.WriteLine($"Opcion invalida, debe ingresar una opcion del 1/{jokersJugador.Count()}.");
                Console.Write("Presione una tecla para continuar...");
                Console.ReadKey();
                UsarJoker(jugador);
            }



        }

        public Carta tomarCartaMazo()
        {
            Carta cartaRecogida = mazo.cartas.Last();
            mazo.cartas.Remove(cartaRecogida);
            Console.WriteLine($"La Carta recogida es un: {cartaRecogida.ImprimeCarta()}");
            return cartaRecogida;
        }
        public Carta tomarCartaPila()
        {
            Carta cartaRecogida = pila.cartas.Last();
            Console.WriteLine($"La Carta recogida es un: {cartaRecogida.ImprimeCarta()}");
            pila.cartas.Remove(cartaRecogida);
            return cartaRecogida;

        }
    }
}
