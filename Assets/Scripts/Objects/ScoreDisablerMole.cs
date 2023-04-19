using KevinV.WhackAMole.Interfaces;

namespace KevinV.WhackAMole.Objects
{
    public class ScoreDisablerMole : NormalMole, IDisabler
    {
        public float disableDuration = 3.0f; //TODO check if public or private is required

        public float GetDisableDuration()
        {
            return disableDuration;
        }

        public void DisableScore()
        {
            //GameManager.Instance.DisableScore(disableDuration); //TODO activate when instance is there
        }

        public override void Whack()
        {
            DisableScore();
            base.Whack();
        }
    }
}
