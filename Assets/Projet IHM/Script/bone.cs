using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bone : MonoBehaviour
{
    public float rotationSpeed = 60.0f; // La vitesse de rotation en degrés par seconde

    void Update()
    {
        // Rotation de l'objet autour de l'axe des Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}