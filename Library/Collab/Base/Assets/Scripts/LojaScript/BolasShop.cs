﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

			//Lista bolaSuporte
			bolaSuporteList.Add(itensBola);

			if (b.bolasComprou == true) {
				item.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + b.bolasNomeSprite);
			} 
			else 
			{
				item.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + b.bolasNomeSprite +"_cinza");
			}
		}
	}

	void UpdateSprite(int bola_id)
	{
		for(int i = 0; i<bolaSuporteList.Count; i++)
		{
			BolasSuporte bolasSuporteScript = bolaSuporteList [i].GetComponent<BolasSuporte> ();

			if (bolasSuporteScript.bolaID == bola_id) 
			{
				for(int j = 0; j<bolasList.Count; j++)
				{
					if(bolasList[j].bolasID == bola_id)
					{
						if (bolasList [j].bolasComprou == true) {
							bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + bolasList [j].bolasNomeSprite);
						} else 
						{
							bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + bolasList [j].bolasNomeSprite +"_cinza");
						}
					}
				}

			}
		}

	}
}
