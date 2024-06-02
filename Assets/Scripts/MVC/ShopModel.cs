using System.Collections.Generic;
using UnityEngine;

namespace ShopMVC
{
    public enum State
    {
        OPTIONS,
        MAIN,
        CONFIRM
    }
    public class ShopModel
    {
        List<ItemData> _inventory;
        public State CurrentState { get; private set; } = State.OPTIONS;
        public void ChangeState(State newState)
        {
            var oldState = CurrentState;
            CurrentState = newState;
        }
    }
}
