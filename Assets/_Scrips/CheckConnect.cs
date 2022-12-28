using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using TMPro;

public class CheckConnect : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;
    private void OnDisable() => YandexGame.GetDataEvent -= CheckSDK;

    private TextMeshProUGUI scoreBest;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(YandexGame.SDKEnabled);
        if (YandexGame.SDKEnabled) 
        {
            CheckSDK();
        }
    }

    // Update is called once per frame
    public void CheckSDK() 
    {
        if (YandexGame.auth)
        {
            Debug.Log("User authorization OK");
        }
        else
        {
            Debug.Log("User authorization FALSE");
            YandexGame.AuthDialog();
        }
        YandexGame.RewardVideoEvent(0);
        GameObject ScoreBO = GameObject.Find("BestScore");
        scoreBest = ScoreBO.GetComponent<TextMeshProUGUI>();
        scoreBest.text ="Best score: " + YandexGame.savesData.bestScore.ToString();
        if ((YandexGame.savesData.achivment)[0] == null)
        {

        }
        else 
        {
            foreach (string val in YandexGame.savesData.achivment) 
            {
                GameObject.Find("ListAchive").GetComponent<TextMeshProUGUI>().text = GameObject.Find("ListAchive").GetComponent<TextMeshProUGUI>().text + val + "\n";
            }
        }
    }
}
