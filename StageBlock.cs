using System;
using UnityEngine;

namespace StageBlocks
{
    public struct StageBlock
    {
        public Rect box;
        public Color color;

        public StageBlock(Rect block, Color? color = null)
        {
            this.box = block;
            this.color = color ?? Color.clear;
        }
        public static implicit operator StageBlock(Rect a) => new StageBlock(a);

        public override string ToString()
        {
            string res = "";
            res += box.position.x + ", " + box.position.y + ", " + box.size.x + ", " + box.size.y;
            if ((int)color.a == 0)
            {
                res += " ; " + ColorUtility.ToHtmlStringRGB(color);
            }
            return res;
        }
        public static StageBlock FromString(string data)
        {

            string[] splits = data.Split(';');
            string[] boxStrings = splits[0].Split(',');
            Rect box = new Rect(
                float.Parse(boxStrings[1]),
                float.Parse(boxStrings[2]),
                float.Parse(boxStrings[3]),
                float.Parse(boxStrings[4])
            );
            if (splits.Length > 1)
            {
                string htmlColor = splits[1][0] == '#' ? splits[1] : $"#{splits[1]}";
                if (ColorUtility.TryParseHtmlString(htmlColor, out Color color) == false)
                {
                    StageBlocks.Log.LogWarning("Failed to load colour. Is it in the right format? e.g. #1a1a22");
                    return new StageBlock(box);
                }
                return new StageBlock(box, color);
            }
            return new StageBlock(box);
        }
    }
}
