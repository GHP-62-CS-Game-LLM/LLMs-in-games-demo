using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private Object[] _context = new Object[] { };

    // Need Game Manager Object

    void Update()
    {
        // Update Game Manager object with the context
    }

    public void SetContext(Object[] context)
    {
        _context = context;
    }
}
