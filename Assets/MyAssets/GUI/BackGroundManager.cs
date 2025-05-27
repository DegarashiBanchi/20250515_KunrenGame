using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    // 定数として最大オフセット値を設定
    private const float MAX_OFFSET = 1f;
    // テクスチャのプロパティ名を定数として設定
    private const string PROPERTY_NAME = "_MainTex";

    // シリアライズフィールドとしてオフセット速度を設定
    [SerializeField] private Vector2 offsetSpeed;
    // マテリアルを保持するための変数
    private Material material;

    // Awakeメソッドはオブジェクトが初めてアクティブになったときに呼び出される
    private void Awake()
    {
        // Imageコンポーネントを取得し、マテリアルを設定
        if (TryGetComponent(out Image image))
        {
            material = image.material;
        }
    }

    // Updateメソッドは毎フレーム呼び出される
    private void Update()
    {
        // マテリアルが存在する場合にオフセットを計算して設定
        if (material != null)
        {
            // Mathf.Repeatを使用して時間に基づくオフセットを計算
            var offset = new Vector2(
                Mathf.Repeat(Time.time * offsetSpeed.x, MAX_OFFSET),
                Mathf.Repeat(Time.time * offsetSpeed.y, MAX_OFFSET)
            );
            // 計算したオフセットをマテリアルに設定
            material.SetTextureOffset(PROPERTY_NAME, offset);
        }
    }

    // OnDestroyメソッドはオブジェクトが破棄されるときに呼び出される
    private void OnDestroy()
    {
        // マテリアルが存在する場合にオフセットをリセット
        if (material != null)
        {
            material.SetTextureOffset(PROPERTY_NAME, Vector2.zero);
        }
    }
}