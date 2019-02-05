using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORCEGOL : MonoBehaviour
{

    public SliderJoint2D morcego;
    public JointMotor2D aux;


    void Start()
    {

        morcego = GetComponent<SliderJoint2D>();
        aux = morcego.motor;
        

    }


    void Update()
    {

        if (morcego.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range(-1, -5);
            morcego.motor = aux;
           
        }

        if (morcego.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range(1, 5);
            morcego.motor = aux;
         
        }
    }
}
