using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpObstacle : Obstacle
{
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {

        Vector2 newPos = new Vector2(pos.x, transform.position.y + transform.lossyScale.y / 2f + lossyScale.y / 2f);
        playerController._verticalSpeed = 0;
        return newPos;
    }
}
