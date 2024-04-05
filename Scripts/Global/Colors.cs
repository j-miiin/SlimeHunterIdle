using UnityEngine;

public class Colors
{
    private static Color _mainYellow;
    private static Color _textBrown;
    private static Color _textRed;

    public static Color MainYellow
    {
        get
        {
            if (_mainYellow == Color.clear) ColorUtility.TryParseHtmlString("#F7D060", out _mainYellow);
            return _mainYellow;
        }
    }

    public static Color TextBrown
    {
        get
        {
            if (_textBrown == Color.clear) ColorUtility.TryParseHtmlString("#7D5A50", out _textBrown);
            return _textBrown;
        }
    }
    public static Color TextRed
    {
        get
        {
            if (_textRed == Color.clear) ColorUtility.TryParseHtmlString("#FF6D60", out _textRed);
            return _textRed;
        }
    }
}
