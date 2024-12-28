using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider dashSlider;
    public Image dashFillImage;
    public Color dashSliderColor;
    public PlayerMotor playerMotor;
    // Start is called before the first frame update
    void Start()
    {
        dashSliderColor = dashFillImage.color;
    }

    // Update is called once per frame
    void Update()
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
    }
}
