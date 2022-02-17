using System;
using SardineFish.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace BongoCatUnity
{
    public class InputRegion : MonoBehaviour
    {
        [SerializeField]
        private Transform Handle;

        public Vector2 Value;

        [InputControl]
        [SerializeField]
        private string controlPath;

        private InputControl _inputControl;

        [SerializeField]
        private HandController HandController;

        [SerializeField]
        private Transform HandTarget;

        private void Awake()
        {
            _inputControl = InputSystem.FindControl(controlPath);
        }

        private void Update()
        {
            if (_inputControl is InputControl<Vector2> control)
            {
                Value = control.ReadValue();
            }
            else
            {
                Value = Vector2.zero;
            }
            
            
            if (Handle)
                Handle.position = transform.localToWorldMatrix.MultiplyPoint(Value * 0.5f).Set(z: Handle.position.z);
            if (HandController)
            {
                HandController.handDown = Value != Vector2.zero;
                if (HandController.handDown && HandTarget)
                {
                    HandController.handPos = HandTarget.position;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Utility.DebugDrawRect(new Rect(Vector2.one * -0.5f, Vector2.one), Color.yellow, transform.localToWorldMatrix);
        }
    }
}