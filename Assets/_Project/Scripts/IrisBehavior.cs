using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class IrisBehavior : MonoBehaviour
{
    public float timeTillAppearance;

    [SerializeField]
    private Transform iris;

    private void Start()
    {
        Timing.RunCoroutine(_EnableIris());
    }

    private IEnumerator<float> _EnableIris()
    {
        yield return Timing.WaitForSeconds(timeTillAppearance);
        
        Appear();
    }

    private void Appear()
    {
        iris.gameObject.SetActive(true);
        //Play audio sound
    }

    public void GetBright()
    {
        // Iris should get brighter -> Talk to artists
        Debug.Log("Iris is getting brighter");
    }
}
