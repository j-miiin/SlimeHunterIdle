using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_SummonResultSlot : UI_Base
{
    [SerializeField] private GameObject _resultSlotObject;
    [SerializeField] private Image _itemIconImage;
    [SerializeField] private Image _gradeIconImage;
    [SerializeField] private Text _quantityText;
    [SerializeField] private GameObject[] _cardParticles;
    [SerializeField] private GameObject[] _showParticles;

    private int _gradeAIdx = 0;
    private int _gradeSIdx = 1;
    private int _gradeSSIdx = 2;
    private WaitForSeconds _effectDelay = new WaitForSeconds(0.1f);
    private WaitForSeconds _resultDelay = new WaitForSeconds(0.1f);

    private void OnEnable()
    {
        CloseUI(UIAnimationType.None, false);
    }

    public void SetResultSlot(Sprite itemIcon, Sprite gradeIcon, int quantity, GradeType grade)
    {
        _itemIconImage.sprite = itemIcon;
        _gradeIconImage.sprite = gradeIcon;
        _quantityText.text = $"x {quantity}";


        if (grade >= GradeType.A)
        {
            _resultSlotObject.SetActive(false);
            StartCoroutine(COShowGradeEffect(grade));
        }
        else
        {
            _resultSlotObject.SetActive(true);
            for (int i = 0; i < _cardParticles.Length; i++) _cardParticles[i].SetActive(false);
            for (int i = 0; i < _showParticles.Length; i++) _showParticles[i].SetActive(false);
        }
    }

    private IEnumerator COShowGradeEffect(GradeType grade)
    {
        yield return _effectDelay;
        _showParticles[_gradeAIdx].SetActive(grade == GradeType.A);
        _showParticles[_gradeSIdx].SetActive(grade == GradeType.S);
        _showParticles[_gradeSSIdx].SetActive(grade == GradeType.SS);
        yield return _resultDelay;
        //_resultSlotObject.FadeIn();
        _resultSlotObject.SetActive(true);
        _cardParticles[_gradeAIdx].SetActive(grade == GradeType.A);
        _cardParticles[_gradeSIdx].SetActive(grade == GradeType.S);
        _cardParticles[_gradeSSIdx].SetActive(grade == GradeType.SS);
    }
}
