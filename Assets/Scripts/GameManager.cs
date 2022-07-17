using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int nextLevel = 0;
    public int CurLevel
    {
        get { return curLevel; }
    }

    private int curLevel = 0;
    private int maxLevels = 3; //всего 4 уровня: 0-4

    public void LoadNextLevel()
    {
        if (nextLevel <= maxLevels)
            SceneManager.LoadScene(nextLevel);
        else
        {
            //игра завершена, начинаем заново
            curLevel = 0;
            SceneManager.LoadScene(curLevel);
        }
    }
}
