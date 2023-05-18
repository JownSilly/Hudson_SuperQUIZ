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
    [SerializeField] private Sprite alertResultadoCorreto;
    [SerializeField] private Sprite alertResultadoErrado;
    [SerializeField] private GameObject inicioDoJogoCanvas;
    [SerializeField] private GameObject quizJogoCanvas;
    [SerializeField] private GameObject fimDeJogoCanvas;
    [SerializeField] private GameObject alertResultadoCanvas;
    private Temporizador temporizador;
    private int posicaoVetor;
    private int contagemRespostasCorreta;
    private int naoCorreta;
    [SerializeField] private TextMeshProUGUI alertResultadoText;
    void Start()
    {
        fimDeJogoCanvas.SetActive(false);
        alertResultadoCanvas.GetComponent<Canvas>().enabled = false;
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
            naoCorreta = contagemRespostasCorreta;
        }
        PararTimer();
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
        if (alternativaTMP[0].GetComponent<Button>().enabled){
            int timeIsOver = contagemRespostasCorreta;
            AlertaResultadoInstantaneo(timeIsOver);
        }
        else
        {
            AlertaResultadoInstantaneo(naoCorreta);
        }

        //Debug.Log("Fimde Jogo");
        //Invoke("HabilitarTelaFimdeJogo", 1f);

        //Invoke("AlterarPergunta", 1f);

    }
    public void AlertaResultadoInstantaneo(int naoContou)
    {
        alertResultadoCanvas.GetComponent<Canvas>().enabled = true;
        if (contagemRespostasCorreta != naoContou)
        {
            Debug.Log("Verdadeuri");
            alertResultadoCanvas.GetComponentInChildren<Image>().sprite = alertResultadoCorreto;
            alertResultadoText.SetText("Parabéns, meu caro Gafanhoto!\n A resposta correta é:\n\n" + perguntaAtual.GetAlternativas()[perguntaAtual.GetAlternativaCorreta()]);
        }
        else
        {
            alertResultadoCanvas.GetComponentInChildren<Image>().sprite = alertResultadoErrado;
            alertResultadoText.SetText("Poxa vida, essa você nao acertou!\n A resposta correta é:\n\n " + perguntaAtual.GetAlternativas()[perguntaAtual.GetAlternativaCorreta()]);
        }
    }
    public void AlterarPergunta()
    {
        alertResultadoCanvas.GetComponent<Canvas>().enabled = false;
        posicaoVetor++;
        if (posicaoVetor >= perguntasTotais.Length)
        {
            quizJogoCanvas.SetActive(false);
            fimDeJogoCanvas.SetActive(true);
            fimDeJogoCanvas.GetComponentInChildren<TextMeshProUGUI>().SetText("Parabéns por ter finalizado o jogo!\n Você acertou " + contagemRespostasCorreta + " de " + perguntasTotais.Length + " perguntas");
        }
        else 
        {
            PopularPergunta();
            ReiniciarOptionsButtons();
        }
    }
    /*
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
    */



}
