using UnityEngine;

public abstract class S_Collectible : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D other);
}