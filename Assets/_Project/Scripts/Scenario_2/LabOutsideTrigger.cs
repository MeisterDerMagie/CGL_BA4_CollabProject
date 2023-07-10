using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabOutsideTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            FindObjectOfType<SceneFlow_Scenario_2>().SetPlayerReachedOutside();
    }
}
