#if UNITY_EDITOR

using Invector.Utils;
using UnityEditor;
using UnityEngine;

namespace Invector.vCharacterController
{
    public partial class vMenuComponent
    {
        [MenuItem("Invector/FootStep System/Add FootStep Component")]
        static void FootStepMenu()
        {
            if (Selection.activeGameObject)
            {
                Selection.activeGameObject.AddComponent<vFootStep>();
            }
            else
            {
                Debug.Log("Please select a GameObject to add the component.");
            }
        }

        [MenuItem("Invector/FootStep System/Create New AudioSurface")]
        static void NewAudioSurface()
        {
            vScriptableObjectUtility.CreateAsset<vAudioSurface>();
        }
    }
}
#endif