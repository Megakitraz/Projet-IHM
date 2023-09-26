using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Obstacle
{

    /*
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
    */


    public override Vector2 Interaction(Vector2 pos, Vector3 localScale, PlayerController playerController)
    {
        Debug.Log("Script Ground");
        playerController._canDoubleJump = true;
        playerController._isGrounded = true;
        return new Vector2(pos.x, transform.position.y + transform.localScale.y/2f + localScale.y/2f);
    }

    public override void ExitInteraction(PlayerController playerController)
    {
        playerController._isGrounded = false;
    }

}
