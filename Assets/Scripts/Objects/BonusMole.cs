using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class BonusMole : NormalMole, IScoreModifier
    {
        private int bonusScore = 5;

        public override int ScoreValue
        {
            get { return 0; }
        }

        public override MoleType GetMoleType()
        {
            return MoleType.Bonus;
        }

        public int ModifyScore(int currentScore)
        {
            return currentScore + bonusScore;
        }
    }
}
