using TMPro;
using UnityEngine;

public class S_UiManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] TextMeshProUGUI _coinText;

    //[Header("RSE")]

    [Header("RSO")]
    [SerializeField] RSO_CurrentCoinHave _rsoCurrentCoinHave;
    [SerializeField] RSO_CoinInArea _rsoCoinsInTheArea;
    [SerializeField] RSO_TotalCoinInArea _rsoTotalCoinsInArea;

    //[Header("SSO")]

    private void Awake()
    {
        UpdateCoinText(0);
    }
    private void OnEnable()
    {
        _rsoCurrentCoinHave.onValueChanged += UpdateCoinText;
        _rsoTotalCoinsInArea.onValueChanged += UpdateTotalCoinText;
    }
    private void OnDisable()
    {
        _rsoCurrentCoinHave.onValueChanged -= UpdateCoinText;
        _rsoTotalCoinsInArea.onValueChanged -= UpdateTotalCoinText;
    }
    void UpdateCoinText(int coinAmmount)
    {
        _coinText.text = $"Coin: {coinAmmount} / {_rsoTotalCoinsInArea.Value}";
    }

    void UpdateTotalCoinText(int maxAmmount)
    {
        _coinText.text = $"Coin: {_rsoCurrentCoinHave.Value} / {maxAmmount}";

    }
}