using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController._canDoubleJump = true;
            playerController._isGrounded = true;
            playerController.transform.position = new Vector3(playerController.transform.position.x, transform.position.y+transform.localScale.y, playerController.transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController._isGrounded = false;
        }
    }
}
