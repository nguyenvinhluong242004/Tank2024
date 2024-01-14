using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevelManager : MonoBehaviour
{
    [SerializeField] private ObjectManager objectManager;
    public DataLevel dataLevel;


    public void settingLevelOnSave()
    {
        int idLV = objectManager.idLV;
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].isSave = true;
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].playerPosition = objectManager.player.transform.position;
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].movePL = objectManager.player.move;
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].blood = objectManager.player.blood;
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].countEnemy = objectManager.countEne;
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].killEnemy = objectManager.killE;

        Enemy[] enes = FindObjectsOfType<Enemy>();
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].enemyPositions = new Vector3[enes.Length];
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].moveEnes = new int[enes.Length];
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].bloodEne = new float[enes.Length];
        for (int i=0; i<enes.Length;i++)
        {
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].enemyPositions[i] = enes[i].transform.position;
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].moveEnes[i] = enes[i].move;
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].bloodEne[i] = enes[i].blood;
        }

        Box[] boxs = FindObjectsOfType<Box>();
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].boxPositions = new Vector3[boxs.Length];
        for (int i = 0; i < boxs.Length; i++)
        {
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].boxPositions[i] = boxs[i].transform.position;
        }

        BulletImpact[] bullets = FindObjectsOfType<BulletImpact>();
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].bulletPositions = new Vector3[bullets.Length];
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].bulletVelocitys = new Vector3[bullets.Length];
        dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].typeBullet = new int[bullets.Length];
        for (int i = 0; i < bullets.Length; i++)
        {
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].bulletPositions[i] = bullets[i].transform.position;
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].bulletVelocitys[i] = bullets[i].GetComponent<BulletImpact>().velocity;
            dataLevel.levelsData[objectManager.idDataLevel].dataLVs[idLV].typeBullet[i] = bullets[i].GetComponent<BulletImpact>().type;

        }
    }    
}
