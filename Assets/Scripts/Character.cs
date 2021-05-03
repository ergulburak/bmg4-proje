﻿using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterSequenceNumber;
    public string characterName;
    public Color sawColor;
    public int price;
    public bool isFree = false;

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(characterName, 0) == 1);
        }
    }

    void Awake()
    {
        characterName = characterName.ToUpper();
    }

    public bool Unlock()
    {
        if (IsUnlocked)
            return true;

        if (CoinManager.Instance.Coins >= price)
        {
            PlayerPrefs.SetInt(characterName, 1);
            PlayerPrefs.Save();
            CoinManager.Instance.RemoveCoins(price);

            return true;
        }

        return false;
    }
}
