Simple usage example, more info on our [Blog](https://www.patreon.com/posts/20756952)
```csharp
public class Example : MonoBehaviour
{
    private FiniteStateMachine _stateMachine;

    void Start()
    {
        _stateMachine = new FiniteStateMachine();
        _stateMachine.GetConfiguration()
            .StartAt(StartState)
            .Then(State2)
            .Wait(1f)
            .Then(State3)
            .Wait(2f)
            .Then(State2);


        _stateMachine.Start();
    }

    private bool State3(StateMachineData data)
    {
        return true;
    }

    private bool State2(StateMachineData data)
    {
        return true;
    }

    private bool StartState(StateMachineData data)
    {
        return true;
    }

    void Update()
    {
        _stateMachine.Step();
    }

}
```
