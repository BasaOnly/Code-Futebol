using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {



	public static UIManager instance;
	[SerializeField]
	private Text pontosUI,bolasUI;
	[SerializeField]
	private GameObject losePainel,winPainel,pausePainel;
	[SerializeField]
	public Button pauseBtn,pauseBTN_Return;
	[SerializeField]
	private Button btnNovamenteLose, btnLevelLose, btnAds;//Botões de Lose
	private Button btnNovamenteFase;
	private Button btnLevelWin,btnNovamenteWin,btnAvancaWin;//Botões de Win
    public Image imgBola;
    public Sprite[] imagensBola;

	public int moedasNumAntes,moedasNumDepois,resultado;

	void Awake()
	{
		
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

		PegaDados ();

	}

	void Carrega(Scene cena, LoadSceneMode modo)
	{
		PegaDados ();
	}

	void PegaDados()
	{
		if (OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3) {

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                //Elementos da UI pontos e bolas
                pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();
                bolasUI = GameObject.Find("bolasUI").GetComponent<Text>();

                //Paineis
                losePainel = GameObject.Find("LosePainel");
                winPainel = GameObject.Find("WinPainel");
                pausePainel = GameObject.Find("PausePainel");
                //Botões de pause
                pauseBtn = GameObject.Find("pause").GetComponent<Button>();
                pauseBTN_Return = GameObject.Find("Pause").GetComponent<Button>();
                //Botões de Lose
                btnNovamenteLose = GameObject.Find("NovamenteLOSE").GetComponent<Button>();
                btnLevelLose = GameObject.Find("MenuFasesLOSE").GetComponent<Button>();
                btnAds = GameObject.Find("AdsBtn").GetComponent<Button>();
                //Botões de Win
                btnLevelWin = GameObject.Find("MenuFasesWIN").GetComponent<Button>();
                btnNovamenteWin = GameObject.Find("NovamenteWIN").GetComponent<Button>();
                btnAvancaWin = GameObject.Find("avancarWIN").GetComponent<Button>();

                //Botões fase
                btnNovamenteFase = GameObject.Find("NovamenteGame").GetComponent<Button>();
                //Eventos

                //MUDA BOLA*********************************************************
                imgBola = GameObject.FindWithTag("imgBola").GetComponent<Image>();
                if(OndeEstou.instance.bolaEmUso == 0)
                {
                    imgBola.sprite = imagensBola[0];
                }
                if (OndeEstou.instance.bolaEmUso == 1)
                {
                    imgBola.sprite = imagensBola[1];
                }
                if (OndeEstou.instance.bolaEmUso == 2)
                {
                    imgBola.sprite = imagensBola[2];
                }

                if (OndeEstou.instance.bolaEmUso == 3)
                {
                    imgBola.sprite = imagensBola[3];
                }
                if (OndeEstou.instance.bolaEmUso == 4)
                {
                    imgBola.sprite = imagensBola[4];
                }
                if (OndeEstou.instance.bolaEmUso == 5)
                {
                    imgBola.sprite = imagensBola[5];
                }



            }
			//Eventos pause
			pauseBtn.onClick.AddListener (Pause);
			pauseBTN_Return.onClick.AddListener (PauseReturn);

			//Eventos You lose

			btnNovamenteLose.onClick.AddListener (JogarNovamente);
			btnLevelLose.onClick.AddListener (Levels);
            btnAds.onClick.AddListener(AppLoveEx.instance.ShowReawarded);

            //Eventos You win
            btnLevelWin.onClick.AddListener (Levels);
			btnNovamenteWin.onClick.AddListener (JogarNovamente);
			btnAvancaWin.onClick.AddListener (ProximaFase);

			//Evento Novamente Game


			btnNovamenteFase.onClick.AddListener (JogarNovamente);

			//

			moedasNumAntes = PlayerPrefs.GetInt ("moedasSave");

		}	
	}

	public void StartUI()
	{
		LigaDesligaPainel ();
	}



	public void UpdateUI()
	{
		pontosUI.text = ScoreManager.instance.moedas.ToString();
		bolasUI.text = GameManager.instance.bolasNum.ToString ();
		moedasNumDepois = ScoreManager.instance.moedas;


	}

	public void GameOverUI()
	{
		losePainel.SetActive (true);
	}

	public void WinGameUI()
	{
		winPainel.SetActive (true);
       
    }

	void LigaDesligaPainel()
	{
		StartCoroutine (tempo());
	}

	void Pause()
	{   
		pausePainel.SetActive (true);
		pausePainel.GetComponent<Animator> ().Play ("MoveUI_PAUSE");	
		Time.timeScale = 0;
	}

	void PauseReturn()
	{		
		pausePainel.GetComponent<Animator> ().Play ("MoveUI_PAUSER");	
		Time.timeScale = 1;
		StartCoroutine (EsperaPause());
	}


	IEnumerator EsperaPause()
	{
		yield return new WaitForSeconds (0.8f);
		pausePainel.SetActive (false);
	}



	IEnumerator tempo()
	{
		Time.timeScale = 1;
		
		yield return new WaitForSeconds (0.01f);
		losePainel.SetActive (false);
		winPainel.SetActive (false);
		pausePainel.SetActive (false);
	}


	void JogarNovamente()
	{
		if (GameManager.instance.win == false /*&& AdsUnity.instance.adsBtnAcionado == true*/) {
			SceneManager.LoadScene (OndeEstou.instance.fase);
			//AdsUnity.instance.adsBtnAcionado = false;
		}
		else if(GameManager.instance.win == false /*&& AdsUnity.instance.adsBtnAcionado == false*/)
		{
			SceneManager.LoadScene (OndeEstou.instance.fase);
			resultado = moedasNumDepois - moedasNumAntes;
			ScoreManager.instance.PerdeMoedas (resultado);
			resultado = 0;

		} else {

			SceneManager.LoadScene (OndeEstou.instance.fase);
			resultado = 0;
		}
	}

	void Levels()
	{
		if (GameManager.instance.win == false /*&& AdsUnity.instance.adsBtnAcionado == true*/) {
			//AdsUnity.instance.adsBtnAcionado = false;
			SceneManager.LoadScene (1);
		}
		else if(GameManager.instance.win == false /*&& AdsUnity.instance.adsBtnAcionado == true*/)
        {
			resultado = moedasNumDepois - moedasNumAntes;
			ScoreManager.instance.PerdeMoedas (resultado);
			resultado = 0;
			SceneManager.LoadScene (1);
		}

		else
		{
			resultado = 0;
			SceneManager.LoadScene (1);
		}
	}


	void ProximaFase()
	{
		if(GameManager.instance.win == true)
		{
			int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene (temp);
		}
	}


}
