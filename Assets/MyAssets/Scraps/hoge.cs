using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class hoge : MonoBehaviour
{
    void Start()
    {
        //  ハッシュセットの生成
        HashSet<int> t = new HashSet<int>();
        //  データの追加
        t.Add(1);
        t.Add(2);
        t.Add(3);
        t.Add(1);
        //  データの出力
        foreach (int i in t)
        {
            Console.WriteLine("{0}", i);
        }
    }

  
}
