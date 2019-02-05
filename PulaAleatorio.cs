using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulaAleatorio : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D monstro1;
    public bool pulo = false;
    public int tempo = 0, trava , trava2;
    public float delay;
   



    void Start()
    {
        monstro1 = GetComponent<Rigidbody2D>();
        trava = 0;
        trava2 = 0;
    }


    void Update()
    {
        if (!pulo && trava2 == 0)
        {
            trava2++;
            StartCoroutine(pular());
        }
    }

    void FixedUpdate()
    {
        if (pulo && trava == 1)
        {
            trava--;
            monstro1.velocity = new Vector2(monstro1.velocity.x, 0f);
            //adiciona força pulo duplo
            monstro1.AddForce(new Vector2(0f, 400f));
            pulo = false;
            trava2--;
        }
    }

    IEnumerator pular()
    {
        
            tempo = Random.RandomRange(2, 5);
            yield return new WaitForSeconds(tempo);
            pulo = true;
            trava++;
        
    }
}
