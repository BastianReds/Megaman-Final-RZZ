using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reinicar : MonoBehaviour
{
    public void Reiniciar()
    {
        SceneManager.LoadScene("EscenaJuego");
    } 
}
