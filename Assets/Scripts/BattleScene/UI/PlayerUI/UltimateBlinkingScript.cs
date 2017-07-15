using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UltimateBlinkingScript : MonoBehaviour {

    Image blinkImage;
    public float interval = 0.5f;
    public float startDelay = 0.0f;
    public bool defaultState = true;
    bool isBlinking = false;

    void Start()
    {
        blinkImage = GetComponent<Image>();
        blinkImage.enabled = defaultState;
    }

    void Update()
    {
        if (!isBlinking && BlinkCondition())
            StartBlink();
        else if (isBlinking && StopBlinkCondition())
            StopBlinking();
    }

    bool BlinkCondition()
    {
        return (blinkImage.fillAmount == 1.0f && !isBlinking);
    }

    bool StopBlinkCondition()
    {
        return blinkImage.fillAmount != 1.0f;
    }

    void StopBlinking()
    {
        CancelInvoke();
        isBlinking = false;
        blinkImage.enabled = defaultState;
    }

    public void StartBlink()
    {
        // do not invoke the blink twice - needed if you need to start the blink from an external object
        if (isBlinking)
            return;

        if (blinkImage != null)
        {
            isBlinking = true;
            InvokeRepeating("ToggleState", startDelay, interval);
            Debug.Log("interval :" + interval + "Start Delay is : "+ startDelay);
            SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("Heartbeat"));
        }
    }

    void ToggleState()
    {
        blinkImage.enabled = !blinkImage.enabled;
    }

}
