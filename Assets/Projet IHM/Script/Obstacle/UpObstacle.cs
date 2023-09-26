using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpObstacle : Obstacle
{
    public override Vector2 Interaction(Vector2 pos, Vector3 localScale, PlayerController playerController)
    {

        Vector2 newPos = new Vector2(pos.x, transform.position.y + transform.localScale.y / 2f + localScale.y / 2f);
        playerController._verticalSpeed = 0;
        return newPos;
    }
}
