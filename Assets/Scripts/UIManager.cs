using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {

	public static UIManager instance;
	private Text pontosUI,bolasUI;
    [SerializeField]
    private GameObject losePainel, winPanel,pausePainel;
    [SerializeField]
    private Button pauseBtn,pauseBTN_Return,btnJogaNovamenteFase,btnMenuFasesPause;
    [SerializeField]
    private Button btnNovamenteLose, btnLevelLose;//Botões de Lose

    private Button btnLevelWin,btnNovamenteWin,btnAvancaWin;//Botões de Win

    public int moedasNumAntes, moedasNumDepois,resultado;

    void Awake()
	{
		if (instance == null) 
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}

		else
		{
			Destroy (gameObject);
		}

		SceneManager.sceneLoaded += Carrega;
        PegaDados();

    }

	void Carrega(Scene cena, LoadSceneMode modo)
	{
        PegaDados();
    }

    void PegaDados()
    {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 7)
        {
            // Elementos da UI pontos e bolas
            pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();
            bolasUI = GameObject.Find("bolasUI").GetComponent<Text>();
            //Paineis
            losePainel = GameObject.Find("LosePainel");
            winPanel = GameObject.Find("WinPainel");
            pausePainel = GameObject.Find("PausePainel");
            //Botões de pause
            pauseBtn = GameObject.Find("pause").GetComponent<Button>();
            pauseBTN_Return = GameObject.Find("Pause").GetComponent<Button>();
            btnMenuFasesPause = GameObject.Find("MenuFasesPause").GetComponent<Button>();
            //Jogar novamente mesma fase
            btnJogaNovamenteFase = GameObject.Find("Restart").GetComponent<Button>();

            

            //Botões de Lose
            btnNovamenteLose = GameObject.Find("NovamenteLOSE").GetComponent<Button>();
            btnLevelLose = GameObject.Find("MenuFasesLOSE").GetComponent<Button>();
            //Botões de Win
            btnLevelWin = GameObject.Find("MenuFasesWIN").GetComponent<Button>();
            btnNovamenteWin = GameObject.Find("NovamenteWIN").GetComponent<Button>();
            if (SceneManager.GetActiveScene().buildIndex != 6)
            {
                btnAvancaWin = GameObject.Find("AvancarWIN").GetComponent<Button>();
            }
            

            //Eventos

            //Eventos pause
            pauseBtn.onClick.AddListener(Pause);
            pauseBTN_Return.onClick.AddListener(PauseReturn);
            btnJogaNovamenteFase.onClick.AddListener(JogarNovamente);
            btnMenuFasesPause.onClick.AddListener(Levels);
            //Eventos You Lose

            btnNovamenteLose.onClick.AddListener(JogarNovamente);
            btnLevelLose.onClick.AddListener(Levels);

            //Eventos You Win
            btnLevelWin.onClick.AddListener(Levels);
            btnNovamenteWin.onClick.AddListener(JogarNovamente);
            if (SceneManager.GetActiveScene().buildIndex != 6)
            {
                btnAvancaWin.onClick.AddListener(ProximaFase);
            }
            moedasNumAntes = PlayerPrefs.GetInt("moedasSave");
        }
    }
    public void StartUI()
    {
        LigaDesligaPainel();
    }

    public void UpdateUI()
	{
		pontosUI.text = ScoreManager.instance.moedas.ToString ();
		bolasUI.text = GameManage.instance.bolasNum.ToString ();
        moedasNumDepois = ScoreManager.instance.moedas;
	}

    public void GameOverUI()
    {
        losePainel.SetActive(true);
    }
    public void WinGameUI()
    {
        winPanel.SetActive(true);
    }
    void LigaDesligaPainel()
    {
        StartCoroutine(tempo());
    }

    void Pause()
    {
        pausePainel.SetActive(true);
        pausePainel.GetComponent<Animator>().Play("MoveUI_PAUSE");
        Time.timeScale = 0;
    }
    void PauseReturn()
    {
        pausePainel.GetComponent<Animator>().Play("MoveUI_PAUSER");
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());
    }

    IEnumerator EsperaPause()
    {
        yield return new WaitForSeconds(0.8f);
        pausePainel.SetActive(false);
    }

    IEnumerator tempo( )
    {
        yield return new WaitForSeconds(0.001f);
        losePainel.SetActive(false);
        winPanel.SetActive(false);
        pausePainel.SetActive(false);
    }

    void JogarNovamente()
    {
        if (GameManage.instance.win == false && AdsUnity.instance.adsBtnAcionado == true)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            AdsUnity.instance.adsBtnAcionado = false;
        }
        else if(GameManage.instance.win == false && AdsUnity.instance.adsBtnAcionado == false)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
        }
        else
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = 0;
        }
        PauseReturn();
    }

    void Levels()
    {
        if (GameManage.instance.win == false && AdsUnity.instance.adsBtnAcionado == true)
        {
            AdsUnity.instance.adsBtnAcionado = false;
            SceneManager.LoadScene(1);
        }
        else if(GameManage.instance.win == false && AdsUnity.instance.adsBtnAcionado == false)
        {
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
            SceneManager.LoadScene(1);
        }
        else
        {
            resultado = 0;
            SceneManager.LoadScene(1);
        }
        OndeEstou.instance.fase = 1;
		PauseReturn ();
    }
    void ProximaFase()
    {
        if (GameManage.instance.win == true)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);
        }
    }

}
