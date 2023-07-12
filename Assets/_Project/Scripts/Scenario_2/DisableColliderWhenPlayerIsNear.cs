using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DisableColliderWhenPlayerIsNear : MonoBehaviour
{
    [SerializeField]
    private float _distanceToDisableCollider = 25f;
        
    private BoxCollider _collider;
    private Transform _player;
    
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _player = FindObjectOfType<CharacterController>().transform;
    }

    private void Update()
    {
        //disable the collider when the player is near
        float distance = Vector3.Distance(_player.position, transform.position);
        _collider.gameObject.layer = distance < _distanceToDisableCollider ? 2 : 0;
    }
}
