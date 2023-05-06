namespace EkoStatRp.Models;

public class MessageBox
{
    public string Message { get; set; } = "";
    public MessageBoxKind Kind { get; set; } = 0;
    public string ColorClass => GetColorClass();

    public MessageBox(string message, MessageBoxKind kind = 0)
    {
        Message = message;
        Kind = kind;
    }

    private string GetColorClass()
    {
        switch (Kind)
        {
            case MessageBoxKind.Success: return "messageBox-success";
            case MessageBoxKind.Error: return "messageBox-error";
            default: return "messageBox-normal";
        }
    }
}

public enum MessageBoxKind
{
    Normal,
    Success,
    Error,
}
