using UnityEngine;

public class StaticScriptableObject<T> : ScriptableObject
{
    [SerializeField] private T _value;

    public T Value
    {
        get => _value;
    }
}
