using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    public Texture2D defaultCursor;
    public Texture2D onClickCursor;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Cursor.SetCursor(onClickCursor, new Vector2(10, 10), CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, new Vector2(10, 10), CursorMode.ForceSoftware );
        }
    }
}
