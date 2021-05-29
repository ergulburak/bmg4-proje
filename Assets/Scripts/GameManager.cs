using System;
using System.ComponentModel.Design.Serialization;
using JetBrains.Annotations;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("Game Manager is null");
            return _instance;
        }
    }

    private int _enemyCount;

    public int EnemyCount
    {
        get => _enemyCount;
        set => _enemyCount = value;
    }

    void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if (_enemyCount == 0)
        {
        }
    }

    #endregion
}