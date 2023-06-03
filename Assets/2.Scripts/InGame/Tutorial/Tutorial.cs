using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject[,] grounds = new GameObject[7, 7];

    public GameObject whiteTile;
    public GameObject blackTile;
    public GameObject groundTiles;
    public GameObject guideLine;
    public GameObject endPortal;

    public TMP_Text guideLineText;
    public List<string> guideText;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if(gameManager.isTutorial)
        {
            if(gameManager.player.transform.position.y <= -10)
            {
                playerReposition();
            }
        }
    }

    private void playerReposition()
    {
        Vector3 initPos = new Vector3(0, 1, 0);
        gameManager.player.transform.position = initPos;
    }

    public void TutorialStart()
    {
        Vector3 spawnPos = new Vector3(0, 1, 0);
        GameObject player = Instantiate(gameManager.characters[gameManager.playerAssetNum], spawnPos, Quaternion.identity);
        gameManager.isPlayerExist = true;
        gameManager.isTutorial = true;
        gameManager.player = player;

        gameManager.player.GetComponent<PlayerMove_Rito_Follow>().CameraViewToggleInside();
        gameManager.player.GetComponent<PlayerMove_Rito_Follow>().CameraViewToggleInside();

        TutorialGroundMaker();
        GuideStart();
    }

    private void GuideStart()
    {
        guideLine.SetActive(true);

        for (int i = 0;i< guideText.Count ;i++)
        {
            StartCoroutine(ShowNext(i, i * 5.0f));
        }

        //Æ©Åä¸®¾ó Æ÷Å» position 0 0 20
        //StartCoroutine(makePortal((guideText.Count - 1) * 5.0f + 1.0f));
    }

    private void TutorialGroundMaker()
    {
        for(int i = 0; i<7;i++)
        {
            for(int j = 0;j<7 ;j++)
            {
                Vector3 pos = new Vector3(-30 + 10 * i , 0, -30 + 10 * j);
                if ( (i+j)%2 == 1)
                {
                    GameObject ground = Instantiate(blackTile, pos, Quaternion.identity);
                    grounds[i,j] = ground;
                    ground.transform.parent = groundTiles.transform;
                }
                else
                {
                    GameObject ground = Instantiate(whiteTile, pos, Quaternion.identity);
                    grounds[i, j] = ground;
                    ground.transform.parent = groundTiles.transform;
                }
                
            }
        }
    }

    IEnumerator ShowNext(int guideIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        guideLineText.text = guideText[guideIndex];
    }

    IEnumerator makePortal(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        endPortal.SetActive(true);
    }

}
