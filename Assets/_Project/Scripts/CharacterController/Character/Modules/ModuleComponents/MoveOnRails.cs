using UnityEngine;
using UnityEngine.Splines;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
[ExecuteInEditMode]
public abstract class MoveOnRails : FirstPersonModule
{
    [SerializeField]
    public Rails Rails;
    
    protected void GlueCharacterToRails()
    {
        if (Rails == null)
            return;
        
        if (transform.position == Rails.Target.position && transform.rotation == Rails.Target.rotation)
            return;
        
        transform.position = Rails.Target.position;
        transform.rotation = Rails.Target.rotation;
        
        #if UNITY_EDITOR
        if(!Application.isPlaying) PrefabUtility.RecordPrefabInstancePropertyModifications(Rails.Target);
        #endif
    }
}