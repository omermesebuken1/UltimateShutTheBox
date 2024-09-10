using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManagerUSTB : MonoBehaviour
{

    [SerializeField] private GameObject[] Boxes;
    public float boxCount;

    public int RoundTotal;
    private bool BoxSelectStarted;

    [SerializeField] private TextMeshProUGUI TotalNumberGUI;

    private float CheckBoxTimer;

    public bool checkGameEnd;



    [SerializeField] private GameObject rollButton;
    [SerializeField] private GameObject RetryButton;
    [SerializeField] private GameObject StartButton;

    [SerializeField] private GameObject PlayAgainButton;



    [SerializeField] private GameObject RewardedButton;

    [SerializeField] private GameObject settings_ALL;
    [SerializeField] private GameObject taptoexitscreen;

    [SerializeField] private GameObject Haptic;


    [SerializeField] private Image sound;
    [SerializeField] private Image vibration;

    [SerializeField] private Sprite soundOn_sprite;
    [SerializeField] private Sprite soundOff_sprite;
    [SerializeField] private Sprite vibOn_sprite;
    [SerializeField] private Sprite vibOff_sprite;

    [SerializeField] private GameObject PP;
    [SerializeField] private GameObject TOU;


    [SerializeField] private AudioClip ReloadSound;
    [SerializeField] private AudioClip winSound;

    [SerializeField] private AudioClip errorSound;

    private bool winSoundCast;
    private bool errorSoundCast;

    [SerializeField] private Material BoxMetarial;
    [SerializeField] private Material NumberMetarial;
    [SerializeField] private Material ShineMetarial;
    [SerializeField] private Material ErrorMetarial;

    public bool rewarded;
    public bool rewardedAlready;


    
    


    private int TotalWins;
    private int GamesPlayed;
    private bool totalWinAdded;
    private bool gamesPlayedAdded;

    private float fireworksTimer;

    [SerializeField] private ParticleSystem fireworksEffect;
    [SerializeField] private ParticleSystem confetti;
    [SerializeField] private GameObject Flames;

    [SerializeField] private GameObject NoAdsButton;


    private void Start()
    {
        Flames.SetActive(false);
        gamesPlayedAdded = false;
        totalWinAdded = false;
        PlayAgainButton.SetActive(false);
        rewardedAlready = false;
        rewarded = false;
        StartButton.SetActive(true);
        winSoundCast = false;
        errorSoundCast = false;
        FindObjectOfType<InterstitialAdsButton>().LoadAd();
        RoundTotal = 0;
        rollButton.SetActive(true);
        taptoexitscreen.SetActive(false);
        prefabStart();
        PP.SetActive(false);
        TOU.SetActive(false);
        NoAdsControl();
        showInterAd();

        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {

                GetComponent<AudioSource>().PlayOneShot(ReloadSound);

            }

        }

        PlayerPrefs.SetInt("SoundCounter", 1);
    }



    private void Update()
    {

        
        NoAdsButtonStatus();
        showBannerAd();

        if (CheckBoxTimer > 1)
        {
            CheckBoxCount();

            CheckGameStatus();

            if (boxCount == 0)
            {
                TotalNumberGUI.text = "WIN";
                totalWinUpdater();
                GamesPlayedUpdater();
                PlayAgainButton.SetActive(true);

                

                if (winSoundCast == false)
                {
                    GetComponent<AudioSource>().PlayOneShot(winSound);
                    winSoundCast = true;
                }


            }

            CheckBoxTimer = 0;
        }

        

        if (checkGameEnd && boxCount != 0)
        {
            if (PlayerPrefs.HasKey("Sound"))
            {
                if (PlayerPrefs.GetInt("Sound") == 1)
                {

                    if (errorSoundCast == false)
                    {
                        GetComponent<AudioSource>().PlayOneShot(errorSound, 0.9f);
                        errorSoundCast = true;
                    }

                }

            }

            for (int i = 0; i < Boxes.Length; i++)
            {
                if (Boxes[i] != null)
                {
                    Boxes[i].GetComponent<Destroyer>().mats[0] = BoxMetarial;
                    Boxes[i].GetComponent<Destroyer>().mats[1] = ErrorMetarial;
                    Boxes[i].GetComponent<Destroyer>().renderer.materials = Boxes[i].GetComponent<Destroyer>().mats;
                }

            }

            TotalNumberGUI.text = "Lose";
            Flames.SetActive(true);
            GamesPlayedUpdater();
            RetryButton.SetActive(true);
        }



        rollButtonActiveStatus();
        StartButtonStatus();
        RewardedButtonStatus();
        WriteRoundTotal();
        Fireworks();

    }

    private void CheckBoxCount()
    {
        boxCount = 0;

        for (int i = 0; i < Boxes.Length; i++)
        {
            if (Boxes[i] != null)
            {
                boxCount = boxCount + 1;
            }

        }

    }

    private void CheckGameStatus()
    {

        if (FindObjectOfType<DiceManager>().diceStop == true)
        {


            checkGameEnd = true;

            for (int i1 = 0; i1 < Boxes.Length; i1++)
            {
                if (Boxes[i1] != null)
                {

                    if (RoundTotal == Boxes[i1].GetComponent<Destroyer>().BoxNumber

                                                         ||

                                                         RoundTotal == 0)
                    {


                        checkGameEnd = false;

                        break;


                    }

                    for (int i2 = Boxes[i1].GetComponent<Destroyer>().BoxNumber; i2 < Boxes.Length; i2++)
                    {
                        if (Boxes[i2] != null)
                        {

                            if (RoundTotal == Boxes[i1].GetComponent<Destroyer>().BoxNumber +
                                                         Boxes[i2].GetComponent<Destroyer>().BoxNumber

                                                         ||

                                                         RoundTotal == 0)
                            {


                                checkGameEnd = false;

                                break;


                            }

                            for (int i3 = Boxes[i2].GetComponent<Destroyer>().BoxNumber; i3 < Boxes.Length; i3++)
                            {
                                if (Boxes[i3] != null)
                                {

                                    if (
                                                         RoundTotal == Boxes[i1].GetComponent<Destroyer>().BoxNumber +
                                                         Boxes[i2].GetComponent<Destroyer>().BoxNumber +
                                                         Boxes[i3].GetComponent<Destroyer>().BoxNumber

                                                         ||

                                                         RoundTotal == 0)
                                    {


                                        checkGameEnd = false;

                                        break;


                                    }

                                    for (int i4 = Boxes[i3].GetComponent<Destroyer>().BoxNumber; i4 < Boxes.Length; i4++)
                                    {
                                        if (Boxes[i4] != null)
                                        {


                                            if (RoundTotal == Boxes[i1].GetComponent<Destroyer>().BoxNumber +
                                                         Boxes[i2].GetComponent<Destroyer>().BoxNumber +
                                                         Boxes[i3].GetComponent<Destroyer>().BoxNumber +
                                                         Boxes[i4].GetComponent<Destroyer>().BoxNumber

                                                         ||

                                                         RoundTotal == 0)
                                            {


                                                checkGameEnd = false;

                                                break;


                                            }







                                        }

                                    }




                                }

                            }




                        }

                    }




                }

            }

        }

    }

    private void WriteRoundTotal()
    {

        if (!BoxSelectStarted)
        {
            RoundTotal = FindObjectOfType<DiceManager>().total;
        }

        if (FindObjectOfType<DiceManager>().diceStop == true)
        {

            BoxSelectStarted = true;


            if (boxCount != 0 && checkGameEnd == false)
            {
                TotalNumberGUI.text = RoundTotal.ToString();
            }

            if (TotalNumberGUI.text == "0")
            {
                TotalNumberGUI.text = " ";
            }


            CheckBoxTimer += Time.deltaTime;

        }


        else
        {
            BoxSelectStarted = false;
            TotalNumberGUI.text = " ";
        }

    }

    public void ReloadLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void rollButtonActiveStatus()
    {

        if (FindObjectOfType<DiceManager>().diceStop == true && RoundTotal == 0 && boxCount != 0 && StartButton.activeSelf == false)
        {

            rollButton.SetActive(true);

        }
        else
        {
            rollButton.SetActive(false);



        }


    }

    public void Settings_Func()
    {
        if (PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
            }
        }

        if (FindObjectOfType<settingsAnim>().status == false)
        {
            settings_ALL.GetComponent<Animator>().SetTrigger("open");
            taptoexitscreen.SetActive(true);
           
        }
        if (FindObjectOfType<settingsAnim>().status == true)
        {
            settings_ALL.GetComponent<Animator>().SetTrigger("close");
            taptoexitscreen.SetActive(false);
            PP.SetActive(false);
            TOU.SetActive(false);
            
        }
    }

    public void TapToExit()
    {
        if (PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Haptic.GetComponent<iOSHapticController>().TriggerImpactMedium();
            }
        }

        if (FindObjectOfType<settingsAnim>().status == true)
        {
            settings_ALL.GetComponent<Animator>().SetTrigger("close");
            taptoexitscreen.SetActive(false);
        }

        PP.SetActive(false);
        TOU.SetActive(false);
    }

    public void sound_On_Off()
    {

        if (PlayerPrefs.HasKey("Sound"))
        {
            int soundBool = PlayerPrefs.GetInt("Sound");

            if (soundBool == 0)
            {
                sound.sprite = soundOn_sprite;
                PlayerPrefs.SetInt("Sound", 1);

            }
            else if (soundBool == 1)
            {
                sound.sprite = soundOff_sprite;
                PlayerPrefs.SetInt("Sound", 0);
            }
        }
        else
        {

            PlayerPrefs.SetInt("Sound", 1);

        }

    }

    public void Vibration_On_Off()
    {

        if (PlayerPrefs.HasKey("Vibration"))
        {
            int vibrationBool = PlayerPrefs.GetInt("Vibration");

            if (vibrationBool == 0)
            {
                vibration.sprite = vibOn_sprite;
                PlayerPrefs.SetInt("Vibration", 1);

            }
            else if (vibrationBool == 1)
            {
                vibration.sprite = vibOff_sprite;
                PlayerPrefs.SetInt("Vibration", 0);
            }
        }
        else
        {

            PlayerPrefs.SetInt("Vibration", 1);

        }


    }

    private void prefabStart()
    {

        if (PlayerPrefs.HasKey("Vibration"))
        {
            int vibrationBool = PlayerPrefs.GetInt("Vibration");

            if (vibrationBool == 1)
            {
                vibration.sprite = vibOn_sprite;


            }
            else if (vibrationBool == 0)
            {
                vibration.sprite = vibOff_sprite;

            }
        }
        else
        {

            PlayerPrefs.SetInt("Vibration", 1);

        }



        if (PlayerPrefs.HasKey("Sound"))
        {
            int soundBool = PlayerPrefs.GetInt("Sound");

            if (soundBool == 1)
            {
                sound.sprite = soundOn_sprite;


            }
            else if (soundBool == 0)
            {
                sound.sprite = soundOff_sprite;

            }
        }
        else
        {

            PlayerPrefs.SetInt("Sound", 1);

        }




    }

    public void ShowPPandTOU()
    {
        PP.SetActive(true);
        TOU.SetActive(true);


    }

    public void privacy()
    {
        Application.OpenURL("https://pages.flycricket.io/ultimate-shut-the-bo/privacy.html");
    }

    public void Terms()
    {
        Application.OpenURL("https://pages.flycricket.io/ultimate-shut-the-bo/terms.html");
    }

    public void NoAdsControl()
    {

        if (PlayerPrefs.HasKey("NoAds") == false)
        {

            PlayerPrefs.SetInt("NoAds", 0);

        }
    }

    public void showInterAd()
    {
        if (PlayerPrefs.HasKey("NoAds"))
        {

            if (PlayerPrefs.GetInt("NoAds") == 0)
            {

                if (PlayerPrefs.HasKey("InterAd"))
                {

                    //print("interad count = "+PlayerPrefs.GetInt("InterAd"));


                    int i = PlayerPrefs.GetInt("InterAd");

                    if (i == 2)
                    {
                        PlayerPrefs.SetInt("InterAd", 0);


                        FindObjectOfType<InterstitialAdsButton>().ShowAd();


                    }
                    else
                    {
                        PlayerPrefs.SetInt("InterAd", i + 1);
                    }

                }
                else
                {
                    PlayerPrefs.SetInt("InterAd", 0);
                }

            }
        }


    }

    public void showBannerAd()
    {

        if (PlayerPrefs.HasKey("NoAds"))
        {

            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                FindObjectOfType<BannerAd>().ShowBannerAd();
            }
            if (PlayerPrefs.GetInt("NoAds") == 1)
            {
                FindObjectOfType<BannerAd>().HideBannerAd();
            }
        }




    }

    private void StartButtonStatus()
    {

        if (FindObjectOfType<DiceShooter>().started)
        {
            StartButton.SetActive(false);
        }
    }

    private void RewardedButtonStatus()
    {
        if (TotalNumberGUI.text == "Lose" && !rewardedAlready)
        {
            RewardedButton.SetActive(true);
        }

        if (rewarded && !rewardedAlready)
        {
            RewardedButton.SetActive(false);
            RetryButton.SetActive(false);
            rollButton.SetActive(true);

        }

        if (FindObjectOfType<DiceManager>().diceStop == false)
        {
            rewarded = false;
            checkGameEnd = false;
            RetryButton.SetActive(false);
            Flames.SetActive(false);
            for (int i = 0; i < Boxes.Length; i++)
            {
                if (Boxes[i] != null)
                {
                    Boxes[i].GetComponent<Destroyer>().mats[0] = BoxMetarial;
                    Boxes[i].GetComponent<Destroyer>().mats[1] = NumberMetarial;
                    Boxes[i].GetComponent<Destroyer>().renderer.materials = Boxes[i].GetComponent<Destroyer>().mats;
                }

            }


        }

    }

    private void totalWinUpdater()
    {

        if(totalWinAdded == false)
        {
            if(PlayerPrefs.HasKey("TotalWins"))
        {
            TotalWins = PlayerPrefs.GetInt("TotalWins");

            PlayerPrefs.SetInt("TotalWins",TotalWins + 1);

            KTGameCenter.SharedCenter().SubmitScore(TotalWins + 1,"TotalWins");

            totalWinAdded = true;

        }
        else
        {
            PlayerPrefs.SetInt("TotalWins",0);
        }

        }
    }

    private void GamesPlayedUpdater()
    {

        if(gamesPlayedAdded == false)
        {
            if(PlayerPrefs.HasKey("GamesPlayed"))
        {
            GamesPlayed = PlayerPrefs.GetInt("GamesPlayed");

            PlayerPrefs.SetInt("GamesPlayed",GamesPlayed + 1);

            KTGameCenter.SharedCenter().SubmitScore(GamesPlayed + 1,"GamesPlayed");

            gamesPlayedAdded = true;

        }
        else
        {
            PlayerPrefs.SetInt("GamesPlayed",0);
        }

        }

    }

    private void Fireworks()
   {

    if(TotalNumberGUI.text == "WIN")
    {

        if(fireworksTimer > 3.5f)
    {
        fireworksTimer = 0;
    } 

    if(fireworksTimer == 0)
    {
        Instantiate(fireworksEffect);
        Instantiate(confetti);

    }

    
    fireworksTimer += Time.deltaTime;
        
    }

   } 

   public void ClearBoard()
   {

        for (int i = 0; i < Boxes.Length; i++)
        {
            if (Boxes[i] != null)
            {
                Destroy(Boxes[i].gameObject);
            }

        }


   }

    public void openGameCenter()
    {
        KTGameCenter.SharedCenter().ShowLeaderboard("USTBLeaderboards");
    }


    private void NoAdsButtonStatus()
    {
        if(PlayerPrefs.HasKey("NoAds"))
        {
            if(PlayerPrefs.GetInt("NoAds") == 1)
            {
                NoAdsButton.SetActive(false);
            }

        }
    }



}



