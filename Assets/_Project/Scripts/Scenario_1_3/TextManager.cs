using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    string[] wallTexts;

    [SerializeField]
    TMP_Text textField;

    int question;

    // Start is called before the first frame update
    void Start()
    {
        textField.text = wallTexts[question];
    }

    public void NextQuestion()
    {
        question++;

        //Empty Text
        textField.text = "";

        //Check if it was the last text
        if (question == wallTexts.Length) return;

        //New Text
        textField.text = wallTexts[question];
    }
}
