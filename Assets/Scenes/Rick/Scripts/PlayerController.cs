using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("PLAYER SETTINGS")]
    [SerializeField] [Range(5, 100)] private int p_sightRange;
    [SerializeField] private LayerMask c_LayerForRay;

    [Header("LOCOMOTION")]
    [SerializeField][Range(10, 100)] private float p_MovementSpeed;

    [Header("PLAYER UI")]
    [SerializeField] private Animator p_UIAnimator;
    [SerializeField] private Animator p_ScoreToAddAnim;
    [SerializeField] private Animator p_ScoreTextAnim;
    [SerializeField] private TextMeshProUGUI p_scoreText;
    [SerializeField] private TextMeshProUGUI p_scoreToAdd;
    [SerializeField] private GameObject p_scoreObj;

    [Header("DEBUG")]
    [SerializeField] private GameObject p_RefToEnemy;
    [SerializeField] private EnemyController p_RefToEnemyController;
    [SerializeField] public Transform p_Transform;
    [SerializeField] private Camera p_Cam;
    [SerializeField] private HidingSpot p_CurrentHidingSpot;
    [SerializeField] public RaycastHit c_ObjHit;
    [SerializeField] public bool p_isHiding;
    [SerializeField] public bool p_canHide;
    [SerializeField] private int p_Score;


    private void Awake()
    {
        if (p_RefToEnemy == null)
        {
            p_RefToEnemy = GameObject.FindGameObjectWithTag("Enemy");
            p_RefToEnemyController = p_RefToEnemy.GetComponent<EnemyController>();
        }

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

        PlayerDetect();
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

    private void PlayerDetect()
    {
        if (Physics.Raycast(p_Cam.transform.position, p_Cam.transform.TransformDirection(Vector3.forward), out c_ObjHit, p_sightRange, c_LayerForRay)) //Checks if the player is looking at something they can interact with
        {
            Debug.DrawRay(p_Cam.transform.position, p_Cam.transform.TransformDirection(Vector3.forward) * c_ObjHit.distance, Color.yellow);
            if (c_ObjHit.transform.name == "Enemy")
            {
                p_RefToEnemyController.e_isSeen = true;
            }
            else if(c_ObjHit.transform.name == "TV")
            {
                p_Score++;
                p_scoreToAdd.text = "+" + p_Score.ToString();
                p_ScoreToAddAnim.SetBool("GainingScore", true);
            }
        }

        else //Not looking at anything
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            if (p_ScoreToAddAnim.GetBool("GainingScore") == true)
            {
                int sumScore;
                int.TryParse(p_scoreText.text, out sumScore);
                sumScore += p_Score;
                p_scoreText.text = sumScore.ToString();
                p_Score = 0;

                p_ScoreToAddAnim.SetBool("GainingScore", false);
                p_ScoreTextAnim.SetTrigger("GainedScore");
            }

            if (p_RefToEnemyController.e_isSeen)
            {
                p_RefToEnemyController.e_isSeen = false;
            }
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

    public void PlayerEnableScore(bool state)
    {
        p_scoreObj.SetActive(state);
    }
}
