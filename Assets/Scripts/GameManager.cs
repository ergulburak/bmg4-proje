using System;
using System.ComponentModel.Design.Serialization;
using Cinemachine;
using JetBrains.Annotations;
using Player;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    private static GameManager _instance;

    public CinemachineVirtualCamera myCamera;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("Game Manager is null");
            return _instance;
        }
    }

    private int _enemyCount;
    public GameObject[] characters;

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
        var player = Instantiate(characters[CharacterManager.Instance.CurrentCharacterIndex], Vector3.zero,
            Quaternion.identity);
        player.GetComponent<PlayerMovementSystem>().trajectory = GetComponent<Trajectory>();
        myCamera.Follow = player.transform;
        myCamera.LookAt = player.transform;
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