using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Animator anm;
    [SerializeField] private ObjectManager objectManager;
    bool isDie;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cham");
        if (collision.CompareTag("Ene"))
        {
            if (!isDie)
            {
                isDie = true;
                anm.Play("die");
                if (objectManager.isSound)
                    objectManager.Aus.PlayOneShot(objectManager.no1);
                Invoke("_des", 0.2f);
                setLose();
            }
        }
    }
    void setLose()
    {
        objectManager.uiLose.SetActive(true);
        objectManager.loadingData.players[objectManager.idPlayer].Point += objectManager.killE;
        objectManager.loadingData.players[objectManager.idPlayer].Gold += objectManager.killE * 10;
        objectManager.loadingData.SavePlayersToFile();
        objectManager.loadingData.resetRank(false);
        objectManager.textLose.text = $"- Kill: {objectManager.killE}/{objectManager.countEne}\n- Gold: +{objectManager.killE * 10}\n- Point: +{objectManager.killE}";
        objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
        objectManager.point.text = objectManager.loadingData.players[objectManager.idPlayer].Point.ToString();
    }
    void _des()
    {

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
