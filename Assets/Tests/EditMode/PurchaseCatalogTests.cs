using NUnit.Framework;

public class PurchaseCatalogTests
{
    [Test]
    public void CatalogContainsExpectedCoinProducts()
    {
        CoinProduct[] products = PurchaseCatalog.GetProducts();

        Assert.AreEqual(4, products.Length);
        Assert.AreEqual(200, products[0].Coins);
        Assert.AreEqual(800, products[1].Coins);
        Assert.AreEqual(2000, products[2].Coins);
        Assert.AreEqual(4000, products[3].Coins);
    }

    [Test]
    public void TryGetProductReturnsMatchingReward()
    {
        CoinProduct product;

        bool found = PurchaseCatalog.TryGetProduct(PurchaseCatalog.Coins2000, out product);

        Assert.IsTrue(found);
        Assert.AreEqual(2000, product.Coins);
        Assert.AreEqual("purchased1000", product.ConfirmationObjectName);
    }

    [Test]
    public void ProductListCannotBeModifiedByCaller()
    {
        CoinProduct[] firstRead = PurchaseCatalog.GetProducts();
        firstRead[0] = new CoinProduct("changed", 1, "changed");

        CoinProduct[] secondRead = PurchaseCatalog.GetProducts();

        Assert.AreEqual(PurchaseCatalog.Coins200, secondRead[0].Id);
        Assert.AreEqual(200, secondRead[0].Coins);
    }
}
