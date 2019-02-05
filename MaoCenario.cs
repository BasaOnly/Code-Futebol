using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoCenario : MonoBehaviour
{
    private Color cor;
    private Renderer imgRender;
    public float vidaImagem;
    public int trava;

    void Start()
    {
        imgRender = gameObject.GetComponent<Renderer>();
        //PEGA A COR DA BOLA
        cor = imgRender.material.GetColor("_Color");
        trava = 1;
        vidaImagem = 2f;
    }

    void Update()
    {

        if (vidaImagem > 0 && trava == 1)
        {
            vidaImagem -= Time.deltaTime;
            //EFEITO QUE FAZ A BOLA INDO DESAPARECENDO
            imgRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaImagem));
            if (vidaImagem <= 0)
            {
                trava = 0;
            }
        }

        else if (trava == 0 && vidaImagem < 2)
        {
            vidaImagem += Time.deltaTime;
            //EFEITO QUE FAZ A BOLA INDO DESAPARECENDO
            imgRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaImagem));
            if (vidaImagem >= 2)
            {
                trava = 1;
            }
        }

    }

    IEnumerator SomeAparece()
    {

        yield return new WaitForSeconds(4f);

    }




}
