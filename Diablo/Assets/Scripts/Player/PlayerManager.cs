using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    private void Awake()
    {

        instance = this;

    }

    public GameObject player;

    //TEMPORARIO caso o player morra recarrega a cena
    public void KillPlayer ()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
