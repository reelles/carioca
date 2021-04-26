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

            mazo = new MazoIngles(true, true);
            mazo.barajar();
            Console.WriteLine("Repartiendo manos...");
            jugadores.ForEach(jugador => jugador.mano = mazo.repartirMano(partida.nCartasMano));
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
                    MostrarMano(jugador);
                    Console.WriteLine($"Partida: {partida.tipoPartida}");
                    
                    //se termina el mazo, se conserva la primera de la pila
                    if (mazo.nCartas == 0) {
                        Carta cartaPila = tomarCartaPila();
                        pila.cartas.Reverse();
                        mazo = pila;
                        pila = new MazoIngles();
                        pila.cartas.Add(cartaPila);
                    }
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
                    AccionesDelTurno(jugador);
                }
            }
            jugadores.ForEach(a => a.calculaPuntajeFinal());

            void AccionesDelTurno(Jugador jugador)
            {
                Console.WriteLine("Seleccione la accion a realizar:");
                Console.WriteLine("1) Ver Mano");
                Console.WriteLine("2) Ver Cartas de Jugadores");
                Console.WriteLine("3) Ordenar mis cartas");
                Console.WriteLine("4) Bajarse");
                Console.WriteLine("5) Bajar a oponentes");
                Console.WriteLine("6) Informacion de partida");
                Console.WriteLine("7) Terminar Turno");
                int opcion = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (opcion)
                {
                    case 1: //Ver mano jugador
                        MostrarMano(jugador);
                        AccionesDelTurno(jugador);
                        break;
                    case 2://Ver cartas de jugadores

                        AccionesDelTurno(jugador);
                        break;
                    case 3://Ordenar Cartas del jugador


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
                        Console.Write("Presione una tecla para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        AccionesDelTurno(jugador);
                        break;

                    case 7://Terminar Turno

                        TerminarTurno(jugador);
                        break;
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
                if (jugador.mano.cartas.Count == 0)
                {
                    enJuego = false;
                    Console.WriteLine($"Fin de la partida, {jugador.nombre} es el ganador!");

                    Console.Write("Presione una tecla para continuar...");
                    Console.ReadLine();

                }
                else {
                    MostrarMano(jugador);
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
