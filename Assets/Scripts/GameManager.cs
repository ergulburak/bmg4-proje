using System;
using System.ComponentModel.Design.Serialization;
using JetBrains.Annotations;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager instance;
    private int _enemyCount;

    public int EnemyCount
    {
        get => _enemyCount;
        set => _enemyCount = value;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Zombie").Length;
    }

    #endregion
}