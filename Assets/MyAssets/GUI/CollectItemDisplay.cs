// ワイプ画面におけるアイテムの取得状況表示を管理するスクリプト。

using R3;
using UnityEngine;

public class CollectItemDisplay : MonoBehaviour
{
    [SerializeField] SO_OpenStatus _openStatus; // プレイヤーのステータスを管理するScriptableObject
    [Header("アイテムアイコンの設定")]
    [SerializeField] SpriteRenderer _itemIcon1; // アイテム1のアイコン
    [SerializeField] SpriteRenderer _itemIcon2; // アイテム2のアイコン
    [SerializeField] SpriteRenderer _itemIcon3; // アイテム3のアイコン
    [SerializeField] SpriteRenderer _keyItemIcon; // キーアイテムのアイコン
    
    private void Start()
    {
        //_openStatusの各アイテム取得状況に応じてアイコンのColorを設定
        _openStatus._hasItem1.Subscribe(isGet =>
        {
            ChangeIconColor(_itemIcon1, isGet);
        });
        _openStatus._hasItem2.Subscribe(isGet =>
        {
            ChangeIconColor(_itemIcon2, isGet);
        });
        _openStatus._hasItem3.Subscribe(isGet =>
        {
            ChangeIconColor(_itemIcon3, isGet);
        });
        _openStatus._hasKeyItem.Subscribe(isGet =>
        {
            ChangeIconColor(_keyItemIcon, isGet);
        });
    }

    // 各アイコンのColorを切り替えるメソッド。
    public void SetItemIconColor(int itemIndex , bool isGet)
    {
        switch (itemIndex)
        {
            case 1:
                ChangeIconColor(_itemIcon1, isGet);
                break;
            case 2:
                ChangeIconColor(_itemIcon2, isGet);
                break;
            case 3:
                ChangeIconColor(_itemIcon3, isGet);
                break;
            case 4:
                ChangeIconColor(_keyItemIcon, isGet);
                break;
            default:
                Debug.LogError("無効なアイテムインデックス: " + itemIndex);
                break;
        }
    }

    // アイコンのカラーを変更するメソッド。
    private void ChangeIconColor(SpriteRenderer icon, bool isGet)
    {
        if (icon == null)
        {
            Debug.LogError("アイコンが設定されていません。");
            return;
        }
        if (isGet)
        {
            // アイテムを取得済みの場合は、アイコンの色を白に設定
            icon.color = Color.white;
        }
        else
        {
            // 未取得の場合は、アイコンの色を黒でアルファ値150（半透明）に設定
            icon.color = new Color(0, 0, 0, 150f / 255f); // 黒色でアルファ値150
        }
    }
}
