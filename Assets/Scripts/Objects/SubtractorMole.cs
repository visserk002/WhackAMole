using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class SubtractorMole : NormalMole, IScoreModifier
    {
        private int subtractValue = 5;

        public override int ScoreValue
        {
            get { return 0; }
        }

        public override int NotWhackedValue
        {
            get { return 0; }
        }

        public override MoleType GetMoleType()
        {
            return MoleType.Subtractor;
        }

        public int ModifyScore(int currentScore)
        {
            return currentScore - subtractValue;
        }
    }
}
