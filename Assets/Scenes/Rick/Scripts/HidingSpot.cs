using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] private PlayerController h_playerRef;
    [SerializeField] private Camera h_Cam;
    [SerializeField] public enum h_Spots {CLOSET, BED, TABLE, FRIDGE, CABINET, LAUNDRY}
    [SerializeField] private h_Spots h_spot;
    [SerializeField] [Range(0.1f, 1f)] private float h_radius;

    [Header("HIDING CAMERA SETTINGS")]
    [SerializeField] [Range(-90, 0)] private float h_CameraRestraintsX;
    [SerializeField] [Range(0, 90)] private float h_CameraRestraintsY;
    [SerializeField] private float h_CamSens;

    [Header("DEBUG")]
    [SerializeField] private bool h_IsHiding;
    [SerializeField] private HidingSpot h_thisHidingSpot;
    [SerializeField] private GameObject h_hidingObj;

    private float rotX;
    private float rotY;

    private void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                h_hidingObj = gameObject.transform.GetChild(i).gameObject;
                h_Cam = h_hidingObj.GetComponentInChildren<Camera>();
                h_Cam.gameObject.SetActive(false);
            }
        }

        h_thisHidingSpot = this.GetComponent<HidingSpot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SphereCollider>().radius = h_radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (h_IsHiding)
        {
            h_Cam.gameObject.SetActive(true);
            CameraControl();

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Leave hideout by:
                //enable player controls
                //enable player camera
                //disable hiding camera
                //disable hiding controls
            }
        }
    }

    private void CameraControl()
    {
        float inputX = Input.GetAxisRaw("Mouse X") * h_CamSens * Time.deltaTime;
        float inputY = Input.GetAxisRaw("Mouse Y") * h_CamSens * Time.deltaTime;

        rotY += inputX;
        rotX -= inputY;

        rotX = Mathf.Clamp(rotX, h_CameraRestraintsX, h_CameraRestraintsY);
        h_Cam.gameObject.transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    public void HidingHere(bool state)
    {
        h_IsHiding = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            h_playerRef = other.GetComponent<PlayerController>();
            h_playerRef.PlayerHide(ref h_thisHidingSpot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            h_playerRef.p_canHide = false;
        }
    }
}
