using UnityEngine;
public class LambdaExample2 : MonoBehaviour
{
    System.Action _jump;
    void Start()
    {
        _jump = () => Debug.Log("ジャンプ!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump();
        }
    }
}