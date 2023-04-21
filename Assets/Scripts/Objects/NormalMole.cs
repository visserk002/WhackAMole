using DG.Tweening;
using KevinV.WhackAMole.Interfaces;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class NormalMole : MonoBehaviour, IMole
    {
        private const float BODY_SCALE_MULTIPLY_AMOUNT = 10f;
        private const float DOMOVE_DURATION = 1f;

        [SerializeField] private MeshRenderer bodyMeshRenderer;

        private int scoreValue = 1;
        protected float heightOfModel;
        protected Quaternion localRotation;
        protected Vector3 localScale;

        public virtual void Awake()
        {
            // Get the height of the bounds by getting the size along the y-axis and multiplying it by 10 because of the Z scale value 
            heightOfModel = bodyMeshRenderer.bounds.size.y * BODY_SCALE_MULTIPLY_AMOUNT;
            //save the localscale and rotation so it can be used to set it after parenting it to the hole 
            localRotation = transform.localRotation;
            localScale = transform.localScale;
        }

        public virtual int ScoreValue
        {
            get { return scoreValue; }
        }

        public virtual bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public virtual MoleType GetMoleType()
        {
            return MoleType.Normal;
        }

        public virtual void Spawn()
        {
            transform.localPosition = new Vector3(0, 0, -heightOfModel);
            transform.localRotation = localRotation;
            transform.localScale = localScale;
            transform.DOLocalMoveZ(0, DOMOVE_DURATION).SetEase(Ease.InExpo);
        }

        public virtual void Hide()
        {
            transform.DOLocalMoveZ(-heightOfModel, DOMOVE_DURATION).SetEase(Ease.OutExpo).OnComplete(HiddenBehaviour);
        }

        public virtual void HiddenBehaviour()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(null);
        }

        public virtual void Whack()
        {
            Hide();
        }
    }
}