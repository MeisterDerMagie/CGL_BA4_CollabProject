using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InteractableObjectExample : MonoBehaviour
{
    public void ScaleUp()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void ScaleDown()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void DoScaleJump()
    {
        transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo);
    }
}
