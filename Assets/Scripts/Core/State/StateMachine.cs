namespace SpaceShootuh.Core.State
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            PreviousState = CurrentState;
            CurrentState = newState;
            newState.Enter();
        }
    }
}
