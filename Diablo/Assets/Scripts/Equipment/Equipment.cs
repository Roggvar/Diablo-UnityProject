using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{

    public EquipmentSlot equipSlot;

    public int armorModifier; // armadura
    public int damageModifier; // dano

    //Gerencia o uso do objeto, substuiu o Virtual de outro script
    public override void Use()
    {

        base.Use();

        EquipmentManager.instance.Equip(this); // equipa o item
        RemoveFromInventory(); // remove do inventario

    }

}

public enum EquipmentSlot {Head, Chest, Legs, Weapon, OffHand, Feet } // partes que podem ser equipadas pelo player
