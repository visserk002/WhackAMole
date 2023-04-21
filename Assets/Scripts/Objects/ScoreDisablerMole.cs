using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class ScoreDisablerMole : NormalMole, IDisabler
    {
        private const float HAT_MULTIPLY_AMOUNT = 20f; //The pivot is not at the bottom of the mesh so we need to multiply it by 2 and multiply that by 10 because of the scale value

        [SerializeField] private MeshRenderer hatMeshRenderer;

        private float disableDuration = 2.0f;

        public override void Awake()
        {
            base.Awake();

            // Because this mole wears a hat we need to add the value of that to the base value from NormaleMole.
            heightOfModel += (hatMeshRenderer.bounds.size.y * HAT_MULTIPLY_AMOUNT);
        }

        public override MoleType GetMoleType()
        {
            return MoleType.Disabler;
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
