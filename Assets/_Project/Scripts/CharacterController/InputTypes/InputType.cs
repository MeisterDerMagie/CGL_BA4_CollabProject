//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder (-50)]
public abstract class InputType
{
    protected FirstPersonController FirstPersonController;

    protected InputType (FirstPersonController firstPersonController)
    {
        FirstPersonController = firstPersonController;
    }

    public abstract void Tick();
}