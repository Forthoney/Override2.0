using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSet : MonoBehaviour
{
    public enum CursorType
    {
        Arrow,
        Crosshair
    }

    public CursorType cType = CursorType.Crosshair;

    public Texture2D ArrowCursor;
    public Texture2D CrosshairCursor;

    // Start is called before the first frame update
    void Start()
    {
        if (cType == CursorType.Arrow)
            SetCursorArrow();
        else
            SetCursorCrosshair();
    }

    public void SetCursorArrow()
    {
        Vector2 cursorOffset = new Vector2(ArrowCursor.width / 2, ArrowCursor.width / 2);
        Cursor.SetCursor(ArrowCursor, cursorOffset, CursorMode.Auto);
    }

    public void SetCursorCrosshair()
    {
        Vector2 cursorOffset = new Vector2(CrosshairCursor.width / 2, CrosshairCursor.height / 2);
        Cursor.SetCursor(CrosshairCursor, cursorOffset, CursorMode.Auto);
    }
}
