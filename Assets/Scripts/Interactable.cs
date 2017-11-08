using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public float radius = 3f; //Distancia a la que debe estar el jugador para interactuar con algo
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    Animator animator;
    int golpes;
    bool muerto;
    private float cooldown = 1.5f;
    private float cooldownTimer;

    public GameObject healthbar;

    private static int foeContador; //Si es static, el valor no vuelve a 0 aunque otro objeto ejecute el script
    public static int oro;
    public Text foe;

    public void Start()
    {
        //Contador de golpes
        animator = GetComponentInChildren<Animator>();
        oro = 0;
        foeContador = 0;
    }

    public virtual void Interact() //Un objeto no hereda este método
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }

        golpes = animator.GetInteger("Golpes");
        muerto = animator.GetBool("Muerto");
        if (Input.GetKeyDown(KeyCode.F) && muerto == false && cooldownTimer == 0)
        {
            Ataque();
            cooldownTimer = cooldown;
        }
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position); //Calcula la distancia entre
                                                                                    //el jugador y el enfocado
            if (distance <= radius)
            {
                Interact();
                //hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected() //Visualiza distancias
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    private void Ataque()
    {
        healthbar.SendMessage("TakeDamage", 40f); //En el script Healthbar
        Debug.Log("Attack to " + transform.name + "!");
        animator.Play("Impact", -1, 0f);
        golpes++;
        animator.SetInteger("Golpes", golpes);
        if (golpes >= 3)
        {
            animator.SetBool("Muerto", true);
            healthbar.SendMessage("DestroyBar");
            foeContador++;
            foe.text = "Enemigos: " + foeContador + "/10";
        }
    }
}
