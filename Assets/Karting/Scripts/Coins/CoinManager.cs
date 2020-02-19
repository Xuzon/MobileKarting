using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public const string COINS_KEY = "TotalCoins";
    private int pickedCoins = 0;
    [SerializeField]
    [Tooltip("Text where I will be displaying the picked coins of this race")]
    protected TMPro.TextMeshProUGUI text;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        if(go != null)
        {
            KartGame.Track.TrackManager track = go.GetComponent<KartGame.Track.TrackManager>();
            if (track != null)
            {
                track.OnEndRace += OnEndRace;
            }
        }
    }

    private void OnEndRace()
    {
        if(PlayerPrefs.HasKey(COINS_KEY))
        {
            pickedCoins += PlayerPrefs.GetInt(COINS_KEY);
        }
        PlayerPrefs.SetInt(COINS_KEY, pickedCoins);
    }

    internal void AddCoin()
    {
        pickedCoins++;
        if(text != null)
        {
            text.text = pickedCoins.ToString();
        }
    }
}
