using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampHealth : MonoBehaviour {

    public Image healthLabel;
	
	// Update is called once per frame
	void Update () {
        Vector3 healthPos = Camera.main.WorldToScreenPoint(this.transform.position); //Toma las coordenadas del juego
                                                                                   //y las amolda a la ventana de juego
        healthLabel.transform.position = healthPos;
	}
}
