using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] InterstitialAdsButton interstitialAdsButton;

    [SerializeField] BannerAd bannerAd;

    [SerializeField] RewardedAdsButton rewardedAdsButton;
 
    void Awake()
    {
        InitializeAds();

    }

    private void Start() {
        
        rewardedAdsButton.LoadAd();
    }


 
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;

            _gameId = _iOSGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        interstitialAdsButton.LoadAd();
        bannerAd.LoadBanner();
        rewardedAdsButton.LoadAd();
       
    

    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}