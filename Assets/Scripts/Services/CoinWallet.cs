using System;
using UnityEngine;

public sealed class CoinWallet
{
    public const string CoinsKey = "coins";

    public int Balance
    {
        get { return PlayerPrefs.GetInt(CoinsKey, 0); }
    }

    public int Add(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("amount", "Coin amount must be positive.");
        }

        int updatedBalance = Balance + amount;
        PlayerPrefs.SetInt(CoinsKey, updatedBalance);
        PlayerPrefs.Save();
        return updatedBalance;
    }

    public bool CanSpend(int amount)
    {
        return amount > 0 && Balance >= amount;
    }

    public bool TrySpend(int amount)
    {
        if (!CanSpend(amount))
        {
            return false;
        }

        PlayerPrefs.SetInt(CoinsKey, Balance - amount);
        PlayerPrefs.Save();
        return true;
    }
}
