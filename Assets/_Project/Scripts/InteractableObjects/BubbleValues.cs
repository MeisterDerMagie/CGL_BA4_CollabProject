using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BubbleValues : MonoBehaviour
{
    public float maxYValue, spawnY;
    [Range(0, 3)]
    public float minSpeed, maxSpeed;

    // Variables for falling down
    public bool fallOnInteract;

    [ShowIf(nameof(fallOnInteract))]
    public float fallingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
