using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomObstacle : Obstacle
{
    /*
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.transform.position = new 
            Vector3(playerController.transform.position.x, transform.position.y-transform.localScale.y+playerController.transform.localScale.y, playerController.transform.position.z);
            playerController._verticalSpeed = 0;
        }
    }
    */
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D)
    {

        Vector2 newPos = new Vector2(pos.x, transform.position.y - transform.lossyScale.y/2f - lossyScale.y/2f);
        playerController._verticalSpeed = 0;
        return newPos;
    }

}
