using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopMVC
{
   [System.Serializable] public enum State
    {
        OPTIONS,
        BUY,
        SELL,
        CONFIRM
    }
    public class ShopModel
    {
        List<ItemData> _inventory;
        public State CurrentState { get; private set; } = State.OPTIONS;
        public delegate void OnStateChange(State newState);
        public OnStateChange onStateChange;
        public ItemData currentItemSelected;

        public void ChangeState(State newState)
        {
            var oldState = CurrentState;
            CurrentState = newState;
            onStateChange?.Invoke(newState);
        }
        public bool TryBuyItem(Inventory playerInvetory, ItemData item)
        {
            if(playerInvetory.money >= item.price)
            {
                playerInvetory.AddItem(item);
                playerInvetory.SpendMoney( item.price);
                
                return true;
            }
            return false;
        }
        public void SellItem(ItemData item)
        {

        }
    }
}
