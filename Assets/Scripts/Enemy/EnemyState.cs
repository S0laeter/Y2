using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public abstract class EnemyState
{
    protected float time { get; set; }
    protected float fixedTime { get; set; }
    protected float lateTime { get; set; }

    public EnemyStateMachine enemyStateMachine;

    public virtual void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        enemyStateMachine = _enemyStateMachine;
    }

    public virtual void OnUpdate()
    {
        time += Time.deltaTime;
    }
    public virtual void OnFixedUpdate()
    {
        fixedTime += Time.deltaTime;
    }
    public virtual void OnLateUpdate()
    {
        lateTime += Time.deltaTime;
    }

    public virtual void OnExit()
    {

    }



    //what in the actual fuck..
    #region Passthrough Methods

    /// <summary>
    /// Removes a gameobject, component, or asset.
    /// </summary>
    /// <param name="obj">The type of Component to retrieve.</param>
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    /// <summary>
    /// Returns the component of type T if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component { return enemyStateMachine.GetComponent<T>(); }

    /// <summary>
    /// Returns the component of Type <paramref name="type"/> if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <param name="type">The type of Component to retrieve.</param>
    /// <returns></returns>
    protected Component GetComponent(System.Type type) { return enemyStateMachine.GetComponent(type); }

    /// <summary>
    /// Returns the component with name <paramref name="type"/> if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <param name="type">The type of Component to retrieve.</param>
    /// <returns></returns>
    protected Component GetComponent(string type) { return enemyStateMachine.GetComponent(type); }
    #endregion

}
