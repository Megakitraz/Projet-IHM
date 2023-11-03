using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySmoke",2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void DestroySmoke()
    {
        Destroy(gameObject);
    }
}
