using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolasShop : MonoBehaviour {

	public static BolasShop instance;

	public List<Bolas> bolasList = new List<Bolas> ();
	public List<GameObject> bolaSuporteList = new List<GameObject> ();

	public GameObject baseBolaItem;
	public Transform conteudo;

	void Awake () 
	{
		if (instance == null) 
		{
			instance = this;
		}

	}

	void Start () 
	{
		FillList ();
	}
	

	void Update () 
	{
		
	}

	void FillList () 
	{
		foreach(Bolas b in bolasList)
		{
			GameObject itensBola = Instantiate (baseBolaItem) as GameObject;
			itensBola.transform.SetParent (conteudo, false);
			BolasSuporte item = itensBola.GetComponent<BolasSuporte> ();

			item.bolaID = b.bolasID;
			item.bolaPreco.text = b.bolasPreco.ToString();
			item.btnCompra.GetComponent<CompraBola> ().bolasIDe = b.bolasID;


			//Lista bolaSuporte
			bolaSuporteList.Add(itensBola);

			if (b.bolasComprou == true) {
				item.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + b.bolasNomeSprite);
				item.bolaPreco.text = "FREE";
			} 
			else 
			{
				item.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + b.bolasNomeSprite + "_cinza");
			}
		}
	}

	public void UpdateSprite(int bola_id)
	{
		for(int i = 0; i < bolaSuporteList.Count;i++)
		{
			BolasSuporte bolasSuportScript = bolaSuporteList [i].GetComponent<BolasSuporte> ();

			if(bolasSuportScript.bolaID == bola_id)
			{
				for(int j = 0; j < bolasList.Count;j++)
				{
					if(bolasList[j].bolasID == bola_id)
					{
						if (bolasList [j].bolasComprou == true) {
							bolasSuportScript.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + bolasList [j].bolasNomeSprite);
							bolasSuportScript.bolaPreco.text = "FREE!";

						} else {

							bolasSuportScript.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + bolasList [j].bolasNomeSprite + "_cinza");
						}
					}

				}
			}
		}
	}
}
