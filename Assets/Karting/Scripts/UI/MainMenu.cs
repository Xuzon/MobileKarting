using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The scene to load on Play button")]
    protected int gameplaySceneIndex = -1;

    [SerializeField]
    [Tooltip("The text where I will write the best historical lap time")]
    protected TMPro.TextMeshProUGUI bestLapText;    
    
    [SerializeField]
    [Tooltip("The text where I will write the total coins")]
    protected TMPro.TextMeshProUGUI totalCoinsText;

    [SerializeField]
    [Tooltip("Text that will be displayed on the best lap text if don't have a record yet")]
    protected string notRecordLapString = "Go and race!!";

    private void Start()
    {
        if(PlayerPrefs.HasKey(KartGame.Track.TrackManager.BEST_TIME_KEY))
        {
            bestLapText.text = PlayerPrefs.GetFloat(KartGame.Track.TrackManager.BEST_TIME_KEY) + "s";
        }
        else
        {
            bestLapText.text = notRecordLapString;
        }

        if(PlayerPrefs.HasKey(CoinManager.COINS_KEY))
        {
            totalCoinsText.text = PlayerPrefs.GetInt(CoinManager.COINS_KEY).ToString();
        }
    }

    public void LoadGame()
    {
        if(gameplaySceneIndex == -1)
        {
            Debug.LogError("You didn't assigned a game scene");
            return;
        }

        SceneManager.LoadScene(gameplaySceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
