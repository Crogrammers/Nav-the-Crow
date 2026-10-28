using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Object List", menuName = "Scriptable Objects/Game Object List")]
public class GameObjListSO : ScriptableObject
{
    public List<Transform> objsList;
}
