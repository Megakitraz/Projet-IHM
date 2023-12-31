using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Obstacle
{


    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {
            Debug.Log("Enter");
            playerController._canDoubleJump = true;
            playerController._isGrounded = true;
            return new Vector2(pos.x, transform.position.y + transform.lossyScale.y / 2f + lossyScale.y / 2f);
        
        
    }

    public override void ExitInteraction(PlayerController playerController)
    {
        //Debug.Log("Exit");
        playerController._isGrounded = false;
    }

}
