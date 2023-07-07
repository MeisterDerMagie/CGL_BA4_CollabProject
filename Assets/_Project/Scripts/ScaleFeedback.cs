//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sectile {
public class ScaleFeedback : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _scaleFactor = 1f;

    public void Scale() => _target.localScale *= _scaleFactor;

    public void RestoreInitalScale() => _target.localScale /= _scaleFactor;
}
}