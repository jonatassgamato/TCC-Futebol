using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBomba : MonoBehaviour {

    private GameObject bombaRep;

	void Start () {

        bombaRep = GameObject.Find("BombaR");

	}
	
	void Update () {
        StartCoroutine (Vida());
	}

    IEnumerator Vida()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(bombaRep);
        Destroy(this.gameObject);
    }
}
