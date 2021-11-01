using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.GameElements;

namespace RB
{
    public class GameElementLoader : GameResources<GameElementType>
    {
        public override void InitLoader()
        {
            LoaderDebug.Log("loading game elements..");

            LoadObj<TestLevel1_Ground>(GameElementType.TEST_LEVEL_1_GROUND, "TestLevel1_Ground");
            LoadObj<TestLevel1_Lighting>(GameElementType.TEST_LEVEL_1_LIGHTING, "TestLevel1_Lighting");
        }
    }
}