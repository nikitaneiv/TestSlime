using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public void LoadLevel(int _sceneNumber)
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}
