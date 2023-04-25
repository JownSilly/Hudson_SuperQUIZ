using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PerguntasSO perguntaAtual;
    [SerializeField] private TextMeshProUGUI enunciadoTexto;
    [SerializeField] private GameObject[] alternativaTMP;
    [Header ("sprites")]
    [SerializeField] private Sprite SpriteCorreta;
    [SerializeField] private Sprite SpriteIncorreta;

    void Start()
    {
        enunciadoTexto.SetText(perguntaAtual.GetEnunciado());
        string[] alternativas = perguntaAtual.GetAlternativas();
        for(int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alternativa = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alternativa.SetText(alternativas[i]);
        }
    }
    public void HandleOption(int alternativaSelecionada)
    {
        Image imageAlt = alternativaTMP[alternativaSelecionada].GetComponent<Image>();
        
        if (alternativaSelecionada == perguntaAtual.GetAlternativaCorreta())
        {
            ChangeButtonSprite(imageAlt, SpriteCorreta);
            DisableOptionButtons();
        }
        else
        {
            ChangeButtonSprite(imageAlt, SpriteIncorreta);
            DisableOptionButtons();
            ChangeButtonSprite(alternativaTMP[perguntaAtual.GetAlternativaCorreta()].GetComponent<Image>(), SpriteCorreta);
        }
    }
    public void ChangeButtonSprite(Image imageButton, Sprite spriteChange)
    {
        imageButton.sprite = spriteChange;
    }
    public void DisableOptionButtons()
    {
        for(int i = 0; i< alternativaTMP.Length; i++)
        {
            Button btnAlt = alternativaTMP[i].GetComponent<Button>();
            btnAlt.enabled = false;
        }
    }
    
}
