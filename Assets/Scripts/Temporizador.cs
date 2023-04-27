using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Temporizador : MonoBehaviour
{
    [SerializeField] float tempoMaximo;
    private float tempoAtual;
    private Slider sliderCp;
    // Start is called before the first frame update
    void Start()
    {
        tempoAtual = 0f;
        sliderCp = GetComponent<Slider>();
        sliderCp.maxValue = tempoMaximo;
        sliderCp.value = tempoAtual;
    }

    // Update is called once per frame
    void Update()
    {
        tempoAtual += 1 * Time.deltaTime;
        if (tempoAtual > tempoMaximo)
        {
            Debug.Log("acabou o tempo");
            tempoAtual = 0f;
        }
        sliderCp.value = tempoAtual;
    }
}
