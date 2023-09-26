using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
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
