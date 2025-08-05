using Data.Models;
using System;

public class QueueEntry
{
    public User PlayerOne { get; set; }
    public User PlayerTwo { get; set; }
    public bool isHardPair { get; set; }

    public QueueEntry(User playerOne, User playerTwo)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
    }
}