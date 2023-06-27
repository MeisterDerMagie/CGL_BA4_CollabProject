using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii.Gaming;
using Wichtel.SceneManagement;

public class SceneFlow_Scenario_1_2 : SceneFlow
{
    //References
    [SerializeField][BoxGroup("References")]
    FirstPersonController firstPersonController;

    //Variables
    bool allBubblesPopped;
    bool shaked;

    [SerializeField][BoxGroup("How long the player needs to shake their head to get to next scene")]
    float shakingTime;

    [SerializeField][BoxGroup("Name of the next Scene")]
    SceneLoader nextScene;

    //Modules
    LookAround lookAround;
    PopBubbles popBubbles;
    Shaking shaking;

    protected override IEnumerator<float> _SceneFlow()
    {
        //get modules
        lookAround = firstPersonController.GetModule<LookAround>();
        popBubbles = firstPersonController.GetModule<PopBubbles>();
        shaking = firstPersonController.GetModule<Shaking>();

        //wait until the player popped all bubbles
        yield return Timing.WaitUntilTrue(() => allBubblesPopped);

        //enable the shaking module and disable the popping module
        popBubbles.SetEnabled(false);
        lookAround.SetEnabled(true);
        shaking.SetEnabled(true);

        //wait until the player shaked their head ...
        yield return Timing.WaitUntilTrue(() => shaking.ShakingTime >= shakingTime);

        //... and then load the next scene
        nextScene.Load();
    }

    public void SetAllBubblesPopped(bool popped) => allBubblesPopped = popped;
}
