using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int idPlayer;
    [SerializeField] private int _typePlayer;
    [SerializeField] public float blood;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator anm;
    [SerializeField] private Blood bl;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private bool isDie, isBullet;
    [SerializeField] public GameObject _bullet1, _bullet3, _light1, _light3, headGun, player, _bl, pa;
    [SerializeField] public bool isShield, isRevival;
    [SerializeField] public GameObject[] item;
    [SerializeField] public int move;
    float horizontalInput;
    float verticalInput;
    
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float speed, speedBullet;

    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        _bl.SetActive(false);
        move = 8;
        speed = objectManager.speedTank;
        item = new GameObject[7];
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 velocity = new Vector2(horizontalInput, verticalInput);
        if (velocity.x == 0f || velocity.y == 0f)
            _rb.velocity = velocity * speed;
        if (verticalInput == 0f)
        {
            if (horizontalInput == -1f)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
                move = 4;
            }   
            else if (horizontalInput == 1f)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
                move = 6;
            }    
        }
        if (horizontalInput == 0f)
        {
            if (verticalInput == 1f)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                move = 8;
            }
            else if (verticalInput == -1f)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -180f);
                move = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isBullet)
        {
            GameObject newBullet;
            isBullet = true;
            objectManager.useBullet.SetActive(false);
            Invoke("resetBullet", 1.2f);
            if (_typePlayer == 1)
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
        if (Input.GetKeyDown(KeyCode.R) && !objectManager.buttonsUseItem[0].isUse)
                getSkill(0);
        else if (Input.GetKeyDown(KeyCode.T) && !objectManager.buttonsUseItem[1].isUse)
            getSkill(1);
        else if (Input.GetKeyDown(KeyCode.Y) && !objectManager.buttonsUseItem[2].isUse)
            getSkill(2);
        else if (Input.GetKeyDown(KeyCode.U) && !objectManager.buttonsUseItem[3].isUse)
            getSkill(3);
        else if (Input.GetKeyDown(KeyCode.I) && !objectManager.buttonsUseItem[4].isUse)
            getSkill(4);
        else if (Input.GetKeyDown(KeyCode.O) && !objectManager.buttonsUseItem[5].isUse)
            getSkill(5);
        else if (Input.GetKeyDown(KeyCode.P) && !objectManager.buttonsUseItem[6].isUse)
            getSkill(6);
        
        if (!isDie)
            pa.transform.position = transform.position + new Vector3(0, 3.48f, 0);
        if (!isDie && blood < 0)
        {
            anm.Play("die");
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.no);
            headGun.SetActive(false);
            Invoke("_des", 1f);
            isDie = true;
            if (!isRevival)
            {
                Invoke("setLose", 0.3f);
            }
        }
    }
    void setLose()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.sLose);
        objectManager.uiLose.SetActive(true);
        objectManager.loadingData.players[objectManager.idPlayer].Point += objectManager.killE;
        objectManager.loadingData.players[objectManager.idPlayer].Gold += objectManager.killE * 10;
        objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
        objectManager.point.text = objectManager.loadingData.players[objectManager.idPlayer].Point.ToString(); 

        objectManager.textLose.text = $"- Kill: {objectManager.killE}/{objectManager.countEne}\n- Gold: +{objectManager.killE * 10}\n- Point: +{objectManager.killE}";
        objectManager.loadingData.SavePlayersToFile();
        objectManager.loadingData.resetRank(false);
        objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].isSave = false;


    }
    void resetBullet()
    {
        isBullet = false;
        objectManager.useBullet.SetActive(true);
    }
    void resetLight1()
    {
        _light1.SetActive(false);
    }
    void resetLight3()
    {
        _light3.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_bl.activeSelf && collision.CompareTag("Ene"))
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("cham");
        velocity = new Vector2(0, 0);
    }
    void getSkill(int id)
    {
        if (!objectManager.buttonsUseItem[id].isUse && objectManager.item[id] && objectManager.loadingData.players[objectManager.idPlayer].Items[id] >= 1 && !objectManager.buttonsUseItem[id].item)
        {
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.use);
            bool check = false;

            if (id == 0)
            {
                check = true;
                objectManager.player.useHp();
                //Invoke("_reset", 1.2f);
                StartCoroutine(ResetWithKey(id, 1.2f));
            }
            if (id == 1)
            {
                check = true;
                objectManager.player.useBuffSpeed();
                //Invoke("_reset", 3f);
                StartCoroutine(ResetWithKey(id, 3f));
            }
            else if (id == 3)
            {
                check = true;
                objectManager.player.useTime();
                //Invoke("_reset", 3f);
                StartCoroutine(ResetWithKey(id, 3f));
            }
            else if (id == 4)
            {
                check = true;
                objectManager.player.useShield();
                //Invoke("_reset", 2.4f);
                StartCoroutine(ResetWithKey(id, 2.4f));
            }
            else if (id == 6 && !isRevival)
            {
                check = true;
                //Invoke("_reset", 2f);
                isRevival = true;
                objectManager.isRevival.SetActive(true);
                StartCoroutine(ResetWithKey(id, 2f));
            }
            if (check)
            {
                item[id] = Instantiate(objectManager.item[id], objectManager.player.transform.position + objectManager.item[id].transform.position, objectManager.item[id].transform.rotation);
                item[id].transform.parent = objectManager.player.pa.transform;
                objectManager.buttonsUseItem[id].isUse = true;

                objectManager.loadingData.players[objectManager.idPlayer].Items[id] -= 1;
                objectManager.buttonsUseItem[id].text.text = objectManager.loadingData.players[objectManager.idPlayer].Items[id].ToString();
                objectManager.loadingData.SavePlayersToFile();
            }    
        }
    }
    IEnumerator ResetWithKey(int key, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Call the _reset method with the provided key
        _reset(key);
    }
    void _reset(int id)
    {
        Destroy(item[id]);
        objectManager.buttonsUseItem[id].isUse = false;
    }
    void _des()
    {
        player.SetActive(false);
        Destroy(player);
        if (isRevival)
        {
            objectManager.pastTranformPlayer = transform.position;
            objectManager.getRevivalPlayer();
        }

    }
    void closeBlood()
    {
        _bl.SetActive(false);
    }
    public void getBullet()
    {
        blood -= 40f;
        bl.blood = blood;

    }
    void getUseHp()
    {
        blood += 40f;
        if (blood > 100f)
            blood = 100f;
        bl.blood = blood;
    }
    public void useHp()
    {
        _bl.SetActive(true);
        Invoke("getUseHp", 0.3f);
        Invoke("closeBlood", 1.2f);
    }    
    void _useShield()
    {
        isShield = false;
    }    
    public void useShield()
    {
        isShield = true;
        Invoke("_useShield", 2.4f);
    }
    public void _useBuffSpeed()
    {
        speed = objectManager.speedTank;
    }
    public void useBuffSpeed()
    {
        speed = 12f;
        Invoke("_useBuffSpeed", 3f);
    }    
    public void _useTime()
    {
        objectManager.isTime = false;
    }
    public void useTime()
    {
        objectManager.isTime = true;
        Invoke("_useTime", 3f);
    }    
}
