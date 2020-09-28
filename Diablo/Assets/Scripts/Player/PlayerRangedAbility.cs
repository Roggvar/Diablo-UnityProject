using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRangedAbility : MonoBehaviour
{

    public float range = 100f; // nao lembro, mas provavelmente nao deve ser importante, provavelmente deve ser privado tbm
    public float bulletForce = 10f; // velocidade do tiro
    public float cooldownTime = 6f; // tempo de cooldown do tiro
    private float cooldownReady = 0f; // quando que o tiro estiver pronto //BUG APARECENDO NO INSPECTOR
    public float movementCooldown = 0.5f; // cooldown para o player voltar a se movimentar
    

    public Transform firePoint; // aloca uma posiçao para spanwar o tiro
    public GameObject bulletPrefab; // aloca o prefab que deve ser spawnado
    Camera cam; // aloca cam
    NavMeshAgent agent; // aloca navmesh

    void Start()
    {

        agent = GetComponent<NavMeshAgent>(); // navmesh
        cam = Camera.main; // cam

    }

    void Update()
    {

        if(Time.time >= cooldownReady && PlayerController.isUsingHability == false) // so eh acionado quando o cooldown tiver zerado
        { 
            
            if (Input.GetKeyDown(KeyCode.Q)) //Testa a tecla Q
            {

                PlayerController.isUsingHability = true; // Diz que o player esta usando uma habilidade

                agent.isStopped = true; // Para a movimentaçao do player

                Shoot();
                BulletSpawn();

                PlayerController.movementReady = Time.time + movementCooldown; // aplica o tempo de cooldown da caminhada
                cooldownReady = Time.time + cooldownTime; // aplica o tempo de cooldown do tiro

            }
        }

        PlayerController.HabilityUse(); // Gerencia a movimentaçao e uso de habilidades

    }

    //Gerencia toda o processo de tiro
    void Shoot()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, range))
        {

            Vector3 hitPosition = hit.point; //Pega a posiçao no campo 3d do raypoint
            hitPosition.y = 0f;

            transform.LookAt(hitPosition); //Faz com que o player olhe para a posiçao do mouse

        }

    }

    //Spawna o tiro e aplica a velocidade
    void BulletSpawn()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // coordenadas do tiro
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); // adiciona a velocidade ao tiro

    }

}
