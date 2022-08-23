using UnityEngine;
using System.Collections;

public class vResetPos : MonoBehaviour
{
    public Transform startPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            other.gameObject.transform.position = startPos.position;
    }

}
