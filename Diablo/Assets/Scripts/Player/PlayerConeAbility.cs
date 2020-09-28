using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerConeAbility : MonoBehaviour
{

    public float range = 100f; // nao lembro, mas provavelmente nao deve ser importante, provavelmente deve ser privado tbm
    public float cooldownTime = 6f; // tempo de cooldown do AoE
    private float cooldownReady = 0f; // quando que o AoE estiver pronto
    public float movementCooldown = 3f; // cooldown para o player voltar a se movimentar

    public Transform firePoint;
    public GameObject conePrefab;

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

            if (Input.GetKeyDown(KeyCode.R)) //Testa a tecla R
            {

                PlayerController.isUsingHability = true; // diz se o player esta usando uma habilidade

                agent.isStopped = true; // para o movimento do player

                Cone();

                PlayerController.movementReady = Time.time + movementCooldown;// aplica o tempo de movimentaçao
                cooldownReady = Time.time + cooldownTime; // aplica o tempo de cooldown do tiro

            }

        }

        PlayerController.HabilityUse();

    }

    void Cone ()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {

            Vector3 hitPosition = hit.point; //Pega a posiçao no campo 3d do raypoint
            hitPosition.y = 0f;

            transform.LookAt(hitPosition); //Faz com que o player olhe para a posiçao do mouse

        }

        GameObject Cone = Instantiate(conePrefab, firePoint.position, firePoint.rotation); // coordenadas do Cone

    }

}
