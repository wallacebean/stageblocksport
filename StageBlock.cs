using System;
using UnityEngine;
using LLBML.Utils;

namespace StageBlocks
{
    public struct StageBlock
    {
        public const float emissionMultiplier = 128f;

        public Rect box;
        public Color color;
        public byte emission;

        public StageBlock(Rect block, Color? color = null)
        {
            this.box = block;
            this.color = color ?? Color.clear;
            this.emission = Convert.ToByte(this.color.a * 255f);
            this.color.a = 1f;
            StageBlocks.Log.LogDebug("Constructed: " + this);
        }
        public static implicit operator StageBlock(Rect a) => new StageBlock(a);

        public override string ToString()
        {
            string res = "";
            res += box.position.x + ", " + box.position.y + ", " + box.size.x + ", " + box.size.y;
            //StageBlocks.Log.LogDebug($"Color alpha is : <{color.a}>");
            res += " ; #" + ColorUtility.ToHtmlStringRGB(color);
            res += StringUtils.BytesToHexString(new byte[] { emission });
            return res;
        }
        public static StageBlock FromString(string data)
        {
            StageBlocks.Log.LogDebug("Load from string: " + data);
            string[] splits = data.Split(';');
            string[] boxStrings = splits[0].Split(',');
            Rect box = new Rect(
                float.Parse(boxStrings[0]),
                float.Parse(boxStrings[1]),
                float.Parse(boxStrings[2]),
                float.Parse(boxStrings[3])
            );
            if (splits.Length > 1)
            {
                string htmlColor = splits[1].Replace(" ", "");
                htmlColor = htmlColor[0] == '#' ? htmlColor : $"#{htmlColor}";
                //StageBlocks.Log.LogDebug($"Color is : <{htmlColor}>");
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
