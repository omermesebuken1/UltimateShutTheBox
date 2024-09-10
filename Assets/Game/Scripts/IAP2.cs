using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP2 : MonoBehaviour
{   
    public string noAdsName = "remove_ads";

    public void OnPurchaseComplete(Product product)
    {

        if(product.definition.id == noAdsName)
        {
            PlayerPrefs.SetInt("NoAds",1);
            print("There will be no ads buddy!");
        }

    }

    public void OnPurchasedFailed(Product product, PurchaseFailureReason reason)
    {
        
        
            print("Product named: "+ product +", " + "couldn't purchased because of " + reason);
        

    }
  
}
