using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    public static InputAction _direction;
    public static InputAction _jump;
    public static InputAction _dash;

    public static bool _jumpKeyDown;
    public static bool _dashKeyDown;

    private static bool _lastPressJump;
    private static bool _lastPressDash;




    private void Start()
    {
        _direction = _playerInput.actions["Move"];
        _jump = _playerInput.actions["Jump"];
        _dash = _playerInput.actions["Dash"];


        _jumpKeyDown = false;
        _dashKeyDown = false;

        _lastPressJump = _jump.IsPressed();
        _lastPressDash = _dash.IsPressed();
    }

    private void Update()
    {
        if(_jump.IsPressed() && !_lastPressJump)
        {
            
            _jumpKeyDown = true;
        }
        else
        {
            _jumpKeyDown = false;
        }


        if (_dash.IsPressed() && !_lastPressDash)
        {

            _dashKeyDown = true;
        }
        else
        {
            _dashKeyDown = false;
        }




    }

    private void LateUpdate()
    {
        _lastPressJump = _jump.IsPressed();
        _lastPressDash = _dash.IsPressed();
    }
}
