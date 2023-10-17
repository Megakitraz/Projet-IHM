using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyUpObstacle : Obstacle
{
    private Side _lastSideUsed = Side.None;

    [SerializeField] private float _sensibilityGoingDown = 0.5f;
    [SerializeField] private float _cooldownUseThisPlateforme = 0.2f;

    private float _lastTimeGoDown;

    private Coroutine _coroutinePlayerGoDown;


    private void Start()
    {
        _lastTimeGoDown = -_cooldownUseThisPlateforme;
    }
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {
        Vector2 newPos = pos;
        _lastSideUsed = SideOfTheObstacle(raycastHit2D.point, newPos, raycastHit2D.transform);
        //Debug.Log("Side = " + _lastSideUsed.ToString());

        switch (_lastSideUsed)
        {
            case Side.Up:
                if (UpDown != Side.Down || Time.time - _lastTimeGoDown < _cooldownUseThisPlateforme) break;
                playerController._canDoubleJump = true;
                playerController._isGrounded = true;
                newPos = new Vector2(pos.x, transform.position.y + transform.lossyScale.y / 2f + lossyScale.y * 10000f / 20001f);
                if (_coroutinePlayerGoDown == null) _coroutinePlayerGoDown = StartCoroutine(PlayerGoDOwn(playerController, -Mathf.Clamp01(Mathf.Abs(_sensibilityGoingDown))));
                break;
        }

        return newPos;
    }



    IEnumerator PlayerGoDOwn(PlayerController playerController , float sensibilityGoingDown)
    {
        while (enabled)
        {
            
            if (InputManager._direction.ReadValue<Vector2>().y < sensibilityGoingDown)
            {
                Debug.Log("Go Down");
                ExitInteraction(playerController);
                _lastTimeGoDown = Time.time;
            }

            yield return new WaitForEndOfFrame();
        }
    }



    public override void ExitInteraction(PlayerController playerController)
    {
        //Debug.Log("Exit");


        if (_lastSideUsed == Side.Up) playerController._isGrounded = false;
        if (_coroutinePlayerGoDown != null)
        {
            StopCoroutine(_coroutinePlayerGoDown);
            _coroutinePlayerGoDown = null;
        }
    }
}
