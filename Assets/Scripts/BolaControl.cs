using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour {
	
	//Posição Seta


	//Seta
//	[SerializeField]private Image setaImg;
	public GameObject setaGo;

	//Ang
	public float zRotate;
	public bool liberaRot = false;
	public bool liberaTiro = false;


	//Força
	private Rigidbody2D bola;
	public float force = 0;
	public GameObject seta2Img;

	//Paredes
	private Transform paredeLD,paredeLE;

    //Mortebola Anim

    [SerializeField]
    private GameObject MorteBolaAnim;

    private Collider2D toqueCol;

	void Awake()
	{
		
		setaGo = GameObject.Find ("Seta");
		seta2Img = setaGo.transform.GetChild (0).gameObject;
		setaGo.GetComponent<Image> ().enabled = false;
		seta2Img.GetComponent<Image> ().enabled = false;
		paredeLD = GameObject.Find ("ParedeLD").GetComponent<Transform> ();
		paredeLE = GameObject.Find ("ParedeLE").GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {

		//Força

		bola = GetComponent<Rigidbody2D>();

		
	}
		
	
	// Update is called once per frame
	void Update () {
		RotacaoSeta ();
		InputDeRotação ();
		LimitaRotacao();
		PosicionaSeta ();

		//Força
		ControlaForca();
		AplicaForca();

		//Paredes
		Paredes();
		
	}

	void PosicionaSeta()
	{
		setaGo.GetComponent<Image>().rectTransform.position = transform.position;
	}
		

	void RotacaoSeta()
	{
		setaGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3 (0,0,zRotate);
	}

	//Input teclado
	void InputDeRotação()
	{
		//if (Input.GetKey (KeyCode.UpArrow)) 
		//{
		//	zRotate += 2.5f;
		//}

		//if (Input.GetKey (KeyCode.DownArrow)) 
		//{
		//	zRotate -= 2.5f;
		//}
		if (liberaRot == true)
		{
			float moveY = Input.GetAxis("Mouse Y");

			if (zRotate < 90 )
			{
				if (moveY > 0)
				{
					zRotate += 2.5f;
				}
			}
			if (zRotate > 0)
			{
				if (moveY < 0)
				{
					zRotate -= 2.5f;
				}
			}
		}
	}
	void LimitaRotacao()
	{
		if (zRotate >= 90)
		{
			zRotate = 90;
		}
		if (zRotate <= 0)
		{
			zRotate = 0;
		}
	}

	void OnMouseDown()
	{

		if(GameManage.instance.tiro == 0)
		{
			liberaRot = true;
			setaGo.GetComponent<Image> ().enabled = true;
			seta2Img.GetComponent<Image> ().enabled = true;

            toqueCol = GameObject.FindGameObjectWithTag("toque").GetComponent<Collider2D>();

        }

	}
	void OnMouseUp()
	{
		liberaRot = false;
		setaGo.GetComponent<Image> ().enabled = false;
		seta2Img.GetComponent<Image> ().enabled = false;

		if(GameManage.instance.tiro == 0 && force > 0)
		{
			liberaTiro = true;
			seta2Img.GetComponent<Image> ().fillAmount = 0;
			AudioManager.instance.SonsFXToca (1);
			GameManage.instance.tiro = 1;
            toqueCol.enabled = false;
		}

	}

	//Força

	void AplicaForca()
	{
		float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
		float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);
		if (liberaTiro == true)
		{
			bola.AddForce(new Vector2(x, y));
			liberaTiro = false;
		}
	}
	void ControlaForca()
	{
		if(liberaRot == true)
		{
			float moveX = Input.GetAxis("Mouse X");
			if(moveX < 0)
			{
				seta2Img.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
				force = seta2Img.GetComponent<Image>().fillAmount * 1000;
			}
			if (moveX > 0)
			{
				seta2Img.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
				force = seta2Img.GetComponent<Image>().fillAmount * 1000;
			}
		}
	}

	void BolaDinamica()
	{
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	//Matar Bolas 
	void Paredes()
	{
		if(this.gameObject.transform.position.x > paredeLD.position.x)
		{
			Destroy (this.gameObject);
			GameManage.instance.bolasEmCena -= 1;
			GameManage.instance.bolasNum -= 1;
		}

		if(this.gameObject.transform.position.x < paredeLE.position.x)
		{
			Destroy (this.gameObject);
			GameManage.instance.bolasEmCena -= 1;
			GameManage.instance.bolasNum -= 1;
		}
	}

	void OnTriggerEnter2D(Collider2D outro)
	{
		if (outro.gameObject.CompareTag ("morte")) 
		{
            if (GameManage.instance.win == true)
            {
                Instantiate(MorteBolaAnim, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                Instantiate(MorteBolaAnim, transform.position, Quaternion.identity);
			    Destroy (this.gameObject);
			    GameManage.instance.bolasEmCena -= 1;
			    GameManage.instance.bolasNum -= 1;
            }
		}
        if (outro.gameObject.CompareTag("win"))
        {
            GameManage.instance.win = true;
            int temp = OndeEstou.instance.fase -2;
            temp++;
            PlayerPrefs.SetInt("Level"+ temp, 1);
        }
	}

}
