namespace carioca.Partidas
{
    public class EscalaReal : IPartida
    {
        public string descripcion => "Cada jugador debe juntar:" +
                                     "\n\ta) Escala de 13 cartas consecutivas de la misma pinta" +
                                     "\n\tb)No se permite utilizar Joker en la escala.";

        public int nCartasMano => 12;

        public bool usarJokers => false;

        public int nTrios => 0;

        public int nEscalas => 0;

        public bool escalaReal => false;

        public bool escalaSucia => false;

        public bool escalaColor => true;
        public enumPartida tipoPartida => enumPartida.EscalaReal;
    }
}