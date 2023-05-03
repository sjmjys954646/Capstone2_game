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

    [Header("로드할 TextAsset 데이터를 넣어주세요")]
    public TextAsset myTxt;

    public List<Dictionary<string, object>> data;

    public List<List<Dictionary<string, object>>> dialogue;

    private void Awake()
    {
        TextLoad();
        makeDialogue();
    }

    public void TextLoad()
    {
        myTxt.text.Substring(0, myTxt.text.Length - 1);
        data = CSVAssetReader.Read(myTxt); 
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
}


