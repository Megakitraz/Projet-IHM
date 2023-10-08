using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObstacle : Obstacle
{
    private Side _lastSideUsed = Side.None;
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {
        Vector2 newPos = pos;
        _lastSideUsed = SideOfTheObstacle(raycastHit2D.point, newPos , raycastHit2D.transform);
        //Debug.Log("Side = " + _lastSideUsed.ToString());

        switch (_lastSideUsed)
        {
            case Side.Up:
                playerController._canDoubleJump = true;
                playerController._isGrounded = true;
                newPos = new Vector2(pos.x, transform.position.y + transform.lossyScale.y / 2f + lossyScale.y * 10000f / 20001f);
                break;
            case Side.Down:
                newPos = new Vector2(pos.x, transform.position.y - transform.lossyScale.y / 2f - lossyScale.y / 2f);
                playerController._verticalSpeed = 0;
                break;
            case Side.Left:
                newPos = new Vector2(transform.position.x - transform.lossyScale.x / 2f - lossyScale.x / 2f, pos.y);
                break;
            case Side.Right:
                newPos = new Vector2(transform.position.x + transform.lossyScale.x / 2f + lossyScale.x / 2f, pos.y);
                break;
        }

        return newPos;
    }

    public override void ExitInteraction(PlayerController playerController)
    {
        //Debug.Log("Exit");


        if(_lastSideUsed == Side.Up) playerController._isGrounded = false;
    }
}
