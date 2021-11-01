using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.GameElements
{
    public class DummyPlayer : GameElement
    {
        [SerializeField] Vector3 _targetPos = Vector3.zero;

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, 0.2f);
        }

        public override void SetTargetPosition(Vector3 position)
        {
            _targetPos = position;
        }
    }
}