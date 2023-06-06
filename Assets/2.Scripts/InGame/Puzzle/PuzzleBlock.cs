using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleBlock : MonoBehaviour, IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField]
    private int index;
    [SerializeField]
    private Vector3 initialPos;
    [SerializeField]
    public bool finished;


    private void Start()
    {
        initialPos = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.setCursorObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.setCursorObject(null);
    }

    public void SetPositionInit()
    {
        gameObject.transform.position = initialPos;
    }

    public int getIndex()
    {
        return index;
    }

    public Vector3 getInitialPos()
    {
        return initialPos;
    }
}
