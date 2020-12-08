using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraPinga : MonoBehaviour
{
    public float _tempoCano = 2.5f;
    public float _timer = 1.6f;
    public GameObject _pinga;
    void Start()
    {

    }
    void Update()
    {
        if (_timer > _tempoCano)
        {
            Vector3 posicao = transform.position;
            float y = Random.Range(posicao.y - 2.7f, posicao.y - 1.25f);
            posicao.y = y;
            GameObject pinga = Instantiate(_pinga, posicao, transform.localRotation, transform);
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
}
