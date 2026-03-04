using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private float _playerScore;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerScore = 0;
        _ScoreText.text = " Score: " + _playerScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatingHP(float amount)
    {
        _playerScore += amount;
        _playerScore = Mathf.Clamp(_playerScore, 0f, 0f);
        _ScoreText.text = " Score: " + _playerScore;
    }
}
