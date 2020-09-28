using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // botao de PlayGame
    public void PlayGame ()
    {
        //TEMPORARIAMENTE DESATIVADO - MOTIVO: NAO EH MINHA RESPONSABILIDADE CUIDAR DESTE LEVEL
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Troca de cena de acordo com a ordem na build

    }

    //TEMPORARIO - ACESSAR O LEVEL TEST
    public void TESTPLAY()
    {

        SceneManager.LoadScene("PLAYTEST"); // Troca de cena de acordo com o nome dado

    }

    // fecha a aplicaçao
    public void QUitGame ()
    {

        Application.Quit();

    }

}
