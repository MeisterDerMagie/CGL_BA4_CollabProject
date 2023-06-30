using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField]
    Light[] lights;

    [SerializeField]
    float fadeTime;

    float[] normalIntensity;

    bool inside;

    // Start is called before the first frame update
    void Start()
    {
        normalIntensity = new float[lights.Length];
        int lightNumber = 0;
        foreach(Light light in lights)
        {
            normalIntensity[lightNumber] = light.intensity;
            lightNumber++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inside = true;
            Debug.Log("Player Entered");
            foreach (Light light in lights)
                light.intensity = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inside = false;
            Debug.Log("Player Exit");
            int lightNumber = 0;
            foreach(Light light in lights)
            {
                StartCoroutine(FadeLightBack(light, normalIntensity[lightNumber]));
                lightNumber++;
            }
        }
    }

    IEnumerator FadeLightBack(Light light, float intensity)
    {
        float currentTime = 0;
        while (currentTime < fadeTime)
        {
            // check if player is inside again
            if (inside == true) yield break;

            currentTime += Time.deltaTime;
            light.intensity = Mathf.Lerp(0, intensity, currentTime / fadeTime);
            yield return null;
        }
        Debug.Log("Done with Fade");
    }
}
