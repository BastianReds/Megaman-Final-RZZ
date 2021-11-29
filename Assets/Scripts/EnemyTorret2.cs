using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTorret2 : MonoBehaviour
{
    BoxCollider2D myCollider;
    Animator myAnimator;

    [SerializeField] GameManager gm;

    [SerializeField] GameObject bullet;
    public bool left = false;

    [SerializeField] float fireRate;
    float nextFire = 0;

    [SerializeField] int vida;
    [SerializeField] GameObject torretaDestruida;

    [SerializeField] AudioClip sfxEnemyDestroy;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("IsShooting", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LookMegamanIzq())
        {
            bullet.transform.localScale = (new Vector2((transform.localScale.x * 1), transform.localScale.y));

            if (Time.time >= nextFire)
            {
                Instantiate(bullet, transform.position - new Vector3(-0.8f, 0, 0), transform.rotation);
                nextFire = Time.time + fireRate;
            }

            left = true;
        }

        else if (LookMegamanDer())

        {
            bullet.transform.localScale = (new Vector2((transform.localScale.x * -1), transform.localScale.y));

            transform.localScale = new Vector2((transform.localScale.x * -1), transform.localScale.y);

            if (Time.time >= nextFire)
            {
                Instantiate(bullet, transform.position - new Vector3(0.8f, 0, 0), transform.rotation);
                nextFire = Time.time + fireRate;
            }
            left = false;
        }

        if (Physics2D.Raycast(myCollider.bounds.center, new Vector2(transform.localScale.x, transform.localScale.y), myCollider.bounds.extents.x - 15f, LayerMask.GetMask("Player"))|| Physics2D.Raycast(myCollider.bounds.center, new Vector2(transform.localScale.x, transform.localScale.y), myCollider.bounds.extents.x + 15f, LayerMask.GetMask("Player")))
        {
            myAnimator.SetBool("isShooting", true);
        }
        else
        {
            myAnimator.SetBool("isShooting", false);
        }
    }

    public bool LookMegamanDer()
    {
        RaycastHit2D hitdeRaycast = Physics2D.Raycast(myCollider.bounds.center, new Vector2(transform.localScale.x, transform.localScale.y), myCollider.bounds.extents.x - 15f, LayerMask.GetMask("Player"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(transform.localScale.x, transform.localScale.y) * (myCollider.bounds.extents.x - 10f), Color.red);

        return hitdeRaycast.collider != null;
    }

    public bool LookMegamanIzq()
    {
        RaycastHit2D hitdeRaycast = Physics2D.Raycast(myCollider.bounds.center, new Vector2(transform.localScale.x, transform.localScale.y), myCollider.bounds.extents.x + 15f, LayerMask.GetMask("Player"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(transform.localScale.x, transform.localScale.y) * (myCollider.bounds.extents.x + 10f), Color.red);

        return hitdeRaycast.collider != null;
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
        Instantiate(torretaDestruida, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
    }
}
