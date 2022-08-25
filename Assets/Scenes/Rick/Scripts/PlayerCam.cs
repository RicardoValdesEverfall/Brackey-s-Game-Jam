using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] public LayerMask c_LayerForRay;
    [SerializeField] private float c_RayLength;

    [Header("DEBUG")]
    [SerializeField] private RaycastHit c_ObjHit;
    [SerializeField] public Transform p_Orientation;


    // Start is called before the first frame update
    void Start()
    {
        CameraLockCursor(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CameraLockCursor(bool state)
    {
        if (state) 
        { 
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Cursor.visible = state;
    }
}
