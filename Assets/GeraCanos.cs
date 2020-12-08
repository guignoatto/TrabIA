using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeraCanos : MonoBehaviour
{
    [SerializeField] public GameObject _spawn;
    [SerializeField] MovSapo _sapo;
    bool mov = false;
    float _timer = 2.6f;
    public GameObject _cano;
    public GameObject botaoReset;
    public float _tempoCano = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        //_spawn = GameObject.Find("CanoSpawn");
        //_sapo = GameObject.Find("Sapo").GetComponent<MovSapo>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicao = _spawn.transform.position;
        if (_timer > _tempoCano)
        {
            if (mov == false)
            {
                float y = Random.Range(posicao.y - 2.7f, posicao.y - 1.25f);
                posicao.y = y;
            }
            else
            {

            }
            MovCano m = Instantiate(_cano, posicao, _spawn.transform.localRotation, transform).GetComponent<MovCano>();
            m._spawn = this._spawn;
            _timer = 0;
        }
        _timer += Time.deltaTime;
        //-1.76f // 1.5f


    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Jogo");
    }
}
