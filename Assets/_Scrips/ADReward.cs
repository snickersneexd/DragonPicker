using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ADReward : MonoBehaviour
{
    private void OnEnable() => YandexGame.CloseVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.CloseVideoEvent -= Rewarded;

    void Rewarded() 
    {
        Debug.Log("Rewarded");
    }

    public void OpenAD() 
    {
        YandexGame.RewardVideoEvent(Random.Range(0, 2));
    }
}
