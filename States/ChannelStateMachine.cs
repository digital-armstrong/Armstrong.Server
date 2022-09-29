using Stateless;

namespace ArmstrongServer.States
{
  public class ChannelStateMachine
  {
    enum State { Danger, Warning, Normal }
    enum Trigger { Assign, Defer, Resolve, Close }

    public void InitialMachine()
    {
      StateMachine<State, Trigger> machine;
    }
  }
}
