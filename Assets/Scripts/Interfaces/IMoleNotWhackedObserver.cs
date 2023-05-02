namespace KevinV.WhackAMole.Interfaces
{
    public interface IMoleNotWhackedObserver : IObserver
    {
        void MoleNotWhacked(int scoreModifier);
    }
}
