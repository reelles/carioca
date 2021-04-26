using carioca.Partidas;
using System;
using System.Text;
using System.Threading.Tasks;

namespace carioca
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Ingrese el numero de jugadores:");
            int nJugadores = int.Parse(Console.ReadLine());
            Juego juego = new Juego(nJugadores);
            foreach (IPartida partida in juego.partidas)
            {
                juego.Iniciar(partida);
            }


        }

    }
}
