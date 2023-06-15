using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    float movementSpeed, fallingSpeed;
    float maxY, spawnY;

    bool falling;

    BubbleValues values;

    // Start is called before the first frame update
    void Start()
    {
        values = gameObject.GetComponentInParent<BubbleValues>();
        movementSpeed = Random.Range(values.minSpeed, values.maxSpeed);
        maxY = values.maxYValue;
        spawnY = values.spawnY;
        fallingSpeed = values.fallingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling == false)
        {
            transform.localPosition += transform.up * movementSpeed * Time.deltaTime;

            if (transform.localPosition.y >= maxY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, spawnY, transform.localPosition.z);
            }
        }
        else
        {
            transform.localPosition -= transform.up * fallingSpeed * Time.deltaTime;
            Destroy(gameObject, 1.2f);
        }
    }

    public void Pop()
    {
        if (!values.fallOnInteract)
            Destroy(gameObject);
        else
            falling = true;
    }
}
