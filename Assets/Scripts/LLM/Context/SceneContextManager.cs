using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using UnityEngine;

public class SceneContextManager : MonoBehaviour
{
    private Dictionary<GameObject, IContext> _sceneContext = new Dictionary<GameObject, IContext>();

    public List<string> globalContext = new List<string>();
    private List<Func<IContext>> _dynamicContex = new List<Func<IContext>>();

    public void SetContext(GameObject obj, IContext context)
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

        sb.AppendLine("Global Context: ");
        foreach (string context in globalContext) sb.AppendLine(context);
        foreach (Func<IContext> context in _dynamicContex) AddContext(sb, context);
        sb.AppendLine("Object-Specific Context:");
        AddContext(sb, () => _sceneContext[obj]);

        return sb.ToString();
    }

    public string GetGlobalContext()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string context in globalContext) sb.AppendLine(context);

        return sb.ToString();
    }

    public void AddToGlobalContext(string context)
    {
        globalContext.Add(context);
    }

    public void AddToDynamicContext(Func<IContext> context)
    {
        _dynamicContex.Add(context);
    }

    public string GetDynamicContext()
    {
        StringBuilder sb = new StringBuilder();

        foreach (Func<IContext> func in _dynamicContex) AddContext(sb, func);
        
        return sb.ToString();
    }

    private static void AddContext(StringBuilder sb, Func<IContext> context)
    {
        MemoryStream stream = new MemoryStream();
        Utf8JsonWriter writer = new Utf8JsonWriter(stream);
        
        context.Invoke().WriteJson(writer);
        writer.Flush();
        
        string jsonString = Encoding.UTF8.GetString(stream.ToArray());
        sb.AppendLine(jsonString);
    }
}
