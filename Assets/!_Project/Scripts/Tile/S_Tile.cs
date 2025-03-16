using UnityEngine;

public class S_Tile : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    //[Header("RSE")]

    //[Header("RSO")]

    //[Header("SSO")]
    [SerializeField] private bool _isWalkable;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _walkableSprite;
    [SerializeField] private Sprite _wallSprite;
    [SerializeField] private Sprite _spawnSprite;
    [SerializeField] private Sprite _exitSprite;

    public bool IsWalkable => _isWalkable;
    public Vector2Int GridPosition { get; private set; }
    public TileType TileType { get; private set; }

    public void Initialize(Vector2Int position, bool isWalkable, TileType type)
    {
        GridPosition = position;
        _isWalkable = isWalkable;
        TileType = type;
        UpdateVisual(type);
    }

    private void UpdateVisual(TileType type)
    {
        switch (type)
        {
            case TileType.Walkable:
                //_spriteRenderer.sprite = _walkableSprite;
                _spriteRenderer.color = Color.white;
                break;
            case TileType.Wall:
                //_spriteRenderer.sprite = _wallSprite;
                _spriteRenderer.color = Color.black;

                break;
            case TileType.Spawn:
                //_spriteRenderer.sprite = _spawnSprite;
                _spriteRenderer.color = Color.blue;

                break;
            case TileType.Exit:
                //_spriteRenderer.sprite = _exitSprite;
                _spriteRenderer.color = Color.green;

                break;
        }
    }
}

public enum TileType
{
    Walkable,
    Wall,
    Spawn,
    Exit
}
