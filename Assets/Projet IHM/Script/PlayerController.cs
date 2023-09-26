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
    public RaycastHit2D _lastRaycastHit2D;

    public bool _lastpressedA = false;

    private Gamepad gamepad;

    private Vector2 _wantedPosition;


    private void LastMovement(Vector2 wantedPos)
    {
        Vector2 newWantedPosition = wantedPos;
        RaycastHit2D hitDownLeft = Physics2D.Raycast(wantedPos + new Vector2(-transform.localScale.x, -transform.localScale.y)/2f, new Vector2(wantedPos.x - transform.position.x , wantedPos.y-transform.position.y),Vector2.Distance(wantedPos,transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(-transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y));
        RaycastHit2D hitDownRight = Physics2D.Raycast(wantedPos + new Vector2(transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(transform.localScale.x, -transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y));
        RaycastHit2D hitUpLeft = Physics2D.Raycast(wantedPos + new Vector2(-transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(-transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y));
        RaycastHit2D hitUPRight = Physics2D.Raycast(wantedPos + new Vector2(transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(wantedPos + new Vector2(transform.localScale.x, transform.localScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y));
        if (hitDownLeft.transform!=null)
        {
            Debug.Log("hitDownLeft = " + hitDownLeft.transform);
            if(_lastRaycastHit2D != hitDownLeft)
            {
                if(hitDownLeft.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscale))
                {
                    newWantedPosition = obscale.Interaction(wantedPos, transform.localScale,this);

                    // At the end of this if
                    if (_lastRaycastHit2D.transform != null)
                    {
                        if (_lastRaycastHit2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                        {
                            obscaleExit.ExitInteraction(this);
                        }
                    }
                    
                    _lastRaycastHit2D = hitDownLeft;

                }
            }
            
            
        }
        else if (hitDownRight.transform != null)
        {
            Debug.Log("hitDownRight = " + hitDownRight.transform);
            if (_lastRaycastHit2D != hitDownRight)
            {
                if (hitDownRight.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscale))
                {
                    newWantedPosition = obscale.Interaction(wantedPos, transform.localScale, this);

                    if(_lastRaycastHit2D.transform != null)
                    {
                        if (_lastRaycastHit2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                        {
                            obscaleExit.ExitInteraction(this);
                        }
                    }
                    
                    _lastRaycastHit2D = hitDownRight; // At the end of this if
                }
            }
        }
        else if (hitUpLeft.transform != null)
        {
            Debug.Log("hitUpLeft = " + hitUpLeft.transform);
            if (_lastRaycastHit2D != hitUpLeft)
            {
                if (hitUpLeft.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscale))
                {
                    newWantedPosition = obscale.Interaction(wantedPos, transform.localScale, this);

                    if (_lastRaycastHit2D.transform != null)
                    {
                       if (_lastRaycastHit2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                        {
                            obscaleExit.ExitInteraction(this);
                        } 
                    }
                    
                    _lastRaycastHit2D = hitUpLeft; // At the end of this if
                }
            }
        }
        else if (hitUPRight.transform != null)
        {
            Debug.Log("hitUPRight = " + hitUPRight.transform);
            if (_lastRaycastHit2D != hitUPRight)
            {
                if (hitUPRight.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscale))
                {
                    newWantedPosition = obscale.Interaction(wantedPos, transform.localScale, this);


                    if (_lastRaycastHit2D.transform != null)
                    {
                        if (_lastRaycastHit2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                        {
                           obscaleExit.ExitInteraction(this);
                        }
                    }
                    
                    _lastRaycastHit2D = hitUPRight; // At the end of this if
                }
            }
        }
        else
        {
            newWantedPosition = new Vector3(wantedPos.x,wantedPos.y,transform.position.z);

            if (_lastRaycastHit2D.transform != null)
            {
                if (_lastRaycastHit2D != hitDownLeft)
                {
                    if (_lastRaycastHit2D.transform.gameObject.TryGetComponent<Obstacle>(out Obstacle obscaleExit))
                    {
                        obscaleExit.ExitInteraction(this);
                    }
                    _lastRaycastHit2D = hitDownLeft;
                }  
            }
            
            
            
        }

        transform.position = newWantedPosition;

    }
    private void CheckPressedKeys()
    {
        if (Gamepad.current.aButton.ReadValue() == 0)
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
            if (Gamepad.current.aButton.ReadValue() > 0f)
            {
                _verticalSpeed += _jumpSpeed;
                
            }
        }
        else
        {
            if (Gamepad.current.aButton.ReadValue() > 0f && _canDoubleJump && !_lastpressedA)
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
        if (Gamepad.current.leftStick.x.ReadValue()>0)
        {
            _currentSpeed = _speed;
        }
        
        else 
            if (Gamepad.current.leftStick.x.ReadValue()<0)
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
