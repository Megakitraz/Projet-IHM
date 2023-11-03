using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _speed;
    public float _maxSpeed;
    public float _currentSpeed;
    public float _gravity;
    public float _jumpSpeed;
    public bool _isGrounded = false;
    public bool _isOnTrampoline = false;
    public bool _isOnWall = false;//todo
    public float _verticalSpeed;
    public bool _canDoubleJump = true;
    public RaycastHit2D _lastRaycastHitDownLeft2D;
    public RaycastHit2D _lastRaycastHitDownRight2D;
    public RaycastHit2D _lastRaycastHitUpLeft2D;
    public RaycastHit2D _lastRaycastHitUpRight2D;

    private Vector2 _wantedPosition;

    [SerializeField] private float _inertieTime;
    [SerializeField] private float _currentInertie;
    [SerializeField] private float _dashPower;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashDuration;
    private float _timeLastDash;


    [SerializeField] private float _wallJumpDuration;
    private float _timeLastWallJump;
    [SerializeField] private float _wallJumpPower;
    [SerializeField] private float _wallJumpDashOutPower;
    [SerializeField] private float _fallSpeedOnWall;
    private Obstacle.Side _leftRightWallJump;

    [SerializeField] private GameObject _puffVFX;
    [SerializeField] private GameObject _puffVFXred;
    [SerializeField] private GameObject _puffVFXblack;

    public Vector2 _directionMovement;


    private void LastMovement(Vector2 wantedPos)
    {
        //Vector2 newWantedPosition = wantedPos;
        //Debug.Log(transform.lossyScale);
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 offset = new Vector2(0f, 0f);
        Vector2 directionUpMovement = new Vector2(Vector3.Cross(_directionMovement, Vector3.forward).x, Vector3.Cross(_directionMovement, Vector3.forward).y);
        if (position2D != wantedPos) offset = (wantedPos - position2D).normalized;

        RaycastHit2D hitDownLeft = Physics2D.Raycast(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x , wantedPos.y-transform.position.y),Vector2.Distance(wantedPos,transform.position), 3);
        //RaycastHit2D hitDownLeft = Physics2D.Raycast(position2D + offset / 100f + (-transform.lossyScale.x * _directionMovement - transform.lossyScale.y * directionUpMovement) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.red, 0.5f, false);

        RaycastHit2D hitDownRight = Physics2D.Raycast(position2D + offset / 100f + new Vector2(transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        //RaycastHit2D hitDownRight = Physics2D.Raycast(position2D + offset / 100f + (transform.lossyScale.x * _directionMovement - transform.lossyScale.y * directionUpMovement) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.blue, 0.5f, false);

        RaycastHit2D hitUpLeft = Physics2D.Raycast(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        //RaycastHit2D hitUpLeft = Physics2D.Raycast(position2D + offset / 100f + (-transform.lossyScale.x * _directionMovement + transform.lossyScale.y * directionUpMovement) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.green, 0.5f, false);

        RaycastHit2D hitUpRight = Physics2D.Raycast(position2D + offset / 100f + new Vector2(transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        //RaycastHit2D hitUpRight = Physics2D.Raycast(position2D + offset / 100f + (transform.lossyScale.x * _directionMovement + transform.lossyScale.y * directionUpMovement) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.yellow, 0.5f, false);



        // Down Right RayCast2D

        if (hitDownRight.transform != null &&
            hitDownRight.transform != _lastRaycastHitDownLeft2D.transform)
        {

            if (hitDownRight.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                wantedPos = obstacle.Interaction(wantedPos, transform.lossyScale, this, hitDownRight, Obstacle.Side.Down, Obstacle.Side.Right);    
            }

            

        }

        if (_lastRaycastHitDownRight2D.transform != null &&
            _lastRaycastHitDownRight2D.transform != hitDownLeft.transform &&
            _lastRaycastHitDownRight2D.transform != hitDownRight.transform &&
            _lastRaycastHitDownRight2D.transform != hitUpLeft.transform &&
            _lastRaycastHitDownRight2D.transform != hitUpRight.transform)
        {
            if (_lastRaycastHitDownRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacleExit))
            {
                //Debug.Log("Exit Down Right");
                obstacleExit.ExitInteraction(this);
            }
        }


        // Down Left RayCast2D


        if (hitDownLeft.transform != null &&
            hitDownLeft.transform != _lastRaycastHitDownRight2D.transform)
        {
            //Debug.Log("hitDownLeft = " + hitDownLeft.transform);
            //Debug.Log("_lastRaycastHitDownLeft2D = " + _lastRaycastHitDownLeft2D.transform);



            if (hitDownLeft.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                wantedPos = obstacle.Interaction(wantedPos, transform.lossyScale, this, hitDownLeft, Obstacle.Side.Down, Obstacle.Side.Left);
            }
        }

        //Debug.Log("_lastRaycastHitDownLeft2D = " + _lastRaycastHitDownLeft2D.transform + "\n hitDownLeft" + hitDownLeft.transform + "\n hitDownRight" + hitDownRight.transform + "\n hitUpLeft" + hitUpLeft.transform + "\n hitUpRight" + hitUpRight.transform );


        if (_lastRaycastHitDownLeft2D.transform != null &&
                    _lastRaycastHitDownLeft2D.transform != hitDownLeft.transform &&
                    _lastRaycastHitDownLeft2D.transform != hitDownRight.transform &&
                    _lastRaycastHitDownLeft2D.transform != hitUpLeft.transform &&
                    _lastRaycastHitDownLeft2D.transform != hitUpRight.transform)
        {
            if (_lastRaycastHitDownLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
            {
                //Debug.Log("Exit Down Left");
                obscaleExit.ExitInteraction(this);
            }
        }




        // Up Right RayCast2D

        if (hitUpRight.transform != null 
            //hitUpRight.transform != _lastRaycastHitDownRight2D.transform &&
            //hitUpLeft.transform != _lastRaycastHitDownLeft2D.transform &&
            //hitUpLeft.transform != _lastRaycastHitUpLeft2D.transform
            )
        {
            Debug.Log("hitUPRight = " + hitUpRight.transform);

            if (hitUpRight.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                wantedPos = obstacle.Interaction(wantedPos, transform.lossyScale, this, hitUpRight, Obstacle.Side.Up, Obstacle.Side.Right);
            }


        }

        if (_lastRaycastHitUpRight2D.transform != null && 
            _lastRaycastHitUpRight2D.transform != hitDownLeft.transform && 
            _lastRaycastHitUpRight2D.transform != hitDownRight.transform && 
            _lastRaycastHitUpRight2D.transform != hitUpLeft.transform && 
            _lastRaycastHitUpRight2D.transform != hitUpRight.transform)
        {
            if (_lastRaycastHitUpRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
            {
                obscaleExit.ExitInteraction(this);
            }
        }
        
        




        // Up Left RayCast2D

        if (hitUpLeft.transform != null 
            //hitUpRight.transform != _lastRaycastHitDownRight2D.transform &&
            //hitUpLeft.transform != _lastRaycastHitDownLeft2D.transform &&
            //hitUpLeft.transform != _lastRaycastHitUpRight2D.transform
            )
        {
            //Debug.Log("hitUpLeft = " + hitUpLeft.transform);

            if (hitUpLeft.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                wantedPos = obstacle.Interaction(wantedPos, transform.lossyScale, this , hitUpLeft, Obstacle.Side.Up, Obstacle.Side.Left);
            }
        }

        if (_lastRaycastHitUpLeft2D.transform != null &&
                    _lastRaycastHitUpLeft2D.transform != hitDownLeft.transform &&
                    _lastRaycastHitUpLeft2D.transform != hitDownRight.transform &&
                    _lastRaycastHitUpLeft2D.transform != hitUpLeft.transform &&
                    _lastRaycastHitUpLeft2D.transform != hitUpRight.transform)
        {
            if (_lastRaycastHitUpLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
            {
                obscaleExit.ExitInteraction(this);
            }
        }
        
        


        
        
        




        // No RayCast

        if (hitDownLeft.transform == null && hitDownRight.transform == null && hitUpLeft.transform == null && hitUpRight.transform == null)
        {
            wantedPos = new Vector3(wantedPos.x,wantedPos.y,transform.position.z);

            if (_lastRaycastHitDownLeft2D.transform != null)
            {
                if (_lastRaycastHitDownLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                {
                    obscaleExit.ExitInteraction(this);
                }
                _lastRaycastHitDownLeft2D = hitDownLeft;
            }


            if (_lastRaycastHitDownRight2D.transform != null)
            {
               if (_lastRaycastHitDownRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
               {
                   obscaleExit.ExitInteraction(this);
               }
               _lastRaycastHitDownRight2D = hitDownLeft;
            }


            if (_lastRaycastHitUpLeft2D.transform != null)
            {
               if (_lastRaycastHitUpLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
               {
                    obscaleExit.ExitInteraction(this);
               }
               _lastRaycastHitUpLeft2D = hitDownLeft;
            }


            if (_lastRaycastHitUpRight2D.transform != null)
            {
                if (_lastRaycastHitUpRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                {
                    obscaleExit.ExitInteraction(this);
                }
                _lastRaycastHitUpRight2D = hitDownLeft;
            }
        }

        _lastRaycastHitDownRight2D = hitDownRight;
        _lastRaycastHitUpRight2D = hitUpRight;
        _lastRaycastHitUpLeft2D = hitUpLeft;
        _lastRaycastHitDownLeft2D = hitDownLeft;

        transform.position = wantedPos;

    }

    private void VerticalMovement()
    {
        if (InputManager._jump == null) return;
        if (_isGrounded || _isOnTrampoline)
        {
            _verticalSpeed=0;
        }
        else if(_isOnWall)
        {
            if(_verticalSpeed > 0)
            {
                _verticalSpeed -= _gravity * Time.deltaTime;
            }
            else
            {
                _verticalSpeed -= _gravity * Time.deltaTime * Mathf.Abs(_fallSpeedOnWall);
            }
            
        }
        else
        {
            _verticalSpeed -= _gravity*Time.deltaTime;
        }

        if (_isGrounded || _isOnTrampoline)
        {
            if (InputManager._jumpKeyDown)
            {
                _verticalSpeed += _jumpSpeed;
                Instantiate(_puffVFX,transform.position,Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("jump");
            }
        }
        else if (_isOnWall)
        {
            if (InputManager._jumpKeyDown)
            {
                _verticalSpeed = _jumpSpeed;
                Instantiate(_puffVFXblack,transform.position,Quaternion.identity);
            };
        }
        else
        {
            if (InputManager._jumpKeyDown && _canDoubleJump)
            {
                _canDoubleJump = false;
                _verticalSpeed = _jumpSpeed;
                Instantiate(_puffVFXred,transform.position,Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("double_jump");
            }
        }


        _wantedPosition = new Vector2(_wantedPosition.x, _wantedPosition.y + _verticalSpeed * transform.lossyScale.y * Time.deltaTime);

    }



    public void ForcedJump(float jumpPower)
    {
        //Debug.Log("ForcedJump");
        _verticalSpeed = _jumpSpeed * jumpPower;
        Instantiate(_puffVFX,transform.position,Quaternion.identity);
        //_wantedPosition = new Vector2(_wantedPosition.x, _wantedPosition.y + _jumpSpeed * jumpPower * transform.lossyScale.y * Time.deltaTime);
    }

    public void WallJump(Obstacle.Side leftRight)
    {
        if (InputManager._jumpKeyDown)
        {
            _leftRightWallJump = leftRight;
            _verticalSpeed = _jumpSpeed * _wallJumpPower;
            _timeLastWallJump = Time.realtimeSinceStartup;
        }
    
    }


    private void Movement()
    {
        
        if (InputManager._direction == null) return;


        float InputDirection = InputManager._direction.ReadValue<Vector2>().x;
        bool InputDash = false;

        if (InputManager._dashKeyDown && (Time.realtimeSinceStartup - _timeLastDash) > _dashCooldown)
        {
            //Debug.Log("Time since last dash = " + (Time.realtimeSinceStartup - _timeLastDash));
            InputDash = true;
            _timeLastDash = Time.realtimeSinceStartup;
            
        }

        if (Time.realtimeSinceStartup - _timeLastWallJump <= _wallJumpDuration)
        {

            if (Time.realtimeSinceStartup - _timeLastWallJump <= _wallJumpDuration / 4f)
            {
                if (_leftRightWallJump == Obstacle.Side.Right)
                {
                    _currentInertie = 1;
                }
                else if (_leftRightWallJump == Obstacle.Side.Left)
                {
                    _currentInertie = -1;
                }
                else
                {
                    _currentInertie = 0;
                }
            }
            else
            {
                if (_leftRightWallJump == Obstacle.Side.Right)
                {
                    _currentInertie += Time.deltaTime / _inertieTime;
                }
                else if (_leftRightWallJump == Obstacle.Side.Left)
                {
                    _currentInertie -= Time.deltaTime / _inertieTime;
                }
                else
                {
                    if (_currentInertie > 0)
                    {
                        _currentInertie -= Time.deltaTime / _inertieTime;
                        if (_currentInertie < 0) _currentInertie = 0f;
                    }
                    else if (_currentInertie < 0)
                    {
                        _currentInertie += Time.deltaTime / _inertieTime;
                        if (_currentInertie > 0) _currentInertie = 0f;
                    }
                }

                _currentInertie = Mathf.Clamp(_currentInertie, -1f, 1f);
            }


            _currentSpeed = Mathf.Lerp(-1, 1, 0.5f + _currentInertie / 2f) * _maxSpeed * _wallJumpDashOutPower;
        }
        else if (InputDash || Time.realtimeSinceStartup - _timeLastDash <= _dashDuration)
        {

            if(Time.realtimeSinceStartup - _timeLastDash == 0)
            {
                if (InputDirection > 0)
                {
                    _currentInertie = 1;
                }
                else if (InputDirection < 0)
                {
                    _currentInertie = -1;
                }
                else
                {
                    _currentInertie = 0;
                }
            }
            else
            {
                if (InputDirection > 0)
                {
                    _currentInertie += Time.deltaTime / _inertieTime;
                }
                else if (InputDirection < 0)
                {
                    _currentInertie -= Time.deltaTime / _inertieTime;
                }
                else
                {
                    if (_currentInertie > 0)
                    {
                        _currentInertie -= Time.deltaTime / _inertieTime;
                        if (_currentInertie < 0) _currentInertie = 0f;
                    }
                    else if (_currentInertie < 0)
                    {
                        _currentInertie += Time.deltaTime / _inertieTime;
                        if (_currentInertie > 0) _currentInertie = 0f;
                    }
                }

                _currentInertie = Mathf.Clamp(_currentInertie, -(0.5f + Mathf.Abs(InputDirection) / 2f), 0.5f + Mathf.Abs(InputDirection) / 2f);
            }
            

            _currentSpeed = Mathf.Lerp(-1 , 1, 0.5f + _currentInertie / 2f) * _maxSpeed * _dashPower;
        }
        else
        {
            if (InputDirection > 0)
            {
                _currentInertie += Time.deltaTime / _inertieTime;
            }
            else if (InputDirection < 0)
            {
                _currentInertie -= Time.deltaTime / _inertieTime;
            }
            else
            {
                if (_currentInertie > 0)
                {
                    _currentInertie -= Time.deltaTime / _inertieTime;
                    if (_currentInertie < 0) _currentInertie = 0f;
                }
                else if (_currentInertie < 0)
                {
                    _currentInertie += Time.deltaTime / _inertieTime;
                    if (_currentInertie > 0) _currentInertie = 0f;
                }
            }

            _currentInertie = Mathf.Clamp(_currentInertie, - (0.5f + Mathf.Abs(InputDirection)/2f), 0.5f + Mathf.Abs(InputDirection) / 2f);

            _currentSpeed = Mathf.Lerp(-1 , 1, 0.5f + _currentInertie / 2f) * _maxSpeed;
        }


        //_wantedPosition = new Vector2(_wantedPosition.x + _currentSpeed * transform.lossyScale.x * Time.deltaTime, _wantedPosition.y);
        _wantedPosition = new Vector2(_wantedPosition.x + (_currentSpeed * transform.lossyScale.x * Time.deltaTime) * _directionMovement.x,
            _wantedPosition.y + (_currentSpeed * transform.lossyScale.x * Time.deltaTime) * _directionMovement.y);
    }

    private void Start()
    {
        _currentInertie = 0;
        _timeLastDash = -_dashCooldown;
        _directionMovement = new Vector2(1, 0);
    }

    void Update()
    {
        _wantedPosition = transform.position;
        Movement();
        VerticalMovement();
    }

    private void LateUpdate()
    {
        LastMovement(_wantedPosition);
    }

}
