namespace carioca.Partidas
{
    public class EscalaColor : IPartida
    {
        public string descripcion => "Cada jugador debe juntar:" +
                                     "\n\ta) Escala de 13 cartas consecutivas del mismo color" +
                                     "\n\tb) Se permite utilizar 1 Joker del color correspondiente a la escala.";

        public int nCartasMano => 12;

        public bool usarJokers => true;

        public int nTrios => 0;

        public int nEscalas => 0;

        public bool escalaReal => false;

        public bool escalaSucia => false;

        public bool escalaColor => true;
        public enumPartida tipoPartida => enumPartida.EscalaColor;
    }
}