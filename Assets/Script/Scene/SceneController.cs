using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン切り替えクラス
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// ロードするシーン
    /// </summary>
    [SerializeField] private string _loadScene;

    /// <summary>
    /// 遅延させたい秒数
    /// </summary>
    public int _delay;

    /// <summary>
    /// ライムラグ
    /// </summary>
    public void TimeLag()
    {
        Invoke("SceneChange", _delay);
    }

    /// <summary>
    /// シーン変更
    /// </summary>
    public void SceneChange()
    {
        SceneManager.LoadScene(_loadScene);
    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }
}
