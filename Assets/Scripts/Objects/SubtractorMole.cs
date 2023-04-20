using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class SubtractorMole : NormalMole, IScoreModifier
    {
        private int subtractScore = -3;

        public override int ScoreValue
        {
            get { return 0; }
        }

        public int ModifyScore(int currentScore)
        {
            return currentScore - subtractScore;
        }
    }
}
