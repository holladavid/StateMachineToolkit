using Sanford.StateMachineToolkit;

namespace TestExample
{
	public enum EventID
	{
		E,
		D,
		C,
		A,
		F,
		B,
		G,
		H,
	}

	public enum StateID
	{
		S1 = 8,
	    S0,
	    S11,
	    S2,
	    S21,
	    S211
	}

	public abstract class ExampleBase : PassiveStateMachine<StateID, EventID>
	{
		private GuardHandler guardFooIsTrue, guardFooIsFalse;

		private ActionHandler actionSetFooToFalse, actionSetFooToTrue;

		protected ExampleBase()
		{
			Initialize();
		}

		private void Initialize()
		{
			InitializeStates();
			InitializeGuards();
			InitializeActions();
			InitializeTransitions();
			InitializeRelationships();
			Initialize(StateID.S0);
		}

		private void InitializeStates()
		{
		    this[StateID.S0].EntryHandler += EntryS0;
		    this[StateID.S1].EntryHandler += EntryS1;
		    this[StateID.S11].EntryHandler += EntryS11;
		    this[StateID.S2].EntryHandler += EntryS2;
		    this[StateID.S21].EntryHandler += EntryS21;
		    this[StateID.S211].EntryHandler += EntryS211;

		    this[StateID.S0].ExitHandler += ExitS0;
		    this[StateID.S1].ExitHandler += ExitS1;
		    this[StateID.S11].ExitHandler += ExitS11;
		    this[StateID.S2].ExitHandler += ExitS2;
		    this[StateID.S21].ExitHandler += ExitS21;
		    this[StateID.S211].ExitHandler += ExitS211;
		}

		private void InitializeGuards()
		{
			guardFooIsTrue = FooIsTrue;
			guardFooIsFalse = FooIsFalse;
		}

		private void InitializeActions()
		{
			actionSetFooToFalse = SetFooToFalse;
			actionSetFooToTrue = SetFooToTrue;
		}

		private void InitializeTransitions()
		{
            AddTransition(StateID.S0, EventID.E, null, StateID.S211);
            AddTransition(StateID.S1, EventID.D, null, StateID.S0);
            AddTransition(StateID.S1, EventID.C, null, StateID.S2);
            AddTransition(StateID.S1, EventID.A, null, StateID.S1);
            AddTransition(StateID.S1, EventID.F, null, StateID.S211);
            AddTransition(StateID.S1, EventID.B, null, StateID.S11);
            AddTransition(StateID.S11, EventID.G, null, StateID.S211);
            AddTransition(StateID.S11, EventID.H, guardFooIsTrue, StateID.S11, actionSetFooToFalse);
            AddTransition(StateID.S2, EventID.C, null, StateID.S1);
            AddTransition(StateID.S2, EventID.F, null, StateID.S11);
            AddTransition(StateID.S21, EventID.B, null, StateID.S211);
            AddTransition(StateID.S21, EventID.H, guardFooIsFalse, StateID.S21, actionSetFooToTrue);
            AddTransition(StateID.S211, EventID.D, null, StateID.S21);
            AddTransition(StateID.S211, EventID.G, null, StateID.S0);
		}

		private void InitializeRelationships()
		{
            SetupSubstates(StateID.S0, HistoryType.None, StateID.S1, StateID.S2);
            SetupSubstates(StateID.S1, HistoryType.None, StateID.S11);
            SetupSubstates(StateID.S2, HistoryType.None, StateID.S21);
            SetupSubstates(StateID.S21, HistoryType.None, StateID.S211);
		}

		protected virtual void EntryS0()
		{
		}

		protected virtual void EntryS1()
		{
		}

		protected virtual void EntryS11()
		{
		}

		protected virtual void EntryS2()
		{
		}

		protected virtual void EntryS21()
		{
		}

		protected virtual void EntryS211()
		{
		}

		protected virtual void ExitS0()
		{
		}

		protected virtual void ExitS1()
		{
		}

		protected virtual void ExitS11()
		{
		}

		protected virtual void ExitS2()
		{
		}

		protected virtual void ExitS21()
		{
		}

		protected virtual void ExitS211()
		{
		}

		protected abstract bool FooIsTrue(object[] args);

		protected abstract bool FooIsFalse(object[] args);

		protected abstract void SetFooToFalse(object[] args);

		protected abstract void SetFooToTrue(object[] args);

		public override void Send(EventID eventID, params object[] args)
		{
			base.Send(eventID, args);
			Execute();
		}
	}
}