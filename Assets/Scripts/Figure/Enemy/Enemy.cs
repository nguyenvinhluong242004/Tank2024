using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int _type, id, move;
    [SerializeField] public float blood, speed, speedBullet;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator anm;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private Blood bl;
    [SerializeField] public GameObject _bullet1, _bullet3, _light1, _light3, headGun, enemy, _bl, pa;

    [SerializeField] public Vector2 up, down, left, right;
    [SerializeField] public Vector2 velocity;
    bool isReset, isDie;


    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        _bl.SetActive(false);
        up = new Vector2(0, 1f);
        down = new Vector2(0, -1f);
        left = new Vector2(-1f, 0);
        right = new Vector2(1f, 0); 
        isReset = true;
        Invoke("_reset", Random.Range(0f, 2f));
        move = 8;
        speed = objectManager.speedEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectManager.isTime)
        {
            _rb.velocity = new Vector2(0, 0);
        }
        else
        {
            if (!isDie && !isReset)
            {
                int randomNumber = Random.Range(1, 5);

                _rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
                if (randomNumber == 1)
                {
                    velocity = up;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                    move = 8;
                }
                else if (randomNumber == 2)
                {
                    velocity = down;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
                    move = 2;
                }
                else if (randomNumber == 3)
                {
                    velocity = right;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
                    move = 6;
                }
                else
                {
                    velocity = left;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
                    move = 4;
                }
                isReset = true;
                Invoke("_reset", Random.Range(3f, 4f));
                Invoke("getGun", 1f);

            }
            _rb.velocity = velocity * speed;
            if (!isDie)
                pa.transform.position = transform.position + new Vector3(0, 3.48f, 0);
            if (!isDie && blood < 0)
            {
                anm.Play("die");
                objectManager.killE += 1;
                objectManager.killEne.text = $"{objectManager.killE}/{objectManager.countEne}";
                if (objectManager.isSound)
                    objectManager.Aus.PlayOneShot(objectManager.no);
                headGun.SetActive(false);
                Invoke("_des", 1f);
                isDie = true;
                checkWin();
            }
        }
    }
    void checkWin()
    {
        if (objectManager.killE == objectManager.countEne)
        {
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.sWin);
            objectManager.idLV += 1;
            if (objectManager.idLV < 4)
            {
                objectManager.loadingData.players[objectManager.idPlayer].Levels[objectManager.idLV] = 1;
                objectManager.imgLV[objectManager.idLV].color = new Color(1f, 1f, 1f, 1f);
            }    
            objectManager.uiWin.SetActive(true);
            int po = Random.Range(7, 10);
            objectManager.loadingData.players[objectManager.idPlayer].Point += objectManager.countEne * po;
            objectManager.loadingData.players[objectManager.idPlayer].Gold += objectManager.countEne * 100;
            objectManager.textWin.text = $"- Kill: {objectManager.countEne}\n- Gold: +{objectManager.countEne * 100}\n- Point: +{objectManager.countEne * po}";
            objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
            objectManager.point.text = objectManager.loadingData.players[objectManager.idPlayer].Point.ToString();

            objectManager.loadingData.SavePlayersToFile();
            objectManager.loadingData.resetRank(false);
        }    
    }    
    void _reset()
    {
        isReset = false;
    }    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("cham");
        velocity = new Vector2(0, 0); 
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_bl.activeSelf && collision.CompareTag("BulletPL"))
        {
            _bl.SetActive(true);
            Invoke("closeBlood", 1f);
        }    
        //if (!isDie && collision.CompareTag("Bullet"))
        //{
        //    anm.Play("die");
        //    headGun.SetActive(false);
        //    Invoke("_des", 1f);
        //    isDie = true;
        //}
    }
    void _des()
    {
        enemy.SetActive(false);
        Destroy(enemy);
    }
    void closeBlood()
    {
        _bl.SetActive(false);
    }    
    void getGun()
    {
        GameObject newBullet;
        if (_type == 1)
        {
            newBullet = Instantiate(_bullet1, transform.position, _bullet1.transform.rotation);
            _light1.SetActive(true);
            Invoke("resetLight1", 0.3f);
        }
        else
        {
            newBullet = Instantiate(_bullet3, transform.position, _bullet3.transform.rotation);
            _light3.SetActive(true);
            Invoke("resetLight3", 0.3f);
        }
        newBullet.transform.parent = objectManager.bullets.transform;
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.ban);
        if (move == 2)
        {
            newBullet.GetComponent<Bullet>().velocity = new Vector3(0, -speedBullet, 0);
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y - 0.7f, newBullet.transform.position.z);
            newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, -180f);
        }
        else if (move == 8)
        {
            newBullet.GetComponent<Bullet>().velocity = new Vector3(0, speedBullet, 0);
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, newBullet.transform.position.z);
            newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, 0f);
        }
        else if (move == 4)
        {
            newBullet.GetComponent<Bullet>().velocity = new Vector3(-speedBullet, 0, 0);
            newBullet.transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y, newBullet.transform.position.z);
            newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, 90f);
        }
        else
        {
            newBullet.GetComponent<Bullet>().velocity = new Vector3(speedBullet, 0, 0);
            newBullet.transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, newBullet.transform.position.z);
            newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, -90f);
        }
        newBullet.GetComponent<Bullet>().setVelocity();
    }
    void resetLight1()
    {
        _light1.SetActive(false);
    }
    void resetLight3()
    {
        _light3.SetActive(false);
    }
    public void getBullet()
    {
        blood -= objectManager.tanks[objectManager.idTank].Damage;
        bl.blood = blood;

    }
}
