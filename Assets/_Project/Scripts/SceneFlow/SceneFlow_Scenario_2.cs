using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class SceneFlow_Scenario_2 : SceneFlow
{
    [SerializeField]
    private float _waitBeforeVoice = 3f;

    //Game flow variables
    [ReadOnly][BoxGroup("Runtime variables")]
    private bool _playerReachedOutside = false;

    protected override IEnumerator<float> _SceneFlow()
    {
        yield return Timing.WaitForSeconds(_waitBeforeVoice);
        AudioManager.Singleton.Play("Trapped_1");

        yield return Timing.WaitUntilTrue(() => _playerReachedOutside);
        AudioManager.Singleton.Play("Or stuck in your view again");
    }

    public void SetPlayerReachedOutside() => _playerReachedOutside = true;
}
