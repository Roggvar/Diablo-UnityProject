using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazers : MonoBehaviour
{

    //  ENCOUNTER DESIGN
    //  15seg(LAZER) + 3seg(INTERMISSION) + 30seg(WEEPINGBOXES) + 2seg(INTERMISSION) + 15seg(SHOOTING) + 5(INTERMISSION) ||
    // TOTAL = 70seg

    [Header("Atributes")]
    public float lazerRotationSpeed; // rotaçao dos lazers
    public float bulletForce = 10f; // velocidade do tiro
    public float speedRotation = 10f; // velocidade de rotaçao do boss
    public float boxForce = 5f; // velocidade de queda da box
    public float fireRate = 1f; // x tiros por segundo

    [Header("Lazer Cooldowns")]
    public float lazerCooldownReady = 0f;
    public static float LazerCooldownTime = 15f;

    [Header("Lazers Transforms")]
    public Transform LazerCenter;
    public GameObject lazerFront;
    public GameObject lazerBack;
    public GameObject lazerRightSide;
    public GameObject lazerLeftSide;

    [Header("Lazer Prefab")]
    public GameObject lazerPrefab;

    [Header("Bullet Tranform")]
    public Transform firePoint;

    [Header("Bullet Prefab")]
    public GameObject bulletPrefab;

    [Header("Box Cooldowns")]
    public float boxCooldownReady = 0f;
    public float boxCooldownTime = 5f;

    [Header("Box Prefab")]
    public GameObject boxPrefab;

    //TIRO
    private Transform target; // transform do alvo do tiro
    private GameObject enemyPlayer; // gameobject do alvo do tiro

    private Vector3 boxTargetPosition;

    //Variaveis de controle
    private bool isUsingLazerHability = false; // controle de habilidade LASER
    private bool isUsingBoxHability = false; // controle de habilidade BOX
    private bool isShooting = false; // controle de habilidade SHOOTING
    private bool rotationChanged = false; // controle da troca de rotaçao dos lazers
    private float rotationDirectionCooldown = LazerCooldownTime / 2; // cooldown para trocar de rotaçao
    public static int boxLimit = 0; // Limite de box ativas durante a luta
    private float fireCooldown = 0f; // cooldown para atirar

    //CONTROLE GERAL DA LUTA
    private float laserMajorCooldown = 0f; // controla o cooldown do laser durante toda a luta
    private float boxMajorCooldwon = 0f; // controla o cooldown do box durante toda a luta
    private float shootingMajorCooldown = 0f; // controla o cooldown do shooting durante toda a luta
    private float majorCooldown = 70f; // // cooldown entre todas as habilidades
    private float boxDone = 0f; // cooldwon de quando acaba de cair as box
    private bool boxStarted = false; // se começou a cair as box
    private bool shootingStarte = false; // se começou a atirar
    private float shootingDone = 0f; // cooldown de quando acaba o shooting

    private void Start()
    {

        enemyPlayer = GameObject.FindGameObjectWithTag("Player"); // acha o objeto com tag player
        target = enemyPlayer.transform; // alvo do boss
        boxTargetPosition = new Vector3(0f, 10f, 0f); // adiciona a altura do spawn das box


        laserMajorCooldown = Time.time + 5f; // adiciona um timer ao laser antes da luta começar
        boxMajorCooldwon = Time.time + 20f; // adiciona um timer ao box antes da luta começar
        shootingMajorCooldown = Time.time + 48f; // adiciona um timer ao shooting antes da luta começar

    }

    private void Update()
    {

        #region Rotaçao do Boss
        //Rotaçao do Boss
        Vector3 direction = target.position - transform.position; //aponta pro target
        Quaternion lookRotation = Quaternion.LookRotation(direction); // matematica da rotaçao do alvo
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation).eulerAngles; // transforma a matematica do quaternion em angulos que a unity compreende (i.e. X, Y, Z)
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f); // rotaciona o alvo apenas no eixo y
        #endregion

        #region Lazers

        LazerCenter.transform.Rotate(0, lazerRotationSpeed * Time.deltaTime, 0); // Rotaçao dos lazers

        //Controle de quando os lazers sao spawnnados
        if (Time.time >= lazerCooldownReady && Time.time >= laserMajorCooldown)
        {

            boxStarted = false; // reseta as variaveis
            shootingStarte = false; // reseta as variaveis
            rotationChanged = false; // reseta as variaveis

            LazerSpawn(); // chama o metodo
            lazerCooldownReady = Time.time + LazerCooldownTime; // cooldown

            rotationDirectionCooldown = Time.time + (LazerCooldownTime / 2); // cooldown para trocar de rotaçao

            laserMajorCooldown = Time.time + majorCooldown; // adiciona o cooldown geral da luta
            boxMajorCooldwon = boxMajorCooldwon + 3f; // adiciona a intermissao antes da box

        }

        //Controle de quando é feita a troca de direçao dos lazers
        if(Time.time >= rotationDirectionCooldown && rotationChanged == false)
        {

            lazerRotationSpeed = -(lazerRotationSpeed); // muda a direçao
            rotationChanged = true; // controle

        }

        #endregion

        #region Weeping Boxes
        if (Time.time >= boxMajorCooldwon)
        {

            if (Time.time >= boxCooldownReady && boxLimit <= 3)
            {

                BoxSpawn(); // chama o metodo de spawn das box
                boxLimit++; // adiciona ao limite de box
                boxCooldownReady = Time.time + boxCooldownTime; // cooldown

                if (boxStarted == false)
                {

                    boxDone = Time.time + 30f; // adiciona o tempo de duraçao das caixas
                    boxStarted = true; // controle

                }

            }

            if(Time.time >= boxDone)
            {

                boxMajorCooldwon = Time.time + majorCooldown; // adiciona o cooldown da luta a box
                shootingMajorCooldown = shootingMajorCooldown + 2f; // adiciona a intermissao da luta antes do shooting

            }

        }
        #endregion

        #region Shooting
        if (Time.time >= shootingMajorCooldown)
        {

            if (shootingStarte == false)
            {

                shootingDone = Time.time + 15f; // tempo de duraçao do shooting
                shootingStarte = true; // controle

            }

            if (fireCooldown <= 0f)
            {

                //isShooting = true; //controle
                BulletSpawn(); // chama o metodo
                fireCooldown = 2.5f; // seta o cooldown

            }

            fireCooldown -= Time.deltaTime; //reduz o cooldown do tiro

            if (Time.time >= shootingDone)
            {

                shootingMajorCooldown = Time.time + majorCooldown; // adiciona o tempo de cooldwon geral da luta
                laserMajorCooldown = laserMajorCooldown + 5f; // adiciona a intermissao antes do laser

            }

        }

        #endregion

    }

    // Metodo de Spawn dos Lazers
    private void LazerSpawn ()
    {

            //LazerFront
            GameObject lazer1 = Instantiate(lazerPrefab, lazerFront.transform.position, lazerFront.transform.rotation);
            lazer1.transform.parent = lazerFront.transform;

            //LazerBack
            GameObject lazer2 = Instantiate(lazerPrefab, lazerBack.transform.position, lazerBack.transform.rotation);
            lazer2.transform.parent = lazerBack.transform;

            //LazerRight
            GameObject lazer3 = Instantiate(lazerPrefab, lazerRightSide.transform.position, lazerRightSide.transform.rotation);
            lazer3.transform.parent = lazerRightSide.transform;

            //LazerLeft
            GameObject lazer4 = Instantiate(lazerPrefab, lazerLeftSide.transform.position, lazerLeftSide.transform.rotation);
            lazer4.transform.parent = lazerLeftSide.transform;

    }

    // Metodo de Spawn dos Bullet
    void BulletSpawn()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); // adiciona a velocidade ao tiro

    }

    // Metodo de spawn das box
    void BoxSpawn()
    {

        GameObject box = Instantiate(boxPrefab,target.position + boxTargetPosition, target.rotation);
        Rigidbody rb = box.GetComponent<Rigidbody>();

        rb.AddForce(target.up * -(boxForce), ForceMode.Impulse);
    }

}
