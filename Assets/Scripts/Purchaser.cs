using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    public const string PRODUCT100 = PurchaseCatalog.Coins200;
    public const string PRODUCT400 = PurchaseCatalog.Coins800;
    public const string PRODUCT1000 = PurchaseCatalog.Coins2000;
    public const string PRODUCT2000 = PurchaseCatalog.Coins4000;

    [SerializeField] private GameObject purchased100;
    [SerializeField] private GameObject purchased400;
    [SerializeField] private GameObject purchased1000;
    [SerializeField] private GameObject purchased2000;
    [SerializeField] private Text coinText;

    private readonly CoinWallet wallet = new CoinWallet();
    public InitializationFailureReason? LastInitializationFailure { get; private set; }
    public bool LastRestoreSucceeded { get; private set; }

    void Start()
    {
        ResolveSceneReferences();
        InitializePurchasing();
        RestorePurchases();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (CoinProduct product in PurchaseCatalog.GetProducts())
        {
            builder.AddProduct(product.Id, ProductType.Consumable);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }

    public void Buy100Coins()
    {
        BuyProductID(PRODUCT100);
    }

    public void Buy400Coins()
    {
        BuyProductID(PRODUCT400);
    }

    public void Buy1000Coins()
    {
        BuyProductID(PRODUCT1000);
    }

    public void Buy2000Coins()
    {
        BuyProductID(PRODUCT2000);
    }

    public void Ok100()
    {
        HideConfirmation(purchased100);
    }

    public void Ok400()
    {
        HideConfirmation(purchased400);
    }

    public void Ok1000()
    {
        HideConfirmation(purchased1000);
    }

    public void Ok2000()
    {
        HideConfirmation(purchased2000);
    }

    void BuyProductID(string productId)
    {
        if (!IsInitialized())
        {
            InitializePurchasing();
            return;
        }

        Product product = storeController.products.WithID(productId);
        if (product != null && product.availableToPurchase)
        {
            storeController.InitiatePurchase(product);
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions(result =>
            {
                LastRestoreSucceeded = result;
            });
        }
    }

    public string GetPriceFromStore(string id)
    {
        if (storeController == null || storeController.products == null)
        {
            return "";
        }

        Product product = storeController.products.WithID(id);
        return product != null ? product.metadata.localizedPriceString : "";
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        LastInitializationFailure = error;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        CoinProduct product;
        if (PurchaseCatalog.TryGetProduct(args.purchasedProduct.definition.id, out product))
        {
            ShowConfirmation(product.ConfirmationObjectName);
            int balance = wallet.Add(product.Coins);
            UpdateCoinText(balance);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        _ = product;
        _ = failureReason;
    }

    private void ResolveSceneReferences()
    {
        purchased100 = ResolveObject(purchased100, "purchased100");
        purchased400 = ResolveObject(purchased400, "purchased400");
        purchased1000 = ResolveObject(purchased1000, "purchased1000");
        purchased2000 = ResolveObject(purchased2000, "purchased2000");

        if (coinText == null)
        {
            GameObject coinTextObject = GameObject.Find("cointext");
            coinText = coinTextObject != null ? coinTextObject.GetComponent<Text>() : null;
        }
    }

    private static GameObject ResolveObject(GameObject current, string objectName)
    {
        return current != null ? current : GameObject.Find(objectName);
    }

    private static void HideConfirmation(GameObject popup)
    {
        if (popup != null)
        {
            popup.transform.localScale = Vector3.zero;
        }
    }

    private void ShowConfirmation(string objectName)
    {
        GameObject popup = GameObject.Find(objectName);
        if (popup != null)
        {
            popup.transform.localScale = Vector3.one;
        }
    }

    private void UpdateCoinText(int balance)
    {
        if (coinText != null)
        {
            coinText.text = balance.ToString();
        }
    }
}
