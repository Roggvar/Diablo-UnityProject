using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    public static EquipmentManager instance;

    private void Awake()
    {

        instance = this;

    }

    Equipment[] currentEquipment; // aloca o equipamente sendo usado

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChange;

    Inventory inventory;

    void Start()
    {

        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

    }

    // Gerencia o ato de equipar um item
    public void Equip (Equipment newItem)
    {

        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {

            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

        }

        if(onEquipmentChange != null)
        {

            onEquipmentChange.Invoke(newItem, oldItem);

        }

        currentEquipment[slotIndex] = newItem;

    }

}
