using KevinV.WhackAMole.Interfaces;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class NormalMole : MonoBehaviour, IMole
    {
        [SerializeField] private MeshRenderer bodyMeshRenderer;

        private int scoreValue = 1;
        protected float heightOfModel;

        public virtual void Start()
        {
            // Get the height of the bounds by getting the size along the y-axis
            heightOfModel = bodyMeshRenderer.bounds.size.y;
        }

        public virtual int ScoreValue
        {
            get { return scoreValue; }
        }

        public virtual void Spawn(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Whack()
        {
            Hide();
        }
    }
}