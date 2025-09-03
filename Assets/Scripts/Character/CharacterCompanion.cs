using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterCompanion : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CharacterShooting shooting;
    [SerializeField] private Queue<Command> commandsQueue = new Queue<Command>();
    [SerializeField] private int maxNumOfCommands;

    private Command currentCommand;

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public CharacterShooting GetShooting()
    {
        return shooting;
    }

    public void AddCommandToQueue(Command newCommand)
    {
        if (commandsQueue.Count > maxNumOfCommands)
        {
            Debug.LogWarning("TOO MANY COMMANDS! WAIT");
            return;
        }

        newCommand.characterTarget = this;
        
        commandsQueue.Enqueue(newCommand);
    }

    private void Update()
    {
        if (currentCommand != null)
        {
            if (currentCommand.IsComplete())
            {
                currentCommand = null;
            }

            return;
        }

        if (commandsQueue.Count > 0)
        {
            currentCommand = commandsQueue.Dequeue();
            currentCommand.Execute();
        }
    }
}
