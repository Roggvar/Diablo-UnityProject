using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{

    PlayerManager playerManager;
    CharacterStats myStats; // alocamento dos status do objeto

    void Start()
    {

        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>(); // aplica o alocamento dos status do objeto, puxando de outro script

    }

    //substitui o virtual da classe Interactable Interact
    public override void Interact()
    {

        base.Interact();

        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>(); // puxa o script CharacterCombat

        if(playerCombat != null)
        {

            playerCombat.Attack(myStats);

        }

    }

}
