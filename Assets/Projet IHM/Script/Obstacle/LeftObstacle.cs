using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftObstacle : Obstacle
{
    public override Vector2 Interaction(Vector2 pos, Vector3 localScale, PlayerController playerController)
    {

        Vector2 newPos = new Vector2(transform.position.x - transform.localScale.x / 2f - localScale.x / 2f, pos.y);
        return newPos;
    }
}