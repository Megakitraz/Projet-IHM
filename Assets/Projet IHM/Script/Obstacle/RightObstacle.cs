using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightObstacle : Obstacle
{
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {
        Vector2 newPos = new Vector2(transform.position.x + transform.lossyScale.x / 2f + lossyScale.x / 2f, pos.y);
        return newPos;
    }
}
