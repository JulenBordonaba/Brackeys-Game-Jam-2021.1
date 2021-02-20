using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CanEditMultipleObjects]
[CustomEditor(typeof(JumpData))]
public class JumpDataEditor : Editor
{
    protected Texture logo;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        JumpData jumpData = MyJump;
        
        ManageLogo();
        
        jumpData.DataType = (JumpDataInputMode)EditorGUILayout.EnumPopup("Data Input Mode",jumpData.DataType);

        GUILayout.Space(10);

        switch (jumpData.DataType)
        {
            case JumpDataInputMode.VelocityGravity:
                DisplayGravityVelocityMode(jumpData);
                break;
            case JumpDataInputMode.HeightTime:
                DisplayHeightTimeMode(jumpData);
                break;
            default:
                break;
        }

        GUILayout.Space(10);

        DisplayKeyOptions(jumpData);

        Save(jumpData);

        serializedObject.ApplyModifiedProperties();


    }

    private void Save(JumpData jumpData)
    {
        if (GUI.changed)
        {
            Undo.RecordObject(jumpData, "save");
            EditorUtility.SetDirty(jumpData);
            AssetDatabase.SaveAssets();
            Repaint();
            Debug.Log("SAVED");
        }
    }

    private void DisplayKeyOptions(JumpData jumpData)
    {
        jumpData.OverrideJumpKey = EditorGUILayout.Toggle("Override Jump Key", jumpData.OverrideJumpKey);

        if (jumpData.OverrideJumpKey)
        {
            jumpData.JumpKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Jump Key", "The key that triggers the jump when pressed"), jumpData.JumpKey);
        }
    }

    private void ManageLogo()
    {
        CheckLogo();

        DisplayLogo();

        GUILayout.Space(20);
    }

    private void DisplayLogo()
    {
        if (logo == null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 20, stretchHeight = true, clipping = TextClipping.Overflow, border = new RectOffset() };
            EditorGUILayout.LabelField("---Jump Data---", style, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }
        else
        {
            GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, imagePosition = ImagePosition.ImageOnly, clipping = TextClipping.Clip };
            GUILayout.Label(logo, style);
        }

    }

    private void CheckLogo()
    {
        if (logo != null) return;

        logo = (Texture)EditorGUIUtility.Load("Jump/JumpData_Logo.png");

    }

    private void DisplayHeightTimeMode(JumpData jumpData)
    {
        jumpData.MaxHeight = EditorGUILayout.FloatField("Max Height", jumpData.MaxHeight);
        jumpData.TimeToMaxHeight = EditorGUILayout.FloatField("Time to reach Max Height", jumpData.TimeToMaxHeight);

        GUILayout.Space(10);

        jumpData.KeyReleaseGravity = EditorGUILayout.FloatField("Key Release Gravity", jumpData.KeyReleaseGravity);
        jumpData.FallGravity = EditorGUILayout.FloatField("Falling Gravity", jumpData.FallGravity);

        SetValuesHeightTimeMode(jumpData);

        ClampGravity(jumpData);

        GUILayout.Space(20);

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 15, stretchHeight = true, clipping = TextClipping.Overflow };
        EditorGUILayout.LabelField("Max Height: " + jumpData.MaxHeight, style, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
    }

    private void DisplayGravityVelocityMode(JumpData jumpData)
    {

        jumpData.InitialVelocity = EditorGUILayout.FloatField("Initial Velocity", jumpData.InitialVelocity);
        jumpData.BaseGravity = EditorGUILayout.FloatField("Base Gravity", jumpData.BaseGravity);
        jumpData.FallGravity = EditorGUILayout.FloatField("Falling Gravity", jumpData.FallGravity);
        jumpData.KeyReleaseGravity = EditorGUILayout.FloatField("Key Release Gravity", jumpData.KeyReleaseGravity);

        ClampGravity(jumpData);

        SetValuesGravityVelocityMode(jumpData);

        GUILayout.Space(20);

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 15, stretchHeight = true, clipping = TextClipping.Overflow };
        EditorGUILayout.LabelField("Max Height: " + jumpData.MaxHeight, style, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
    }

    private void SetValuesGravityVelocityMode(JumpData jumpData)
    {
        float h0 = 0;
        float v = jumpData.InitialVelocity;
        float g = Mathf.Abs(jumpData.BaseGravity);
        float t = Mathf.Abs(v / g);
        jumpData.TimeToMaxHeight = t;

        float hmax = h0 + v * t - ((0.5f * g) * Mathf.Pow(t, 2));


        if (v == 0) hmax = 0;

        if (g == 0) hmax = float.PositiveInfinity;

        jumpData.MaxHeight = hmax;
    }

    private void SetValuesHeightTimeMode(JumpData jumpData)
    {
        float h = jumpData.MaxHeight;
        float t = jumpData.TimeToMaxHeight;

        float g = (2 * h) / Mathf.Pow(t, 2);

        float v = Mathf.Sqrt(2 * g * h);

        jumpData.InitialVelocity = v;
        jumpData.BaseGravity = g;


    }

    private void ClampGravity(JumpData jumpData)
    {
        bool isPositive = jumpData.BaseGravity >= 0;

        if (isPositive)
        {
            jumpData.KeyReleaseGravity = Mathf.Max(jumpData.KeyReleaseGravity, jumpData.BaseGravity);
            jumpData.FallGravity = Mathf.Max(jumpData.FallGravity, jumpData.BaseGravity);
        }
        else
        {
            jumpData.KeyReleaseGravity = Mathf.Min(jumpData.KeyReleaseGravity, jumpData.BaseGravity);
            jumpData.FallGravity = Mathf.Min(jumpData.FallGravity, jumpData.BaseGravity);
        }
    }

    public JumpData MyJump
    {
        get
        {
            return (JumpData)target;
        }
    }
}
