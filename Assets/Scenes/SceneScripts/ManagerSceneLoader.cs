// 開始時にマネージャシーンをロードするスクリプト。

using System.IO;
using AnnulusGames.SceneSystem;
using UnityEngine;

public class ManagerSceneLoader : MonoBehaviour
{
    [SerializeField] private SceneReference managerSceneRef; // マネージャシーンの参照

    // マネージャシーンが未ロードなら Additive でロードする
    private void Start()
    {
        // managerSceneRefからパスを取得
        var path = Path.GetFileNameWithoutExtension(managerSceneRef.assetPath);

        // シーンが未ロードなら Additive でロード 
        if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(path).isLoaded)
        {
            // Additive ロードして待機
            Scenes.LoadSceneAsync(managerSceneRef, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log($"\"{path}\" は既にロード済みです。");
        }

    }
}
