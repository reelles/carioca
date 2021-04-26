namespace carioca.Partidas
{
    public class EscalaSucia : IPartida
    {
        public string descripcion => "Cada jugador debe juntar:" +
                                     "\n\ta) Escala de 13 cartas consecutivas independiente del color o pinta" +
                                     "\n\tb) Se permite utilizar todos los Jokers que se quiera.";

        public int nCartasMano => 12;

        public bool usarJokers => true;

        public int nTrios => 0;

        public int nEscalas => 0;

        public bool escalaReal => false;

        public bool escalaSucia => false;

        public bool escalaColor => true;
        public enumPartida tipoPartida => enumPartida.EscalaSucia;
    }
}