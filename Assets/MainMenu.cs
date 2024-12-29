using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    public TextMeshProUGUI IPText;

    void Start()
    {
        if (particleSystem != null)
        {
            particleSystem.Play(); // Play the particle system on start
        }
        else
        {
            Debug.LogWarning("ParticleSystem not assigned!");
        }
        
        PlayerPrefs.SetString("IP", "127.0.0.1");
    }

    public void StartHost()
    {
        PlayerPrefs.SetString("Mode", "Host");
        SceneManager.LoadScene(1);
    }

    public void OnUpdateIPText()
    {
        PlayerPrefs.SetString("IP", IPText.text);
    }

    public void StartClient()
    {
        PlayerPrefs.SetString("Mode", "Client");
        string cleanedIP = System.Text.RegularExpressions.Regex.Replace(IPText.text, "[^0-9.]", ""); 
        PlayerPrefs.SetString("IP", cleanedIP);
        SceneManager.LoadScene(1);
    }
}
