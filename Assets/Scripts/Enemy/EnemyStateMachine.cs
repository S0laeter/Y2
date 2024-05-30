using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public string enemyType;
    private EnemyState mainEnemyStateType;

    public EnemyState currentState { get; private set; }
    private EnemyState nextState;
    
    private void Awake()
    {
        SetNextEnemyStateToMain();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
        {
            SetCurrentState(nextState);
            nextState = null;
        }

        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    //change state locally
    private void SetCurrentState(EnemyState _newState)
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
    public void SetNextEnemyStateToMain()
    {
        nextState = mainEnemyStateType;
    }
    
    private void OnValidate()
    {
        if (mainEnemyStateType == null)
        {

            //different enemy types will have different base states
            switch (enemyType)
            {
                case "Enemy1":
                    mainEnemyStateType = new Enemy1IdleState();
                    break;
                case "Enemy2":
                    mainEnemyStateType = new Enemy2IdleState();
                    break;
                case "Enemy3":
                    mainEnemyStateType = new Enemy3IdleState();
                    break;
                case "Boss1":
                    //mainEnemyStateType = new Boss1IdleState();
                    break;
                default:
                    break;
            }

        }
    }

}
