using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoMenuIniciar : MonoBehaviour {

    private Animator barraAnim;
    private bool sobe;

	public void Jogar()
    {
        SceneManager.LoadScene(1);
    }
    public void AnimaMenu()
    {
        barraAnim = GameObject.FindGameObjectWithTag("barraAnimTag").GetComponent<Animator>();
        if (sobe == false)
        {
            barraAnim.Play("CONF_UI");
            sobe = true;
        }
        else
        {
            barraAnim.Play("CONF_UI_INVERSE");
            sobe = false;
        }
        
    }
}
