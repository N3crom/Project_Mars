using UnityEngine;

/*
  Posiblement changer en CollectibleDetection si jamais on veut ajouter d'autres types de collectibles
*/
public class S_CoinDetection : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    //[Header("RSE")]

    [Header("RSO")]
    [SerializeField] RSO_CoinInArea _rsoCoinsInTheArea;
    [SerializeField] RSO_TotalCoinInArea _rsoTotalCoinsInArea;

    //[Header("SSO")]

    private void Awake()
    {
        _rsoCoinsInTheArea.Value = 0;
        _rsoTotalCoinsInArea.Value = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Coin detected! ");

        if (collision.CompareTag("Coin"))
        {
            _rsoCoinsInTheArea.Value++;
            _rsoTotalCoinsInArea.Value++;
            Debug.Log($"Coin in the area: {_rsoCoinsInTheArea.Value}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            _rsoCoinsInTheArea.Value--;
            Debug.Log($"Coin in the area left: {_rsoCoinsInTheArea.Value}");
        }
    }

}