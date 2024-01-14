using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] private Animator anm;
    [SerializeField] private bool isDie;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public int type;
    public Vector3 velocity;
    float speed;
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        speed = objectManager.speedBullet;
    }
    // Update is called once per frame
    void Update()
    {
        if (!objectManager.isTime)
            rb.velocity = velocity * speed;
        else
            rb.velocity = new Vector2(0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cham");
        //if (!collision.CompareTag("Player"))
        {
            velocity = new Vector3(0, 0, 0);
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.no1);
            if (!isDie)
            {
                if (collision.CompareTag("Player") && gameObject.CompareTag("Ene"))
                {
                    Debug.Log("gunnn");
                    if (!collision.gameObject.GetComponent<Player>().isShield)
                        collision.gameObject.GetComponent<Player>().getBullet();
                }
                else if (collision.CompareTag("Enemy") && gameObject.CompareTag("BulletPL"))
                {
                    Debug.Log("suu");
                    collision.gameObject.GetComponent<Enemy>().getBullet();
                }
                isDie = true;
                anm.Play("gun");
                Invoke("_des", 0.2f);
            }
        }
    }
    void _des()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
