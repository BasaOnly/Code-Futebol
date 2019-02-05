using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//Bola
	[SerializeField]
	public GameObject[] bola;
	public int bolasNum = 3;
	public int bolasEmCena = 0;
	public Transform pos;
	public bool win, lose;
	public int tiro = 0;
	public bool jogoComecou;
    public Image imgBola;
    public int primeiraVez;
    public GameObject mao;

    private bool adsUmaVez = false;


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

		pos = GameObject.Find ("posStart").GetComponent<Transform> ();
        


    }

	void Carrega(Scene cena, LoadSceneMode modo)
	{	
		
		if(OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
		{
           
            pos = GameObject.Find ("posStart").GetComponent<Transform> ();
			StartGame ();   
        }
	}
 
    void Start () {	


		StartGame ();
      


    }
	
	// Update is called once per frame
	void Update () {

		ScoreManager.instance.UpdateScore ();

		UIManager.instance.UpdateUI ();

       

        if (bolasNum <= 0 && win == false)
			{
				GameOver ();
			}

			if(win == true)
			{
                lose = false;
				WinGame ();
			}

		if(win == false)
		{
            lose = true;
			NascBolas ();
		}

			

	
		

	}

	void NascBolas()
	{
		if (OndeEstou.instance.fase >= 4) {

			if (bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= 0.05f) {
                

                Instantiate (bola[OndeEstou.instance.bolaEmUso], new Vector2 (pos.position.x, pos.position.y), Quaternion.identity);
				bolasEmCena += 1;
				tiro = 0;
			}
		}
		else
		{

			if (bolasNum > 0 && bolasEmCena == 0) {				
				Instantiate (bola[OndeEstou.instance.bolaEmUso], new Vector2 (pos.position.x, pos.position.y), Quaternion.identity);
				bolasEmCena += 1;
				tiro = 0;
			}

		}

	}

	void GameOver()
    {
        UIManager.instance.pauseBtn.interactable = false;
        UIManager.instance.GameOverUI ();
		jogoComecou = false;
		if(adsUmaVez == false)
		{
            AppLoveEx.instance.ShowAd();
            adsUmaVez = true;
		}
	}

	void WinGame()
	{
        UIManager.instance.pauseBtn.interactable = false;
        UIManager.instance.WinGameUI ();
		jogoComecou = false;

	}

	void StartGame()
	{
		jogoComecou = true;
		bolasNum = 3;
		bolasEmCena = 0;
		win = false;

		UIManager.instance.StartUI ();
		adsUmaVez = false;
        AppLoveEx.instance.umaVez = true;

	}



}
