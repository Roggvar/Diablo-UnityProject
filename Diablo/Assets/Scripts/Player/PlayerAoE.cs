using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAoE : MonoBehaviour
{

    public float cooldownTime = 6f; // tempo de cooldown do AoE
    float cooldownReady = 0f; // quando que o AoE estiver pronto
    public float movementCooldown = 3f; // cooldown para o player voltar a se movimentar


    public Transform player;
    public GameObject aoePrefab;

    Camera cam;
    PlayerMotor motor;
    NavMeshAgent agent;

    void Start()
    {

        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {

        if (Time.time >= cooldownReady && PlayerController.isUsingHability == false) // so eh acionado quando o cooldown tiver zerado
        {

            if (Input.GetKeyDown(KeyCode.E)) //Testa a tecla E
            {

                PlayerController.isUsingHability = true; // diz se o player esta usando uma habilidade

                agent.isStopped = true; // para o movimento do player

                AoE();

                PlayerController.movementReady = Time.time + movementCooldown;// aplica o tempo de movimentaçao
                cooldownReady = Time.time + cooldownTime; // aplica o tempo de cooldown do tiro

            }

        }

        PlayerController.HabilityUse();

    }


    void AoE()
    {

        GameObject AoE = Instantiate(aoePrefab, player.position, player.rotation); // coordenadas do AoE

    }
    
}
