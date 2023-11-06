using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Utility : MonoBehaviour
{
    static StringBuilder stringBuilder = new StringBuilder();
    static StringBuilder stringReverse = new StringBuilder();

    static public string GetCommaNumberString(long number)
    {
        stringBuilder.Clear();
        int cnt = 0;
        while(number > 0)
        {
            if(cnt == 3)
            {
                stringBuilder.Append(',');
                cnt = 0;
            }
            stringBuilder.Append(number % 10);
            number /= 10;
            cnt++;
        }

        if (stringBuilder.Equals(""))
            return "0";

        for(int i = stringBuilder.Length - 1; i >= 0; i--)
        {
            stringReverse.Append(stringBuilder[i]);
        }

        return stringReverse.ToString();
    }
}
