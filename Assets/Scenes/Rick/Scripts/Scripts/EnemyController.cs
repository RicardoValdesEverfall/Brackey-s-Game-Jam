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

    [Header("DEBUG")]
    [SerializeField] public ZoneManager e_ZoneToInvestigate;
    [SerializeField] private Animator e_AnimatorController;
    [SerializeField] private GameObject e_playerObjRef;
    [SerializeField] private Transform e_playerTransformRef;
    [SerializeField] private enum e_States {PATROL, INVESTIGATE, CHASE };
    [SerializeField] private e_States e_currentState;
    [SerializeField] private float e_Rotation;
    [SerializeField] private float e_Speed;
    [SerializeField] private int e_PatrolPointIndex;
    [SerializeField] public bool e_isSeen;
    [SerializeField] private float timeSeen = 0;

    private void Awake()
    {
        if (e_NavMeshAgent == null) { e_NavMeshAgent = GetComponent<NavMeshAgent>(); }
        if (e_AnimatorController == null) { e_AnimatorController = GetComponentInChildren<Animator>(); }
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

    }

    private void EnemyChase()
    {

    }

    private void EnemyDetection()
    {
        if (e_isSeen && e_currentState != e_States.INVESTIGATE) //Goal is to trigger a reaction from the creature then an investigation if the player is directly looking at it for longer than X (X being e_TImeToReact)
        {
            timeSeen += Time.deltaTime;
            if (timeSeen >= e_TimeToReact * e_suspicion)
            {
                e_AnimatorController.Play("LookAround", 0, 0);
            }
            else if (timeSeen >= e_TimeToReact)
            {
                e_currentState = e_States.INVESTIGATE;
                timeSeen = 0;
            }
        }
    }

    public void EnemySetZone(ZoneManager zone)
    {
        e_ZoneToInvestigate = zone;
    }
}
