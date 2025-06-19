using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SceneContextManager : MonoBehaviour
{
    private Dictionary<GameObject, string[]> _sceneContext = new Dictionary<GameObject, string[]>();

    public void SetContext(GameObject obj, string[] context)
    {
        _sceneContext[obj] = context;
        foreach (GameObject ob in _sceneContext.Keys)
        {
            string tot = "";
            foreach (string st in _sceneContext[ob])
                tot += $"{st}, ";
            print($"{ob.name} : {tot}");
        }
    }

    void Update()
    {

    }

}
