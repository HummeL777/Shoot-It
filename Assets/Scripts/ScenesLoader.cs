using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime;

    public void LoadLevel(int levelToLoad)
    {
        StartCoroutine(LevelLoader(levelToLoad));
    }

    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LevelLoader(currentLevel + 1));
    }

    private IEnumerator LevelLoader(int level_id)
    {
        _transition.SetBool("LevelLoading", true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(level_id);
    }
}
