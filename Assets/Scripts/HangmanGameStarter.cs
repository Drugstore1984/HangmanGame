using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangmanGameStarter : MonoBehaviour
{
    private HangmanGame game;

    private string word;

    [Header("UI Fields")]
    [SerializeField] private Text gameInfoText;
    [SerializeField] private Text remainigTries;
    [SerializeField] private Text triedLetters;
    [SerializeField] private Text currentState;

    [SerializeField] private InputField pickALetterField;

    [Header("UI Menues")]
    [SerializeField] private GameObject wonMenu;
    [SerializeField] private GameObject lostMenu;

    private char enteredLetter;
    
    private void Awake()
    {
        GameObject gameObject = new GameObject("HangmanGame");
        game = gameObject.AddComponent<HangmanGame>();
    }
  
    private void Start()
    {
        word = game.GenerateWord();
        gameInfoText.text = $"Guess the word of {word.Length} letters";
        currentState.text = game.ShowWordStart();
        RemainingLetters();
    }
    private void RemainingLetters()
    {
        remainigTries.text = $"Remaining tries = {game.RemainingTries}";
    }
    public void EnterLetter()
    {
        char enteredLetter = pickALetterField.text.ToCharArray()[0];
        string curState = game.GuessLetter(enteredLetter);
        currentState.text = curState;

        triedLetters.text = $"Tried letters: {game.TriedLetters}";
        RemainingLetters();

        if (game.GameStatus == GameStatus.Lost)
        {
            lostMenu.SetActive(true);
            Text wordResult = lostMenu.GetComponentInChildren<Text>();
            wordResult.text = $"The word was {game.Word}!";
        }
        else if (game.GameStatus == GameStatus.Won)
        {
            wonMenu.SetActive(true);
        }
    }
    
}
