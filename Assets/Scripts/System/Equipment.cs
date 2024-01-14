using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Equipment : MonoBehaviour
{
    [SerializeField] private int idx;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private LoadingData loadingData;
    [SerializeField] public TMP_Text text, infor, cost;
    [SerializeField] public Image bt;
    [SerializeField] private Player player;
    [SerializeField] public int mode;
    void Start()
    {
        characters[objectManager.idTank].SetActive(true);
        if (loadingData.players[objectManager.idPlayer].Equipments[objectManager.idTank] == 1)
        {
            mode = 1;
            text.text = "Select";
            bt.color = new Color(5f / 255f, 1f, 0f, 1f);
        }
        else if (loadingData.players[objectManager.idPlayer].Equipments[objectManager.idTank] == 0)
        {
            mode = 0;
            text.text = "Buy";
            bt.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            mode = 2;
            text.text = "Selected";
            bt.color = new Color(0f, 174f / 255f, 1f, 1f);
        }
        cost.text = objectManager.tanks[idx].Price.ToString();
        infor.text = $"- Name: Advanced\n- Gun barrel: {objectManager.tanks[objectManager.idTank].TypeGun}\n- Reliability: {objectManager.tanks[objectManager.idTank].Blood}\n- Damage: {objectManager.tanks[objectManager.idTank].Damage}\n- Range: 10m";
    }
    public void next()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        characters[idx].SetActive(false);
        idx++;
        if (idx > 4)
            idx = 0;
        characters[idx].SetActive(true);
        if (loadingData.players[objectManager.idPlayer].Equipments[idx] == 1)
        {
            mode = 1;
            text.text = "Select";
            bt.color = new Color(5f / 255f, 1f, 0f, 1f);
        }
        else if (loadingData.players[objectManager.idPlayer].Equipments[idx] == 0)
        {
            mode = 0;
            text.text = "Buy";
            bt.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            mode = 2;
            text.text = "Selected";
            bt.color = new Color(0f, 174f / 255f, 1f, 1f);
        }
        cost.text = objectManager.tanks[idx].Price.ToString();
        infor.text = $"- Name: Advanced\n- Gun barrel: {objectManager.tanks[idx].TypeGun}\n- Reliability: {objectManager.tanks[idx].Blood}\n- Damage: {objectManager.tanks[idx].Damage}\n- Range: 10m";

    }
    public void back()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        characters[idx].SetActive(false);
        idx--;
        if (idx < 0)
            idx = 4;
        characters[idx].SetActive(true);
        if (loadingData.players[objectManager.idPlayer].Equipments[idx] == 1)
        {
            mode = 1;
            text.text = "Select";
            bt.color = new Color(5f / 255f, 1f, 0f, 1f);
        }
        else if (loadingData.players[objectManager.idPlayer].Equipments[idx] == 0)
        {
            mode = 0;
            text.text = "Buy";
            bt.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            mode = 2;
            text.text = "Selected";
            bt.color = new Color(0f, 174f / 255f, 1f, 1f);
        }
        cost.text = objectManager.tanks[idx].Price.ToString();
        infor.text = $"- Name: Advanced\n- Gun barrel: {objectManager.tanks[idx].TypeGun}\n- Reliability: {objectManager.tanks[idx].Blood}\n- Damage: {objectManager.tanks[idx].Damage}\n- Range: 10m";
    }
    public void setTank()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        if (mode == 1)
        {
            loadingData.players[objectManager.idPlayer].Equipments[objectManager.idTank] = 1;
            objectManager.idTank = idx;
            loadingData.players[objectManager.idPlayer].Equipments[objectManager.idTank] = 2;
            text.text = "Selected";
            bt.color = new Color(0f, 174f / 255f, 1f, 1f);
            loadingData.SavePlayersToFile();

        }    
        else if (mode == 0)
        {
            if (objectManager.tanks[objectManager.idTank].Price > objectManager.loadingData.players[objectManager.idPlayer].Gold)
            {
                objectManager.uiNotBuy.SetActive(true);
            }
            else
            {
                objectManager.uiBuy.SetActive(true);
                objectManager.textBuy.text = $"Are you sure you want to buy this Tank for {int.Parse(cost.text)} gold?";
                objectManager.tankOrItem = 0;
                objectManager.idItem = idx;
                objectManager.cost = objectManager.tanks[objectManager.idTank].Price;
            }
        }    
    }  
}
