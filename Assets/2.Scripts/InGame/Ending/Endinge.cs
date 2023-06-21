using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Endinge : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    bool finish = false;
    [SerializeField]
    GameObject Credit;

    private void Start()
    {
        StartCoroutine(WaitSeven());
    }

    // Update is called once per frame
    void Update()
    {
        Credit.transform.Translate(Vector3.up * Time.deltaTime * 250f);
    }

    private IEnumerator WaitSeven()
    {
        yield return new WaitForSeconds(7f);
        StartCoroutine(FadeTextToFullAlpha());
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");

    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
    }
}
