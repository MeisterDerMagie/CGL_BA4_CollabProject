using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBubble : MonoBehaviour, IBubble
{
    [HideInInspector]
    public bool move;
    bool falling;

    [SerializeField]
    float movementSpeed, stopZPos, fallingSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        /*if (falling == true)
            transform.localPosition -= transform.up * fallingSpeed * Time.deltaTime;

        if (move == true)
        {
            transform.localPosition -= transform.forward * movementSpeed * Time.deltaTime;
        }

        if (transform.localPosition.z >= stopZPos) move = false;*/
    }

    public void Pop()
    {
        Destroy(gameObject);
        FindObjectOfType<SceneFlow_Scenario_1_3>().PoppedFinalBubble();
    }
}
