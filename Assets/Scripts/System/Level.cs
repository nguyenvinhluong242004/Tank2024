using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] private int idLV;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image img;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private LoadingData loadingData;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        loadingData = FindObjectOfType<LoadingData>();
        img = GetComponent<Image>();
        text.text = "LV "+ (idLV + 1).ToString();
        if (idLV < 4)
        {
            if (loadingData.players[objectManager.idPlayer].Levels[idLV] == 0)
                img.color = new Color(152f / 255f, 152f / 255f, 152f / 255f, 1f);
        }
        else
        {
            img.color = new Color(152f / 255f, 152f / 255f, 152f / 255f, 1f);
        }

    }
    public void getLevel()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        if (idLV < 4 && loadingData.players[objectManager.idPlayer].Levels[idLV] == 1 && objectManager.levels[idLV])
        {
            objectManager.idLV = idLV;
            if (!objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].isSave)
            {
                objectManager.killE = 0;
                objectManager.idLevel.text = (idLV + 1).ToString();
                objectManager.level.SetActive(false);
                objectManager.mainPlay.SetActive(true);
                objectManager._level = Instantiate(objectManager.levels[idLV], objectManager.levels[idLV].transform.position, objectManager.levels[idLV].transform.rotation);
                objectManager._player = Instantiate(objectManager.tank[objectManager.idTank], objectManager._position[objectManager.idLV], objectManager.tank[objectManager.idTank].transform.rotation);
                objectManager.player = objectManager._player.GetComponentInChildren<Player>();
                objectManager.virtualCamera.Follow = objectManager.player.transform;
                objectManager.countEne = GameObject.FindGameObjectsWithTag("Enemy").Length;
                objectManager.killEne.text = $"{objectManager.killE}/{objectManager.countEne}";
                objectManager.bag.SetActive(true);
            }
            else
            {
                objectManager.uiResumeOrNew.SetActive(true);
            }
        }
        else if(idLV < 4 && loadingData.players[objectManager.idPlayer].Levels[idLV] == 0)
        {
            objectManager.uiNote.SetActive(true);
            objectManager.textNote.text = "The level is locked, please pass the previous level to unlock it!";
        }
        else
        {
            objectManager.uiNote.SetActive(true);
            objectManager.textNote.text = "The level has not been released yet.We will update as soon as possible!";
        }
    }    
}
