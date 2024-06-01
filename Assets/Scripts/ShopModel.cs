using System.Collections.Generic;
using UnityEngine;

namespace ShopMVC
{
    public class ShopModel
    {
        List<Item> _inventory;
        public State CurrentState { get; private set; } = State.OPTIONS;
        public void ChangeState(State newState)
        {
            var oldState = CurrentState;
            CurrentState = newState;
        }
    }
}
