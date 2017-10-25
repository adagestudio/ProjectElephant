using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) //Si pulso botón izquierdo del ratón
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Mueve la cámara al punto seleccionado
                                                                 //de forma lineal
            RaycastHit hit; //Guarda la información del objeto clicado

            if(Physics.Raycast(ray, out hit, 100, movementMask)) //Si se ha clicado un objeto que pertenezca a la máscara
            {
                motor.MoveToPoint(hit.point);

                //Parar de enfocarse en otros objetos
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1)) //Si pulso botón derecho del ratón
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Mueve la cámara al punto seleccionado
                                                                 //de forma lineal
            RaycastHit hit; //Guarda la información del objeto clicado

            if (Physics.Raycast(ray, out hit, 100)) //Si se puede interactuar con el objeto
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                //Enfocarse en el objeto
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
