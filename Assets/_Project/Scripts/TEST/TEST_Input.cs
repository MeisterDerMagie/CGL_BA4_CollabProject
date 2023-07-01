using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Input : MonoBehaviour
{
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) test.GetComponent<TEST_Inheritance>().Test();
    }
}
