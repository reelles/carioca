namespace carioca.Partidas
{
    public class DosTriosUnaEscala : IPartida
    {
        public string descripcion => "Cada jugador debe juntar:" +
                                    "\n\ta) Una Escala, cartas de Orden consecutivo de la misma pinta. " +
                                    "\n\tb) Dos Trios, cartas de igual valor independiente de su color o pinta." +
                                    "\n\tc) Al bajarse lo puede hacer usando como maximo un Joker por escala.";

        public int nCartasMano => 12;

        public bool usarJokers => true;

        public int nTrios => 2;

        public int nEscalas => 1;

        public bool escalaReal => false;

        public bool escalaSucia => false;

        public bool escalaColor => false;
        public enumPartida tipoPartida => enumPartida.DosTriosUnaEscala;
    }
}