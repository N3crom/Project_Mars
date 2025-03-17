using UnityEngine;

public class S_GhostPickUp : S_Collectible
{
    public RSE_OnGhostPickUpCollected _rseOnGhostPickUpCollected;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _rseOnGhostPickUpCollected.RaiseEvent();
            Destroy(gameObject);
        }
    }
}
