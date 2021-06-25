using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatboxScriptHelper : MonoBehaviour
{
    public static void UpdateUiTextboxPos(GameObject objToAttachTo, float yPosCanvas, RectTransform canvasRect, RectTransform uiObjRt)
    {
        // Offset position above object box (in world space)
        float offsetPosY = objToAttachTo.transform.position.y + yPosCanvas;

        // Final position of marker in world space
        Vector3 offsetPos = new Vector3(objToAttachTo.transform.position.x, offsetPosY, objToAttachTo.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Set position of ui element we want to show
        uiObjRt.localPosition = canvasPos;
    }
}
