using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PermanentJumpData", menuName = "Controls/PermanentJumpData", order = 1)]
public class PermanentJumpData : JumpData
{
    public JumpData copyData;



    [ContextMenu("CopyData")]
    public void CopyData()
    {
        dataType = copyData.DataType;
        initialVelocity = copyData.InitialVelocity;
        baseGravity = copyData.BaseGravity;
        keyReleaseGravity = copyData.KeyReleaseGravity;
        fallGravity = copyData.FallGravity;
        maxHeight = copyData.MaxHeight;
        timeToMaxHeight = copyData.TimeToMaxHeight;
        overrideJumpKey = copyData.OverrideJumpKey;
        jumpKey = copyData.JumpKey;
    }

}
