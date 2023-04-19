using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class SubtractorMole : NormalMole, IScoreModifier
    {
        public int subtractScore; //TODO check if needs to be public

        public override int ScoreValue
        {
            get { return scoreValue; }
        }

        public int ModifyScore(int currentScore)
        {
            return currentScore - subtractScore;
        }

        public override void Whack()
        {
            //GameManager.Instance.DisableScore(); //TODO activate when gamemanager is up and running
            base.Whack();
        }
    }
}
