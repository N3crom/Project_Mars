using UnityEngine;

public class S_Coin : S_Collectible
{
    [Header("RSE")]
    [SerializeField] RSE_OnCoinCollected _rseOnCoinCollected;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _rseOnCoinCollected.RaiseEvent();
            Destroy(gameObject);
            Debug.Log("Coin collected!");
        }
    }
}