using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("LOCOMOTION")]
    [SerializeField][Range(10, 100)] private float p_MovementSpeed;

    [Header("DEBUG")]
    [SerializeField] public Transform p_Transform;
    [SerializeField] private Camera p_Cam;
    [SerializeField] public bool p_isHiding;
    [SerializeField] private HidingSpot p_CurrentHidingSpot;

    void Start()
    {

    }

    private void Update()
    {
        if (p_isHiding == false)
        {
            PlayerInput();
        } 
    }

    private void FixedUpdate()
    {

    }

    private void PlayerInput()
    {

    }


    public void PlayerHide(HidingSpot spot)
    {
        if (Input.GetKeyDown(KeyCode.E) && p_isHiding == false)
        {
            p_isHiding = true;
        }
    }

    public bool GetPlayerState()
    {
        return p_isHiding;
    }
}
