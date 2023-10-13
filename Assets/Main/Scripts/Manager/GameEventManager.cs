using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour
{
    private interface IEventHelp
    {

    }
    private class EventHelp : IEventHelp
    {
        private event Action _action;
        public EventHelp(Action action)
        {
            _action = action;
        }
        //��Ӻ���
        public void AddCall(Action action)
        {
            _action += action;
        }
        //�Ƴ��¼�
        public void Remove(Action action)
        {
            _action -= action;
        }
        //�����¼�
        public void Call()
        {
            _action?.Invoke();
        }
      }
    private class EventHelp<T> : IEventHelp
    {
        private event Action<T> _action;
        public EventHelp(Action<T> action)
        {
            _action = action;
        }
        //��Ӻ���
        public void AddCall(Action<T> action)
        {
            _action += action;
        }
        //�Ƴ��¼�
        public void Remove(Action<T> action)
        {
            _action -= action;
        }
        //�����¼�
        public void Call(T value)
        {
            _action?.Invoke(value);
        }
    }
    private class EventHelp<T1,T2> : IEventHelp
    {
        private event Action<T1,T2> _action;
        public EventHelp(Action<T1, T2> action)
        {
            _action = action;
        }
        //��Ӻ���
        public void AddCall(Action<T1, T2> action)
        {
            _action += action;
        }
        //�Ƴ��¼�
        public void Remove(Action<T1, T2> action)
        {
            _action -= action;
        }
        //�����¼�
        public void Call(T1 value1,T2 Value2)
        {
            _action?.Invoke(value1,Value2);
        }
    }

    private Dictionary<string, IEventHelp> _eventCenter = new Dictionary<string, IEventHelp>();

}
