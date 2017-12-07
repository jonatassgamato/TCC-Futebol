using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdsUnity : MonoBehaviour {

    public static AdsUnity instance;
    private Button btnAds;

    public bool adsBtnAcionado = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += PegaBtn;
    }

    void PegaBtn(Scene cena, LoadSceneMode modo)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 7)
        {

            btnAds = GameObject.Find("AdsBtn").GetComponent<Button>();
            btnAds.onClick.AddListener(AdsBtn);
        }
    }

    void AdsBtn()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo",new ShowOptions() { resultCallback = AdsAnalise });
            adsBtnAcionado = true;
        }
    }

    void AdsAnalise(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            ScoreManager.instance.ColetaMoedas(300);
        }
    }

    public void showAds()
    {
        if (PlayerPrefs.HasKey("AdsUnity"))
        {
            if (PlayerPrefs.GetInt("AdsUnity") == 3)
            {
                if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                }
                PlayerPrefs.SetInt("AdsUnity", 1);
            }
            else
            {
                PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("AdsUnity", 1);
        }
    }
	
}
