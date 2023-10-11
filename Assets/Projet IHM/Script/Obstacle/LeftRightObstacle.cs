using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightObstacle : Obstacle
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;
    [SerializeField] private bool _goRightFirst = true;
    [SerializeField] private AnimationCurve _speedCurve;



    private float _currentSpeed;
    private Side _lastSideUsed = Side.None;
    private Vector2 _leftToRight;
    private Transform _transformPlayer;

    private Coroutine _coroutinePlayerFollowMovement;



    private void Start()
    {
        StartCoroutine(MovementLeftRight());
    }
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {
        Vector2 newPos = pos;
        _lastSideUsed = SideOfTheObstacle(raycastHit2D.point, newPos , raycastHit2D.transform);
        //Debug.Log("Side = " + _lastSideUsed.ToString());

        switch (_lastSideUsed)
        {
            case Side.Up:
                if (UpDown != Side.Down) break;
                playerController._canDoubleJump = true;
                playerController._isGrounded = true;

                newPos = new Vector2(pos.x, transform.position.y + transform.lossyScale.y / 2f + lossyScale.y * 10000f / 20001f);
                _transformPlayer = playerController.transform;
                if (_coroutinePlayerFollowMovement == null) _coroutinePlayerFollowMovement = StartCoroutine(PlayerFollowMovement());
                //if(_coroutinePlayerFollowMovement.Current == null) StartCoroutine(_coroutinePlayerFollowMovement);

                break;
            case Side.Down:
                if (UpDown != Side.Up) break;
                newPos = new Vector2(pos.x, transform.position.y - transform.lossyScale.y / 2f - lossyScale.y / 2f);
                playerController._verticalSpeed = 0;
                break;
            case Side.Left:
                if (RightLeft != Side.Right) break;
                playerController._isOnWall = true;
                playerController.WallJump(Side.Left);
                newPos = new Vector2(transform.position.x - transform.lossyScale.x / 2f - lossyScale.x / 2f, pos.y);
                break;
            case Side.Right:
                if (RightLeft != Side.Left) break;
                playerController._isOnWall = true;
                playerController.WallJump(Side.Right);
                newPos = new Vector2(transform.position.x + transform.lossyScale.x / 2f + lossyScale.x / 2f, pos.y);
                break;
        }

        return newPos;
    }

    IEnumerator MovementLeftRight()
    {
        _leftToRight = new Vector2(_right.position.x - _left.position.x, _right.position.y - _left.position.y).normalized;
        float distancePlateformRight = 0f;
        float distanceLeftRight = Vector2.Distance(_left.position, _right.position);

        while (enabled)
        {
            distancePlateformRight = Vector2.Distance(transform.position, _right.position);
            _currentSpeed = _speedCurve.Evaluate(distancePlateformRight/distanceLeftRight) * _speed;

            if (_goRightFirst)
            {
                transform.Translate(_leftToRight * _currentSpeed * Time.deltaTime);

                if (distancePlateformRight / distanceLeftRight < 0.05f) _goRightFirst = false;
            }
            else
            {
                _currentSpeed = -_currentSpeed;
                transform.Translate(_leftToRight * _currentSpeed * Time.deltaTime);

                if ((distanceLeftRight - distancePlateformRight) / distanceLeftRight < 0.05f) _goRightFirst = true;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PlayerFollowMovement()
    {
        while (enabled)
        {
            _transformPlayer.Translate(_leftToRight * _currentSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public override void ExitInteraction(PlayerController playerController)
    {
        //Debug.Log("Exit");


        if(_lastSideUsed == Side.Up) playerController._isGrounded = false;
        playerController._isOnWall = false;

        if (_coroutinePlayerFollowMovement != null)
        {
            StopCoroutine(_coroutinePlayerFollowMovement);
            _coroutinePlayerFollowMovement = null;
        }


        
    }
}
