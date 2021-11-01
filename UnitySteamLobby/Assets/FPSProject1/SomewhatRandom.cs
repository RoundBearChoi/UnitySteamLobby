using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public static class SomewhatRandom
    {
        public static string GetRandomName(int letters)
        {
            System.Random seed = new System.Random();

            string name = string.Empty;

            int frontInt = seed.Next((int)101);

            name += frontInt.ToString();

            for (int i = 0; i < letters; i++)
            {
                int randomAlphabet = seed.Next((int)65, (int)91);
                name += ((char)randomAlphabet).ToString();
            }

            return name;
        }

        public static uint GetRandomKey()
        {
            //uint max 4,294,967,295

            System.Random seed = new System.Random();

            string id = string.Empty;

            int first = seed.Next(4); // 0 to 3
            id += first.ToString();

            for (int i = 0; i < 9; i++) // 9 digits
            {
                int num = seed.Next(10); // 0 to 9
                id += num.ToString();
            }

            // 0 to 3,000,000,000

            uint result = uint.Parse(id);
            GeneralDebug.Log("random uint generated: " + result);

            return result;
        }
    }
}