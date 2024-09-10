using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    
    string gameID = "5026196";
    void Start()
    {
        Advertisement.Initialize(gameID);
        ShowBanner();
    }

    public void ShowBanner()
    {
        
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_iOS");
        
        
    }

    
    
}
