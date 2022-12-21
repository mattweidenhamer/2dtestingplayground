using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flasher : MonoBehaviour
{
    private float durationToFlash = 0f;
    [SerializeField] float timeBetweenFlash;
    [SerializeField] Color32 defaultTextColor;
    [SerializeField] Color32[] flashColors;
    private int currentColor = 0;
    private float timeSinceLastFlash = 0f;
    private bool isFlashing = false;
    private SpriteRenderer spRenderer;
    private float timeOn;
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isFlashing){
            timeSinceLastFlash += Time.deltaTime;
            if(timeSinceLastFlash >= timeBetweenFlash){
                flash();
            }
        }
    }
    public void flash()
    {
        timeSinceLastFlash = 0f;
        if(++currentColor >= flashColors.Length){
            currentColor = 0;
        }
        spRenderer.color = flashColors[currentColor];
        if (timeOn >= durationToFlash){
            isFlashing = false;
            spRenderer.color = defaultTextColor;
        }
    }
    public void startFlasher(float duration)
    {
        durationToFlash = duration;
        timeOn = 0f;
        currentColor = 0;
        isFlashing = true;
        flash();
    }
}
