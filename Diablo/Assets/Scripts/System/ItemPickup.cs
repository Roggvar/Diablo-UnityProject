using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;

    //Puxa o void que customizavel do interactable
    public override void Interact()
    {

        base.Interact();

        PickUp();

    }

    // Gerencia a pega do item
    void PickUp()
    {

        Debug.Log("Pegou o Item");

        bool wasPickuped = Inventory.instance.Add(item); // adiciona o item ao inventario do player

        // Quando for pego, destroi o item no mundo
        if(wasPickuped)
        {

            Destroy(gameObject);

        }

    }

}
