using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using System;

public class UIDisplay : NetworkBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Secondary Weapon")]
    [SerializeField] Image weaponImage;
    [SerializeField] TextMeshProUGUI ammoText;
    SecondaryWeapon secondaryWeapon;

    void Awake()
    {
        Player.OnPlayerSpawned += Player_OnPlayerSpawned;

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        secondaryWeapon = FindObjectOfType<SecondaryWeapon>();
    }

    private void Player_OnPlayerSpawned(Player player)
    {
        if (!player.IsLocalPlayer) { return; }

        playerHealth = player.GetComponent<Health>();
        healthSlider.maxValue = playerHealth.GetHealth();
        weaponImage.overrideSprite = null;

        playerHealth.OnHealthUpdated += PlayerHealth_OnHealthUpdated;
    }

    private void PlayerHealth_OnHealthUpdated(int currentHealth)
    {
        healthSlider.value = currentHealth;
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

    void Update()
    {
        ammoText.text = secondaryWeapon.GetCurrentAmmo().ToString();
        weaponImage.overrideSprite = secondaryWeapon.GetWeaponSprite();

        ammoText.enabled = secondaryWeapon.GetCurrentAmmo() != 0;
        weaponImage.enabled = secondaryWeapon.GetCurrentAmmo() != 0;
    }
}
