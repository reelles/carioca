using System;
using System.Collections.Generic;

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
        }
        public void calculaPuntajeFinal()
        {
            _puntaje = +mano.puntajeMano;
        }
    }
}
