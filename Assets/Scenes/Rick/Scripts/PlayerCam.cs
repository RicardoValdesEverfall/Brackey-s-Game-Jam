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
    [SerializeField] public float p_RotationX;
    [SerializeField] public float p_RotationY;


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

    private void PlayerDetect()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out c_ObjHit, c_RayLength, c_LayerForRay))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * c_ObjHit.distance, Color.yellow);
        }

        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
