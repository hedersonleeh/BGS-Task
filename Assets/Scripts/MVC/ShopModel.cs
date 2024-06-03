using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopMVC
{
    [System.Serializable]
    public enum State
    {
        OPTIONS,
        BUY,
        SELL,
        CONFIRM
    }
    public class ShopModel
    {
        public List<ItemData> inventory { get; private set; }
        public State CurrentState { get; private set; } = State.OPTIONS;
        public State LastState { get; private set; } = State.OPTIONS;
        public delegate void OnStateChange(State newState);
        public delegate void OnInventoryUpdate();
        public OnStateChange onStateChange;
        public OnInventoryUpdate onInventoryUpdate;
        public ItemData currentItemSelected;

        public ShopModel(List<ItemData> inventory)
        {
            this.inventory = inventory;
        }

        public void ChangeState(State newState)
        {
            LastState = CurrentState;
            CurrentState = newState;
            onStateChange?.Invoke(newState);
        }

        public bool TryBuyItem(Inventory playerInvetory, ItemData item)
        {
            if (playerInvetory.money >= item.price)
            {
                playerInvetory.AddItem(item);
                playerInvetory.SpendMoney(item.price);
                inventory.Remove(item);
                onInventoryUpdate?.Invoke();
                return true;
            }
            return false;
        }
        public void SellItem(Inventory playerInvetory, ItemData item)
        {
            if (playerInvetory.DiscardItem(item.ID))
            {
                inventory.Add(item);
                playerInvetory.GainMoney(item.price);
                onInventoryUpdate?.Invoke();
            }
        }
    }
}
