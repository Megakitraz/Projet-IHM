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


    protected Side SideOfTheObstacle(Vector2 positionCollision, Vector2 positionJoueur , Transform obstacle)
    {
        if (obstacle == null) 
        { 
            return Side.None;
            //Debug.Log("None");
        }
        float distanceUp = Mathf.Abs(positionCollision.y - (obstacle.position.y + obstacle.lossyScale.y / 2f));
        float distanceDown = Mathf.Abs(positionCollision.y - (obstacle.position.y - obstacle.lossyScale.y / 2f));
        float distanceRight = Mathf.Abs(positionCollision.x - (obstacle.position.x + obstacle.lossyScale.x / 2f));
        float distanceLeft = Mathf.Abs(positionCollision.x - (obstacle.position.x - obstacle.lossyScale.x / 2f));

        float distanceUpJoueur = Mathf.Abs(positionJoueur.y - (obstacle.position.y + obstacle.lossyScale.y / 2f));
        float distanceDownJoueur = Mathf.Abs(positionJoueur.y - (obstacle.position.y - obstacle.lossyScale.y / 2f));
        float distanceRightJoueur = Mathf.Abs(positionJoueur.x - (obstacle.position.x + obstacle.lossyScale.x / 2f));
        float distanceLeftJoueur = Mathf.Abs(positionJoueur.x - (obstacle.position.x - obstacle.lossyScale.x / 2f));

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

        switch (collisionSide)
        {
            case Side.Up:
                if (distanceUp == distanceLeft && distanceUpJoueur > distanceLeftJoueur) { 
                    collisionSide = Side.Left;
                    Debug.Log("Coin Left");
                }
                if (distanceUp == distanceRight && distanceUpJoueur > distanceRightJoueur) collisionSide = Side.Right;
                break;
            case Side.Down:
                if (distanceDown == distanceLeft && distanceDownJoueur > distanceLeftJoueur) collisionSide = Side.Left;
                if (distanceDown == distanceRight && distanceDownJoueur > distanceRightJoueur) collisionSide = Side.Right;
                break;
            case Side.Left:
                if (distanceLeft == distanceUp && distanceLeftJoueur > distanceUpJoueur) { 
                    collisionSide = Side.Up;
                    Debug.Log("Coin Up");
                }
                if (distanceLeft == distanceDown && distanceLeftJoueur > distanceDownJoueur) collisionSide = Side.Down;
                break;
            case Side.Right:
                if (distanceRight == distanceUp && distanceRightJoueur > distanceUpJoueur) collisionSide = Side.Up;
                if (distanceRight == distanceDown && distanceRightJoueur > distanceDownJoueur) collisionSide = Side.Down;
                break;
        }
        Debug.Log("collisionSide = " + collisionSide);
        return collisionSide;

    }
}
