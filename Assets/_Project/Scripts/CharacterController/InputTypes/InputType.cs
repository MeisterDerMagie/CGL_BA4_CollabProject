//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputType
{
    protected FirstPersonController FirstPersonController;

    protected InputType (FirstPersonController firstPersonController)
    {
        FirstPersonController = firstPersonController;
    }

    public abstract void Tick();
}