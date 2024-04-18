using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    //public string customName;
    private EnemyState mainStateType;

    public EnemyState currentState { get; private set; }
    private EnemyState nextState;

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
            nextState = null;
        }

        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    //change state locally
    private void SetState(EnemyState _newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = _newState;
        currentState.OnEnter(this);
    }

    //change the next state from other scripts
    public void SetNextState(EnemyState _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }
    private void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.OnLateUpdate();
        }
    }

    //reset state
    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }
    private void Awake()
    {
        SetNextStateToMain();
    }
    private void OnValidate()
    {
        if (mainStateType == null)
        {
            //if (customName == "Enemy")
            mainStateType = new IdleEnemyState();
        }
    }

}
