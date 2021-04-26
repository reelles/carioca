namespace carioca.Partidas
{
    public class DosEscalas : IPartida
    {

        public string descripcion => "Cada jugador debe juntar:" +
                                     "\n\ta) Dos escalas trios, cartas de Orden consecutivo de la misma pinta." +
                                     "\n\tb) Al bajarse lo puede hacer usando como maximo un Joker por escala.";

        public int nCartasMano => 12;

        public bool usarJokers => true;

        public int nTrios => 0;

        public int nEscalas => 2;

        public bool escalaReal => false;

        public bool escalaSucia => false;

        public bool escalaColor => false;

        public enumPartida tipoPartida =>  enumPartida.DosEscalas;
    }
}