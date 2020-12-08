using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MovCano : MonoBehaviour
{
    
    public GameObject _spawn;
    public float _speed = .01f;
    Rigidbody2D rbd;
    bool seen = false;
    public float _velocidadeSubirDescer;
    MovSapo _sapo;
    bool mov = false;
    public float porcentagemMov = 70;
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        //_sapo = GameObject.Find("Sapo").GetComponent<MovSapo>();

        //if (_sapo.pontos < 10)
        //{
        porcentagemMov = 30;
        //}
        //else
        //{
        //    porcentagemMov = 50;
        //}
        //if (_sapo.pontos >= 5)
        //{
        if (Random.Range(1, 10) <= porcentagemMov / 10)
        {
            mov = true;
        }
        //}
        rbd.velocity = UnityEngine.Vector3.left * _speed;
    }
    void Update()
    {
        if (mov && gameObject.CompareTag("Pinga") == false)
        {
            UnityEngine.Vector2 posicao = transform.position;
            if (posicao.y < _spawn.transform.position.y - 1.76f)
            {
                if (_velocidadeSubirDescer < 0)
                {
                    _velocidadeSubirDescer = -_velocidadeSubirDescer;
                }
            }
            else if (posicao.y > _spawn.transform.position.y + 1.5f == true)
            {
                if (_velocidadeSubirDescer > 0)
                {
                    _velocidadeSubirDescer = -_velocidadeSubirDescer;
                }
            }
            posicao.y += _velocidadeSubirDescer * Time.deltaTime;
            transform.position = posicao;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fim"))
        {
            Destroy(gameObject);
        }
    }
}
