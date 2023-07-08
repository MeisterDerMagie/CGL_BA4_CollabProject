using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wichtel.Extensions;

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
        image.color = image.color.With(a: 0f);
    }

    public void FadeIn(float shakingTime)
    {
        Color newColor = image.color;
        float percentage = shakingTime / timeToShake;
        float newAlpha = maxAlpha * percentage;
        newColor.a = newAlpha;
        image.color = newColor;
    }

    public void FadeOut()
    {
        Color newColor = image.color;
        newColor.a = 0;
        image.color = newColor;
    }
}
