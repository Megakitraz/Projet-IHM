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

    public bool _lastpressedA = false;

    private Gamepad gamepad;


    private void LastMovement(Vector2 wantedPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(wantedPos, new Vector2(wantedPos.x - transform.position.x, wantedPos.y-transform.position.y),Vector2.Distance(wantedPos,transform.position));
        if (hit.transform!=null)
        {
            Debug.Log("hit = "+hit.transform);
            transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(wantedPos.x,wantedPos.y,transform.position.z);
        }
        //if(Physics2D.Raycast(wantedPos, new Vector2(wantedPos.x - transform.position.x, wantedPos.y-transform.position.y), out RaycastHit2D hit, Mathf.Infinity, 0)){
        //    Debug.Log("hit = "+hit.transform);
        //}

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
        LastMovement(new Vector2(transform.position.x, transform.position.y + _verticalSpeed*Time.deltaTime));
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
        transform.position = new Vector3(transform.position.x + _currentSpeed*Time.deltaTime, transform.position.y, transform.position.z);
}

    void Update()
    {
        Movement();
        VerticalMovement();
        CheckPressedKeys();
    }

}
