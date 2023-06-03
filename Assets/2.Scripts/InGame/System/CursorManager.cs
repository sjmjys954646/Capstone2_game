using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private bool isCarrying = false;
    [SerializeField]
    private GameObject carryingObject;
    [SerializeField]
    private GameObject onCursorObject;
    [SerializeField]
    private GameObject puzzle0Manager;

    /***********************************************************************
   *                               SingleTon
   ***********************************************************************/
    #region .
    private static CursorManager instance = null;

    public static CursorManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(!GameManager.Instance.isPuzzleGoing)
        {
            if(!isCarrying && Input.GetMouseButtonDown(0))
            {
                if (onCursorObject == null)
                    return;

                if (onCursorObject.tag == "Block")
                {
                    isCarrying = true;
                    carryingObject = onCursorObject;
                }
            }

            if (isCarrying && Input.GetMouseButtonUp(0))
            {
                isCarrying = false;
                carryingObject.GetComponent<PuzzleBlock>().SetPositionInit();

                GameObject tmp = carryingObject;
                StartCoroutine(checkAnswer(tmp));

                carryingObject = null;
            }

            if (isCarrying)
                carryingObject.transform.position = Input.mousePosition;
        }
    }

    public void setCursorObject(GameObject obj)
    {
        onCursorObject = obj;
    }

    public bool IsCarrying()
    {
        return isCarrying;
    }

    IEnumerator checkAnswer(GameObject tmp)
    {
        yield return new WaitForSeconds(0.1f);

        if(onCursorObject == null)
            yield break;

        if (onCursorObject.tag == "EmptyBlock")
        {
            //정답비교
            if (puzzle0Manager.GetComponent<Puzzle0Manager>().answerCompare(
                onCursorObject.GetComponent<EmptyBlock>().getIndex(),
                tmp.GetComponent<PuzzleBlock>().getIndex()
                ))
            {
                tmp.transform.position = onCursorObject.transform.position;
                tmp.GetComponent<PuzzleBlock>().finished = true;
                Debug.Log("heygho");
            }
        }
        yield return null;
    }

}
