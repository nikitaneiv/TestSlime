using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountDamage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        _text.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }

    public void SetDamageValue(int currentDamage)
    {
        _text.gameObject.SetActive(true);
        _text.text = currentDamage.ToString();
        _text.DOFade(0f, 0.5f).OnComplete(() => AnimationText());
    }
    
    private void AnimationText()
    {
        _text.gameObject.SetActive(false);
        _text.DOFade(1f, 0.1f);
    }
}