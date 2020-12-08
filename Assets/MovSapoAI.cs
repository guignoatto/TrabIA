using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MovSapoAI : Agent
{
    [SerializeField] private GameObject _geraCanos;
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
    Quaternion resetRotation;

    MovSapo _sapo;

    public override void Initialize()
    {
        resetRotation = transform.rotation;
        rbd = GetComponent<Rigidbody2D>();
        c = GetComponent<CircleCollider2D>();
        pontosText = GameObject.Find("Pontos").GetComponent<UnityEngine.UI.Text>();
        pontosPingaText = GameObject.Find("Pontos Pinga").GetComponent<UnityEngine.UI.Text>();
        timeSlerp = 0;
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void OnEpisodeBegin()
    {
        ResetEnviroment();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rotacao = transform.rotation;
        rotacao.z = -.3f;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, timeSlerp);
        timeSlerp += Time.deltaTime / 20;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetMouseButtonDown(0))
        {
            actionsOut[0] = 1;
        }
    }

    private void Jump()
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

    private void ResetEnviroment()
    {
        dead = false;
        transform.localPosition = new Vector3(-5, 1, 1);
        transform.rotation = resetRotation;
        Transform[] canos = _geraCanos.GetComponentsInChildren<Transform>();

        foreach (Transform c in canos)
        {
            if (c.gameObject != _geraCanos)
                Destroy(c.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.gameObject.CompareTag("Cano"))
        {
            AddReward(-10);
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("Ponto"))
        {
            pontos += 1;
            pontosText.text = "Pontos: " + pontos;
            AddReward(2f);
        }
        else if (collision.gameObject.CompareTag("Pinga"))
        {
            pontosPinga += 1;
            pontosPingaText.text = "Pontos pinga: " + pontosPinga;
            Destroy(collision.gameObject);
            AddReward(2f);
        }
    }
}


