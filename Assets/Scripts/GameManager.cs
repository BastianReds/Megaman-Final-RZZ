using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject[] enemigos;
    [SerializeField] Text EnemigosCuentaTxt;
    [SerializeField] GameObject ganaste;
    private int numEnemigos;

    // Start is called before the first frame update
    void Start()
    {
        
        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        numEnemigos = enemigos.Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemigos == 0)
        {
            ganaste.SetActive(true);
        }

        TextoNumEnemies();
    }

    public void ReducirNumEnemigos()
    {
        numEnemigos = numEnemigos - 1;
    }

    void TextoNumEnemies()
    {
        EnemigosCuentaTxt.text = numEnemigos.ToString();
    }
}
