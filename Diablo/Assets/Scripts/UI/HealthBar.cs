using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    // gerencia a vida maxima em relaçao ao slider e player
    public void SetMaxHealth (int health)
    {

        slider.maxValue = health; // valor maximo do slider equivale a vida do player
        slider.value = health; // garante que o slider ira começar com o valor maximo da vida do player

    }

    //gerencia a atualizaçao do slider em relaçao a vida do player
    public void setHealth (int health)
    {

        slider.value = health; // seta o valor do slider para a vida do player

    }

}
