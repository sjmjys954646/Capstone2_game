using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameManager gameManager;
    private ScenarioManager scenarioManager;
    public GameObject characterSelectButtons;
    public GameObject characterSelectGameObject;
    public GameObject selectGuide;

    //0 cube
    //1 sphere
    //2 capsule

    [SerializeField]
    private List<GameObject> characters = new List<GameObject>();
    [SerializeField]
    private List<Vector3> pos = new List<Vector3>();
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private int middle = 0;

    private void Start()
    {
        scenarioManager = ScenarioManager.Instance;
    }

    public void CharacterSelectStart()
    {
        characterSelectButtons.SetActive(true);
        characterSelectGameObject.SetActive(true);
        selectGuide.SetActive(true);

        StartCoroutine(CoroutineOneSecond());

        pos.Add(new Vector3(0f, 0f, 0f));
        pos.Add(new Vector3(6f, 0f, 6f));
        pos.Add(new Vector3(-6f, 0f, 6f));

        charactersDisable();
    }

    public void characterSelectEnd()
    {
        gameManager.playerAssetNum = characters[middle].GetComponent<IngameSelect>().index;
        // 줌인하고 튜토리얼 시작
        characterSelectButtons.SetActive(false);
        characterSelectGameObject.SetActive(false);
        selectGuide.SetActive(false);

        scenarioManager.TutorialStart();
    }

    private void charactersDisable()
    {
        //이부분 자유롭게 변경
        characters[0].SetActive(true);
        characters[1].SetActive(true);
        characters[2].SetActive(true);
    }
    public void LeftPressed()
    {
        for (int i = 0; i < 3; i++)
            characterMoveLeft(i);
        characters.Insert(0, characters[2]);
        characters.RemoveAt(3);
        charactersDisable();
    }

    public void RightPressed()
    {
        for (int i = 0; i < 3; i++)
            characterMoveRight(i);
        characters.Add(characters[0]);
        characters.RemoveAt(0);
        charactersDisable();
    }

    private void characterMoveLeft(int characterIdx)
    {
        Vector3 characterAfterPos = pos[(characterIdx + 1) % 3];
        characters[characterIdx].transform.position = Vector3.Lerp(transform.position, characterAfterPos, speed);
    }

    private void characterMoveRight(int characterIdx)
    {
        if (characterIdx == 0)
            characterIdx = 3;
        Vector3 characterAfterPos = pos[(characterIdx - 1)];
        if (characterIdx == 3)
            characterIdx = 0;
        characters[characterIdx].transform.position = Vector3.Lerp(transform.position, characterAfterPos, speed);
    }

    IEnumerator CoroutineOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
