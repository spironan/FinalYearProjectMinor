using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public enum MAIN_OPTIONS
{
    START_PVP,
    PRACTICE,
    SETTINGS,
    CONTROLGUIDE,
    CREDITS,
    EXIT,
    TOTAL,
};

public enum QUIT_OPTIONS
{
    YES,
    NO,
    TOTAL,
};

public enum SETTINGS_OPTIONS
{
    BGM,
    SFX,
    TOTAL,
};

public class NewMainMenuScript : MonoBehaviour {
    public GameObject mainMenuHolder;
    public GameObject confirmationToquitHolder;
    public GameObject settingsHolder;
    public GameObject controlGuideHolder;
    public GameObject creditsHolder;

    public float timerToMovieScene;

    float movieSceneCountdown;
    bool prevMoved,curMoved;

    MAIN_OPTIONS mainMenu_button = MAIN_OPTIONS.START_PVP;
    QUIT_OPTIONS quit_button = QUIT_OPTIONS.NO;
    SETTINGS_OPTIONS settings_button = SETTINGS_OPTIONS.BGM;

    Button[] mainMenu_buttons = null;
    Button[] quit_buttons = null;
    Button[] settings_buttons = null;

    EventSystem eventSystem;

    bool disableMainMenu = false;
    bool disableConfirmationToQuit = true;
    bool disableSettings = true;
    bool disableControlGuide = true;
    bool disableCredits = true;

    bool runOnce = false;

    // Use this for initialization
    void Awake() {
        eventSystem = GameObject.FindWithTag("EventSystem").GetComponent<EventSystem>();
        //mainMenuHolder = GameObject.FindWithTag("MainMenuOptionsHolder");
        //confirmationToquitHolder = GameObject.FindWithTag("ConfirmationToQuitHolder");

        //mainMenu_button = MAIN_OPTIONS.START_PVP;

        if (mainMenu_buttons == null)
            mainMenu_buttons = mainMenuHolder.GetComponentsInChildren<Button>();
        if (quit_buttons == null)
            quit_buttons = confirmationToquitHolder.GetComponentsInChildren<Button>();
        if (settings_buttons == null)
            settings_buttons = settingsHolder.GetComponentsInChildren<Button>();

        movieSceneCountdown = timerToMovieScene;
        prevMoved = curMoved = false;
        
        //set all sliders value to be the same as the volume.


        confirmationToquitHolder.SetActive(false);
        settingsHolder.SetActive(false);
        controlGuideHolder.SetActive(false);
        creditsHolder.SetActive(false);
        StartCoroutine(MainMenu_HighlightButton());
    }

