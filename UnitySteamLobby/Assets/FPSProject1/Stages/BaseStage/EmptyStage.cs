using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class EmptyStage : BaseStage
    {
        public override void InitGameElements()
        {

        }

        public override void InitStandardCanvas()
        {

        }

        public override void OnEnter()
        {
            StageDebug.Log("empty stage onenter");
        }

        public override void OnExit()
        {
            StageDebug.Log("empty stage onexit");
        }

        public override void OnFixedUpdate()
        {
            StageDebug.Log("empty stage onfixedupdate");
        }

        public override void OnUpdate()
        {
            StageDebug.Log("empty stage onupdate");
        }

        public override void OnLateUpdate()
        {
            StageDebug.Log("empty stage onlateupdate");
        }
    }
}