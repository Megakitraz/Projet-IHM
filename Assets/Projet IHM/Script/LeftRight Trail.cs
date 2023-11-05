using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightTrail : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    Color tmp;

    void Start()
    {
        Invoke("DestroyTrail",1f);
    }
    void Update()
    {
        tmp = spriteRenderer.color;
        tmp.a -= 2f * Time.deltaTime;
        spriteRenderer.color = tmp;
    }

        void DestroyTrail()
    {
        Destroy(gameObject);
    }
}
