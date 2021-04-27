using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Managers
{
    public class UIManager : MonoBehaviour
    {
    #region  Variables
    public UIPanel[] UIPanels;
    
    [Header("Image References")]
    public Sprite[] audioSprites;
    public Sprite[] sfxSprites;
    public Image sfxImage;
    public Image audioImage;
    
    [Header("UI References")]
    public Text distanceText;
    public Text coinsText;

    //Reference to the Previously active panel
    private GameObject previousPanel;
    private bool _isMusicEnabled = true;
    private bool _isSfxEnabled = true;

    #endregion

    #region Unity Messages
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += SwitchPanel;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= SwitchPanel;
    }
    public void Update()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.inMenu:
                break;
            case GameManager.GameState.inPaused:
                break;
            case GameManager.GameState.inGame:
                //endlessCoinsText.text = "Coins: " + GameManager.Instance.CurrentCoins;
                break;
            case GameManager.GameState.Results:
                
                //resultsCoinsText.text = "Coins: " + GameManager.Instance.CurrentCoins;
                break;
            case GameManager.GameState.Dead:
                break;
        }
    }
    #endregion

    #region UI Switching
    private void SwitchPanel()
    {
        if (previousPanel == null)
            return;
        
        previousPanel.SetActive(false);

        for (int i = 0; i < UIPanels.Length; i++)
        {
            if (UIPanels[i].stateForUI == GameManager.Instance.gameState)
            {
                previousPanel = UIPanels[i].uiPanel;
                previousPanel.SetActive(true);
                break;
            }
        }
    }
    #endregion

    #region Audio
    public void PlayMenuSound() => AudioManager.Instance.PlayAudio(AudioTypes.SFX_MENU);
    public void MusicToggle()
    {
        _isMusicEnabled = !_isMusicEnabled;
        audioImage.sprite = audioSprites[_isMusicEnabled? 1 : 0];
            
        AudioManager.Instance.MuteTrack(AudioTrackType.Music, _isMusicEnabled);
    }

    public void SfxToggle()
    {
        _isSfxEnabled = !_isSfxEnabled;
        sfxImage.sprite = sfxSprites[_isSfxEnabled? 1 : 0];
        
        AudioManager.Instance.MuteTrack(AudioTrackType.SFX, _isSfxEnabled);
    }
    public void SetAudioSettings(bool isMusicMuted, bool isSFXMuted)
    {
        _isSfxEnabled = isSFXMuted;
        _isMusicEnabled = isMusicMuted;

        audioImage.sprite = audioSprites[_isMusicEnabled? 1 : 0];
        sfxImage.sprite = sfxSprites[_isSfxEnabled? 1 : 0];

        AudioManager.Instance.MuteTrack(AudioTrackType.Music, _isMusicEnabled);
        AudioManager.Instance.MuteTrack(AudioTrackType.SFX, _isSfxEnabled);
    }
    #endregion
}

#region Structs
[System.Serializable]
public struct UIPanel
{
    //GameObejct Reference to the UIPanel
    public GameObject uiPanel;
    //The State that the UIPanel will be active for
    public GameManager.GameState stateForUI;
}
#endregion
}
