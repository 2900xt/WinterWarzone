using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider dashSlider, healthSlider;
    public Image dashFillImage;
    public Color dashSliderColor;
    public PlayerMotor playerMotor;
    public PlayerData playerData;
    public TextMeshProUGUI score0Text, score1Text;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        dashSliderColor = dashFillImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMotor != null)
        {
            dashSlider.value = 1 - playerMotor.curDashCooldownTime / playerMotor.dashCooldownTime;
            if(playerMotor.curDashCooldownTime <= 0)
            {
                dashFillImage.color = Color.green;
            }
            else
            {
                dashFillImage.color = dashSliderColor;
            }

            healthSlider.value = playerData.health.Value / 100f;
        }
        else 
        {
            //try to find player
            NetworkManager.Singleton.LocalClient?.PlayerObject?.TryGetComponent(out playerMotor);
            NetworkManager.Singleton.LocalClient?.PlayerObject?.TryGetComponent(out playerData);
        }

        if(gameManager == null)
        {
            gameManager = NetworkManager.Singleton.GetComponent<GameManager>();
        }

        score0Text.text = gameManager.score1.Value.ToString();
        score1Text.text = gameManager.score2.Value.ToString();
    }
}
