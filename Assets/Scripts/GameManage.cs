using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {

	public static GameManage instance;

	//Bola
	[SerializeField]
	public GameObject[] bola;
	public int bolasNum = 3;
	public bool bolaMorreu = false;
	public int bolasEmCena = 0;
	public Transform pos;
    public bool win;
	public int tiro = 0;
    //public int ondeEstou;
    public bool jogoComecou;

    private bool adsUmaVez = false;

    void Awake () {
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}

		else
		{
			Destroy (gameObject);
		}

		SceneManager.sceneLoaded += Carrega;
        pos = GameObject.Find("Posição Inicial").GetComponent<Transform>();

    }

	void Carrega(Scene cena, LoadSceneMode modo)
	{
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 7)
        {
		    pos = GameObject.Find ("Posição Inicial").GetComponent<Transform> ();
            StartGame();
        }

    }

	void Start () 
	{
        StartGame();
        ScoreManager.instance.GameStartScoreM ();

	}


	void Update () {
		ScoreManager.instance.UpdateScore ();
		UIManager.instance.UpdateUI ();

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			SceneManager.LoadScene ("Level12");
		}
		NascBolas ();
        if (bolasNum <= 0 && win != true)
        {
            GameOver();
        }

        if( win == true)
        {
            Wingame();
        }
	}

	void NascBolas()
	{
        if (OndeEstou.instance.fase >= 6)
        {
            if (bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= -0.10f && win != true)
            {
				Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
        else
        {
            if (bolasNum > 0 && bolasEmCena == 0 && win != true)
            {
				Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
	}
    void GameOver()
    {
        UIManager.instance.GameOverUI();
        jogoComecou = false;

        if (adsUmaVez == false)
        {
            AdsUnity.instance.showAds();
            adsUmaVez = true;
        }

    }
    void Wingame()
    {
        UIManager.instance.WinGameUI();
        jogoComecou = false;
    }
    void StartGame()
    {
        jogoComecou = true;
        bolasNum = 3;
        bolasEmCena = 0;
        win = false;
        UIManager.instance.StartUI();
        adsUmaVez = false;
    }
}
