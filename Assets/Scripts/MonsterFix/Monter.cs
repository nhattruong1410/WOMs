using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monter 
{
    MonterBase _base;
    int level;

    public int HP { get; set; }
    public List<Move> Moves { get; set; }
    public Monter(MonterBase pBase, int pLevel)
    {
        _base = pBase;
        level = pLevel;
        HP = MaxHP;

        Moves = new List<Move>();
        foreach (var move in _base.LearnableMoves)
        {
            if (move.Level <= level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4) break;
        }
    }
    public int MaxHP
    {
        get { return Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 10; }
    }
    public int Attack
    {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }
    public int Defence
    {
        get { return Mathf.FloorToInt((_base.Defence * level) / 100f) + 5; }
    }
}
