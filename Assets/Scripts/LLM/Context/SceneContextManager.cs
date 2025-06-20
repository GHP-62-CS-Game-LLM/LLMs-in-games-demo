using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SceneContextManager : MonoBehaviour
{
    private Dictionary<GameObject, string[]> _sceneContext = new Dictionary<GameObject, string[]>();

    private List<string> _globalContext = new List<string>();
    private List<Func<string>> _dynamicContex = new List<Func<string>>();

    public void SetContext(GameObject obj, string[] context)
    {
        _sceneContext[obj] = context;
        // foreach (GameObject ob in _sceneContext.Keys)
        // {
        //     string tot = "";
        //     foreach (string st in _sceneContext[ob])
        //         tot += $"{st}, ";
        //     print($"{ob.name} : {tot}");
        // }
    }

    public string GetContext(GameObject obj)
    {
        StringBuilder sb = new StringBuilder();

        foreach (string context in _globalContext) sb.AppendLine(context);
        foreach (Func<string> context in _dynamicContex) sb.AppendLine(context.Invoke());
        foreach (string context in _sceneContext[obj]) sb.AppendLine(context);

        return sb.ToString();
    }

    public string GetGlobalContext()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string context in _globalContext) sb.AppendLine(context);

        return sb.ToString();
    }

    public void AddToGlobalContext(string context)
    {
        _globalContext.Add(context);
    }

    public void AddToDyanmicContext(Func<string> context)
    {
        _dynamicContex.Add(context);
    }

    public string GetDynamicContext()
    {
        StringBuilder sb = new StringBuilder();

        foreach (Func<string> func in _dynamicContex) sb.AppendLine(func.Invoke());
        
        return sb.ToString();
    }
}
