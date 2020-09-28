using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    public HealthBar healthbar;

    void Start()
    {

        EquipmentManager.instance.onEquipmentChange += OnEquipmentChange;
        healthbar.SetMaxHealth(maxHealth);

    }

    void Update()
    {

        healthbar.setHealth((int)currentHealth);

    }


    // Gerencia a troca de equipamento do player
    void OnEquipmentChange (Equipment newItem, Equipment oldItem)
    {
        //item novo
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier); // adicionado a armadura do item ao player
            damage.AddModifier(newItem.damageModifier); // adiciona o dano do item ao player
        }

        // item velho
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier); // remove a armadura do item do player
            damage.RemoveModifier(oldItem.damageModifier); // remove o dano do item ao player
        }

    }

    // Gerencia a morte do player
    public override void Die()
    {

        base.Die();

        PlayerManager.instance.KillPlayer();

    }

}
