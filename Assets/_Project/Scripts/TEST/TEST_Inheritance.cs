using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Inheritance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Test();
    }

    public virtual void Test()
    {
        Debug.Log("Inheritance Script");
    }

    protected void Rotation()
    {
        //Rotate
    }
}
