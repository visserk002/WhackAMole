using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;

namespace KevinV.WhackAMole.Objects
{
    public class ScoreDisablerMole : NormalMole, IDisabler
    {
        private float disableDuration = 3.0f;

        public float GetDisableDuration()
        {
            return disableDuration;
        }

        public void DisableScore()
        {
            GameManager.Instance.DisableScoring(disableDuration);
        }

        public override void Whack()
        {
            DisableScore();
            base.Whack();
        }
    }
}
