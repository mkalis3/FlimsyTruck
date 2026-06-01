using NUnit.Framework;
using UnityEngine;

public class CoinWalletTests
{
    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey(CoinWallet.CoinsKey);
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey(CoinWallet.CoinsKey);
    }

    [Test]
    public void AddStoresUpdatedBalance()
    {
        var wallet = new CoinWallet();

        int balance = wallet.Add(200);

        Assert.AreEqual(200, balance);
        Assert.AreEqual(200, PlayerPrefs.GetInt(CoinWallet.CoinsKey));
    }

    [Test]
    public void TrySpendRejectsAmountsAboveBalance()
    {
        var wallet = new CoinWallet();
        wallet.Add(100);

        bool spent = wallet.TrySpend(150);

        Assert.IsFalse(spent);
        Assert.AreEqual(100, wallet.Balance);
    }

    [Test]
    public void TrySpendSubtractsAmount()
    {
        var wallet = new CoinWallet();
        wallet.Add(300);

        bool spent = wallet.TrySpend(120);

        Assert.IsTrue(spent);
        Assert.AreEqual(180, wallet.Balance);
    }
}
