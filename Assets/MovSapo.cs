using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovSapo : MonoBehaviour
{
    Rigidbody2D rbd;
    CircleCollider2D c;
    SpriteRenderer sprite;
    public float force = 1, timeSlerp = 0;
    bool dead = false;
    Quaternion rotacao;
    UnityEngine.UI.Text pontosText, pontosPingaText;
    public int pontos, pontosPinga;
    public GameObject botaoReset;
    public Sprite sapoBem, sapoMal;

    MovSapo _sapo;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        c = GetComponent<CircleCollider2D>();
        pontosText = GameObject.Find("Pontos").GetComponent<UnityEngine.UI.Text>();
        pontosPingaText = GameObject.Find("Pontos Pinga").GetComponent<UnityEngine.UI.Text>();
        timeSlerp = 0;
        sprite = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        rotacao = transform.rotation;
        rotacao.z = -.3f;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, timeSlerp);
        timeSlerp += Time.deltaTime / 20;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!dead)
            {
                rbd.velocity = Vector2.zero;
                rbd.AddForce(Vector2.up * force);
                Quaternion rot = transform.rotation;
                rot.z = .3f;
                transform.rotation = rot;
                timeSlerp = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (sprite.sprite == sapoBem)
            {
                sprite.sprite = sapoMal;

            }
            else
            {
                sprite.sprite = sapoBem;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Cano"))
        {
            dead = true;
            c.gameObject.layer = 8;
            rotacao = transform.rotation;
            rotacao.z = -.3f;
            transform.rotation = rotacao;
            botaoReset.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ponto"))
        {
            pontos += 1;
            pontosText.text = "Pontos: " + pontos;
        }
        else if (collision.gameObject.CompareTag("Pinga"))
        {
            pontosPinga += 1;
            pontosPingaText.text = "Pontos pinga: " + pontosPinga;
            Destroy(collision.gameObject);
        }
    }
}
