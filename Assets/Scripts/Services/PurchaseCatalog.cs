using System;

public struct CoinProduct
{
    public readonly string Id;
    public readonly int Coins;
    public readonly string ConfirmationObjectName;

    public CoinProduct(string id, int coins, string confirmationObjectName)
    {
        Id = id;
        Coins = coins;
        ConfirmationObjectName = confirmationObjectName;
    }
}

public static class PurchaseCatalog
{
    public const string Coins200 = "com.tmkapps.flimsytruck.coins200";
    public const string Coins800 = "coins800";
    public const string Coins2000 = "ccoins2000";
    public const string Coins4000 = "coins4000";

    private static readonly CoinProduct[] Products =
    {
        new CoinProduct(Coins200, 200, "purchased100"),
        new CoinProduct(Coins800, 800, "purchased400"),
        new CoinProduct(Coins2000, 2000, "purchased1000"),
        new CoinProduct(Coins4000, 4000, "purchased2000")
    };

    public static CoinProduct[] GetProducts()
    {
        CoinProduct[] copy = new CoinProduct[Products.Length];
        Array.Copy(Products, copy, Products.Length);
        return copy;
    }

    public static bool TryGetProduct(string productId, out CoinProduct product)
    {
        for (int i = 0; i < Products.Length; i++)
        {
            if (String.Equals(Products[i].Id, productId, StringComparison.Ordinal))
            {
                product = Products[i];
                return true;
            }
        }

        product = default(CoinProduct);
        return false;
    }
}
