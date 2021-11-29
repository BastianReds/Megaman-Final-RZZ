using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator myAnimator;

    [SerializeField] float speed;
    [SerializeField] AudioClip sfxBulletDestroy;

    private GameObject player;
    Rigidbody2D rb;

    bool destruido = false;
    bool fin = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.localScale.x < 0 && rb.velocity.x < 0.1f && destruido==false)
        {
            rb.velocity = new Vector2(-speed, 0);
            Destruir();
        }

        else if (player.transform.localScale.x > 0 && rb.velocity.x >-0.1f && destruido == false)
        {
            rb.velocity = new Vector2(speed, 0);
            Destruir();
        }   
    }

    public void Fin()
    {
        fin = true;
        if (fin == true)
        {
            Destroy(this.gameObject);
        }
    }

    public void Destruir()
    {
        destruido = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (destruido == true)
        {
            myAnimator.SetTrigger("Destroy");        
        }
        AudioSource.PlayClipAtPoint(sfxBulletDestroy, Camera.main.transform.position);
    }   
}
