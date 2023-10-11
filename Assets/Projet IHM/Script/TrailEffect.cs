using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    
    
    void Start()
    {
        InstantiateTrail();
    }

    void InstantiateTrail()
    {
        GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.identity);
        Invoke("InstantiateTrail",0.05f);
    }
}
