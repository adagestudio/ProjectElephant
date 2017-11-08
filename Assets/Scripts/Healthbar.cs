using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public Image health;
    public Canvas barra;
    public GameObject referencia;

    float hp, maxHp = 100f;

	// Use this for initialization
	void Start () {
        hp = maxHp;
	}
	
	public void TakeDamage(float amount)
    {
        hp = Mathf.Clamp(hp - amount, 0f, maxHp); //El nuevo valor será el anterior menos el daño recibido,
                                                  //manteniéndose entre 0 y la máxima vida
        health.transform.localScale = new Vector2(hp / maxHp, 1);
    }

    public void DestroyBar()
    {
        Destroy(referencia.gameObject);
        Destroy(barra.gameObject);
        Debug.Log("He destruido la barra");
    }
}
