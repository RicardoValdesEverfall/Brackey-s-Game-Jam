using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] public Transform[] z_InvestigatePoints;
    [SerializeField] public GameObject[] z_HidingSpots;

    [Header("DEBUG")]
    [SerializeField] private int z_ZoneIndex;
    [SerializeField] private EnemyController z_RefToEnemy;
    [SerializeField] private GameObject z_RefToPlayer;

    private void Awake()
    {
        z_RefToEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        z_RefToPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            z_RefToEnemy.EnemySetZone(this.GetComponent<ZoneManager>());
        }
    }
}
