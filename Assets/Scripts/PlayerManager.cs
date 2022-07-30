using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Image playerNameSubmittedCheckmark;
    [SerializeField] float playerNameSubmittedIndicationTime = 1f;

    Leaderboard leaderboard;

    void Awake()
    {
        leaderboard = FindObjectOfType<Leaderboard>();
        playerNameSubmittedCheckmark.enabled = false;
    }

    void Start()
    {
        playerNameInputField.characterLimit = 20;
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
        playerNameSubmittedCheckmark.enabled = true;
        yield return new WaitForSeconds(time);
        playerNameSubmittedCheckmark.enabled = false;
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
