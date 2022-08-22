using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("LOCOMOTION")]
    [SerializeField][Range(10, 100)] private float p_MovementSpeed;

    [Header("DEBUG")]
    [SerializeField] public Transform p_Transform;
    [SerializeField] private Vector3 p_MoveDirection;
    [SerializeField] private Rigidbody p_Rb;
    [SerializeField] private Camera p_Cam;
    [SerializeField] private bool p_isHiding;
    [SerializeField] private float p_InputH;
    [SerializeField] private float p_InputV;

    void Start()
    {
        if (p_Rb == null) { p_Rb = GetComponent<Rigidbody>(); }
        p_Rb.freezeRotation = true;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        p_InputH = Input.GetAxisRaw("Horizontal");
        p_InputV = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMove()
    {
        p_MoveDirection = p_Transform.forward * p_InputV + p_Transform.right * p_InputH;
        p_Rb.AddForce(p_MoveDirection.normalized * p_MovementSpeed, ForceMode.Force);
    }

    private void PlayerHide()
    {

    }

    public bool GetPlayerState()
    {
        return p_isHiding;
    }
}
