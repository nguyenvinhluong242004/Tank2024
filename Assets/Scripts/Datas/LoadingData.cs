using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using System;

public class DataPlayer
{
    public string Name { get; set; }
    public int Point { get; set; }
    public int Gold { get; set; }
    public List<int> Items { get; set; }
    public DateTime Day { get; set; }
    public List<int> Equipments { get; set; }
    public List<int> Levels { get; set; }
    public bool Music { get; set; }
    public bool Sound { get; set; }
}
public class LoadingData : MonoBehaviour
{
    public List<DataPlayer> players;
    string filePath;
    [SerializeField] private LoadingGame LoadingGame;
    [SerializeField] private Player playerMove;
    [SerializeField] private TMP_InputField namePlayer;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private GameObject _rankPlayer, contentRank;
    [SerializeField] private GameObject[] _rankPlayers;
    [SerializeField] private RectTransform rect;
    [SerializeField] private string _namePlayer;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "dataGame.json");
        players = ReadPlayersFromFile(filePath);
        //players.Clear();
        //SavePlayersToFile();
    }
    public void resetRank(bool k)
    {
        int n = players.Count;
        if (k)
            _rankPlayers = new GameObject[n];
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (players[j].Point < players[j + 1].Point)
                {
                    DataPlayer temp = players[j];
                    players[j] = players[j + 1];
                    players[j + 1] = temp;
                }
            }
        }
        for (int i = 0; i < players.Count; i++)
        {
            if (_namePlayer == players[i].Name)
            {
                objectManager.idPlayer = i;
                break;
            }
        }
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log($"Name: {players[i].Name}, Point: {players[i].Point}, Gold: {players[i].Gold}");
            if (k)
            {
                _rankPlayers[i] = Instantiate(_rankPlayer, contentRank.transform.position, _rankPlayer.transform.rotation);
                _rankPlayers[i].transform.SetParent(contentRank.transform, false);
                rect.sizeDelta += new Vector2(0f, 110f);
                rect.anchoredPosition += new Vector2(0, -55f);
            }
            _rankPlayers[i].GetComponent<Rank>().idx.text = (i + 1).ToString();
            _rankPlayers[i].GetComponent<Rank>()._name.text = players[i].Name;
            _rankPlayers[i].GetComponent<Rank>().point.text = players[i].Point.ToString();
            if (objectManager.dataLevelManager.dataLevel.levelsData[i]._name == _namePlayer)
            {
                objectManager.idDataLevel = i;
            }    
        }
    }
        
    public void Login()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        _namePlayer = namePlayer.text;
        if (_namePlayer.Length > 0)
        {
            int id = -1;
            int i = 0;
            foreach (var player in players)
            {
                if (_namePlayer == player.Name)
                {

                    objectManager.textName.text = _namePlayer;
                    id = i;
                    break;
                }
                i++;
            }
            if (id != -1)
            {
                objectManager.idPlayer = id;
                objectManager.gold.text = players[id].Gold.ToString();
                objectManager.point.text = players[id].Point.ToString();
                objectManager.isMusic = players[id].Music;
                objectManager.isSound = players[id].Sound;
                if (players[id].Music)
                    objectManager.musicOff.SetActive(false);
                else
                    objectManager.musicOn.SetActive(false);
                if (players[id].Sound)
                    objectManager.soundOff.SetActive(false);
                else
                    objectManager.soundOn.SetActive(false);
                for (int ii=0; ii<players[id].Equipments.Count; ii++)
                    if (players[id].Equipments[ii] == 2)
                    {
                        objectManager.idTank = ii;
                        break;
                    }
            }
            else
            {
                objectManager.idPlayer = players.Count;
                objectManager.idTank = 0;
                objectManager.gold.text = "10000";
                objectManager.point.text = "1";
                objectManager.isMusic = true;
                // Thêm một tài khoản mới
                DataPlayer newPlayer = new DataPlayer
                {
                    Name = _namePlayer,
                    Point = 1,
                    Gold = 10000,
                    Items = new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                    Day = DateTime.Now,
                    Equipments = new List<int> { 2, 0, 0, 0, 0 },
                    Levels = new List<int> { 1, 0, 0, 0 },
                    Music = true,
                    Sound = true
                };
                players.Add(newPlayer);
                // Lưu lại thông tin vào file JSON
                objectManager.textName.text = _namePlayer;
                SavePlayersToFile();
                objectManager.isMusic = true;
                objectManager.isSound = true;
                objectManager.musicOff.SetActive(false);
                objectManager.soundOff.SetActive(false);
                objectManager.dataLevelManager.dataLevel.add(_namePlayer);
            }
            objectManager.login.SetActive(false);
            objectManager.play.SetActive(true);
            objectManager.play.SetActive(true);

            resetRank(true);
            if (!objectManager.isMusic)
                objectManager.Aus.Stop();
        }    
        
    }    
    static List<DataPlayer> ReadPlayersFromFile(string filePath)
    {
        // Đọc dữ liệu từ file JSON và chuyển đổi thành List<Player>
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<DataPlayer>>(jsonContent);
        }

        // Nếu file không tồn tại, trả về một List<Player> mới
        return new List<DataPlayer>();
    }
    public void SavePlayersToFile()
    {
        // Chuyển đối List<Player> thành chuỗi JSON và lưu vào file
        string jsonContent = JsonConvert.SerializeObject(players, Formatting.Indented);
        File.WriteAllText(filePath, jsonContent);
    }
}
