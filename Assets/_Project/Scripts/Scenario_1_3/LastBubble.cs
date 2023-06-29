using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBubble : MonoBehaviour
{
    [HideInInspector]
    public bool move;

    [SerializeField]
    float movementSpeed, stopZPos;

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            transform.localPosition -= transform.forward * movementSpeed * Time.deltaTime;
        }

        if (transform.localPosition.z >= stopZPos) move = false;
    }
}
