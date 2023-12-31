using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Obstacle
{
    private Side _lastSideUsed = Side.None;
    [SerializeField] private float _trampolinePower = 5f;
    [SerializeField] private AnimationCurve _animationTrampolineCurve;
    [SerializeField] private float _animationTime = 0.25f;
    [SerializeField] private string _soundName;

    private Coroutine _coroutineAnimationTrampoline;
    private bool _isOnTrampoline;
    private bool _animationTrampolineIsRunning;

    private void Start()
    {
        _animationTrampolineIsRunning = false;
    }
    public override Vector2 Interaction(Vector2 pos, Vector3 lossyScale, PlayerController playerController, RaycastHit2D raycastHit2D, Side UpDown, Side RightLeft)
    {
        Vector2 newPos = pos;
        _lastSideUsed = SideOfTheObstacle(raycastHit2D.point, newPos, raycastHit2D.transform);
        //Debug.Log("Side = " + _lastSideUsed.ToString());


        switch (_lastSideUsed)
        {
            case Side.Up:
                if (UpDown != Side.Down) break;
                playerController._canDoubleJump = true;
                playerController._isOnTrampoline = true;
                if (!_animationTrampolineIsRunning) _coroutineAnimationTrampoline = StartCoroutine(AnimationTrampoline(playerController, lossyScale));
                //StartCoroutine(AnimationTrampoline(playerController, lossyScale));
                FindObjectOfType<AudioManager>().Play(_soundName);
                //Debug.Log("jumpPower Activate");
                newPos = new Vector2(pos.x, transform.position.y + transform.lossyScale.y / 2f + lossyScale.y * 10000f / 20001f);
                break;
            case Side.Down:
                if (UpDown != Side.Up) break;
                newPos = new Vector2(pos.x, transform.position.y - transform.lossyScale.y / 2f - lossyScale.y / 2f);
                playerController._verticalSpeed = 0;
                break;
            case Side.Left:
                if (RightLeft != Side.Right) break;
                newPos = new Vector2(transform.position.x - transform.lossyScale.x / 2f - lossyScale.x / 2f, pos.y);
                break;
            case Side.Right:
                if (RightLeft != Side.Left) break;
                newPos = new Vector2(transform.position.x + transform.lossyScale.x / 2f + lossyScale.x / 2f, pos.y);
                break;
        }

        return newPos;
    }

    IEnumerator AnimationTrampoline(PlayerController playerController, Vector3 lossyScale)
    {
        _animationTrampolineIsRunning = true;
        float initPosY = transform.position.y;
        float initTime = Time.time;
        float currentTime = 0f;
        Transform transformPlayer = playerController.transform;
        Vector3 lastPosition = transform.position;

        while (currentTime < _animationTime)
        {
            lastPosition = transform.position;
            currentTime = Time.time - initTime;

            transform.position = new Vector3(transform.position.x, initPosY + _animationTrampolineCurve.Evaluate(currentTime / _animationTime), transform.position.z);
            if (playerController._isOnTrampoline) transformPlayer.Translate(transform.position-lastPosition);
            yield return new WaitForEndOfFrame();
        }

        transform.position = new Vector3(transform.position.x, initPosY, transform.position.z);

        if (playerController._isOnTrampoline) 
        {
            playerController._isOnTrampoline = false;
            playerController.ForcedJump(_trampolinePower);
        }
        _animationTrampolineIsRunning = false;
    }

    public override void ExitInteraction(PlayerController playerController)
    {
        //Debug.Log("Exit");


        //if (_lastSideUsed == Side.Up) playerController._isGrounded = false;

        if (_lastSideUsed == Side.Up) playerController._isOnTrampoline = false;
    }
}
