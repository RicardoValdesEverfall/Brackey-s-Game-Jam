using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] public float p_SensX;
    [SerializeField] public float p_SensY;

    [Header("DEBUG")]
    [SerializeField] public Transform p_Orientation;
    [SerializeField] public float p_RotationX;
    [SerializeField] public float p_RotationY;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * p_SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * p_SensY;

        p_RotationY += mouseX;
        p_RotationX -= mouseY;

        p_RotationX = Mathf.Clamp(p_RotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(p_RotationX, p_RotationY, 0);
        p_Orientation.rotation = Quaternion.Euler(0, p_RotationY, 0);
    }
}
