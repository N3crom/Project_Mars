using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_UiManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] TextMeshProUGUI _coinText;
    [SerializeField] TextMeshProUGUI _staminaText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _losePanel;

    [Header("RSE")]
    [SerializeField] RSE_OnAllCoinCollected _rseAllCoinCollected;
    [SerializeField] RSE_OnLevelFinish _rseOnLevelFinish;
    [SerializeField] RSE_OnStaminaDepleted _rseOnStaminaDepleted;
    [SerializeField] RSE_LoadMainMenu _rseLoadMainMenu;
    [SerializeField] RSE_LoadNextLevel _rseLoadNextLevel;
    [SerializeField] RSE_OnPlayerStuckInsideWall _rseOnPlayerStuckInsideWall;
    [SerializeField] RSE_UpdateTimer _rseUpdateTimer;

    [Header("RSO")]
    [SerializeField] RSO_CurrentCoinHave _rsoCurrentCoinHave;
    [SerializeField] RSO_CoinInArea _rsoCoinsInTheArea;
    [SerializeField] RSO_TotalCoinInArea _rsoTotalCoinsInArea;
    [SerializeField] RSO_CurrentPlayerStamina _rsoCurrentPlayerStamina;

    //[Header("SSO")]

    private void Awake()
    {
        UpdateCoinText(0);
    }
    private void OnEnable()
    {
        _rsoCurrentCoinHave.onValueChanged += UpdateCoinText;
        _rsoTotalCoinsInArea.onValueChanged += UpdateTotalCoinText;
        _rsoCurrentPlayerStamina.onValueChanged += UpdateStaminaText;
        _rseUpdateTimer.action += UpdateTimerText;
        _rseOnLevelFinish.action += Win;
        _rseOnStaminaDepleted.action += Lose;
        _rseOnPlayerStuckInsideWall.action += Lose;
    }
    private void OnDisable()
    {
        _rsoCurrentCoinHave.onValueChanged -= UpdateCoinText;
        _rsoTotalCoinsInArea.onValueChanged -= UpdateTotalCoinText;
        _rsoCurrentPlayerStamina.onValueChanged -= UpdateStaminaText;
        _rseUpdateTimer.action -= UpdateTimerText;
        _rseOnLevelFinish.action -= Win;
        _rseOnStaminaDepleted.action -= Lose;
        _rseOnPlayerStuckInsideWall.action -= Lose;
    }
    void UpdateCoinText(int coinAmmount)
    {
        _coinText.text = $"Coin: {coinAmmount} / {_rsoTotalCoinsInArea.Value}";
    }

    void UpdateStaminaText(int coinAmmount)
    {
        _staminaText.text = $"Stamina: {_rsoCurrentPlayerStamina.Value}";
    }

    void UpdateTotalCoinText(int maxAmmount)
    {
        _coinText.text = $"Coin: {_rsoCurrentCoinHave.Value} / {maxAmmount}";

    }
    void UpdateTimerText(float timerAmmount)
    {
        _timerText.text = $"Timer left: {((int)timerAmmount)}s";
    }
    void Lose()
    {
        _losePanel.SetActive(true);
    }

    void Win()
    {
        _winPanel.SetActive(true);
    }

    public void OnMainMenuButtonClicked()
    {
        _rseLoadMainMenu.RaiseEvent();
    }

    public void OnContinueButtonClicked()
    {
        _rseLoadNextLevel.RaiseEvent();
    }
}