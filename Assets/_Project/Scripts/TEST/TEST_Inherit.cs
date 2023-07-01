using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Inherit : TEST_Inheritance
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Rotation();
    }

    public override void Test()
    {
        base.Test();
        Debug.Log("Child Class");
    }
}
