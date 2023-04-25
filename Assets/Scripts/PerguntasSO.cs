using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "SuperQuiz/Nova pergunta",
    fileName = "pergunta-"
    )]
public class PerguntasSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] private string enunciado;
    [SerializeField] private string[] alternativas;
    [SerializeField] private int alternativaCorreta;
    [SerializeField] private string id;

    public string GetEnunciado()
    {
        return enunciado;
    }
    public string GetId()
    {
        return id;
    }
    public string[] GetAlternativas()
    {
        return alternativas;
    }
    public int GetAlternativaCorreta()
    {
        return alternativaCorreta;
    }
}
