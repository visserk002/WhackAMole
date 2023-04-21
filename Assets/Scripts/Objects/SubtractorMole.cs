using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class SubtractorMole : NormalMole, IScoreModifier
    {
        private int subtractScore = -3;
        protected new MoleType moleType = MoleType.Subtractor;

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
