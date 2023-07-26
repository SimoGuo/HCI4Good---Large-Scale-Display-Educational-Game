using System;

public enum Interlocutor
{
    Null,
    Character1,
    Character2,
    Character3,
    Character4,
    Enemy
}

[Serializable]
public class Dialog
{
    public bool isNeedReset;
    public Interlocutor interlocutor1;
    public Interlocutor interlocutor2;
    public string dialog;
}
