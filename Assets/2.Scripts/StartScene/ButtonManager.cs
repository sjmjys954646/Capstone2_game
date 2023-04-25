using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject InitButton;

    public GameObject singlePlay;

    // Start is called before the first frame update
    void Start()
    {
        InitButton.SetActive(true);

        singlePlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickSInglePlayerButton()
    {
        InitButton.SetActive(false);

        singlePlay.SetActive(true);
    }

    public void onClickReturnButton()
    {
        singlePlay.SetActive(false);

        InitButton.SetActive(true);
    }

    public void onClickStartNewButton()
    {
        SceneManager.LoadScene("InGameScene");
    }
}
