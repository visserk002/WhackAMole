using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class ScoreDisablerMole : NormalMole, IDisabler
    {
        private const float HAT_DIVIDE_AMOUNT = 20f; //The pivot is not at the bottom of the mesh so we need to divide it by 2 and multiply that by 10 because of the scale value

        [SerializeField] private MeshRenderer hatMeshRenderer;

        private float disableDuration = 2.0f;
        protected new MoleType moleType = MoleType.Disabler;

        public override void Start()
        {
            base.Start();

            // Because this mole wears a hat we need to add the value of that to the base value from NormaleMole.
            heightOfModel += (hatMeshRenderer.bounds.size.y / HAT_DIVIDE_AMOUNT);
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
