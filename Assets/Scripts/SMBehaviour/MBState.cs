using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m17
{
    public abstract class MBState : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Exit();
    }
}
