using DG.Tweening;
using KevinV.WhackAMole.Interfaces;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class NormalMole : MonoBehaviour, IMole
    {
        private const float BODY_SCALE_DIVIDE_AMOUNT = 10f;
        private const float DOMOVE_DURATION = 0.5f;

        [SerializeField] private MeshRenderer bodyMeshRenderer;

        private int scoreValue = 1;
        protected float heightOfModel;

        public virtual void Start()
        {
            // Get the height of the bounds by getting the size along the y-axis and dividing it by 10 because of the Z scale value 
            heightOfModel = bodyMeshRenderer.bounds.size.y / BODY_SCALE_DIVIDE_AMOUNT;
        }

        public virtual int ScoreValue
        {
            get { return scoreValue; }
        }

        public virtual void Spawn(Vector3 position)
        {
            transform.localPosition = new Vector3(position.x, position.y, heightOfModel);
            transform.DOLocalMoveZ(0, DOMOVE_DURATION);
        }

        public virtual void Hide()
        {
            transform.DOLocalMoveZ(-heightOfModel, DOMOVE_DURATION);
        }

        public virtual void Whack()
        {
            Hide();
        }
    }
}