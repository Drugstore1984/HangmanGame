using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HangmanGame : MonoBehaviour
{
    private readonly int allowedMisses=8;
    private bool[] openIndexes;
    private int triesCounter = 0;
    private string triedLetters;

    public GameStatus GameStatus { get; private set; } = GameStatus.NotStarted;

    public string Word { get; private set; }

    public string TriedLetters
    {
        get
        {
            var chars = triedLetters.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
    public int RemainingTries
    {
        get
        {
            return allowedMisses - triesCounter;
        }
    }
    public int RemaningTries
    {
        get
        {
            return allowedMisses - triesCounter;
        }
    }

    public string GenerateWord()
    {
        var textFile = Resources.Load<TextAsset>("Text/WordsDictionary");
        string fullText = textFile.ToString();
        string[] words = fullText.Split('\n');
        int randomIndex = UnityEngine.Random.Range(0,words.Length-1);
        
        Word = words[randomIndex].Trim();

        openIndexes = new bool[Word.Length];
         
        GameStatus = GameStatus.InProgress;
        return Word;
    }
    public string ShowWordStart()
    {
        string result = string.Empty;

        for (int i = 0; i < Word.Length; i++)
        {
            result += "_ ";
        }
        return result;
    }
    public string GuessLetter(char letter)
    {
        if (triesCounter == allowedMisses)
        {
            throw new InvalidOperationException($"Exeeded the max misses number: {allowedMisses}");
        }
        if (GameStatus != GameStatus.InProgress)
        {
            throw new InvalidOperationException($"Inaproppriate game status: {GameStatus}");
        }
        bool openAny = false;
        string result = string.Empty;

        for (int i = 0; i < Word.Length; i++)
        {
            if (Word[i] == letter)
            {
                openIndexes[i] = true;
                openAny = true;
            }
            if (openIndexes[i])
            {
                result += Word[i];
            }
            else
            {
                result += " _ ";
            }
        }
        if (!openAny)
        {
            triesCounter++;
        }
        triedLetters += letter;

        if (isWin())
        {
            GameStatus = GameStatus.Won;
        }
        else if (triesCounter == allowedMisses)
        {
            GameStatus = GameStatus.Lost;
        }

        return result;
    }
    private bool isWin()
    {
        foreach (var cur in openIndexes)
        {
            if (!cur)
            {
                return false;
            }
        }
        return true;
    }
}
