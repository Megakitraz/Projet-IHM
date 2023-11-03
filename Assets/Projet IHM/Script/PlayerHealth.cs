using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;


    [SerializeField] private int _maxHealth;

    private void Start()
    {
        SetHealth(_maxHealth);
    }

    public void SetHealth(int health)
    {
        _health = Mathf.Clamp(health, 0, _maxHealth);
        Debug.Log("Hp = " + _health);
    }

    public void IncrHealth(int incr)
    {
        _health += incr;
        Debug.Log("Hp = " + _health);

        if (_health <= 0)
        {
            PlayerDying();
        }
    }

    private void PlayerDying()
    {
        SceneManager.LoadScene(2);
    }

}
