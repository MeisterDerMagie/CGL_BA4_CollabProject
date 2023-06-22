using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class InitializeTobii : MonoBehaviour
{
    public TobiiSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        TobiiAPI.Start(settings);
        DontDestroyOnLoad(gameObject);
    }
}
