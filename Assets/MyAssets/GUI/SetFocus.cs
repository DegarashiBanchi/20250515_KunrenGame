// EventSystemにオブジェクトをフォーカスさせるスクリプト。

using UnityEngine;
using UnityEngine.EventSystems;

public class SetFocusObject : MonoBehaviour
{
    // 現在のイベントシステム
    private EventSystem eventSystem;

    // ゲームオブジェクトを渡された時に、EventSystemのナビゲーションにセットするメソッド
    public void SetFocusOnObject(GameObject obj)
    {
        // eventSystemがNullであれば現在のEventSystemをセット
        if ((eventSystem ??= EventSystem.current) != null)
        {
            // 渡されたオブジェクトにフォーカスをセット。
            eventSystem.SetSelectedGameObject(obj);
        }
        else
        {
            Debug.LogWarning("EventSystem is not found.");
        }
    }
}