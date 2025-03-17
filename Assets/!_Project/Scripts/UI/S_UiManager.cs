using TMPro;
using UnityEngine;

public class S_UiManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] TextMeshProUGUI _coinText;
    [SerializeField] TextMeshProUGUI _staminaText;
    [SerializeField] GameObject _winPanel;

    [Header("RSE")]
    [SerializeField] RSE_OnAllCoinCollected _rseAllCoinCollected;
    [SerializeField] RSE_OnLevelFinish _rseOnLevelFinish;

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
        _rseOnLevelFinish.action += Win;
    }
    private void OnDisable()
    {
        _rsoCurrentCoinHave.onValueChanged -= UpdateCoinText;
        _rsoTotalCoinsInArea.onValueChanged -= UpdateTotalCoinText;
        _rsoCurrentPlayerStamina.onValueChanged -= UpdateStaminaText;
        _rseOnLevelFinish.action -= Win;
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

    void Win()
    {
        _winPanel.SetActive(true);
    }
}