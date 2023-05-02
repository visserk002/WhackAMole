using System;
using System.Collections;
using DG.Tweening;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using UnityEngine;

namespace KevinV.WhackAMole.Objects
{
    public class NormalMole : MonoBehaviour, IMole
    {
        private const float BODY_SCALE_MULTIPLY_AMOUNT = 10f;
        private const float DOMOVE_DURATION = 1f;

        [SerializeField] private MeshRenderer bodyMeshRenderer;

        private int scoreValue = 1;
        private int notWhackedValue = -1;
        protected float timeOfMoleVisible = .8f;
        protected float heightOfModel;
        protected Quaternion localRotation;
        protected Vector3 localScale;

        public bool whacked { get; private set; }

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

        public virtual int NotWhackedValue
        {
            get { return notWhackedValue; }
        }

        public virtual bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public virtual MoleType GetMoleType()
        {
            return MoleType.Normal;
        }

        //This method sets the transform of the mole to the values of the prefab because it has a new parent and we dont want the values to change.
        public virtual void Spawn()
        {
            whacked = false;

            transform.localPosition = new Vector3(0, 0, -heightOfModel);
            transform.localRotation = localRotation;
            transform.localScale = localScale;
            transform.DOLocalMoveZ(0, DOMOVE_DURATION).SetEase(Ease.InExpo);

            StartCoroutine(MoleLifespanCoroutine());
        }

        //Handle that the mole needs to disappear after a certain time and call the NotWhacked method which handles any logic that the mole must do 
        private IEnumerator MoleLifespanCoroutine()
        {
            yield return new WaitForSeconds(timeOfMoleVisible + DOMOVE_DURATION);

            if (IsActive() && !whacked)
            {
                NotWhacked();
            }
        }

        //Call the event so the GameManager gets updated that a mole wasn't whacked
        public virtual void NotWhacked()
        {
            Hide();
            ObserverManager.Instance.NotifyMoleNotWhackedObservers(NotWhackedValue);
        }

        public virtual void Hide()
        {
            //Use DoTween to move the mole down based on the height of the mole by using an ease. When its complete it calls the HiddenBehaviour method to further handle it.
            transform.DOLocalMoveZ(-heightOfModel, DOMOVE_DURATION).SetEase(Ease.OutExpo).OnComplete(HiddenBehaviour);
        }

        public virtual void HiddenBehaviour()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(null);
        }

        //This method gets called by the gamemanager when an user has whacked a mole.
        public virtual void Whack()
        {
            whacked = true;
            StopCoroutine(MoleLifespanCoroutine());
            Hide();     
        }
    }
}