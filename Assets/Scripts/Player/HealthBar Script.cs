//using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    //Done By Isaiah Ragland

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
        //cam = Camera.main.GetComponent<ThirdPersonCamera>();
        movement = GetComponent<movement>();
    }
    private void Update()
    {
        if ( _currentHP <= 0)
        {
            Debug.Log("You are Dead");
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
        //animation for healthBar
        //_healthBarFill.DOFillAmount(targetFillamount, _fillSpeed);
        //gradient
        _healthBarFill.color= _colored.Evaluate(targetFillamount);
    }

    // ADDED BY CAMERON
    public void TakeDamage(float damage, GameObject source)
    {
        if (!movement.isDodging)
        {
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
