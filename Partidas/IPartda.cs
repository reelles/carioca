namespace carioca.Partidas
{
    public interface IPartida
    {
        string descripcion { get; }
        int nCartasMano { get; }
        bool usarJokers { get; }
        int nTrios { get; }
        int nEscalas { get; }
        bool escalaReal { get; }
        bool escalaSucia { get; }
        bool escalaColor { get; }
        enumPartida tipoPartida { get; }

    }
}