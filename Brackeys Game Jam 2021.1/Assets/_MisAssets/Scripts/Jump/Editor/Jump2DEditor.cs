using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Jump2D))]
public class Jump2DEditor : Editor
{
    protected Texture logo;
    protected Jump2D jump2D;

    public override void OnInspectorGUI()
    {
        


        ManageTarget();

        ManageLogo();

        DisplayBaseData();

        DisplayExtraOptions();
        
        DisplayHeight();
        


    }

    private void ManageTarget()
    {
        if(jump2D==null)
        {
            jump2D = MyJump;
        }
    }

    private void DisplayHeight()
    {
        if (!jump2D.Allowed) return;

        GUILayout.Space(20);

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 15, stretchHeight = true, clipping = TextClipping.Overflow };
        EditorGUILayout.LabelField("Max Height: " + ReachedHeight.ToString(), style, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        GUILayout.Space(20);
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
            EditorGUILayout.LabelField("---Jump2D---", style, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
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

        logo = (Texture)EditorGUIUtility.Load("Jump/Jump2D_Logo.png");

    }

    private void DisplayBaseData()
    {

        GUIStyle style = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 13};

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(new GUIContent("Allowed", "If not allowed you wont jump"),style, GUILayout.Width(115), GUILayout.ExpandWidth(true));
        jump2D.Allowed = EditorGUILayout.Toggle(jump2D.Allowed);

        GUILayout.EndHorizontal();

        if (!jump2D.Allowed) return;

        GUILayout.Space(15);


        style = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 13 };
        EditorGUILayout.LabelField("Jump Data", style);

        jump2D.BaseJumpData = EditorGUILayout.ObjectField(new GUIContent("Base Jump Data", "The data for the base jump"), jump2D.BaseJumpData, typeof(JumpData), true) as JumpData;

        jump2D.RelativeSpace = (Space)EditorGUILayout.EnumPopup(new GUIContent("Jump Relative Space", "Relative sapce to the jump [local/global] (Not implemented)"), jump2D.RelativeSpace);

        jump2D.JumpKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Jump Key", "The key that triggers the jump when pressed"), jump2D.JumpKey);

        //jump2D.FloorLayers = EditorGUILayout.l

        GUILayout.Space(15);

        //Extra options
        style = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, fontSize = 13 };
        EditorGUILayout.LabelField("Extra Options", style);
        
        jump2D.AllowExtraOptions = EditorGUILayout.BeginToggleGroup("Allow Extra Options", jump2D.AllowExtraOptions);

        jump2D.AllowExtraJumps = EditorGUILayout.Toggle("Extra Jumps", jump2D.AllowExtraJumps);
        jump2D.AllowChainJumps = EditorGUILayout.Toggle("Chain Jumps", jump2D.AllowChainJumps);

        EditorGUILayout.EndToggleGroup();




    }

    private void DisplayExtraOptions()
    {
        if (!jump2D.Allowed) return;
        if (!jump2D.AllowExtraOptions) return;

        serializedObject.Update();

        if (jump2D.AllowExtraJumps)
        {
            GUILayout.Space(10);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("extraJumps"), true);
        }

        if (jump2D.AllowChainJumps)
        {
            GUILayout.Space(10);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("chainJumps"), true);
        }


        serializedObject.ApplyModifiedProperties();
    }
    
    public float ReachedHeight
    {
        get
        {
            Jump2D jump2D = MyJump;
            if (jump2D.BaseJumpData == null) return 0;
            float h0 = 0;
            float v = jump2D.BaseJumpData.InitialVelocity;
            float g = Mathf.Abs(jump2D.BaseJumpData.BaseGravity);
            float t = Mathf.Abs(v / g);

            float hmax = h0 + v * t - ((0.5f * g) * Mathf.Pow(t, 2));

            return hmax;
        }
    }
    
    public Jump2D MyJump
    {
        get
        {
            return (Jump2D)target;
        }
    }
}
