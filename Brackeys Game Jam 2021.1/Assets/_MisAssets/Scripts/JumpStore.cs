using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpStore", menuName = "Store/JumpStore", order = 1)]
public class JumpStore : ScriptableObject
{
    public List<JumpData> jumps = new List<JumpData>();
}
