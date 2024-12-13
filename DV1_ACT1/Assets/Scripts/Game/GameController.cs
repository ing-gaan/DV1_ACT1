
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("---------- Event buses")]
    [SerializeField] private GameEventBusScrObj _gameEventBusScrObj;
    //[SerializeField] private TimeEventBusScrObj _timeEventBus;

    [Header("---------- Game defaults")]
    [SerializeField] private Player _player;
    [SerializeField] private int _maxPlayerLives = 3;
    [SerializeField] private int _pointsToWin = 10;

    [Header("---------- UI texts")]
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _pointsText;


    private IEnumerator _oneLiveLostCoroutine;
    private IEnumerator _gameOverCoroutine;
    private IEnumerator _winCoroutine;

    private int _remainigPlayerLives;
    private int _enemyKills = 0;
    

    private void OnEnable()
    {
        _gameEventBusScrObj.OneLiveLostEvent += OneLiveLost;
        _gameEventBusScrObj.EnemyKillEvent += EnemyKill;
        
    }

    private void OnDisable()
    {
        _gameEventBusScrObj.OneLiveLostEvent -= OneLiveLost;
        _gameEventBusScrObj.EnemyKillEvent -= EnemyKill;
    }

    private void OneLiveLost()
    {
        _remainigPlayerLives--;
        _player.gameObject.SetActive(false);
        _livesText.text = _remainigPlayerLives.ToString();

        if (_remainigPlayerLives <= 0)
        {
            _gameOverCoroutine = GameOverCoroutine();
            StartCoroutine(_gameOverCoroutine);
            return;
        }
        _oneLiveLostCoroutine = PlayerDeadCoroutine();
        StartCoroutine(_oneLiveLostCoroutine);        
    }

    private void EnemyKill()
    {
        _enemyKills++;
        _pointsText.text = _enemyKills.ToString() + " / " + _pointsToWin.ToString();

        if(_enemyKills >= _pointsToWin)
        {
            _winCoroutine = WinCoroutine();
            StartCoroutine(_winCoroutine);
        }
    }



    private void Start()
    {
        _remainigPlayerLives = _maxPlayerLives;
        _livesText.text = _remainigPlayerLives.ToString();
        _pointsText.text = _enemyKills.ToString() + " / " + _pointsToWin.ToString();
    }

    

    private IEnumerator PlayerDeadCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        _player.gameObject.SetActive(true);
    }

    private IEnumerator WinCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("Win");

    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("GameOver");

    }
}
