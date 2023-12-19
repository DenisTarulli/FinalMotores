using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int killCounter;
    public bool eventActive;

    public bool gameOver;

    public bool greenRune;
    public bool purpleRune;
    public bool blueRune;

    [SerializeField] private Image greenRuneImg;
    [SerializeField] private Image purpleRuneImg;
    [SerializeField] private Image blueRuneImg;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Ambience");

        eventActive = false;

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        else
            Load();
    }

    public void UpdateRunesUI()
    {
        if (greenRune)
            greenRuneImg.color = new Color32(255, 255, 255, 255);

        if (purpleRune)
            purpleRuneImg.color = new Color32(255, 255, 255, 255);

        if (blueRune)
            blueRuneImg.color = new Color32(255, 255, 255, 255);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void GameOverScreen()
    {
        Cursor.lockState = CursorLockMode.None;

        gameOver = true;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);

        if (FindObjectOfType<PlayerActions>().currentHealth <= 0)
            loseText.SetActive(true);
        else
            winText.SetActive(true);
    }
}
