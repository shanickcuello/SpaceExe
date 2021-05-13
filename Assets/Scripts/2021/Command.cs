public class Command
{
    public Console.allCommands command;
    public string name;
    public string description;

    public override string ToString()
    {
        return "[" + name + ">>>" + description + "]";
    }
}
