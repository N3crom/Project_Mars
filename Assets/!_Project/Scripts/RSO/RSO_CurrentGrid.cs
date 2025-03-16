using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentGrid", menuName = "Data/RSO/RSO_CurrentGrid")]
public class RSO_CurrentGrid : RuntimeScriptableObject<Dictionary<Vector2Int, S_Tile>> {}