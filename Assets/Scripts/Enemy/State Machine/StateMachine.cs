using System;
using System.Collections.Generic;

public class StateMachine
{
    private IState currentState;
    public IState CurrentState { get { return currentState;} }
    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitions = new List<Transition>();
    private List<Transition> anyTransitions = new List<Transition>();
    private static List<Transition> emptyTransitions = new List<Transition>(0);

    public void UpdateState() {  
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        currentState?.UpdateState();
    }

    public void SetState(IState state) {  
        if (state  == currentState)
        {
            return;
        }

        currentState?.ExitState();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
        {
            currentTransitions = emptyTransitions;
        }
         
        currentState.EnterState();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate) { 
        if (transitions.TryGetValue(from.GetType(), out var _transitions) == false)
        {
            _transitions = new List<Transition>();
            transitions[from.GetType()] = _transitions;
        }
        
        _transitions.Add(new Transition(to, predicate));
    }

    public void AddAnyTransition(IState state, Func<bool> predicate) {
        anyTransitions.Add(new Transition(state, predicate));
    }

    private class Transition {
        public Func<bool> condition {get;}
        public IState To {get;}

        public Transition(IState _to, Func<bool> _condition) {
            To = _to;
            condition = _condition;
        }
    }

    private Transition GetTransition() { 
        foreach (var transition in anyTransitions) 
        {
            if (transition.condition())
            {
                return transition;
            }
        }

        foreach (var transition in currentTransitions) 
        {
            if (transition.condition())
            {
                return transition;
            }
        }

        return null;
    }
}
