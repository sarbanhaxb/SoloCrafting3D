using UnityEngine;

public interface IUIElement<T>
{
    void EnableFor(T item);
    void Disable();
}

public interface IUIElement<T1, T2>
{
    void EnableFor(T1 item, T2 calllback);
    void Disable();
}
