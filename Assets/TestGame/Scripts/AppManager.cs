using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private Image _win;
    [SerializeField] private Image _lose;

    public void ShowPanel(PanelType type)
    {
        _container.gameObject.SetActive(true);

        if (type == PanelType.Win)
        {
            _win.gameObject.SetActive(true);

            var player = FindObjectOfType<PlayerComponent>();
            player.SetAnimationByName("idle");
        }
        else
        {
            _lose.gameObject.SetActive(true);

            var enemies = FindObjectsOfType<EnemyComponent>();

            foreach (var enemy in enemies)
            {
                enemy.SetAnimationByName("win");
            }
        }
    }

    public void LevelComplete(GameObject go)
    {
        ShowPanel(PanelType.Win);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum PanelType
{ 
    Win,
    Lose
}
