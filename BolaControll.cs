using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControll : MonoBehaviour
{



    //Seta
    public GameObject setaGO;
    public bool sobe = true, entrou;
    public bool desce = false;

    //Ang
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    //Força
    private Rigidbody2D bola;
    public float force = 0;
    public GameObject seta2Img;
    //Paredes
    private Transform paredeLD, paredeLE;
    //MorteBola Anim
    [SerializeField]
    private GameObject MorteBolaAnim;

    //toque
    private Collider2D toqueCol;

    public bool morreParado;
    public Animator anim;
    public Sprite[] bolaMonstro;
    public SpriteRenderer spriteBola;

    void Awake()
    {

        setaGO = GameObject.Find("Seta");
        seta2Img = setaGO.transform.GetChild(0).gameObject;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        paredeLD = GameObject.Find("ParedeLD").GetComponent<Transform>();
        paredeLE = GameObject.Find("ParedeLE").GetComponent<Transform>();
        spriteBola = GetComponent<SpriteRenderer>();
    }


    // Use this for initialization
    void Start()
    {
        AppLoveEx.instance.umaVez = true;
        entrou = false;
        morreParado = false;
        anim = GetComponent<Animator>();
        bola = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        AtivaAnim();

        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();
        PosicionaSeta();
        //Força
        ControlaForca();
        AplicaForca();
        //Paredes
        Paredes();
        if (bola.velocity.magnitude < 0.5f && morreParado)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }



    void PosicionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;

    }

    void RotacaoSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    void InputDeRotacao()
    {

        if (liberaRot == true)
        {

            float moveY = Input.GetAxis("Mouse Y");

            if (zRotate < 90)
            {
                if (moveY > 0)
                {
                    zRotate += 1.5f;
                }
            }

            if (zRotate > 0)
            {
                if (moveY < 0)
                {
                    zRotate -= 1.5f;
                }
            }


        }

    }

    void LimitaRotacao()
    {
        if (zRotate >= 90)
        {
            zRotate = 90;
        }

        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    void OnMouseDown()
    {
        if (GameManager.instance.tiro == 0)
        {
         
            liberaRot = true;
            setaGO.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;

            toqueCol = GameObject.FindGameObjectWithTag("toque").GetComponentInChildren<Collider2D>();
        }



    }

    void OnMouseUp()
    {

        liberaRot = false;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        if (GameManager.instance.tiro == 0 && force > 0)
        {
            liberaTiro = true;
            seta2Img.GetComponent<Image>().fillAmount = 0;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
            toqueCol.enabled = false;
        }

    }

    //Força

    void AplicaForca()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro == true)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
            StartCoroutine(morreParadoBola());
        }

    }

    void ControlaForca()
    {

        if (liberaRot == true)
        {
            bool moveX = Input.GetMouseButton(0);

            if (moveX && sobe == true)
            {
                seta2Img.GetComponent<Image>().fillAmount += 0.5f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
                if (seta2Img.GetComponent<Image>().fillAmount == 1)
                {
                    sobe = false;
                    desce = true;
                }

            }

            else if (moveX && desce == true)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 0.5f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
                if (seta2Img.GetComponent<Image>().fillAmount == 0)
                {
                    sobe = true;
                    desce = false;
                }
            }
        }
    }

    void BolaDinamica()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void Paredes()
    {
        if (this.gameObject.transform.position.x > paredeLD.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (this.gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("morte"))
        {
            Instantiate(MorteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (outro.gameObject.CompareTag("win"))
        {
            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fase - 2;

            PlayerPrefs.SetInt("Level" + temp, 1);
        }
    }

    IEnumerator morreParadoBola()
    {
        yield return new WaitForSeconds(3);
        morreParado = true;
    }

    public void AtivaAnim()
    {
        if (OndeEstou.instance.bolaEmUso == 5)
        {   
            anim.Play("bolaNascAnim5");
            if (!entrou)
            {
                entrou = true;
                StartCoroutine(trocaSpriteMonstro());
            }
         }

        if (OndeEstou.instance.bolaEmUso == 4)
        {
            anim.Play("bolaNascAnim4");
        }

        if (OndeEstou.instance.bolaEmUso == 3)
        {
            anim.Play("bolaNascAnim3");
        }

        if (OndeEstou.instance.bolaEmUso == 2)
        {
            anim.Play("bolaNascAnim2");
        }

        if (OndeEstou.instance.bolaEmUso == 1)
        {
            anim.Play("bolaNascAnim1");
        }

        if (OndeEstou.instance.bolaEmUso == 0)
        {
            anim.Play("bolaNascAnim");
        }
    }

    IEnumerator trocaSpriteMonstro()
    {
        while (!morreParado)
        {
            spriteBola.sprite = bolaMonstro[0];
            yield return new WaitForSeconds(0.4f);
            spriteBola.sprite = bolaMonstro[1];
            yield return new WaitForSeconds(0.4f);
            spriteBola.sprite = bolaMonstro[2];
            yield return new WaitForSeconds(0.4f);
            spriteBola.sprite = bolaMonstro[3];
            yield return new WaitForSeconds(0.4f);

        }
    }
}
