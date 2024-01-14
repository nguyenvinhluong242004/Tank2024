using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class dataLV
{
    public Vector3 playerPosition;
    public int movePL;
    public float blood;
    public Vector3[] enemyPositions;
    public int[] moveEnes;
    public float[] bloodEne;
    public Vector3[] boxPositions;
    public Vector3[] bulletPositions;
    public int[] typeBullet;
    public Vector3[] bulletVelocitys;
    public int countEnemy, killEnemy;
    public bool isSave;
}
[System.Serializable]
public class LevelData
{
    public string _name;
    public List<dataLV> dataLVs = new List<dataLV>();
}
[CreateAssetMenu(fileName = "GameProgressData", menuName = "Custom/GameProgressData", order = 1)]
public class DataLevel : ScriptableObject
{
    public List<LevelData> levelsData = new List<LevelData>();
    public string _name;

    public void add(string _name)
    {

        LevelData level = new LevelData();
        level._name = _name;
        for (int i = 0; i < 4; i++)
        {
            dataLV data = new dataLV();
            level.dataLVs.Add(data);
        }
        levelsData.Add(level);
    }    
}
