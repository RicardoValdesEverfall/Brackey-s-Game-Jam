using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("LOCOMOTION")]
    [SerializeField][Range(10, 100)] private float p_MovementSpeed;

    [Header("PLAYER UI")]
    [SerializeField] private Animator p_UIAnimator;

    [Header("DEBUG")]
    [SerializeField] public Transform p_Transform;
    [SerializeField] private Camera p_Cam;
    [SerializeField] public bool p_isHiding;
    [SerializeField] public bool p_canHide;
    [SerializeField] private HidingSpot p_CurrentHidingSpot;

    private void Awake()
    {
        p_Cam = GetComponentInChildren<Camera>();
        p_Transform = GetComponent<Transform>();
        p_UIAnimator = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    private void Update()
    {
        if (p_canHide == true)
        {
            PlayerInput();
        } 
    }

    private void FixedUpdate()
    {

    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && p_isHiding == false && p_canHide)
        {
            p_UIAnimator.SetTrigger("Fade");
            p_isHiding = true;
            p_Cam.enabled = false;
            p_CurrentHidingSpot.HidingHere(true);
        }
    }

    public void PlayerHide(ref HidingSpot spot)
    {
        p_CurrentHidingSpot = spot;
        p_canHide = true;
    }

    public bool GetPlayerState()
    {
        return p_isHiding;
    }
}
