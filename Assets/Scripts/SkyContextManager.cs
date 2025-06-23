using System;
using System.Text.Json;
using AC.CSky;
using UnityEngine;

public class WorldTimeContext : IContext
{
    public string Type { get; } = "DateTime";

    private DateTime _dateTime;

    public WorldTimeContext(DateTime dateTime)
    {
        _dateTime = dateTime;
    }
    
    public void WriteJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("DateTime", _dateTime.ToString());
        writer.WriteEndObject();
    }
}


public class SkyContextManager : MonoBehaviour
{
    private SceneContextManager _manager;
    private CSky_TimeOfDay _timeOfDay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _manager = FindFirstObjectByType<SceneContextManager>();
        _timeOfDay = GetComponent<CSky_TimeOfDay>();

        _manager.AddToDynamicContext(() => new WorldTimeContext(_timeOfDay.DateTime));
    }
}
