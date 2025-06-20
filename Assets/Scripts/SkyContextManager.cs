using AC.CSky;
using UnityEngine;

public class SkyContextManager : MonoBehaviour
{
    private SceneContextManager _manager;
    private CSky_TimeOfDay _timeOfDay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _manager = FindFirstObjectByType<SceneContextManager>();
        _timeOfDay = GetComponent<CSky_TimeOfDay>();
        
        _manager.AddToDyanmicContext(() => $"Current DateTime: {_timeOfDay.DateTime}");
    }
}
