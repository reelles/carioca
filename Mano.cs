using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class Mano
    {

        public List<Carta> cartas { get; set; }
        public int puntajeMano
        {
            get { return cartas.Sum(a => a.valor); }
        }

    }
}
