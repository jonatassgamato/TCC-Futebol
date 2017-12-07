using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaManager : MonoBehaviour
{

    [SerializeField]
    private GameObject bombaFx;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D (Collision2D outro)
    {
        if (outro.gameObject.CompareTag("bola"))
        {
            Instantiate(bombaFx, new Vector2 (this.transform.position.x, this.transform.position.y),Quaternion.identity);
        }
    }
}
