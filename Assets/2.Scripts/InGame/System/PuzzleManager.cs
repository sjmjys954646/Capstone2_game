using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    public int puzzleNum;


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

    public void PuzzleStart()
    {
        GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().MainCameraChange();
        GameManager.Instance.mainCam.orthographic = true;
    }

    public void PuzzleEnd()
    {
        GameManager.Instance.player.GetComponent<PlayerMove_Rito_Follow>().CameraInitialize();
    }
}
