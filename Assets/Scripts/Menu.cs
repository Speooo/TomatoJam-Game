using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private TextMeshProUGUI sfxVolText;
    [SerializeField] private TextMeshProUGUI musicVolText;

    private void Start()
    {
        sfxVolText.text = (AudioManager.Instance.sfxVolume * 100).ToString();
        musicVolText.text = (AudioManager.Instance.musicVolume * 100).ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void IncreaseSFXVolume()
    {
        AudioManager.Instance.IncreaseSFXVolume();
        sfxVolText.text = (Mathf.Round(AudioManager.Instance.sfxVolume * 100)).ToString();
    }

    public void DecreaseSFXVolume()
    {
        AudioManager.Instance.DecreaseSFXVolume();
        sfxVolText.text = (Mathf.Round(AudioManager.Instance.sfxVolume * 100)).ToString();
    }

    public void IncreaseMusicVolume()
    {
        AudioManager.Instance.IncreaseMusicVolume();
        musicVolText.text = (Mathf.Round(AudioManager.Instance.musicVolume * 100)).ToString();
    }

    public void DecreaseMusicVolume()
    {
        AudioManager.Instance.DecreaseMusicVolume();
        musicVolText.text = (Mathf.Round(AudioManager.Instance.musicVolume * 100)).ToString();
    }
}
