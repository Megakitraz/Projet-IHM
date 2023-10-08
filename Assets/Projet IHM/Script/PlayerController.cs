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

    private Vector2 _wantedPosition;


    private void LastMovement(Vector2 wantedPos)
    {
        //Vector2 newWantedPosition = wantedPos;
        //Debug.Log(transform.lossyScale);
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 offset = new Vector2(0f, 0f);
        if (position2D != wantedPos) offset = (wantedPos - position2D).normalized;

        RaycastHit2D hitDownLeft = Physics2D.Raycast(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x , wantedPos.y-transform.position.y),Vector2.Distance(wantedPos,transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y),Color.red,0.5f,false);

        RaycastHit2D hitDownRight = Physics2D.Raycast(position2D + offset / 100f + new Vector2(transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(transform.lossyScale.x, -transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.blue, 0.5f, false);

        RaycastHit2D hitUpLeft = Physics2D.Raycast(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
        Debug.DrawRay(position2D + offset / 100f + new Vector2(-transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Color.green, 0.5f, false);

        RaycastHit2D hitUpRight = Physics2D.Raycast(position2D + offset / 100f + new Vector2(transform.lossyScale.x, transform.lossyScale.y) / 2f, new Vector2(wantedPos.x - transform.position.x, wantedPos.y - transform.position.y), Vector2.Distance(wantedPos, transform.position), 3);
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

        if (hitUpRight.transform != null &&
            hitUpRight.transform != _lastRaycastHitDownRight2D.transform &&
            hitUpLeft.transform != _lastRaycastHitDownLeft2D.transform &&
            hitUpLeft.transform != _lastRaycastHitUpLeft2D.transform)
        {
            //Debug.Log("hitUPRight = " + hitUpRight.transform);

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

        if (hitUpLeft.transform != null &&
            hitUpRight.transform != _lastRaycastHitDownRight2D.transform &&
            hitUpLeft.transform != _lastRaycastHitDownLeft2D.transform &&
            hitUpLeft.transform != _lastRaycastHitUpRight2D.transform)
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

        if (InputManager._jump == null) return;
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
        _wantedPosition = new Vector2(_wantedPosition.x, _wantedPosition.y + _verticalSpeed * transform.lossyScale.y * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y + _verticalSpeed*Time.deltaTime, transform.position.z);

    }
    private void Movement()
    {
        if (InputManager._direction == null) return;
        if (InputManager._direction.ReadValue<Vector2>().x > 0)
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
        _wantedPosition = new Vector2(_wantedPosition.x + _currentSpeed * transform.lossyScale.x * Time.deltaTime, _wantedPosition.y);
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
