using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BubbleBehavior : MonoBehaviour
{
    float movementSpeed, fallingSpeed;
    float maxY, spawnY;

    bool falling;

    BubbleValues values;

    GazeAware gazeAware;

    // Start is called before the first frame update
    void Start()
    {
        values = gameObject.GetComponentInParent<BubbleValues>();
        movementSpeed = Random.Range(values.minSpeed, values.maxSpeed);
        maxY = values.maxYValue;
        spawnY = values.spawnY;
        fallingSpeed = values.fallingSpeed;

        gazeAware = GetComponent<GazeAware>();
    }

    // Update is called once per frame
    void Update()
    {
        if (falling == false)
        {
            transform.localPosition -= transform.up * movementSpeed * Time.deltaTime;

            if (transform.localPosition.y <= maxY)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, spawnY, transform.localPosition.z);
            }
        }
        else
        {
            transform.localPosition -= transform.up * fallingSpeed * Time.deltaTime;
            Destroy(gameObject, 1.2f);
        }

        if (gazeAware.HasGazeFocus == true)
            ChangeAppearance(6.5f);
        else
            ChangeAppearance(0);
    }

    public void Pop()
    {
        if (!values.fallOnInteract)
            Destroy(gameObject);
        else
            falling = true;
    }

    public void ChangeAppearance(float value)
    {
        GetComponent<MeshRenderer>().material.SetFloat("Divide Color", value);
        Debug.Log("Changed");
    }
}
