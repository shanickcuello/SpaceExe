using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    [SerializeField] GameObject console;
    [SerializeField] Text consoleText;
    [SerializeField] InputField inputConsole;

    public delegate void allCommands();
    public Dictionary<string, Command> commandsDic = new Dictionary<string, Command>();

    public static Console instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

    }

    private void Start()
    {

        ClearConsole();
        allCommands clear = ClearConsole;
        RegisterCommand("cls", "Limpia la consola", ClearConsole);
        RegisterCommand("help", "Muestra los comandos disponibles", Help);
    }

    private void Update()
    {
        CheckEnter();
    }

    public void RegisterCommand(string cmdName, string description, allCommands command)
    {
        Command newCommand = new Command();
        newCommand.name = cmdName;
        newCommand.description = description;
        newCommand.command = command;
        commandsDic.Add(cmdName, newCommand);
    }

    private void CheckEnter()
    {
        if (Input.GetKeyDown(KeyCode.Return) && CheckConsoleOpen)
        {
            Write(inputConsole.text);

            if (commandsDic.ContainsKey(inputConsole.text))
            {
                try
                {
                    commandsDic[inputConsole.text].command.Invoke();
                }
                catch (Exception error)
                {
                    Write("Hubo un error: " + error.Message + "\n" + error.StackTrace);
                }
            }
            else
            {
                Write("El comando '" + inputConsole.text + "' no existe. Ejecute 'help' para conocer los comandos");
            }

            inputConsole.text = "";
        }
    }

    bool CheckConsoleOpen
    {
        get
        {
            if (console.activeSelf)
                return true;
            else
                return false;
        }
    }

    public void SetActiveConsole()
    {
        console.SetActive(!console.activeSelf);
        inputConsole.Select();
    }

    public void Write(string txt)
    {
        consoleText.text += "\n" + txt;
    }

    void Help()
    {
        Write("Lista de comandos:");
        foreach (var item in commandsDic)
        {
            Write(item.Value.ToString());
        }
    }

    void ClearConsole()
    {
        consoleText.text = "";
    }

}
