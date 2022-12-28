using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using TMPro;
public class DragonPicker : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoadSave;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoadSave;


    // Start is called before the first frame update
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6f;
    public float energyShieldRadius = 1.5f;

    public TextMeshProUGUI scoreGT;
    private TextMeshProUGUI playerName;

    public List<GameObject> shieldList;

    void Start()
    {
        if (YandexGame.SDKEnabled) 
        {
            GetLoadSave();
        }
        shieldList = new List<GameObject>();
        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate<GameObject>(energyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(1*i, 1*i, 1*i);
            shieldList.Add(tShieldGo);
        }
        GameObject playerNamePrefabGUI = GameObject.Find("PlayerName");
        playerName = playerNamePrefabGUI.GetComponent<TextMeshProUGUI>();
        playerName.text = YandexGame.playerName;
        Debug.Log(YandexGame.playerName);
    }

    public void GetLoadSave() 
    {
        Debug.Log(YandexGame.savesData.score);
    }

    public void DragonEggDestroy() 
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray) 
        {
            Destroy(tGO);
        }
        int shieldIndex = shieldList.Count - 1;
        GameObject tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        if (shieldList.Count == 0) 
        {
            GameObject ScoreGO = GameObject.Find("Score");
            scoreGT = ScoreGO.GetComponent<TextMeshProUGUI>();
            string[] achiveList = YandexGame.savesData.achivment;
            achiveList[0] = "12312";
            UserSave(int.Parse(scoreGT.text), YandexGame.savesData.bestScore, achiveList);
            YandexGame.NewLeaderboardScores("TOPPlayerScore", int.Parse(scoreGT.text));
            SceneManager.LoadScene("_0Scene");
            GetLoadSave();
        }
    }

    public void UserSave(int currentScore, int currentBestScore, string[] curentAchive)
    {
        YandexGame.savesData.score = currentScore;
        if (currentScore > currentBestScore)
        {
            YandexGame.savesData.bestScore = currentScore;
        }
        YandexGame.savesData.achivment = curentAchive;
        YandexGame.SaveProgress();
    }
}
