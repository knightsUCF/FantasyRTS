

// S T A T E S /////////////////////////////////////////////////////////////////////////

public enum AnimationState
{
    Idle,
    Walk,
    Attack
}

public enum ActionState
{
    None,
    MoveToLocation,
    Pursue,
    Engage,
    Escape
}

public AnimationState animationState;
public ActionState actionState;


// M E T H O D S ////////////////////////////////////////////////////////////////////////

void SetActionState()
{
    switch (actionState)
    {
        case ActionState.MoveToLocation():
            animationState = AnimationState.Walk;
            break;
        case ActionState.Pursue:
            break;
    }
}

void SetAnimationState()
{
    switch (animationState)
    {
        case AnimationState.Idle:
            animator.SetInteger("animationState", 0);
            break;
        case AnimationState.Walk:
            animator.SetInteger("animationState", 1);
            break;
        case AnimationState.Attack:
            animator.SetInteger("animationState", 2);
            break;
    }
}


void SetOverallStates()
{
    if (selected) CheckForMouseClickMovement();

    SetAnimationState();
    SetActionState();
}


// M A I N ///////////////////////////////////////////////////////////////////////////////


void Update()
{
    SetOverallStates();
}


//////////////////////////////////////////////////////////////////////////////////////////
