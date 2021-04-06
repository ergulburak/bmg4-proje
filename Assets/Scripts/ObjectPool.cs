using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private List<GameObject> _bloodParticlesPool;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public List<GameObject> BloodParticlesPool
    {
        get => _bloodParticlesPool;
        set => _bloodParticlesPool = value;
    }

    private void Start()
    {
        _bloodParticlesPool = new List<GameObject>();
    }
}