using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : MonoBehaviour {

    Animator animator;

    private float cooldown = 1.5f;
    public float cooldownTimer;

    public Interactable enemigo;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if(cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer == 0) //Si no estoy realizando un ataque
        {
            animator.Play("Attack", -1, 0f);
            cooldownTimer = cooldown;
        }
	}
}
