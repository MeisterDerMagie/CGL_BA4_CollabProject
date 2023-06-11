//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TEST_EnableModules : MonoBehaviour
{
    public FirstPersonController fpc;

    [Button]
    public void EnableTeleport() => fpc.EnableModule<Teleport>();

    [Button]
    public void DisableTeleport() => fpc.DisableModule<Teleport>();

    [Button]
    public void EnableWalk() => fpc.EnableModule<Walk>();
    
    [Button]
    public void DisableWalk() => fpc.DisableModule<Walk>();

    [Button]
    public void EnableMoveOnRails() => fpc.EnableModule<MoveOnRailsScripted>();

    [Button]
    public void DisableMoveOnRails() => fpc.DisableModule<MoveOnRailsScripted>();

    [Button]
    public void EnableInteract() => fpc.EnableModule<Interact>();

    [Button]
    public void DisableInteract() => fpc.DisableModule<Interact>();
}