using System;
using UnityEngine;

namespace BongoCatUnity
{
    public class HandController : MonoBehaviour
    {
        
        [SerializeField]
        private HandPositionController HandUpCtrl;

        [SerializeField]
        private HandPositionController HandDownCtrl;

        public bool handDown = false;

        public Vector2 handPos;

        private void Awake()
        {
            handPos = HandDownCtrl.Position;
        }

        private void LateUpdate()
        {
            HandDownCtrl.gameObject.SetActive(handDown);
            HandUpCtrl.gameObject.SetActive(!handDown);
            
            if (handDown)
            {
                HandDownCtrl.Position = handPos;
            }
            else
            {
                HandUpCtrl.Position = handPos;
            }
            
            handDown = false;
        }
    }
}