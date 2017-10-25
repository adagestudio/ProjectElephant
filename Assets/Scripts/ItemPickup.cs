using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable { //Deriva de esa clase. Hereda variables y métodos

    public Text gold;

    public override void Interact()
    {
        base.Interact(); //Ejecuta el método original
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up item");
        oro++;
        //Add to inventory
        gold.text = "Oro: " + oro + "/10";
        
        Destroy(gameObject);
    }

}
