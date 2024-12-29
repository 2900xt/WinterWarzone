using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
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
    }

    public void StartHost()
    {
        PlayerPrefs.SetString("Mode", "Host");
        SceneManager.LoadScene(1);
    }

    public void StartClient()
    {
        PlayerPrefs.SetString("Mode", "Client");
        SceneManager.LoadScene(1);
    }
}
