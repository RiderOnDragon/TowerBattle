using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _pathPoints = new List<Transform>();

    public List<Transform> PathPoints { get => _pathPoints; }
    
    public static Path Singleton;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }
}
