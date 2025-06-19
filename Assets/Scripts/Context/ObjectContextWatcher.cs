using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    
}
