using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SceneContextManager : MonoBehaviour
{
    private Dictionary<GameObject, string[]> _sceneContext = new Dictionary<GameObject, string[]>();

    public void SetContext(GameObject obj, string[] context)
    {
        _sceneContext[obj] = context;
    }

    void Update()
    {
    }

}
