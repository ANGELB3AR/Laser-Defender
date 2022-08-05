using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
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
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        secondaryWeapon = FindObjectOfType<SecondaryWeapon>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        weaponImage = null;
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        ammoText.text = secondaryWeapon.GetCurrentAmmo().ToString();
        weaponImage.overrideSprite = secondaryWeapon.GetWeaponSprite();
    }
}
