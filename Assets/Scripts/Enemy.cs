using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    BoxCollider2D myCollider;
    Animator myAnimator;

    [SerializeField] GameObject player;
    [SerializeField] float radioDeteccion;
    private GameObject path;

    [SerializeField] GameManager gm;

    [SerializeField] int vida;
    
    [SerializeField] AudioClip sfxEnemyDestroy;

    // Start is called before the first frame update
    void Start()
    {
        path = GameObject.Find("Path");
        myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Physics2D.OverlapCircle(transform.position, radioDeteccion, LayerMask.GetMask("Player")) != null)
        {
            path.SetActive(true);
        }
        else
        {
            path.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {
            vida--;

            if (vida == 0)
            {     
                AudioSource.PlayClipAtPoint(sfxEnemyDestroy, Camera.main.transform.position);
                myAnimator.SetTrigger("Destruido");               
            }
        }
    }

    public void Destruido()
    {
        gm.ReducirNumEnemigos();
        Destroy(this.gameObject);
    }
}
