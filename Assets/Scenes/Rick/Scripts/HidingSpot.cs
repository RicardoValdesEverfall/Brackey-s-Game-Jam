using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] private PlayerController h_playerRef;
    [SerializeField] private Camera h_Cam;
    [SerializeField] private Camera h_PlayerCamRef;
    [SerializeField] public enum h_Spots {CLOSET, BED, TABLE, FRIDGE, CABINET, LAUNDRY}
    [SerializeField] private h_Spots h_spot;
    [SerializeField] [Range(0.1f, 1f)] private float h_radius;

    [Header("DEBUG")]
    [SerializeField] private bool h_IsHiding;

    private void Awake()
    {
        h_Cam = GetComponentInChildren<Camera>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SphereCollider>().radius = h_radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (h_playerRef != null)
        {
            h_IsHiding = h_playerRef.p_isHiding;
        }

        if (h_IsHiding)
        {
            //Disable player controls
            //Disable player camera
            //Enable hiding camera
            //Enable hiding controls

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            h_playerRef = other.GetComponent<PlayerController>();
            h_playerRef.PlayerHide(this.GetComponent<HidingSpot>());
        }
    }
}
