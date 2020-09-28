using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazersScript : MonoBehaviour
{

    public float damage = 10f; // dano
    private bool isKinematicDisable = false; //

    //variaveis de controle
    private float cooldownTime = 1.5f; // tempo de cooldown pro iskinematic voltar
    private float cooldownTimeReady = 0f;
    private float destroyTime = BossLazers.LazerCooldownTime; // tempo para destruir
    private float destroyTimeReady = 0f;

    private void Start()
    {

        destroyTimeReady = Time.time + destroyTime; 

    }

    private void Update()
    {
        //volta o kinematic pro gameobject
        if (Time.time >= cooldownTimeReady)
        {

            gameObject.GetComponent<Rigidbody>().isKinematic = false; // desativa o kinematic
            isKinematicDisable = false;

        }

        if(Time.time >= destroyTimeReady)
        {

            Destroy(gameObject);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        CharacterStats target = collision.transform.GetComponent<CharacterStats>(); // puxa de outro script

        if (target != null || collision.gameObject.tag == "Player") // quando colide com o player
        {

            target.TakeDamage(damage); // toma dano

            gameObject.GetComponent<Rigidbody>().isKinematic = true; // ativa o kinematic do lazer
            isKinematicDisable = true; // variavel de controle
            cooldownTimeReady = Time.time + cooldownTime; // cooldown

        }
        
        if (collision.gameObject.tag == "Box") // se acertar uma weeping box
        {

            Destroy(collision.gameObject); // destroi a box

        }
        
    }
}
