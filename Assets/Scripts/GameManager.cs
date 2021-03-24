using System;
using System.ComponentModel.Design.Serialization;
using JetBrains.Annotations;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    private static GameManager _ınstance;

    private void Awake()
    {
        if (_ınstance == null)
            _ınstance = this;
    }

    #endregion
}