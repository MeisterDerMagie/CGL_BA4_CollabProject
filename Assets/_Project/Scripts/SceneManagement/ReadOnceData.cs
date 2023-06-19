//(c) copyright by Martin M. Klöckener
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class ReadOnceData<T>
{
    [SerializeField][ReadOnly]
    private T _defaultValue;
    
    [ShowInInspector][ReadOnly]
    private T _currentValue;
    
    public T Value
    {
        get
        {
            //return current value and reset it to the defaultValue. 
            T currentValue = _currentValue;
            _currentValue = _defaultValue;
            return currentValue;
        }
    }
    
    public ReadOnceData(T defaultValue)
    {
        _defaultValue = defaultValue;
    }

    public void SetValue(T newValue)
    {
        _currentValue = newValue;
    }
}