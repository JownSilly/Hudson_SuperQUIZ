using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Perguntas")]
    [SerializeField] private PerguntasSO perguntaAtual;
    [SerializeField] private PerguntasSO[] perguntasTotais;
    [SerializeField] private TextMeshProUGUI enunciadoTexto;
    [SerializeField] private GameObject[] alternativaTMP;
    [Header("sprites")]
    [SerializeField] private Sprite SpriteCorreta;
    [SerializeField] private Sprite SpriteIncorreta;
    [SerializeField] private Sprite spritePadrao;
    [SerializeField] private GameObject inicioDoJogoCanvas;
    [SerializeField] private GameObject quizJogoCanvas;
    [SerializeField] private GameObject fimDeJogoCanvas;
    private Temporizador temporizador;
    private int posicaoVetor;
    private int contagemRespostasCorreta;
    [SerializeField] private TextMeshProUGUI mensagemFimdeJogo;
    void Start()
    {
        posicaoVetor = 0;
        contagemRespostasCorreta = 0;
        temporizador = GetComponent<Temporizador>();
        temporizador.RegistrarParada(OnParadaTimer);
        enunciadoTexto.SetText(perguntaAtual.GetEnunciado());
        string[] alternativas = perguntaAtual.GetAlternativas();
        for (int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alternativa = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alternativa.SetText(alternativas[i]);
        }
    }
    public void HandleOption(int alternativaSelecionada)
    {
        DisableOptionButtons();
        PararTimer();
        Image imageAlt = alternativaTMP[alternativaSelecionada].GetComponent<Image>();
        if (alternativaSelecionada == perguntaAtual.GetAlternativaCorreta())
        {
            ChangeButtonSprite(imageAlt, SpriteCorreta);
            contagemRespostasCorreta++;
        }
        else
        {
            ChangeButtonSprite(imageAlt, SpriteIncorreta);
            ChangeButtonSprite(alternativaTMP[perguntaAtual.GetAlternativaCorreta()].GetComponent<Image>(), SpriteCorreta);
        }
    }
    public void ChangeButtonSprite(Image imageButton, Sprite spriteChange)
    {
        imageButton.sprite = spriteChange;
    }
    public void DisableOptionButtons()
    {
        for (int i = 0; i < alternativaTMP.Length; i++)
        {
            Button btnAlt = alternativaTMP[i].GetComponent<Button>();
            btnAlt.enabled = false;
        }
    }
    void ReiniciarOptionsButtons()
    {
        for (int i = 0; i < alternativaTMP.Length; i++)
        {
            Button btnAlt = alternativaTMP[i].GetComponent<Button>();
            btnAlt.enabled = true;
        }
        for (int i = 0; i < alternativaTMP.Length; i++)
        {
            Image imageBtn = alternativaTMP[i].GetComponent<Image>();
            ChangeButtonSprite(imageBtn, spritePadrao);
        }
        temporizador.Zerar();
    }
    public void PopularPergunta()
    {
        perguntaAtual = perguntasTotais[posicaoVetor];
        enunciadoTexto.SetText(perguntaAtual.GetEnunciado());
        string[] alternativas = perguntaAtual.GetAlternativas();
        for (int i = 0; i < alternativas.Length; i++)
        {
            TextMeshProUGUI alternativa = alternativaTMP[i].GetComponentInChildren<TextMeshProUGUI>();
            alternativa.SetText(alternativas[i]);
        }
        
    }
    void PararTimer()
    {
        temporizador.Parar();
    }
    private void OnParadaTimer()
    { 
        if (posicaoVetor >= perguntasTotais.Length - 1){

            Debug.Log("Fimde Jogo");
            Invoke("HabilitarTelaFimdeJogo", 1f);
        }
        else{
                Invoke("AlterarPergunta",1f);
            }
    }
    void AlterarPergunta()
    {
        posicaoVetor++;
        //posicaoVetor = 0;
        //contagemRespostasCorreta = 0;
        PopularPergunta();
        ReiniciarOptionsButtons();
    }
    public void JogarNovamente()
    {
        posicaoVetor = 0;
        contagemRespostasCorreta = 0;
        fimDeJogoCanvas.SetActive(false);
        quizJogoCanvas.SetActive(true);
        PopularPergunta();
        ReiniciarOptionsButtons();
    }
    void HabilitarTelaFimdeJogo()
    {
        mensagemFimdeJogo.SetText("Parabéns Você Acertou "+ contagemRespostasCorreta+" de "+ perguntasTotais.Length);
        fimDeJogoCanvas.SetActive(true);
        quizJogoCanvas.SetActive(false);
    }



}
