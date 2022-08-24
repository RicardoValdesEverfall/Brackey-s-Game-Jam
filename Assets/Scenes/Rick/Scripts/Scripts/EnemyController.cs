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

    [Header("DEBUG")]
    [SerializeField] private Animator e_AnimatorController;
    [SerializeField] private GameObject e_playerObjRef;
    [SerializeField] private Transform e_playerTransformRef;
    [SerializeField] private enum e_States {PATROL, INVESTIGATE, CHASE };
    [SerializeField] private e_States e_currentState;
    [SerializeField] private float e_Rotation;
    [SerializeField] private float e_Speed;
    [SerializeField] private int e_PatrolPointIndex;

    private void Awake()
    {
        if (e_NavMeshAgent == null) { e_NavMeshAgent = GetComponent<NavMeshAgent>(); }
        if (e_AnimatorController == null) { e_AnimatorController = GetComponentInChildren<Animator>(); }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        e_Rotation = transform.rotation.y;
        e_Speed = e_NavMeshAgent.speed;

        e_AnimatorController.SetFloat("Speed", e_Speed);
        e_AnimatorController.SetFloat("Rotation", e_Rotation);

        EnemyPatrol();
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

    }
}
