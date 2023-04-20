using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class BonusMole : NormalMole, IScoreModifier
    {
        public int bonusScore; //TODO check if this should be public or can be private

        public override int ScoreValue
        {
            get { return 0; }
        }

        public int ModifyScore(int currentScore)
        {
            return currentScore + bonusScore;
        }
    }
}
