using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public virtual Vector2 Interaction(Vector2 pos, Vector3 localScale, PlayerController playerController) {
        Debug.Log("Script Obstacle");
        return pos;
    }

    public virtual void ExitInteraction(PlayerController playerController) { }
}
