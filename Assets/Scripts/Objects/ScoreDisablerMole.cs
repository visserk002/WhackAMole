using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class ScoreDisablerMole : NormalMole, IDisabler
    {
        [SerializeField] private MeshRenderer hatMeshRenderer;

        private float disableDuration = 2.0f;

        public override void Start()
        {
            base.Start();

            // Because this mole wears a hat we need to add the value of that to the base value from NormaleMole. Because the pivot is not at the bottom of the mesh we need to divide it by 2.
            heightOfModel += (hatMeshRenderer.bounds.size.y/2f);
        }

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
