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
    private float _scaleFactor = 0.2f;

    public void Scale() => _target.localScale += Vector3.one * _scaleFactor;

    public void RestoreInitalScale() => _target.localScale -= Vector3.one * _scaleFactor;
}
}