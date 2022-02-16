using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class HandController : MonoBehaviour
    {
        [SerializeField]
        private HandPositionController leftHandUpCtrl;

        [SerializeField]
        private HandPositionController leftHandDownCtrl;
        
        [SerializeField]
        private HandPositionController rightHandUpCtrl;

        [SerializeField]
        private HandPositionController rightHandDownCtrl;

        public bool leftHandDown = false;

        public bool rightHandDown = false;

        private void Update()
        {
            leftHandDown = false;
            rightHandDown = false;
            if (Input.GetKey(KeyCode.Z))
            {
                leftHandDown = true;
            }

            if (Input.GetKey(KeyCode.X))
                rightHandDown = true;

            
            leftHandDownCtrl.gameObject.SetActive(leftHandDown);
            leftHandUpCtrl.gameObject.SetActive(!leftHandDown);
            
            rightHandDownCtrl.gameObject.SetActive(rightHandDown);
            rightHandUpCtrl.gameObject.SetActive(!rightHandDown);
        }
    }
}