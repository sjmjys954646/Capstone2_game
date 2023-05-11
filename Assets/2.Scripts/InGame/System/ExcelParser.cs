using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using TMPro;


public class CSVAssetReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(TextAsset file) // string -> TextAsset  으로 변경. 파일 링크로 읽기
    {
        var list = new List<Dictionary<string, object>>();
        //TextAsset data = Resources.Load(file) as TextAsset 를 삭제.file 자체가 TextAsset이기 때문

        var lines = Regex.Split(file.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}



public class ExcelParser : MonoBehaviour
{
    /***********************************************************************
    *                               SingleTon
    ***********************************************************************/
    #region .
    private static ExcelParser instance = null;

    public static ExcelParser Instance
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


    [Header("로드할 TextAsset 데이터를 넣어주세요")]
    public TextAsset dialogueText;
    public TextAsset itemText;

    public List<Dictionary<string, object>> data;

    public List<List<Dictionary<string, object>>> dialogue;
    public List<List<Dictionary<string, object>>> itemList;

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


        DialogueTextLoad();
        makeDialogue();

        ItemTextLoad();
        makeItemList();

        //Debug.Log(itemList[0][0]["ItemName"].ToString());
    }


    public void DialogueTextLoad()
    {
        dialogueText.text.Substring(0, dialogueText.text.Length - 1);
        data = CSVAssetReader.Read(dialogueText); 
    }

    public void ItemTextLoad()
    {
        itemText.text.Substring(0, itemText.text.Length - 1);
        data = CSVAssetReader.Read(itemText);
    }

    public void makeDialogue()
    {
        int finalNum = Convert.ToInt32(data[data.Count - 1]["EventNumber"].ToString());

        dialogue = new List<List<Dictionary<string, object>>>(finalNum + 1);

        for(int i = 0;i < finalNum + 1;i++)
        {
            List<Dictionary<string, object>> p = new List<Dictionary<string, object>>();
            dialogue.Add(p); 
        }

        for (int i = 0; i < data.Count; i++)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>() { 
                { "Talker", data[i]["Talker"].ToString() },
                { "Conversation", data[i]["Conversation"].ToString() },
            };
            //Debug.Log(Convert.ToInt32(data[i]["EventNumber"].ToString()));
            //Debug.Log(data[i]["Talker"].ToString());
            //Debug.Log(data[i]["Conversation"].ToString());
            dialogue[Convert.ToInt32(data[i]["EventNumber"].ToString())].Add(dic);
        }
    }

    public void makeItemList()
    {
        int finalNum = Convert.ToInt32(data[data.Count - 1]["ItemNumber"].ToString());

        itemList = new List<List<Dictionary<string, object>>>(finalNum + 1);

        for (int i = 0; i < finalNum + 1; i++)
        {
            List<Dictionary<string, object>> p = new List<Dictionary<string, object>>();
            itemList.Add(p);
        }

        for (int i = 0; i < data.Count; i++)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>() {
                { "ItemName", data[i]["ItemName"].ToString() },
                { "ItemDescription", data[i]["ItemDescription"].ToString() },
            };
            itemList[Convert.ToInt32(data[i]["ItemNumber"].ToString())].Add(dic);
        }
    }
}


