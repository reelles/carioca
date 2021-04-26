using System;

namespace carioca
{
    public class Carta
    {
        public class PintaCarta
        {
            public enumPinta pinta { get; }
            public enumColorCarta colorCarta { get; }
            public PintaCarta(enumPinta pinta, enumColorCarta colorCarta)
            {
                this.pinta = pinta;
                this.colorCarta = colorCarta;

            }
        }

        public enumColorMazo colorMazo { get; }
        public int numero { get; }
        public int valor { get; }
        public string nombre { get; }
        public PintaCarta pinta { get; }
        public Carta(enumPinta pinta, enumColorCarta color, enumColorMazo colorMazo)
        {
            if (pinta != enumPinta.Joker)
                throw new Exception("Este metodo solo permite crear Jockers");
            this.colorMazo = colorMazo;
            this.numero = 0;
            this.pinta = new PintaCarta(pinta, color);
            this.valor = 30;
            this.nombre = "Joker";
        }

        public Carta(enumPinta pinta, int numero, enumColorMazo colorMazo)
        {
            if (pinta == enumPinta.Joker)
                throw new Exception("Jockers deben ser creados indicando su color");
            this.colorMazo = colorMazo;
            this.numero = numero;
            this.pinta = new PintaCarta(pinta, pinta == enumPinta.Corazon || pinta == enumPinta.Diamante ? enumColorCarta.color : enumColorCarta.negro);

            switch (pinta)
            {
                case enumPinta.Joker:
                    valor = 30;
                    nombre = "Joker";
                    break;
                default:
                    switch (numero)
                    {
                        case 11:
                            nombre = "J";
                            valor = 10;
                            break;
                        case 12:
                            nombre = "Q";
                            valor = 10;
                            break;
                        case 13:
                            nombre = "K";
                            valor = 10;
                            break;
                        case 1:
                            nombre = "A";
                            valor = 20;
                            break;
                        default:
                            nombre = numero.ToString();
                            valor = numero;
                            break;
                    }

                    break;
            }

        }
    }
}
