using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspetosManager : MonoBehaviour {

    private SliderJoint2D espinho;
    private JointMotor2D aux;
    
	void Start () {
        espinho = GetComponent<SliderJoint2D>();
        aux = espinho.motor;
	}
	
	void Update () {
		if(espinho.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range(-1, -5);
            espinho.motor = aux;
        }
        if (espinho.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range(1, 5);
            espinho.motor = aux;
        }
    }
}
