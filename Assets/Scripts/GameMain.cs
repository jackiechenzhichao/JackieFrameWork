using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JackieFrame
{
    public class GameMain : MonoBehaviour
    {
        private void Awake()
        {
            LuaMgr.instance.Init();
        }
    }
}