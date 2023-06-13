using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisBehavior : MonoBehaviour
{
    public float timeTillAppearance;

    float timeSinceStart;

    MeshRenderer meshRenderer;

    SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart >= timeTillAppearance)
        {
            meshRenderer.enabled = true;
            sphereCollider.enabled = true;
            //Play audio sound
        }
    }

    public void GetBright()
    {
        // Iris should get brighter -> Talk to artists
        Debug.Log("Iris is getting brighter");
    }
}
