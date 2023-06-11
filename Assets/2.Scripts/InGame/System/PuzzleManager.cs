using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    public int curpuzzleNum;
    [SerializeField]
    public List<GameObject> eachPuzzle = new List<GameObject>();
    [SerializeField]
    GameObject puzzleBackground;
    [SerializeField]
    public List<string> eachPuzzleTitle = new List<string>();
    [SerializeField]
    public List<string> eachPuzzleContent = new List<string>();
    [SerializeField]
    private TMP_Text chapterName;
    [SerializeField]
    private TMP_Text chapterContent;
    [SerializeField]
    private GameObject subject;


    /***********************************************************************
   *                               SingleTon
   ***********************************************************************/
    #region .
    private static PuzzleManager instance = null;

    public static PuzzleManager Instance
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

    public void PuzzleStart(int puzzleNum)
    {
        curpuzzleNum = puzzleNum;
        GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().MainCameraChange();
        GameManager.Instance.mainCam.orthographic = true;
        GameManager.Instance.isPuzzleGoing = true;
        puzzleBackground.SetActive(true);
        eachPuzzle[curpuzzleNum].SetActive(true);

        PuzzleTitleShow();

        if (GameManager.Instance.conversationGoing)
            ScenarioManager.Instance.ConversationEnd();

        //커서 조정
        if (!GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive)
        {
            GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive = !GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive;
            GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().ShowCursor(GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive);
        }
    }
        

    public void PuzzleEnd()
    {
        GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().CameraInitialize();
        GameManager.Instance.isPuzzleGoing = false;
        puzzleBackground.SetActive(false);
        eachPuzzle[curpuzzleNum].SetActive(false);

        if (GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive)
        {
            GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive = !GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive;
            GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().ShowCursor(GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive);
        }
    }

    private void PuzzleTitleShow()
    {
        subject.SetActive(true);
        chapterName.text = eachPuzzleTitle[curpuzzleNum];
        chapterContent.text = "[ " + eachPuzzleContent[curpuzzleNum] + " ]";
        StartCoroutine(TitleDisappear());
    }

    IEnumerator TitleDisappear()
    {
        yield return new WaitForSeconds(3f);
        chapterName.text = "";
        chapterContent.text = "";
        subject.SetActive(false);
    }
}
