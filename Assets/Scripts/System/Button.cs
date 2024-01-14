using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private ObjectManager objectManager;
    public void getShop()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.shop.SetActive(!objectManager.shop.activeSelf);
    }
    public void getCloseInforItem()
    {
        objectManager.uiInfor.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
        
    public void LogOut()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
        
        
    public void getEquip()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.equip.SetActive(!objectManager.equip.activeSelf);
    }
    public void getSetting()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.setting.SetActive(!objectManager.setting.activeSelf);
    }
    public void getBag()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.bag.SetActive(!objectManager.bag.activeSelf);
    }
    public void backStart()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.level.SetActive(false);
        objectManager.rank.SetActive(false);
        objectManager.help.SetActive(false);
        objectManager.start.SetActive(true);
    }
    public void play()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.level.SetActive(true);
        objectManager.start.SetActive(false);
    }
    public void rank()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.rank.SetActive(true);
        objectManager.start.SetActive(false);
    }
    public void help()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.help.SetActive(true);
        objectManager.start.SetActive(false);
    }
    public void getCloseUiNote()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.uiNote.SetActive(false);
    }
    public void getSound()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.loadingData.players[objectManager.idPlayer].Sound = !objectManager.loadingData.players[objectManager.idPlayer].Sound;
        if (objectManager.loadingData.players[objectManager.idPlayer].Sound)
        {
            objectManager.isSound = true;
            objectManager.soundOff.SetActive(false);
            objectManager.soundOn.SetActive(true);
        }
        else
        {
            objectManager.isSound = false;
            objectManager.soundOn.SetActive(false);
            objectManager.soundOff.SetActive(true);
        }
        objectManager.loadingData.SavePlayersToFile();
    }
    public void getMusic()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.loadingData.players[objectManager.idPlayer].Music = !objectManager.loadingData.players[objectManager.idPlayer].Music;
        if (objectManager.loadingData.players[objectManager.idPlayer].Music)
        {
            objectManager.isMusic = true;
            objectManager.Aus.Play();
            objectManager.musicOff.SetActive(false);
            objectManager.musicOn.SetActive(true);
        }
        else
        {
            objectManager.isMusic = false;
            objectManager.Aus.Stop();
            objectManager.musicOn.SetActive(false);
            objectManager.musicOff.SetActive(true);
        }
        objectManager.loadingData.SavePlayersToFile();
    }
    public void getCloseUiBuy()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.uiBuy.SetActive(false);
    }
    public void getCloseUiNotBuy()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.uiNotBuy.SetActive(false);
    }
    public void getBuy()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        if (objectManager.tankOrItem == 1)
        {
            objectManager.loadingData.players[objectManager.idPlayer].Gold -= objectManager.cost;
            objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
            objectManager.loadingData.players[objectManager.idPlayer].Items[objectManager.idItem] += 1;
            objectManager.buttonsUseItem[objectManager.idItem].updateCount();
            objectManager.loadingData.SavePlayersToFile();
            objectManager.uiBuy.SetActive(false);
        }
        else
        {
            objectManager.loadingData.players[objectManager.idPlayer].Gold -= objectManager.cost;
            objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();

            objectManager.loadingData.players[objectManager.idPlayer].Equipments[objectManager.idItem] = 1;

            objectManager.equip.GetComponent<Equipment>().mode = 1;
            objectManager.equip.GetComponent<Equipment>().text.text = "Select";
            objectManager.equip.GetComponent<Equipment>().bt.color = new Color(5f / 255f, 1f, 0f, 1f);


            objectManager.loadingData.SavePlayersToFile();
            objectManager.uiBuy.SetActive(false);
        }

    }
    public void backMenuLevel()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        Destroy(objectManager._level);
        if (objectManager._player)
            Destroy(objectManager._player);
        foreach (Transform child in objectManager.bullets.transform)
        {
            Destroy(child.gameObject);
        }
        objectManager.uiLose.SetActive(false);
        objectManager.uiWin.SetActive(false);
        objectManager.mainPlay.SetActive(false);
        objectManager.level.SetActive(true);
        objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
    }
    public void backWhilePlay()
    {
        objectManager.isTime = true;
        objectManager.uiSave.SetActive(true);
    }
    public void saveYes()
    {
        objectManager.dataLevelManager.settingLevelOnSave();
        objectManager.isTime = true;
        objectManager.uiSave.SetActive(false);
        backMenuLevel();
    }   
    public void saveNo()
    {
        objectManager.isTime = false;
        objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].isSave = false;
        objectManager.uiSave.SetActive(false);
        backMenuLevel();
    } 
    public void resumeLevel()
    {
        objectManager.isTime = true;

        objectManager.countEne = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].countEnemy;
        objectManager.killE = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].killEnemy;
        objectManager.idLevel.text = objectManager.idLV.ToString();
        objectManager.level.SetActive(false);
        objectManager.mainPlay.SetActive(true);
        objectManager._level = Instantiate(objectManager.levels[objectManager.idLV], objectManager.levels[objectManager.idLV].transform.position, objectManager.levels[objectManager.idLV].transform.rotation);
        objectManager._player = Instantiate(objectManager.tank[objectManager.idTank], objectManager._position[objectManager.idLV], objectManager.tank[objectManager.idTank].transform.rotation);
        objectManager.player = objectManager._player.GetComponentInChildren<Player>();
        objectManager.virtualCamera.Follow = objectManager.player.transform;
        objectManager.uiWin.SetActive(false);
        objectManager.uiLose.SetActive(false);
        Enemy[] enes = FindObjectsOfType<Enemy>();
        Box[] boxs = FindObjectsOfType<Box>();
        objectManager.killEne.text = $"{objectManager.killE}/{objectManager.countEne}";
        objectManager.idLevel.text = (objectManager.idLV + 1).ToString();
        objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();

        objectManager.player.transform.position = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].playerPosition;
        if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].movePL == 2)
        {
            objectManager.player.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -180f);
            objectManager.player.move = 2;
        }
        else if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].movePL == 8)
        {
            objectManager.player.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
            objectManager.player.move = 8;
        }
        else if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].movePL == 4)
        {
            objectManager.player.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
            objectManager.player.move = 4;
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
            objectManager.player.move = 6;
        }
        objectManager.player.blood = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].blood;
        int countE = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].enemyPositions.Length;
        int countB = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].boxPositions.Length;
        int countBullet = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletPositions.Length;
        for (int i=countE; i<enes.Length; i++)
        {
            enes[i].enemy.SetActive(false);
            Destroy(enes[i].enemy);
        }    
        for (int i=countB; i<boxs.Length; i++)
        {
            boxs[i].gameObject.SetActive(false);
            Destroy(boxs[i].gameObject);
        }    
        for (int i=0; i<countB; i++)
        {
            boxs[i].transform.position = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].boxPositions[i];
        }
        for (int i = 0; i < countE; i++)
        {
            enes[i].transform.position = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].enemyPositions[i]; 
            if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].moveEnes[i] == 8)
            {
                enes[i].velocity = enes[i].up;
                enes[i].transform.eulerAngles = new Vector3(enes[i].transform.eulerAngles.x, enes[i].transform.eulerAngles.y, 0f);
                enes[i].move = 8;
            }
            else if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].moveEnes[i] == 2)
            {
                enes[i].velocity = enes[i].up;
                enes[i].transform.eulerAngles = new Vector3(enes[i].transform.eulerAngles.x, enes[i].transform.eulerAngles.y, 180f);
                enes[i].move = 2;
            }
            else if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].moveEnes[i] == 6)
            {
                enes[i].velocity = enes[i].up;
                enes[i].transform.eulerAngles = new Vector3(enes[i].transform.eulerAngles.x, enes[i].transform.eulerAngles.y, -90f);
                enes[i].move = 6;
            }
            else
            {
                enes[i].velocity = enes[i].up;
                enes[i].transform.eulerAngles = new Vector3(enes[i].transform.eulerAngles.x, enes[i].transform.eulerAngles.y, 90f);
                enes[i].move = 4;
            }
            enes[i].blood = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bloodEne[i];
        }
        for (int i=0; i<countBullet; i++)
        {
            GameObject newBullet;
            if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].typeBullet[i] == 0)
            {
                newBullet = Instantiate(objectManager.PL_Bullet1, objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletPositions[i], objectManager.PL_Bullet1.transform.rotation);
            }
            else
            {
                newBullet = Instantiate(objectManager.Ene_Bullet1, objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletPositions[i], objectManager.Ene_Bullet1.transform.rotation);
            }
            newBullet.transform.parent = objectManager.bullets.transform;
            newBullet.GetComponent<BulletImpact>().velocity = objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletVelocitys[i];
            if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletVelocitys[i].y < 0)
            {
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, -180f);
            }
            else if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletVelocitys[i].y > 0)
            {
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, 0f);
            }
            else if (objectManager.dataLevelManager.dataLevel.levelsData[objectManager.idDataLevel].dataLVs[objectManager.idLV].bulletVelocitys[i].x < 0)
            {
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, 90f);
            }
            else
            {
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, -90f);
            }
        }    

        objectManager.uiResumeOrNew.SetActive(false);
        objectManager.isTime = false;
    }   

    public void openLevel()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
            Destroy(objectManager._level);
        if (objectManager._player)
            Destroy(objectManager._player);
        foreach (Transform child in objectManager.bullets.transform)
        {
            Destroy(child.gameObject);
        }
        if (objectManager.idLV < 4 && objectManager.loadingData.players[objectManager.idPlayer].Levels[objectManager.idLV] == 1)
        {
            objectManager.countEne = 0;
            objectManager.killE = 0;
            objectManager.idLevel.text = objectManager.idLV.ToString();
            objectManager.level.SetActive(false);
            objectManager.mainPlay.SetActive(true);
            objectManager._level = Instantiate(objectManager.levels[objectManager.idLV], objectManager.levels[objectManager.idLV].transform.position, objectManager.levels[objectManager.idLV].transform.rotation);
            objectManager._player = Instantiate(objectManager.tank[objectManager.idTank], objectManager._position[objectManager.idLV], objectManager.tank[objectManager.idTank].transform.rotation);
            objectManager.player = objectManager._player.GetComponentInChildren<Player>();
            objectManager.virtualCamera.Follow = objectManager.player.transform;
            objectManager.uiWin.SetActive(false);
            objectManager.uiLose.SetActive(false);
            objectManager.countEne = objectManager.levels[objectManager.idLV].GetComponentsInChildren<Enemy>().Length;
            objectManager.killEne.text = $"{objectManager.killE}/{objectManager.countEne}";
            objectManager.idLevel.text = (objectManager.idLV + 1).ToString();
            objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();

            objectManager.uiResumeOrNew.SetActive(false);
        }
        else
        {
            objectManager.level.SetActive(true);
            objectManager.mainPlay.SetActive(false);
            objectManager.uiWin.SetActive(false);
            objectManager.uiLose.SetActive(false);
            objectManager.uiNote.SetActive(true);
            objectManager.textNote.text = "The level has not been released yet.We will update as soon as possible!";
        }
    }

}
