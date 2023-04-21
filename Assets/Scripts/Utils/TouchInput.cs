using System.Collections;
using System.Collections.Generic;
using KevinV.WhackAMole.Interfaces;
using KevinV.WhackAMole.Managers;
using UnityEngine;

namespace KevinV.WhackAMole.Utils
{
    public class TouchInput : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitData;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitData))
                {
                    // Check if the hit object has a Mole component
                    IMole mole = hitData.collider.GetComponentInParent<IMole>();
                    if (mole != null)
                    {
                        // Call WhackMole on the GameManager instance and pass the mole as parameter
                        gameManager?.WhackMole(mole);
                    }
                }
            }
        }
    }
}

