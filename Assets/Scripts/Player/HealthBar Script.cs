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

    // ADDED BY CAMERON
    private ThirdPersonCamera cam;

    movement movement;

    void Start()
    {
        _currentHP = _MaxHP;
        _HpText.text = " Health: " + _currentHP;
        cam = Camera.main.GetComponent<ThirdPersonCamera>();
        movement = GetComponent<movement>();
    }
    private void Update()
    {
        if ( _currentHP <= 0)
        {
            Debug.Log("You are Dead");
            SceneManager.LoadScene("MainScene");
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
    public void TakeDamage(float damage)
    {
        if (!movement.isDodging)
        {
            UpdatingHP(-damage);
            Debug.Log("Player took damage");
        }
        
        if (cam != null)
        {
            cam.Shake(0.2f, 0.15f);
        }
    }

    public void HealHealth(float heal)
    {
        Mathf.Clamp(heal, 0f, _MaxHP);
        UpdatingHP(heal);
    }
}
