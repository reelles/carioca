using System;
using System.Collections.Generic;
using System.Linq;

namespace carioca
{
    public class CartasMesa
    {
        public enumTipoGrupo tipoGrupo { get; }
        private List<Carta> _cartas { get; set; }
        public List<Carta> Cartas
        {
            get {
                return _cartas;
            }
        }
        public bool visible { get; set; }
        public CartasMesa(enumTipoGrupo tipoGrupo)
        {
            this.tipoGrupo = tipoGrupo;
            visible = false;
            _cartas = new List<Carta>();
        }
        public Carta AgragarCarta(Carta carta)
        {
            if (_cartas.Count() != 0)
            {
                if (carta.pinta.nombre == enumPinta.Joker && _cartas.Any(a => a.pinta.nombre == enumPinta.Joker) // Si se esta insertando un joker y ya hay uno en el grupo
                    || carta.pinta.nombre == enumPinta.Joker && tipoGrupo == enumTipoGrupo.EscalaReal // Si se esta insertando un Joker y es un grupo de tipo Escala real
                    )
                    return null;
                else if (carta.pinta.nombre == enumPinta.Joker)
                {
                    _cartas.Add(carta);
                    return carta;
                }
                else
                {
                    switch (tipoGrupo)
                    {
                        case enumTipoGrupo.Trio:
                            if (_cartas.All(a => a.numero == carta.numero) && (!visible && (_cartas.Count() < 3)))
                            {
                                _cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.Escala:
                            if (_cartas.Any(a => a.pinta.nombre == carta.pinta.nombre && a.numero != carta.numero) && (!visible && (_cartas.Count() < 4)))
                            {
                                _cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.EscalaSucia:
                            if (_cartas.Any(a=>a.numero != carta.numero) && (!visible && (_cartas.Count() < 12)))
                            {
                                _cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.EscalaColor:
                            if (_cartas.Any(a => a.pinta.colorCarta == carta.pinta.colorCarta && a.numero != carta.numero) && (!visible && (_cartas.Count() < 12)))
                            {
                                _cartas.Add(carta);
                                return carta;
                            }
                            break;
                        case enumTipoGrupo.EscalaReal:
                            if (_cartas.Any(a => a.pinta.nombre == carta.pinta.nombre && a.numero != carta.numero) && (!visible && (_cartas.Count() < 12)))
                            {
                                _cartas.Add(carta);
                                return carta;
                            }
                            break;
                    }
                    return null;
                }
            }
            else
            {
                _cartas.Add(carta);
                return carta;
            }

        }
        public IList<Carta> AgragarCarta(IList<Carta> cartas)
        {
            IList<Carta> resultAddCarta = new List<Carta>();

            foreach (Carta carta in cartas)
            {
                var retCart = this.AgragarCarta(carta);
                if (retCart == null)
                {
                    return null;
                }
                else
                    resultAddCarta.Add(carta);
            }
            return resultAddCarta;
        }

        public void Revelar()
        {
            switch (this.tipoGrupo)
            {
                case enumTipoGrupo.Trio:
                    visible = _cartas.Count() == 3 && _cartas.GroupBy(a => a.numero).Count() == 1;
                    break;
                case enumTipoGrupo.Escala:
                    if (_cartas.Count() == 4)
                    {
                        int lowCard = _cartas.Min(a => a.numero);
                        foreach (Carta carta in _cartas.OrderBy(a => a.numero).Skip(1))
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
                    visible = _cartas.Select(a => a.numero).Distinct().Count() == _cartas.Count() && _cartas.GroupBy(a => a.pinta.nombre).Count() == 1;
                    break;
                case enumTipoGrupo.EscalaSucia:
                    visible = _cartas.Select(a => a.numero).Distinct().Count() == 13;
                    break;
                case enumTipoGrupo.EscalaColor:
                    visible = _cartas.Select(a => a.numero).Distinct().Count() == 13 && _cartas.GroupBy(a => a.pinta.colorCarta).Count() == 1;
                    break;
                case enumTipoGrupo.EscalaReal:
                    visible = _cartas.Select(a => a.numero).Distinct().Count() == 13 && _cartas.GroupBy(a => a.pinta.nombre).Count() == 1;
                    break;
            }

            if (!visible)
            {
                throw new Exception("No se cumplen las reglas para revelar cartas.");
            }
        }
    }
}