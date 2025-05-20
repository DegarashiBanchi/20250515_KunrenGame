// ステータスの表示を管理するスクリプト。

using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField] SO_OpenStatus openStatus; // ステータスのScriptableObject

    // debug
    [SerializeField] TMP_Text HP; // HPの表示用TextMeshProUGUI


    private void Start()
    {
        // openStatus._currentHPを監視し、増減があればHPを更新する。
        //openStatus._currentHP.Subscribe((int hp) =>
        //{
        //    HP.text = hp.ToString();
        //});

    }


    // HPの表示更新。
    public void UpdateHP()
    {
        // ステータスのHPを取得
       
    }

}