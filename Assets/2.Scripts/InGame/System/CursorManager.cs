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
        if(GameManager.Instance.isPuzzleGoing)
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
            if (PuzzleManager.Instance.eachPuzzle[0].GetComponent<PuzzleEachManager>().answerCompare(
                onCursorObject.GetComponent<EmptyBlock>().getIndex(),
                tmp.GetComponent<PuzzleBlock>().getIndex()
                ))
            {
                tmp.transform.position = onCursorObject.transform.position;
                tmp.GetComponent<PuzzleBlock>().finished = true;
            }
        }
        else if (onCursorObject.tag == "EmptyBlock2")
        {
            if (PuzzleManager.Instance.eachPuzzle[1].GetComponent<PuzzleEachManager>().answerComparePattern2(
                onCursorObject.GetComponent<EmptyBlock>().getIndex(),
                tmp.GetComponent<PuzzleBlock>().getIndex()
                ))
                tmp.GetComponent<PuzzleBlock>().finished = true;

            tmp.transform.position = onCursorObject.transform.position;
        }

        yield return null;
    }

}
