using DG.Tweening;
using Keiwando.BigInteger;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private CharacterType _target;
    [SerializeField] private bool _isCameraShake;
    [SerializeField] private bool _isApplyKnockback;
    [SerializeField] private float _knockbackPower;
    [SerializeField] private float _knockbackDuration;

    private List<PixelCharacter> _targetList = new List<PixelCharacter>();
    private string _targetTag;
    private BigInteger _power;
    private Camera _camera;

    private void Awake()
    {
        _targetTag = _target.ToStringEx();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _targetList.Clear();
    }

    public void SetPower(BigInteger power)
    {
        _power = power;
    }

    public void Attack(BigInteger power)
    {
        _power = power;
        Attack();
    }

    public void Attack()
    {
        if (_targetList.Count == 0) return;

        List<PixelCharacter> killedTargetList = new List<PixelCharacter>();

        for (int i = 0;  i < _targetList.Count; i++)
        {
            _targetList[i].HealthSystem.ChangeHealth(-_power);
            if (_isApplyKnockback)
            {
                _targetList[i].Controller.ApplyKnockback(transform, _knockbackPower, _knockbackDuration);
            }
            if (_targetList[i].HealthSystem.IsDead) killedTargetList.Add(_targetList[i]);
        }

        for (int i =0; i < killedTargetList.Count; i++)
        {
            _targetList.Remove(killedTargetList[i]);
        }

        if (_isCameraShake) _camera.DOShakePosition(0.1f, new Vector3(0.1f, 0.1f, 0.1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_targetTag)) return;
        _targetList.Add(collision.GetComponent<PixelCharacter>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(_targetTag)) return;
        PixelCharacter character = collision.GetComponent<PixelCharacter>();
        if (_targetList.Contains(character)) _targetList.Remove(character);
    }
}
