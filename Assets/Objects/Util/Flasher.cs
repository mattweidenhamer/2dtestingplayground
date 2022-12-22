using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flasher : MonoBehaviour
{
    private float durationToFlash = 0f;
    [SerializeField] float timeBetweenFlash;
    private Color defaultColor;
    [SerializeField] Color[] flashColors;
    private int currentColor = 0;
    private float timeSinceLastFlash = 0f;
    private bool isFlashing = false;
    private SpriteRenderer spRenderer;
    private float timeOn;
    void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
        spRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isFlashing){
            timeSinceLastFlash += Time.deltaTime;
            timeOn += Time.deltaTime;
            if(timeSinceLastFlash >= timeBetweenFlash){
                flash();
            }
            if (timeOn >= durationToFlash){
                Debug.Log("Stopping flash");
                isFlashing = false;
                spRenderer.color = defaultColor;
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
    }
    public void startFlasher(float duration)
    {
        Debug.Log("Starting flasher");
        //StopCoroutine("flasher");
        //StartCoroutine(flasher(duration));
        durationToFlash = duration;
        timeOn = 0f;
        currentColor = 0;
        isFlashing = true;
        flash();
    }
    //TODO turn flasher into an IEnumerator
    IEnumerator flasher(float duration){
        isFlashing = true;
        currentColor = 0;
        while(true){
            flash();
            yield return new WaitForSeconds(timeBetweenFlash);
        }
        isFlashing = false;
    }
}