    public IEnumerator MainMenu_HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(mainMenu_buttons[(int)mainMenu_button].gameObject);
    }

    public IEnumerator Quit_HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(quit_buttons[(int)quit_button].gameObject);
    }

    public IEnumerator Settings_HighlightButton()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(settings_buttons[(int)settings_button].gameObject);
    }

    // Update is called once per frame
    void Update () {
        if(!runOnce)
        {
            GameManager.Instance.RestoreDefaults();

            Slider temp = settings_buttons[(int)SETTINGS_OPTIONS.BGM].gameObject.GetComponentInChildren<Slider>();
            temp.value = SoundSystem.Instance.GetVolumeByType((AUDIO_TYPE.BACKGROUND_MUSIC));

            Slider temp2 = settings_buttons[(int)SETTINGS_OPTIONS.SFX].gameObject.GetComponentInChildren<Slider>();
            temp2.value = SoundSystem.Instance.GetVolumeByType(AUDIO_TYPE.SOUND_EFFECTS);

            GameManager.Instance.ChangeState(GAMESTATE.MAIN_MENU);
            for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
            {
                GameManager.Instance.GetPlayer(i).UnAssign();//unassign player info
            }

            runOnce = true;
        }
        if (!disableMainMenu)
        {
            MainActions();
        }
        else if(!disableConfirmationToQuit)
        {
            ConfirmationToQuitActions();
        }
        else if(!disableSettings)
        {
            SettingsActions();
        }
        else if (!disableControlGuide)
        {
            ControlGuideActions();
        }
        else if (!disableCredits)
        {
            CreditsActions();
        }

        if (!curMoved && !prevMoved)
        {
            movieSceneCountdown -= Time.deltaTime;
            if (movieSceneCountdown <= 0.0f)
                LoadingScreenManager.LoadScene("VideoScene");
        }
        else if (!curMoved && prevMoved)
        {
            movieSceneCountdown = timerToMovieScene;
        }
        prevMoved = curMoved;
        curMoved = false;
    }

    void SettingsActions()
    {
        if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
        {
            if (settings_button < SETTINGS_OPTIONS.TOTAL - 1)
            {
                settings_button++;
                settings_buttons[(int)settings_button].Select();
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
            }
            curMoved = true;
        }
        else if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_UP))
        {
            if (settings_button > SETTINGS_OPTIONS.BGM)
            {
                settings_button--;
                settings_buttons[(int)settings_button].Select();
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
            }
            curMoved = true;
        }

        if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
        {
            Slider temp = settings_buttons[(int)settings_button].gameObject.GetComponentInChildren<Slider>();
            if (temp != null)
            {
                temp.value += 10 * Time.deltaTime;
                SoundSystem.Instance.OnValueChanged(temp, (AUDIO_TYPE)settings_button);
            }
            curMoved = true;
        }
        else if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
        {
            Slider temp = settings_buttons[(int)settings_button].gameObject.GetComponentInChildren<Slider>();
            if (temp != null)
            {
                temp.value -= 10 * Time.deltaTime;
                SoundSystem.Instance.OnValueChanged(temp, (AUDIO_TYPE)settings_button);
            }
            curMoved = true;
        }

        if (GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.BACK))
        {
            StartCoroutine(MainMenu_HighlightButton());
            disableMainMenu = false;
            disableSettings = true;
            settingsHolder.SetActive(false);

            curMoved = true;
        }
    }

    void ConfirmationToQuitActions()
    {
        if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
        {
            if (quit_button < QUIT_OPTIONS.TOTAL - 1)
            {
                quit_button++;
                quit_buttons[(int)quit_button].Select();
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
            }
            curMoved = true;
        }
        else if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
        {
            if (quit_button > QUIT_OPTIONS.YES)
            {
                quit_button--;
                quit_buttons[(int)quit_button].Select();
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
            }
            curMoved = true;
        }
        if (GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.BACK))
        {
            StartCoroutine(MainMenu_HighlightButton());
            disableMainMenu = false;
            disableConfirmationToQuit = true;
            confirmationToquitHolder.SetActive(false);

            curMoved = true;
        }
        if (GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.SELECT))
        {
            SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));
            switch (quit_button)
            {
                case QUIT_OPTIONS.YES:
                    {
                        //app should close here do saving here
                        ExitGameScript.ExitApplication();
                        Application.Quit();
                    }
                    break;
                case QUIT_OPTIONS.NO:
                    {
                        StartCoroutine(MainMenu_HighlightButton());
                        disableMainMenu = false;
                        disableConfirmationToQuit = true;
                        confirmationToquitHolder.SetActive(false);
                    }
                    break;
            }
            curMoved = true;
        }
    }

    void MainActions()
    {
        if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_DOWN))
        {
            if (mainMenu_button < MAIN_OPTIONS.TOTAL - 1)
            {
                mainMenu_button++;
                mainMenu_buttons[(int)mainMenu_button].Select();
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
            }
            curMoved = true;
        }
        else if (GameManager.Instance.GetMasterPlayerData().controller.getAxisActionBoolDown(ACTIONS.MOVE_UP))
        {
            if (mainMenu_button > MAIN_OPTIONS.START_PVP)
            {
                mainMenu_button--;
                mainMenu_buttons[(int)mainMenu_button].Select();
                SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("SelectOption"));
            }
            curMoved = true;
        }

        if (GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.SELECT))
        {
            SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("ExecuteOption"));
            switch (mainMenu_button)
            {
                case MAIN_OPTIONS.START_PVP:
                    {
                        GameManager.Instance.SetGameMode(GAME_MODES.LOCAL_PVP);
                        LoadingScreenManager.LoadScene("CharacterSelectScene");
                    }
                    break;
                case MAIN_OPTIONS.PRACTICE:
                    {
                        GameManager.Instance.SetGameMode(GAME_MODES.PRACTICE);
                        LoadingScreenManager.LoadScene("CharacterSelectScene");
                    }
                    break;
                case MAIN_OPTIONS.SETTINGS:
                    {
                        StartCoroutine(Settings_HighlightButton());
                        disableMainMenu = true;
                        disableSettings = false;
                        settingsHolder.SetActive(true);
                    }
                    break;
                case MAIN_OPTIONS.CONTROLGUIDE:
                    {
                        //StartCoroutine(Settings_HighlightButton());
                        disableMainMenu = true;
                        disableControlGuide = false;
                        controlGuideHolder.SetActive(true);
                    }
                    break;
                case MAIN_OPTIONS.CREDITS:
                    {
                        //StartCoroutine(Settings_HighlightButton());
                        disableMainMenu = true;
                        disableCredits = false;
                        creditsHolder.SetActive(true);
                    }
                    break;
                case MAIN_OPTIONS.EXIT:
                    {
                        StartCoroutine(Quit_HighlightButton());
                        disableMainMenu = true;
                        disableConfirmationToQuit = false;
                        confirmationToquitHolder.SetActive(true);
                        //Application.Quit();
                    }
                    break;
            }
            curMoved = true;
        }
    }

    void ControlGuideActions()
    {
        if (GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.BACK)
            || GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.SELECT))
        {
            StartCoroutine(MainMenu_HighlightButton());
            disableMainMenu = false;
            disableControlGuide = true;
            controlGuideHolder.SetActive(false);

            curMoved = true;
        }
    }

    void CreditsActions()
    {
        if (GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.BACK)
            || GameManager.Instance.GetMasterPlayerData().controller.getButtonAction(ACTIONS.SELECT))
        {
            StartCoroutine(MainMenu_HighlightButton());
            disableMainMenu = false;
            disableCredits = true;
            creditsHolder.SetActive(false);
        }
        curMoved = true;
    }
}
