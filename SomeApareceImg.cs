using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeApareceImg : MonoBehaviour
{
    private Color cor;
    private Renderer bolaRender;
    public float vidaImagem;
    public int trava;

    void Start()
    {
        bolaRender = gameObject.GetComponent<Renderer>();
        //PEGA A COR DA BOLA
        cor = bolaRender.material.GetColor("_Color");
        trava = 1;
        vidaImagem = 4f;
    }

    void Update()
    {

        if(vidaImagem > 0 && trava == 1)
        {
            vidaImagem -= Time.deltaTime;
            //EFEITO QUE FAZ A BOLA INDO DESAPARECENDO
            bolaRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaImagem));
            if(vidaImagem <= 1)
            {
                trava = 0;
            }
        }

        else if(trava == 0 && vidaImagem < 4)
        {
            vidaImagem += Time.deltaTime;
            //EFEITO QUE FAZ A BOLA INDO DESAPARECENDO
            bolaRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaImagem));
            if(vidaImagem >= 4)
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
