using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.Sound;

namespace RB
{
    public class MenuSoundLoader : GameResources<MenuSoundType>
    {
        public override void InitLoader()
        {
            LoaderDebug.Log("loading menu sounds..");

            LoadObj<MenuSound>(MenuSoundType.TRANSMISSION, "MenuSound_Transmission");
        }
    }
}