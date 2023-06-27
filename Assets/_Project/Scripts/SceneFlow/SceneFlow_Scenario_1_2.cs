using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii.Gaming;

public class SceneFlow_Scenario_1_2 : SceneFlow
{
    //References
    [SerializeField][BoxGroup("References")]
    FirstPersonController firstPersonController;

    //Variables
    bool allBubblesPopped;
    bool shaked;

    [SerializeField][field: BoxGroup("How long the player needs to shake their head to get to next scene")]
    float shakingTime;

    [SerializeField][field: BoxGroup("Name of the next Scene")]
    string nextScene;

    //Modules
    LookAround lookAround;
    PopBubbles popBubbles;
    Shaking shaking;

    protected override IEnumerator<float> _SceneFlow()
    {
        lookAround = firstPersonController.GetModule<LookAround>();
        popBubbles = firstPersonController.GetModule<PopBubbles>();

        yield return Timing.WaitUntilTrue(() => allBubblesPopped);

        popBubbles.SetEnabled(false);
        lookAround.SetEnabled(true);
        shaking.SetEnabled(true);

        yield return Timing.WaitUntilTrue(() => shaked);

        SceneManager.LoadScene(nextScene);
    }

    public void SetAllBubblesPopped(bool popped) => allBubblesPopped = popped;
    public void ShakingTime(float time)
    {
        if (time >= shakingTime) Debug.Log("Goal Reached");
    }
}
