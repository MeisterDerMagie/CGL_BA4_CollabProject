using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUG_LoadScene : MonoBehaviour
{
    [SerializeField]
    private SceneReference scene;

    [Button]
    private void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
