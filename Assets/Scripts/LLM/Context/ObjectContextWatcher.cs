using UnityEngine;

public class ObjectContextWatcher : MonoBehaviour
{
    public bool enablePositionTracking = false;

    public string name = "";
    public string description = "";
    
    private ObjectContext _objectContext;
    private SceneContextManager _scm;

    void Awake()
    {
        _objectContext = new ObjectContext
        {
            Name = name, 
            Description = description, 
            ObjectKinematic = enablePositionTracking ? 
                new ObjectKinematicContext(transform, GetComponent<Rigidbody>()) : 
                null
        };
        
        _scm = FindAnyObjectByType<SceneContextManager>();
        
        _scm.SetContext(gameObject, _objectContext);
    }

    // public void SetContext(string[] cont)
    // {
    //     _objectContext = cont;
    //     _scm.SetContext(gameObject, _objectContext);
    // }

    public string GetContext() => _scm.GetContext(gameObject);
}
