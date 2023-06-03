using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    public int curpuzzleNum;
    [SerializeField]
    List<GameObject> eachPuzzle = new List<GameObject>();
    [SerializeField]
    GameObject puzzleBackground;



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


        //Ŀ�� ����
        if(!GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().State.isCursorActive)
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
}
