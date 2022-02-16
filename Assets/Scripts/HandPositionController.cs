using System;
using SardineFish.Utils;
using UnityEngine;
using UnityEngine.U2D;

namespace DefaultNamespace
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
        
        private void Awake()
        {
        }

        public void SetPos(Vector2 pos)
        {
            _shapeController.spline.SetPosition(pointIndex, _shapeController.transform.worldToLocalMatrix.MultiplyPoint(pos.ToVector3(transform.position.z)));
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