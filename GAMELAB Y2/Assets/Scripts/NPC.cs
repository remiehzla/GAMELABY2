using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Will spawn only during turn 0
    // Will walk around the map, over tiles only
    // Will display pop-ups/emotes
    // Tile layer (6)

    [SerializeField] private float movementSpeed;

    private void FixedUpdate()
    {
        int layerMask = 1 << 8;
        
        RaycastHit hit;
    }

}
