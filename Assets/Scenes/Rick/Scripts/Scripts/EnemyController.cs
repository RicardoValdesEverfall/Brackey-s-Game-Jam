using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] public NavMeshAgent e_NavMeshAgent;
    [SerializeField][Range(10,100)] private float e_sightRange;
    [SerializeField][Range(10, 100)] private float e_moveSpeed;
    [SerializeField] private Transform[] e_PatrolPoints;
    [SerializeField] private float e_TimeToReact;
    [SerializeField][Range(0.01f, 0.75f)] private float e_suspicion;
    [SerializeField] private LayerMask e_LayerForRay;
    [SerializeField] private Transform e_Head;

    [Header("DEBUG")]
    [SerializeField] public ZoneManager e_ZoneToInvestigate;
    [SerializeField] private Animator e_AnimatorController;
    [SerializeField] private GameObject e_playerObjRef;
    [SerializeField] private Transform e_playerTransformRef;
    [SerializeField] private PlayerController e_playerRef;
    [SerializeField] private RaycastHit e_objHit;
    [SerializeField] private enum e_States {PATROL, INVESTIGATE, CHASE };
    [SerializeField] private e_States e_currentState;
    [SerializeField] private float e_Rotation;
    [SerializeField] private float e_Speed;
    [SerializeField] private int e_PatrolPointIndex;
    [SerializeField] private int e_InvestigatePointIndex;
    [SerializeField] public bool e_isSeen;
    [SerializeField] private float timeSeen = 0;
    [SerializeField] private int e_EnemyLevel;
    [SerializeField] private bool e_ignoreSeen = false;

    private void Awake()
    {
        if (e_NavMeshAgent == null) { e_NavMeshAgent = GetComponent<NavMeshAgent>(); }
        if (e_AnimatorController == null) { e_AnimatorController = GetComponentInChildren<Animator>(); }
        e_playerRef = e_playerObjRef.GetComponent<PlayerController>();
        e_currentState = e_States.PATROL;
    }

    void Start()
    {
        
    }

    void Update()
    {
        e_Rotation = transform.rotation.y;
        e_Speed = e_NavMeshAgent.speed;

        e_AnimatorController.SetFloat("Speed", e_Speed);
        e_AnimatorController.SetFloat("Rotation", e_Rotation);

        EnemyDetection();

        switch (e_currentState)
        {
            case e_States.PATROL:
                EnemyPatrol();
                break;
            case e_States.CHASE:
                EnemyChase();
                break;
            case e_States.INVESTIGATE:
                EnemyInvestigate();
                break;
        }
    }

    private void EnemyPatrol()
    {
        e_NavMeshAgent.SetDestination(e_PatrolPoints[e_PatrolPointIndex].position);
        float distanceRemaining = Vector3.Distance(transform.position, e_NavMeshAgent.destination);
        

        if (distanceRemaining <= e_NavMeshAgent.stoppingDistance)
        {
            if (e_PatrolPointIndex + 1 != e_PatrolPoints.Length)
            {
                e_PatrolPointIndex++;
            }
            else { e_PatrolPointIndex = 0; }
        }  
    }

    private void EnemyInvestigate()
    {
        e_NavMeshAgent.SetDestination(e_ZoneToInvestigate.z_InvestigatePoints[e_InvestigatePointIndex].position);
        float distanceRemaining = Vector3.Distance(transform.position, e_NavMeshAgent.destination);
        
        if (e_NavMeshAgent.isStopped && distanceRemaining > e_NavMeshAgent.stoppingDistance)
        {
            e_NavMeshAgent.isStopped = false;
            e_AnimatorController.applyRootMotion = false;
        }
        
        else if (distanceRemaining <= e_NavMeshAgent.stoppingDistance)
        {
            e_NavMeshAgent.isStopped = true;
            e_AnimatorController.applyRootMotion = true;
            e_AnimatorController.Play("Roar", 0, 0);

            if (e_EnemyLevel > 1)
            {

            }
        }
    }

    private void EnemyChase()
    {
        e_NavMeshAgent.speed += e_EnemyLevel + 1;
    }

    private void EnemyDetection()
    {
        if (e_isSeen && e_currentState != e_States.INVESTIGATE && e_ignoreSeen == false) //Goal is to trigger a reaction from the creature then an investigation if the player is directly looking at it for longer than X (X being e_TImeToReact)
        {
            timeSeen += Time.deltaTime;
            if (timeSeen >= e_TimeToReact * e_suspicion && e_NavMeshAgent.isStopped == false)
            {
                e_NavMeshAgent.isStopped = true;
                e_AnimatorController.applyRootMotion = true;
                e_AnimatorController.SetTrigger("LookAround");
            }

            if (timeSeen >= e_TimeToReact && e_EnemyLevel >= 1)
            {
                e_currentState = e_States.INVESTIGATE;
            }
            else if (timeSeen >= e_TimeToReact)
            {
                e_ignoreSeen = true;
                e_currentState = e_States.PATROL;
            }
        }
        else
        {
            if (timeSeen > 0 && e_ignoreSeen == true)
            {
                timeSeen -= Time.deltaTime;
                if (timeSeen == 0)
                {
                    e_ignoreSeen = false;
                }
            }
        }

        if (Physics.Raycast(e_Head.transform.position, e_Head.transform.TransformDirection(Vector3.forward), out e_objHit, e_sightRange, e_LayerForRay)) //Checks if the player is looking at something they can interact with
        {
            Debug.DrawRay(e_Head.transform.position, e_Head.transform.TransformDirection(Vector3.forward) * e_objHit.distance, Color.yellow);
            if (e_objHit.transform.name == "Player" && e_playerRef.p_isHiding == false && e_currentState != e_States.CHASE)
            {
                e_currentState = e_States.CHASE;
            }
        }

    }

    public void EnemySetZone(ZoneManager zone)
    {
        e_ZoneToInvestigate = zone;
    }
}
