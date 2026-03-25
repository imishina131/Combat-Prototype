// Combat Prototype
// Irina Mishina & Cameron Lee Czysz-Mille & Isaiah Ragland
// 2026-03-24
//using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public float _MaxHP = 100;
    public float _currentHP;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TextMeshProUGUI _HpText;
    //[SerializeField] private float _fillSpeed;
    [SerializeField] private Gradient _colored;

    public float knockbackStrength = 5f;
    Vector3 impact = Vector3.zero;
    private CharacterController character;
    float mass = 3.0f;

    movement movement;

    void Start()
    {
        character = GetComponent<CharacterController>();
        _currentHP = _MaxHP;
        _HpText.text = " Health: " + _currentHP;
        movement = GetComponent<movement>();
    }
    private void Update()
    {
        if ( _currentHP <= 0)
        {
            Debug.Log("You are Dead");
            Enemy._playerScore = 0;
            SceneManager.LoadScene("MainScene");
        }

        if (impact.magnitude > 0.2)
        {
            character.Move(impact * Time.deltaTime);

            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("healthPickup"))
        {
            HealHealth(10);
            Destroy(other.gameObject);
        }
    }

    public void UpdatingHP (float amount)
    {
        _currentHP += amount;
        _currentHP=Mathf.Clamp(_currentHP,0f, _MaxHP);
        _HpText.text = " Health: " + _currentHP;
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        float targetFillamount = _currentHP / _MaxHP;
        _healthBarFill.fillAmount = targetFillamount;
        _healthBarFill.color= _colored.Evaluate(targetFillamount);
    }

    // ADDED BY CAMERON
    public void TakeDamage(float damage, GameObject source)
    {
        if (!movement.isDodging)
        {
            //knockback and dodge by Irina
            Vector3 knockbackDirection = (gameObject.transform.position - source.transform.position).normalized;
            impact += knockbackDirection * knockbackStrength / mass;
            UpdatingHP(-damage);
            Debug.Log("Player took damage");
        }
    }

    public void HealHealth(float heal)
    {
        Mathf.Clamp(heal, 0f, _MaxHP);
        UpdatingHP(heal);
    }
}
