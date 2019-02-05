using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonRed : MonoBehaviour
{
    public SliderJoint2D morcego;
    public JointMotor2D aux;
    private Transform trans;
    private int trava;
    private bool corre;



    void Start()
    {
        trans = GetComponent<Transform>();
        morcego = GetComponent<SliderJoint2D>();
        aux = morcego.motor;
        trava = 0;
        corre = false;
    }


    void Update()
    {

        if (morcego.limitState == JointLimitState2D.UpperLimit)
        {
            if (!corre)
            {
                StartCoroutine(Parado());
            }
            if (corre)
            {
                //Move
                aux.motorSpeed = -4;
                morcego.motor = aux;
                corre = false;
            }
            //Flip
            if (trava == 0)
            {
                trava++;
                trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
            }
        }

        if (morcego.limitState == JointLimitState2D.LowerLimit)
        {
            if (!corre)
            {
                StartCoroutine(Parado());
            }
            if (corre)
            {
                //Move
                aux.motorSpeed = 4;
                morcego.motor = aux;
                corre = false;
            }
            //Flip
            if (trava == 1)
            {
                trava--;
                trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
            }
        }
    }

    IEnumerator Parado()
    {
        yield return new WaitForSeconds(2f);
        corre = true;
    }
}
