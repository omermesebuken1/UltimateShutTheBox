using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class IAPController : MonoBehaviour,IStoreListener
{
    
    IStoreController controller;

    public string product;

    public void Start()
    {
        IAPStart();
        
    }

    public void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(product,ProductType.NonConsumable);

        UnityPurchasing.Initialize(this,builder);

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Initiliaze Failed.");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed.");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {

        if(string.Equals(e.purchasedProduct.definition.id,product))
        {
            

            if(PlayerPrefs.HasKey("NoAds") == true)
            {
                PlayerPrefs.SetInt("NoAds",1);
            }

            return PurchaseProcessingResult.Complete;
        }
        else
        {
            return PurchaseProcessingResult.Pending;
        }

    }

    public void IAPButton(string id)
    {
        Product product = controller.products.WithID(id);

        if(product != null && product.availableToPurchase)
        {
            controller.InitiatePurchase(product);
            print("Buying...");
        }
        else
        {
            print("Not Buying.");
        }
    }




}
