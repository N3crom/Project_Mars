using UnityEngine;

public class S_CoinManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnCoinCollected _rseOnCoinCollected;
    [SerializeField] RSE_OnAllCoinCollected _rseAllCoinCollected;

    [Header("RSO")]
    [SerializeField] RSO_CurrentCoinHave _rsoCurrentCoinHave;
    [SerializeField] RSO_CoinInArea _coinsInTheArea;

    //[Header("SSO")]

    void Awake()
    {
        _rsoCurrentCoinHave.Value = 0;
    }
    private void OnEnable()
    {
        _rseOnCoinCollected.action += AddCoin;
        _coinsInTheArea.onValueChanged += TcheckAllCoinCollected;
    }

    private void OnDisable()
    {
        _rseOnCoinCollected.action -= AddCoin;
        _coinsInTheArea.onValueChanged -= TcheckAllCoinCollected;
    }

    void AddCoin()
    {
        _rsoCurrentCoinHave.Value++;
    }

    void TcheckAllCoinCollected(int coinInArea)
    {
        if(coinInArea == 0)
        {
            _rseAllCoinCollected.RaiseEvent();
            Debug.Log("All coins collected!");
        }
        else
        {
            //Debug.Log("Not all coins collected!");
        }
    }
}