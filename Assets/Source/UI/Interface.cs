using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    [SerializeField] private ChoiseUnitPanel _choiseUnitPanel;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private PlayerTower _playerTower;
    private EnemyTower _enemyTower;

    public void Init(PlayerData playerData)
    {
        _choiseUnitPanel.Init(playerData);

        _playerTower = FindObjectOfType<PlayerTower>();
        _enemyTower = FindObjectOfType<EnemyTower>();

        _playerTower.Destroyed += Lose;
        _enemyTower.Destroyed += Win;
    }

    private void OnDestroy()
    {
        _playerTower.Destroyed -= Lose;
        _enemyTower.Destroyed -= Win;
    }

    private void Lose()
    {
        _choiseUnitPanel.gameObject.SetActive(false);
        _losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Win()
    {
        _choiseUnitPanel.gameObject.SetActive(false);
        _winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
