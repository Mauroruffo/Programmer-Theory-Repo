using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class LoadGameRank : MonoBehaviour
{
    public TextMeshProUGUI BestPlayerName;

    // Static variables to hold best score data
    private static int bestScore;
    private static string bestPlayer;

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    class SaveData
    {
        public int HighestScore;
        public string TheBestPlayer;
    }

    private void SetBestPlayer()
    {
        if (bestPlayer == null && bestScore == 0)
        {
            BestPlayerName.text = "";
        }
        else
        {
            BestPlayerName.text = $"Best Score - {bestPlayer}: {bestScore}";
        }
    }

    public void Init()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("Entered Init");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.TheBestPlayer;
            bestScore = data.HighestScore;
            SetBestPlayer();
        }
    }


}
