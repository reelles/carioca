namespace carioca.Partidas
{
    public class TresTrios : IPartida
    {
        public string descripcion => "Cada jugador debe juntar:" +
                                     "\n\ta) Tres Trios, cartas de igual valor independiente de su color o pinta." +
                                     "\n\tb) Al bajarse lo puede hacer usando como maximo un Joker por trio.";

        public int nCartasMano => 12;

        public bool usarJokers => true;

        public int nTrios => 3;

        public int nEscalas => 0;

        public bool escalaReal => false;

        public bool escalaSucia => false;

        public bool escalaColor => false;
        public enumPartida tipoPartida => enumPartida.TresTrios;
    }
}