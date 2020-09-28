using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] // cria na unuty uma nova sei la oq
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    // Gerencia o uso do item, pode ser substituido devido ao Virtual
    public virtual void Use()
    {



        //Debug.Log("Usou " + name);

    }

    // Gerencia a remoçao do item do inventario do player
    public void RemoveFromInventory ()
    {

        Inventory.instance.Remove(this);

    }

}
