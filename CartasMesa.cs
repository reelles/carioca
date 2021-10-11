using System;
using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class CartasMesa
    {
        public enumTipoGrupo tipoGrupo { get; }
        private List<Carta> cartas { get; set; }
        public bool visible { get; set; }
        public CartasMesa(enumTipoGrupo tipoGrupo)
        {
            this.tipoGrupo = tipoGrupo;
            visible = false;
            cartas = new List<Carta>();
        }
        public Carta AgragarCarta(Carta carta)
        {
            if (cartas.Count() != 0)
            {
                if (carta.pinta.nombre == enumPinta.Joker && cartas.Any(a => a.pinta.nombre == enumPinta.Joker) // Si se esta insertando un joker y ya hay uno en el grupo
                    || carta.pinta.nombre == enumPinta.Joker && tipoGrupo == enumTipoGrupo.EscalaReal // Si se esta insertando un Joker y es un grupo de tipo Escala real
                    )
                    return null;
                else if (carta.pinta.nombre == enumPinta.Joker)
                {
                    cartas.Add(carta);
                    return carta;
                }
                else
                {
                    switch (tipoGrupo)
                    {
                        case enumTipoGrupo.Trio:
                            if (cartas.All(a => a.numero == carta.numero) && !(!visible && (cartas.Count() < 3)))
                            {
                                cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.Escala:
                            if (cartas.Any(a => a.pinta.nombre == carta.pinta.nombre && a.numero != carta.numero) && !(!visible && (cartas.Count() < 4)))
                            {
                                cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.EscalaSucia:
                            if (cartas.Any(a=>a.numero != carta.numero) && !(!visible && (cartas.Count() < 12)))
                            {
                                cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.EscalaColor:
                            if (cartas.Any(a => a.pinta.colorCarta == carta.pinta.colorCarta && a.numero != carta.numero) && !(!visible && (cartas.Count() < 12)))
                            {
                                cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.EscalaReal:
                            if (cartas.Any(a => a.pinta.nombre == carta.pinta.nombre && a.numero != carta.numero) && !(!visible && (cartas.Count() < 12)))
                            {
                                cartas.Add(carta);
                                return carta;
                            }
                            break;
                    }
                    return null;
                }
            }
            else
            {
                cartas.Add(carta);
                return carta;
            }

        }
        public IList<Carta> AgragarCarta(IList<Carta> cartas)
        {
            foreach (Carta carta in cartas)
            {
                if (this.AgragarCarta(carta) == null)
                {
                    return null;
                }
            }
            return cartas;
        }

        public void Revelar()
        {
            switch (this.tipoGrupo)
            {
                case enumTipoGrupo.Trio:
                    visible = cartas.Count() == 3 && cartas.GroupBy(a => a.numero).Count() == 1;
                    break;
                case enumTipoGrupo.Escala:
                    if (cartas.Count() == 4)
                    {
                        int lowCard = cartas.Min(a => a.numero);
                        foreach (Carta carta in cartas.OrderBy(a => a.numero).Skip(1))
                        {
                            if (lowCard == 13)
                            {
                                lowCard = 0;
                            }
                            lowCard++;
                            if (lowCard != carta.numero)
                            {
                                visible = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        visible = false;
                    }
                    visible = cartas.Select(a => a.numero).Distinct().Count() == cartas.Count() && cartas.GroupBy(a => a.pinta.nombre).Count() == 1;
                    break;
                case enumTipoGrupo.EscalaSucia:
                    visible = cartas.Select(a => a.numero).Distinct().Count() == 13;
                    break;
                case enumTipoGrupo.EscalaColor:
                    visible = cartas.Select(a => a.numero).Distinct().Count() == 13 && cartas.GroupBy(a => a.pinta.colorCarta).Count() == 1;
                    break;
                case enumTipoGrupo.EscalaReal:
                    visible = cartas.Select(a => a.numero).Distinct().Count() == 13 && cartas.GroupBy(a => a.pinta.nombre).Count() == 1;
                    break;
            }

            if (!visible)
            {
                throw new Exception("No se cumplen las reglas para revelar cartas.");
            }
        }
    }
}