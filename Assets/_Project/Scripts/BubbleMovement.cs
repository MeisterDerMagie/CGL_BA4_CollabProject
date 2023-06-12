using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    float movementSpeed;
    float maxY, spawnY;

    BubbleValues values;

    // Start is called before the first frame update
    void Start()
    {
        values = gameObject.GetComponentInParent<BubbleValues>();
        movementSpeed = Random.Range(values.minSpeed, values.maxSpeed);
        maxY = values.maxYValue;
        spawnY = values.spawnY;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += transform.up * movementSpeed * Time.deltaTime;

        if (transform.localPosition.y >= maxY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, spawnY, transform.localPosition.z);
        }
    }
}
