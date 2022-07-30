using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] SpriteRenderer playerNameSubmittedCheckMark;
    [SerializeField] float playerNameSubmittedIndicationTime = 1f;

    Leaderboard leaderboard;

    void Awake()
    {
        leaderboard = FindObjectOfType<Leaderboard>();
    }

    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        { 
            if (response.success)
            {
                Debug.Log("Successfully set player name");
                StartCoroutine(PlayerNameSubmittedIndicator(playerNameSubmittedIndicationTime));
            }
            else
            {
                Debug.Log("Could not set player name " + response.Error);
            }
        });
    }

    IEnumerator PlayerNameSubmittedIndicator(float time)
    {
        playerNameSubmittedCheckMark.enabled = true;
        yield return new WaitForSeconds(time);
        playerNameSubmittedCheckMark.enabled = false;
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
