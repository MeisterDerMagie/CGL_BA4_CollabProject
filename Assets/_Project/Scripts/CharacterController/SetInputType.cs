//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInputType : MonoBehaviour
{
    public void SetInputType_Player()
    {
        FindObjectOfType<FirstPersonInputManager>().SetInputType_Player();
    }
}