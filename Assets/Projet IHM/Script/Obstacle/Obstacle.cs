using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected enum Side {Left, Right, Up, Down, None };

    public virtual Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D) {
        Debug.Log("Script Obstacle");
        return pos;
    }

    public virtual void ExitInteraction(PlayerController playerController) { }


    protected Side SideOfTheObstacle(Vector2 position, Transform obstacle)
    {
        if (obstacle == null) 
        { 
            return Side.None;
            //Debug.Log("None");
        }
        float distanceUp = Mathf.Abs(position.y - (obstacle.position.y + obstacle.lossyScale.y / 2f));
        float distanceDown = Mathf.Abs(position.y - (obstacle.position.y - obstacle.lossyScale.y / 2f));
        float distanceRight = Mathf.Abs(position.x - (obstacle.position.x + obstacle.lossyScale.x / 2f));
        float distanceLeft = Mathf.Abs(position.x - (obstacle.position.x - obstacle.lossyScale.x / 2f));

        //float minDistance = Mathf.Min(Mathf.Min(distanceUp, distanceDown),Mathf.Min(distanceRight, distanceLeft));
        float minDistance = distanceUp;
        Side collisionSide = Side.Up;
        if(minDistance > distanceDown)
        {
            minDistance = distanceDown;
            collisionSide = Side.Down;
        }
        if (minDistance > distanceLeft)
        {
            minDistance = distanceLeft;
            collisionSide = Side.Left;
        }
        if (minDistance > distanceRight)
        {
            minDistance = distanceRight;
            collisionSide = Side.Right;
        }
        return collisionSide;



    }
}
