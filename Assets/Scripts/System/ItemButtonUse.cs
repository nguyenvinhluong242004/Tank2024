using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButtonUse : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] public TMP_Text text;
    [SerializeField] private int id;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] public GameObject item;
    [SerializeField] public bool isUse;
    // Start is called before the first frame update
    void Start()
    {
        updateCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = FindObjectOfType<Player>();
    }
    public void updateCount()
    {
        text.text = objectManager.loadingData.players[objectManager.idPlayer].Items[id].ToString();
    }    
    public void getUseItem()
    {
        if (!Input.GetKeyDown(KeyCode.Space) && !isUse && objectManager.item[id] && objectManager.loadingData.players[objectManager.idPlayer].Items[id] >=1 && objectManager.player && !objectManager.player.item[id])
        {
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.use);

            bool check = false;
            if (id == 0)
            {
                check = true;
                objectManager.player.useHp();
                Invoke("_reset", 1.2f);
            }
            if (id == 1)
            {
                check = true;
                objectManager.player.useBuffSpeed();
                Invoke("_reset", 3f);
            }
            else if (id == 3)
            {
                check = true;
                objectManager.player.useTime();
                Invoke("_reset", 3f);
            }   
            else if (id == 4)
            {
                check = true;
                objectManager.player.useShield();
                Invoke("_reset", 2.4f);
            }
            else if (id == 6 && !objectManager.player.isRevival)
            {
                check = true;
                objectManager.isRevival.SetActive(true);
                objectManager.player.isRevival = true;
                Invoke("_reset", 2f);
            }
            if (check)
            {
                item = Instantiate(objectManager.item[id], objectManager.player.transform.position + objectManager.item[id].transform.position, objectManager.item[id].transform.rotation);
                item.transform.parent = objectManager.player.pa.transform;
                isUse = true;
                objectManager.loadingData.players[objectManager.idPlayer].Items[id] -= 1;
                objectManager.buttonsUseItem[id].text.text = objectManager.loadingData.players[objectManager.idPlayer].Items[id].ToString();
                objectManager.loadingData.SavePlayersToFile();
            }    
        }    
    }
    void _reset()
    {
        Destroy(item);
        isUse = false;
    }    
}
