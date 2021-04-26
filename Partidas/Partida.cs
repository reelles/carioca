using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carioca.Partidas
{
    static class Partida
    {
        public static IPartida ObtenerPartida(enumPartida enumPartida)
        {
            switch (enumPartida)
            {
                case enumPartida.DosTrios:
                    return new DosTrios();
                case enumPartida.UnTrioUnaEscala:
                    return new UnTrioUnaEscala();
                case enumPartida.DosEscalas:
                    return new DosEscalas();
                case enumPartida.TresTrios:
                    return new TresTrios();
                case enumPartida.DosTriosUnaEscala:
                    return new DosTriosUnaEscala();
                case enumPartida.UnTrioDosEscalas:
                    return new UnTrioUnaEscala();
                case enumPartida.TresEscalas:
                    return new TresEscalas();
                case enumPartida.CuatroTrios:
                    return new CuatroTrios();
                case enumPartida.EscalaColor:
                    return new EscalaColor();
                case enumPartida.EscalaSucia:
                    return new EscalaSucia();
                case enumPartida.EscalaReal:
                    return new EscalaReal();
            }
            throw new Exception("No se pudo iniciar generar una partida.");

        }
    }
}
