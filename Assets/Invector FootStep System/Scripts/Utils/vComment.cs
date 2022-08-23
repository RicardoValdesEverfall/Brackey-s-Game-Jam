using UnityEngine;

namespace Invector.Utils
{
    public class vComment : MonoBehaviour
    {
#if UNITY_EDITOR
        
        [Multiline]
        public string comment;        

#endif
    }
}