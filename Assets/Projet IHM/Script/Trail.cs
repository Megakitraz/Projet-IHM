using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private GameObject trail;
   
    void Start()
    {
        Invoke("DestroyTrail",1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x*0.99f, transform.localScale.y*0.99f, transform.localScale.z);
    }
    void DestroyTrail()
    {
        Destroy(trail);
    }
}
