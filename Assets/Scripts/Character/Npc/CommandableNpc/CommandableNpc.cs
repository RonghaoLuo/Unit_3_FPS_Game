using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandableNpc : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Queue<NpcCommand> commandsQueue = new Queue<NpcCommand>();
    [SerializeField] protected int maxNumOfCommands;

    protected NpcCommand currentCommand;

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public void AddCommandToQueue(NpcCommand newCommand)
    {
        if (commandsQueue.Count > maxNumOfCommands)
        {
            Debug.LogWarning("TOO MANY COMMANDS! WAIT");
            return;
        }

        newCommand.targetingNpc = this;

        commandsQueue.Enqueue(newCommand);

        if (currentCommand == null)
        {
            ExecuteNextCommand();
        }
    }

    private void ExecuteNextCommand()
    {
        if (commandsQueue.Count == 0)
        {
            currentCommand = null;
            return;
        }

        currentCommand = commandsQueue.Dequeue();
        currentCommand.OnCommandComplete = ExecuteNextCommand;
        currentCommand.Execute();
    }

    protected virtual void Update()
    {
        //if (currentCommand != null)
        //{
        //    if (currentCommand.IsComplete())
        //    {
        //        currentCommand = null;
        //    }

        //    return;
        //}

        //if (commandsQueue.Count > 0)
        //{
        //    currentCommand = commandsQueue.Dequeue();
        //    currentCommand.Execute();
        //}
    }
}
