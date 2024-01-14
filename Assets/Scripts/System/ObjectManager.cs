using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class Tank
{
    public int Damage { get; set; }
    public int Blood { get; set; }
    public int TypeGun { get; set; }
    public int Price { get; set; }
    
}
public class ObjectManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public DataLevelManager dataLevelManager;
    [SerializeField] public int idPlayer, idDataLevel;
    [SerializeField] public float speedTank, speedEnemy, speedBullet;
    [SerializeField] public int idTank, idLV, idItem, cost, tankOrItem, countEne, killE; // 0: tank      ||     1: item
    [SerializeField] public LoadingData loadingData;
    [SerializeField] public TMP_Text gold, textBuy, idLevel, textNotBuy, killEne, textWin, textLose, point, textName, textNote, textInfor;
    [SerializeField] public GameObject login, play, shop, equip, setting, loading, start, mainPlay, level, bag, uiBuy, uiNotBuy, uiWin, uiLose, rank, uiNote, help, uiSave, uiResumeOrNew, uiInfor, logOut;
    [SerializeField] public Player player;
    [SerializeField] public GameObject[] levels, tank;
    [SerializeField] public Image[] imgLV;
    [SerializeField] public Vector3[] _position;
    [SerializeField] public ItemButtonUse[] buttonsUseItem;
    [SerializeField] public GameObject _level, _player, bullets;
    [SerializeField] public GameObject soundOn, soundOff, musicOn, musicOff, useBullet, isRevival;
    [SerializeField] public GameObject[] item;
    [SerializeField] public string[] inforItems;
    [SerializeField] public GameObject PL_Bullet1, Ene_Bullet1;
    [SerializeField] public bool isTime, isMusic, isSound;
    public Tank[] tanks;
    [SerializeField] public AudioSource Aus;
    [SerializeField] public AudioClip no, cham, ban, no1, click, use, sWin, sLose;

    [SerializeField] public Vector3 pastTranformPlayer;
    private void Start()
    {
        tanks = new Tank[5];

        for (int i = 0; i < tanks.Length - 1; i++)
        {
            tanks[i] = new Tank
            {
                Damage = 40 + 10*i,
                Blood = 100,
                TypeGun = 1,
                Price = 5000 + 1000*i
            };
        }
        tanks[4] = new Tank
        {
            Damage = 102,
            Blood = 100,
            TypeGun = 2,
            Price = 10000
        };
    }
    public void getRevivalPlayer()
    {
        _player = Instantiate(tank[idTank], pastTranformPlayer, tank[idTank].transform.rotation);
        player = _player.GetComponentInChildren<Player>();
        virtualCamera.Follow = player.transform;
        isRevival.SetActive(false);
    }
}
