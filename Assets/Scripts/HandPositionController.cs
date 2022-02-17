using System;
using SardineFish.Utils;
using UnityEngine;
using UnityEngine.U2D;

namespace BongoCatUnity
{
    [ExecuteInEditMode]
    public class HandPositionController : MonoBehaviour
    {
        [SerializeField]
        private SpriteShapeController _shapeController;
        [SerializeField]
        private int pointIndex;

        [SerializeField]
        private Transform Target;


        public Vector2 Position
        {
            get
            {
                if (Target)
                    return Target.position;
                return Vector2.zero;
            }
            set
            {
                if(Target)
                    Target.position = value.ToVector3(transform.position.z);
            }
            
        }

        private void Awake()
        {
        }

        public void SetPos(Vector2 pos)
        {
            var pointPos = _shapeController.spline.GetPosition(pointIndex);
            Debug.Log($"{pointPos}, {_shapeController.spline.GetPosition(0)}");
            _shapeController.spline.SetPosition(pointIndex, _shapeController.transform.worldToLocalMatrix.MultiplyPoint(pos.ToVector3(0)).Set(z:0));
        }


        private void Update()
        {
            if (Target)
            {
                SetPos(Target.position.ToVector2());
            }
        }
    }
}