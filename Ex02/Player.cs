using System;


namespace Ex02
{
    public class Player
    {
        private static readonly int sr_MaxPlayerNameLength = 20;


        public static bool IsPlayerNameValid(string i_PlayerName)
        {
            bool validPlayerName = i_PlayerName.Length <= sr_MaxPlayerNameLength && !(i_PlayerName.Contains(" "));
            return validPlayerName;
        }
    }
}
