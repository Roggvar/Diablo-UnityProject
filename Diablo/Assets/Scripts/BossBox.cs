using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBox : MonoBehaviour
{

    private Vector3 playerTarget; // alvo da caixa

    private bool foundPlayer = false; // variavel de controle
    private float speed = 5f; // velocidade de movimento da caixa
    private float spawmMovementTimer = 5f; // cooldown de spawn para começar a se mover

    [Header("Cooldown")]
    public float boxCooldownReady = 0f; // coolwond de controle
    public float boxCooldownTime = 5f; // cooldown para se mover

    public float damage = 10f; // dano da caixa

    private void Start()
    {

        boxCooldownReady = Time.time + spawmMovementTimer; // adiciona um time para a caixa começar a se mover

    }


    private void Update()
    {

        BoxManager(); // chama o metodo de movimento da caixa

    }

    //gerencia o movimento da caixa
    private void BoxManager ()
    {

        // acha a posiçao do player
        if (Time.time >= boxCooldownReady && foundPlayer == false)
        {

            playerTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
            foundPlayer = true;

            boxCooldownReady = Time.time + boxCooldownTime; // cooldown

        }

        // move a caixa na direçao do player
        if (foundPlayer == true)
        {

            transform.position = Vector3.MoveTowards(transform.position, playerTarget, speed * Time.deltaTime);

        }

        // reseta o controle do movimento da caixa
        if (Time.time >= boxCooldownReady && foundPlayer == true)
        {
            foundPlayer = false;

        }

    }

    //gerencia a colisao da caixa
    private void OnCollisionEnter(Collision collision)
    {

        CharacterStats target = collision.transform.GetComponent<CharacterStats>(); //Puxa de outro script

        if (target != null || collision.gameObject.tag == "Player") // testa se o alvo for diferente de vazio e um player
        {

            target.TakeDamage(damage); // uso do script CharacterStats para o dano
            Destroy(gameObject); // quando colidir com um inimigo ira se destruir
            BossLazers.boxLimit--; // diminui o numero de boxes do boss

        }
        else if(collision.gameObject.tag == "Box") // se colidir com outras boxes
        {

            Destroy(gameObject); // se destroi
            BossLazers.boxLimit--; // diminui o numero de boxes do boss

        }

    }

}
