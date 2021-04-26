using System;
using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class MazoIngles
    {
        public List<Carta> cartas { get; set; }
        public int nCartas { get { return cartas.Count(); } }

        public MazoIngles()
        {
            cartas = new List<Carta>();
        }
        /// <summary>
        /// inicializa un mazo de cartas ingles
        /// </summary>
        /// <param name="doble">Indica si se requiere un mazo doble (rojo/azul)</param>
        /// <param name="usarJokers">Indica si se utilizaran jokes</param>
        public MazoIngles(bool doble, bool usarJokers)
        {
            crearMazoSimple(usarJokers, enumColorMazo.azul);
            //si se necesitan dos mazos
            if (doble)
            {
                crearMazoSimple(usarJokers, enumColorMazo.rojo);
            }


        }
        public void barajar()
        {

            Random rm = new Random();
            cartas = cartas.OrderBy(a => rm.NextDouble()).ToList();
        }
        public Mano repartirMano(int nCartasMano)
        {
            var manoJugador = new Mano() { cartas = this.cartas.Take(nCartasMano).ToList() };
            this.cartas.RemoveRange(0, nCartasMano);
            return manoJugador;
        }

        private void crearMazoSimple(bool usarJokers, enumColorMazo colorMazo)
        {
            cartas = cartas ?? new List<Carta>();
            foreach (string pinta in Enum.GetNames(typeof(enumPinta)).Where(a => a != "Joker"))
            {
                foreach (var nCarta in Enumerable.Range(1, 13))
                {

                    cartas.Add(new Carta((enumPinta)Enum.Parse(typeof(enumPinta), pinta), nCarta, colorMazo));
                }
            }
            if (usarJokers)
            {
                cartas.AddRange(new List<Carta>{
                        new Carta(enumPinta.Joker,enumColorCarta.color,colorMazo),
                        new Carta(enumPinta.Joker,enumColorCarta.negro,colorMazo)
                    });
            }
        }
    }
}
