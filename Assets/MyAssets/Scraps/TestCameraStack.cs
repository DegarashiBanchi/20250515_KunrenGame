using UnityEngine;

public class TestCameraStack : MonoBehaviour
{
    Camera camera; // カメラを保持するための変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ////メラコンポーネントを取得
        //camera = GetComponent<Camera>();
        //if (camera == null)
        //{
        //    Debug.LogError("Camera component not found on this GameObject.");
        //    return;
        //}
        //// カメラのスタックを設定
        //camera.cameraStack.Clear();
        //camera.cameraStack.Add(camera);

        //// ここで他のカメラを追加することも可能
        //// 例: camera.cameraStack.Add(otherCamera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
