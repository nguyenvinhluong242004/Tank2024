using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int cost;
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text text;
    [SerializeField] private ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        text.text = cost.ToString();   
    }

    public void getBuyItem()
    {
        if (!objectManager.item[id])
        {
            objectManager.textNotBuy.text = "Items will be released as soon as possible!";
            objectManager.uiNotBuy.SetActive(true);
        }
        else if (cost > objectManager.loadingData.players[objectManager.idPlayer].Gold)
        {
            objectManager.textNotBuy.text = "You don't have enough money!!!";
            objectManager.uiNotBuy.SetActive(true);
        }
        else
        {
            objectManager.uiBuy.SetActive(true);
            objectManager.textBuy.text = $"Are you sure you want to buy this item for {cost} gold?";
            objectManager.idItem = id;
            objectManager.tankOrItem = 1;
            objectManager.cost = cost;
        }

    }
    public void getInforItem()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.textInfor.text = objectManager.inforItems[id];
        objectManager.uiInfor.SetActive(!objectManager.uiInfor.activeSelf);
    }
}
