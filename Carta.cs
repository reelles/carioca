using System;

namespace carioca
{
    public class Carta
    {
        public class Pinta
        {
            public enumPinta nombre { get; }
            public enumColorCarta colorCarta { get; }
            public Pinta(enumPinta pinta, enumColorCarta colorCarta)
            {
                this.nombre = pinta;
                this.colorCarta = colorCarta;

            }
        }

        public enumColorMazo colorMazo { get; }
        public int numero { get; }
        public int valor { get; }
        public string nombre { get; }
        public Pinta pinta { get; }
        public Carta(enumPinta pinta, enumColorCarta color, enumColorMazo colorMazo)
        {
            if (pinta != enumPinta.Joker)
                throw new Exception("Este metodo solo permite crear Jockers");
            this.colorMazo = colorMazo;
            this.numero = 0;
            this.pinta = new Pinta(pinta, color);
            this.valor = 30;
            this.nombre = "Joker";
        }

        public Carta(enumPinta pinta, int numero, enumColorMazo colorMazo)
        {
            if (pinta == enumPinta.Joker)
                throw new Exception("Jockers deben ser creados indicando su color");
            this.colorMazo = colorMazo;
            this.numero = numero;
            this.pinta = new Pinta(pinta, pinta == enumPinta.Corazon || pinta == enumPinta.Diamante ? enumColorCarta.roja : enumColorCarta.negro);            
            switch (numero)
            {
                case 11:
                    nombre += "J";
                    valor = 10;
                    break;
                case 12:
                    nombre += "Q";
                    valor = 10;
                    break;
                case 13:
                    nombre += "K";
                    valor = 10;
                    break;
                case 1:
                    nombre += "A";
                    valor = 20;
                    break;
                default:
                    nombre += numero.ToString();
                    valor = numero;
                    break;
            }
            switch (this.pinta.nombre)
            {
                case enumPinta.Picas:
                    nombre += (char)9824;
                    break;
                case enumPinta.Trebol:
                    nombre += (char)9827;
                    break;
                case enumPinta.Diamante:
                    nombre += (char)9830;
                    break;
                case enumPinta.Corazon:
                    nombre += (char)9829;
                    break;
            }
        }
        public string ImprimeCarta()
        {

            return ($"{this.nombre} {(this.pinta.nombre == enumPinta.Joker ? this.pinta.colorCarta : "")}");
        }
    }
}
