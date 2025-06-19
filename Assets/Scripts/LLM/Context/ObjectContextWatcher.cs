using UnityEngine;

public class ObjectContextWatcher : MonoBehaviour
{
    private string[] _objectContext;
    private SceneContextManager scm;

    void Awake()
    {
        scm = FindAnyObjectByType<SceneContextManager>();
    }

    public void SetContext(string[] cont)
    {
        _objectContext = cont;
        scm.SetContext(gameObject, _objectContext);
    }

    public string[] GetContext() => scm.GetContext(gameObject);
}
