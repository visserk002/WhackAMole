using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class SubtractorMole : NormalMole, IScoreModifier
    {
        public int subtractScore; //TODO check if needs to be public

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
