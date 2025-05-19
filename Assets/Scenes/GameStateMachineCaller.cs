using System.IO;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks; // UniTaskを使用するために必要
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachineCaller : MonoBehaviour
{
    [SerializeField] SceneReference _managerScene; // マネージャシーンの参照

    private void Start()
    {
        confirmManagerSceneLoad();
    }

    // マネージャシーンのキャッチ。
    private async void confirmManagerSceneLoad()
    {
        // sceneReferenceのassetPathからシーン名を抽出（拡張子を除く）
        string sceneName = Path.GetFileNameWithoutExtension(_managerScene.assetPath);

        // シーンがロードされるまで待機
        await WaitForSceneLoad(sceneName);

        // シーンがロードされたら、GameStateMachineを取得して処理を行う
        GameObject managerObject = GameObject.Find("GameStateMachine");
        if (managerObject != null)
        {
            SM_GameState gameStateMachine = managerObject.GetComponent<SM_GameState>();
            if (gameStateMachine != null)
            {
                Debug.Log("GameStateMachine found and ready to use!");
                // ここでGameStateMachineを使用した処理を追加可能
            }
            else
            {
                Debug.LogError("GameStateMachine component not found in the manager scene!");
            }
        }
        else
        {
            Debug.LogError("GameStateMachine object not found in the manager scene!");
        }
    }

    // シーンがロードされるのを待つ非同期メソッド
    private async UniTask WaitForSceneLoad(string sceneName)
    {
        while (true)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName && scene.isLoaded)
                {
                    return; // シーンがロードされたら終了
                }
            }
            // シーンが見つからない場合、1フレーム待機して再確認
            await UniTask.Yield();
        }
    }
}