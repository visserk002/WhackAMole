namespace KevinV.WhackAMole.Interfaces
{
    public interface IScoreObserver : IObserver
    {
        void OnScoreUpdated(int score);
    }
}
