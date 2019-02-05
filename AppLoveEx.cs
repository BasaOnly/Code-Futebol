using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppLoveEx : MonoBehaviour
{//16 21
    public static AppLoveEx instance;
    public bool umaVez = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        AppLovin.LoadRewardedInterstitial();

    }



    // Start is called before the first frame update
    void Start()
    {
        AppLovin.InitializeSdk();
        AppLovin.PreloadInterstitial();
        AppLovin.LoadRewardedInterstitial();


    }

    public void ShowAd()
    {

        if (PlayerPrefs.HasKey("AdAppLovin"))
        {
            if (PlayerPrefs.GetInt("AdAppLovin") == 4)
            {
                if (AppLovin.HasPreloadedInterstitial())
                {
                    AppLovin.ShowInterstitial();
                }

                PlayerPrefs.SetInt("AdAppLovin", 1);
            }
            else
            {
                PlayerPrefs.SetInt("AdAppLovin", PlayerPrefs.GetInt("AdAppLovin") + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("AdAppLovin", 1);
        }

        if (PlayerPrefs.GetInt("AdAppLovin") > 5)
        {
            PlayerPrefs.SetInt("AdAppLovin", 1);
        }

    }

    public void ShowReawarded()
    {
      
      

        if (AppLovin.IsIncentInterstitialReady())
        {
            if (umaVez)
            {
                umaVez = false;
                AppLovin.ShowRewardedInterstitial();
                ScoreManager.instance.ColetaMoedas(500);
            }
        }
    }

}
