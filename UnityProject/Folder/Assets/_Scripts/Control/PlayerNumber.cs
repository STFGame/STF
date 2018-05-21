using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PlayerNumber
{
    None,

    PlayerOne,
    PlayerTwo,
    PlayerThree,
    PlayerFour
}

public class PlayerNumberUtility
{
    public static PlayerNumber[] m_PlayerNumbers = Enum.GetValues(typeof(PlayerNumber)).Cast<PlayerNumber>().ToArray();
}
