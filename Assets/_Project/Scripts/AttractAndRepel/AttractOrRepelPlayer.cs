//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[HideMonoScript]
public class AttractOrRepelPlayer : MonoBehaviour
{
    [SerializeField][DisableInPlayMode]
    private AttractAndRepel.Mode mode;
    public AttractAndRepel.Mode Mode => mode;
}