using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class bone : MonoBehaviour
{
    public float rotationSpeed = 60.0f; // La vitesse de rotation en degrés par seconde
    [SerializeField] private GameObject fireworks;

    void Update()
    {
        // Rotation de l'objet autour de l'axe des Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        FindObjectOfType<AudioManager>().Play("crowd");
        FindObjectOfType<AudioManager>().Play("fireworks");
        FindObjectOfType<AudioManager>().Stop("main");
        Invoke("WinScene",10f);
        transform.position = new Vector3(100f,100f,100f);
        fireworks.SetActive(true);
    }



    private void WinScene()
    {
        SceneManager.LoadScene(3);
    }
}