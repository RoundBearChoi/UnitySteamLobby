using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.UI;
using RB.GameElements;

namespace RB
{
    public class etcLoader : GameResources<etcResourceType>
    {
        public override void InitLoader()
        {
            LoaderDebug.Log("loading etc..");

            LoadObj<DefaultCamera>(etcResourceType.DEFAULT_CAMERA, "DefaultCamera");
            LoadObj<TestLevel1_Camera>(etcResourceType.TEST_LEVEL_1_CAMERA, "TestLevel1_Camera");

            LoadObj<TestPlayer>(etcResourceType.TEST_PLAYER, "TestPlayer");
            LoadObj<DummyPlayer>(etcResourceType.DUMMY_PLAYER, "DummyPlayer");
        }
    }
}