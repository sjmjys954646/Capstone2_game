using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptyBlock : MonoBehaviour, IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField]
    private int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.setCursorObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.setCursorObject(null);
    }

    public int getIndex()
    {
        return index;
    }
}
