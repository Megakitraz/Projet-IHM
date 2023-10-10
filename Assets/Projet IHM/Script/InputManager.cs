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


    private void Start()
    {
        _direction = _playerInput.actions["Move"];
        _jump = _playerInput.actions["Jump"];
        _dash = _playerInput.actions["Dash"];
    }
}
