using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float _speed;
    public float _currentSpeed;
    public float _gravity;
    public float _jumpSpeed;
    public bool _isGrounded = false; //todo
    public float _verticalSpeed;
    public bool _canDoubleJump = true;
    public RaycastHit2D _lastRaycastHitDownLeft2D;
    public RaycastHit2D _lastRaycastHitDownRight2D;
    public RaycastHit2D _lastRaycastHitUpLeft2D;
    public RaycastHit2D _lastRaycastHitUpRight2D;

    public bool _lastpressedA = false;

    private Gamepad gamepad;

    private Vector2 _wantedPosition;


    private void LastMovement(Vector2 wantedPos)
    {
        //Vector2 newWantedPosition = wantedPos;
        RaycastHit2D hitDownLeft = Physics2D.Raycast(wantedPos + new Vector2(-transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x , wantedPos.y-transform.position.y),Vector2.Distance(wantedPos,transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(-transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y),Color.red,0.5f,false);

        RaycastHit2D hitDownRight = Physics2D.Raycast(wantedPos + new Vector2(transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.blue, 0.5f, false);

        RaycastHit2D hitUpLeft = Physics2D.Raycast(wantedPos + new Vector2(-transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(-transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.green, 0.5f, false);

        RaycastHit2D hitUpRight = Physics2D.Raycast(wantedPos + new Vector2(transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.yellow, 0.5f, false);

        if (hitDownRight.transform != null)
        {
            //Debug.Log("hitDownRight = " + hitDownRight.transform);
            if (_lastRaycastHitDownRight2D.transform != hitDownRight.transform)
            {
                if (hitDownRight.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
                {
                    wantedPos = obstacle.Interaction(wantedPos, transform.localScale, this);

                    if (_lastRaycastHitDownRight2D.transform != null)
                    {
                        if (_lastRaycastHitDownRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                        {
                            obscaleExit.ExitInteraction(this);
                        }
                    }

                    
                }

                _lastRaycastHitDownRight2D = hitDownRight; // At the end of this if
            }

            

        }

        if (hitUpRight.transform != null)
        {
            //Debug.Log("hitUPRight = " + hitUpRight.transform);

            if (hitUpRight.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                wantedPos = obstacle.Interaction(wantedPos, transform.localScale, this);


                if (_lastRaycastHitUpRight2D.transform != null)
                {
                    if (_lastRaycastHitUpRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                }

                _lastRaycastHitUpRight2D = hitUpRight; // At the end of this if
            }


        }



        if (hitUpLeft.transform != null)
        {
            //Debug.Log("hitUpLeft = " + hitUpLeft.transform);

            if (hitUpLeft.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                wantedPos = obstacle.Interaction(wantedPos, transform.localScale, this);

                if (_lastRaycastHitUpLeft2D.transform != null)
                {
                    if (_lastRaycastHitUpLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                }
                _lastRaycastHitUpLeft2D = hitUpLeft; // At the end of this if

            }

            



        }


        if (hitDownLeft.transform!=null)
        {
            Debug.Log("hitDownLeft = " + hitDownLeft.transform);
            Debug.Log("_lastRaycastHitDownLeft2D = " + _lastRaycastHitDownLeft2D.transform);

            if (_lastRaycastHitDownLeft2D.transform != hitDownLeft.transform)
            {
                
                if (hitDownLeft.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
                {
                    wantedPos = obstacle.Interaction(wantedPos, transform.localScale, this);

                    // At the end of this if
                    if (_lastRaycastHitDownLeft2D.transform != null)
                    {
                        if (_lastRaycastHitDownLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                        {
                            obscaleExit.ExitInteraction(this);
                        }
                    }

                    //Debug.Log("Ici");
                    _lastRaycastHitDownLeft2D = hitDownLeft;
                }
                
            }

            

        }
        
        
        
        if(!(hitDownLeft.transform != null && hitDownRight.transform != null && hitUpLeft.transform != null && hitUpRight.transform != null))
        {
            wantedPos = new Vector3(wantedPos.x,wantedPos.y,transform.position.z);

            if (_lastRaycastHitDownLeft2D.transform != null)
            {
                if (_lastRaycastHitDownLeft2D != hitDownLeft)
                {
                    if (_lastRaycastHitDownLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                    _lastRaycastHitDownLeft2D = hitDownLeft;
                }  
            }


            if (_lastRaycastHitDownRight2D.transform != null)
            {
                if (_lastRaycastHitDownRight2D != hitDownLeft)
                {
                    if (_lastRaycastHitDownRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                    _lastRaycastHitDownRight2D = hitDownLeft;
                }
            }


            if (_lastRaycastHitUpLeft2D.transform != null)
            {
                if (_lastRaycastHitUpLeft2D != hitDownLeft)
                {
                    if (_lastRaycastHitUpLeft2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                    _lastRaycastHitUpLeft2D = hitDownLeft;
                }
            }


            if (_lastRaycastHitUpRight2D.transform != null)
            {
                if (_lastRaycastHitUpRight2D != hitDownLeft)
                {
                    if (_lastRaycastHitUpRight2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                    _lastRaycastHitUpRight2D = hitDownLeft;
                }
            }
        }

        transform.position = wantedPos;

    }
    private void CheckPressedKeys()
    {
        if (!InputManager._jump.IsPressed())
        {
            _lastpressedA = false;
        }
        else
        {
            _lastpressedA = true;
        }
    }
    private void VerticalMovement()
    {
        if (_isGrounded) {_verticalSpeed=0;}
        else
        {
            _verticalSpeed -= _gravity*Time.deltaTime;
        }

        if (_isGrounded)
        {
            if (InputManager._jump.IsPressed())
            {
                _verticalSpeed += _jumpSpeed;
                
            }
        }
        else
        {
            if (InputManager._jump.IsPressed() && _canDoubleJump && !_lastpressedA)
            {
                _canDoubleJump = false;
                _verticalSpeed = _jumpSpeed;
            }
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y + _verticalSpeed*Time.deltaTime, transform.position.z);
        //LastMovement(new Vector2(transform.position.x, transform.position.y + _verticalSpeed*Time.deltaTime));
        _wantedPosition = new Vector2(_wantedPosition.x, _wantedPosition.y + _verticalSpeed * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y + _verticalSpeed*Time.deltaTime, transform.position.z);

    }
    private void Movement()
    {
        if (InputManager._direction.ReadValue<Vector2>().x>0)
        {
            _currentSpeed = _speed;
        }
        
        else 
            if (InputManager._direction.ReadValue<Vector2>().x < 0)
        {
           _currentSpeed = -_speed; 
        }
            else 
            {
                _currentSpeed = 0;
            }
        //transform.position = new Vector3(transform.position.x + _currentSpeed*Time.deltaTime, transform.position.y, transform.position.z);
        //LastMovement(new Vector2(transform.position.x + _currentSpeed * Time.deltaTime, transform.position.y));
        _wantedPosition = new Vector2(_wantedPosition.x + _currentSpeed * Time.deltaTime, _wantedPosition.y);
    }

    void Update()
    {
        _wantedPosition = transform.position;
        Movement();
        VerticalMovement();
        CheckPressedKeys();
    }

    private void LateUpdate()
    {
        LastMovement(_wantedPosition);
    }

}
