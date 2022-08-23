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
    [SerializeField] private bool p_isHiding;

    void Start()
    {

    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {

    }

    private void PlayerInput()
    {

    }


    public void PlayerHide()
    {

    }

    public bool GetPlayerState()
    {
        return p_isHiding;
    }
}
