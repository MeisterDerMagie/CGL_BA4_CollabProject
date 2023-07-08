using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakingFeedback : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)]
    private float maxAlpha = 1f;

    private float timeToShake;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        timeToShake = FindObjectOfType<SceneFlow_Scenario_1_2>().shakingTime;
        image = GetComponent<Image>();
    }

    public void FadeIn(float shakingTime)
    {
        Color newColor = image.color;
        float percentage = shakingTime / timeToShake;
        float newAlpha = maxAlpha * percentage;
        newColor.a = maxAlpha;
        image.color = newColor;
    }

    public void FadeOut()
    {
        Color newColor = image.color;
        newColor.a = 0;
        image.color = newColor;
    }
}
