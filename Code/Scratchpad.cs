/*

the main problem now is how to move into the walking state, well we had this working before, we would detect if MoveToMouseLocation()
so probably check this under a unit being selected, and have the MoveToMouseClickLocation set the walk state


So how do we take care of the not allowed? well we should have bools

bool acquireEnemiesWithinZoneAllowed = false;
bool attackWhenNearby = false; (in case of disengaging if the player runs away)


What if the player tries to run away and they are cornered? Well we will need some additional mechanism to detect if we are stuck
If we are stuck then go into the fight mode if there is an enemy there
And what if we are just stuck by obstacles? Well we can always fight the obstacles, in the case of structures


So go through the states, and determined, which action states are allowed / disallowed, this should be one of the final parts here before moving on to code



// S T A T E  M A C H I N E ////////////////////////////////////////////////////////////////

A. Start / Default

Action state: None
Animation state: Idle

Allowed action state transitions:

- all 


////////////////////////////////////////////////////////////////////////////////////////////

B. Check for mouse click movement

Action state: Move to location
Animation state: Walk


Allowed action state transitions:

- c: do not auto acquire enemies when moving to a specific location
- d: we do want this one! (do not attack when nearby, so we can get away, WELL, what if we do want to move there and attack)

BIG PROBLEM HERE, the difference between moving somewhere to fight, and moving away to escape

so as long as we are moving don't attack, we want to go the mouse clicks location no matter, what, unless of course enemies block our way
then we will have to fight, but how to implement this?

well we could do a crude way, where we always allow attacking within the vicinity, let's just go with this one to simply


////////////////////////////////////////////////////////////////////////////////////////////


C. Check for enemies entering auto aquire zone

Action state: Pursue
Animation state: Walk

Allowed action state transitions:

////////////////////////////////////////////////////////////////////////////////////////////

D. Attack when nearby

Action state: Engage
Animation state: Attack

* we do not want to pursue new enemies

Allowed action state transitions:

////////////////////////////////////////////////////////////////////////////////////////////

E. Disengage if player clicks away

Action state: Escape
Animation state: Walk



Allowed action state transitions:

All (?) except for C: * we do not want to check for enemies entering auto aquire zone



*/


// S E T T I N G S ///////////////////////////////////////////////////////////////////////
    
public bool selected = false;
public bool engaged = false;




// S T A T E S ////////////////////////////////////////////////////////////////////////

// We can only have one animation state, and one action state, at a time
// An additional action state is required for more complex state maneuvers,
// while we are only in one animation at a time no matter what, this can be a decoupled modular state

// key example of why we need an additional action state:
// pursue vs move to location, both will use the walk animation state, but they will have a different action state

// another key example why we need an additional action state:
// if a group of units is clamoring around to fight at one location, perhaps we will move some units around to flank attack,
// so they will be using the walk state, while still being in the pursue action state, which will switch between attack, and walking,
// while having the overarching action state of "pursue"


// Animation state

public enum AnimationState
{
    Idle,
    Walk,
    Attack
}

public AnimationState animationState;


// Action state

public enum ActionState
{
    MoveToLocation,
    Pursue,
    Escape
    // NearEnemy // structure or enemy character
}

public ActionState actionState;




// M E T H O D S //////////////////////////////////////////////////////////////////////////////////////////////////////


void CheckForMouseClickMovement()
{
    if (Input.GetMouseButton(0)) // TODO: add condition which detects we actually want to move the characters, versus clicking on a GUI button or something
    {
        // state = State.Walk;

        // set move to location state as action state, since if we simply set the state to walk,
        // then we will have confusion whether we are moving to a location,
        // or if we are walking around an enemy to flank them, which would be action state = near enemy
        
        // once the player unit arrives at a location, the flank can also be auto performed, so we will switch between the
        // movement and attack animations while being in the move to location action state

        actionState = ActionState.MoveToLocation();
        
        agent.isStopped = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MoveAgent(hit.point); // agent.SetDestination(hit.point); // move agent
        }
    }
}



void DetectedEnemy()
{
    // important to set this so we don't acquire new enemies that come into the collider zone
    // although we will want to disengage if the player moves away
    // so on mouse click, disengage, "engage = false"

    engaged = true; 


    // now is there any conflict between auto detect enemies and pursuing an enemy on the map?

    Debug.Log("Detected enemy");
    // actionState = ActionState.NearEnemy();
    // or pursue enemy as long as they are within the collider range,
    // so as long as they don't leave the collider trigger exit zone

    // so perhaps instead of NearEnemy(), we could have a pursue enemy state

    actionState = ActionState.Pursue();
}



void OnTriggerEnter(Collider c)
{
    // if we need more collider IDs, we can simply make a separate collider tagged "Enemy",
    // like for example calvary, etc

    // important to check here that the units are not already engaged,
    // so that they don't leave the current fight and go the new enemy which just entered the large collider acquizition zone
    // the enemy detection collider will be roughly the size of the screen, or maybe a little smaller, set this as a variable to experiment
    
    if (c.tag == "Enemy" && !engaged)
    {
        DetectedEnemy();
    }
}

    

void SetAnimateState()
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


void SetActionState()
{
    switch (actionState)
    {
        case ActionState.MoveToLocation():
            animationState = AnimationState.Walk;
            break;

        case ActionState.Pursue:
            
            break;

        case ActionState.NearEnemy:
            // important to make a distinction here of being "near the enemy" instead of just attack, that way we will auto acquire, or do any of the near enemy state settings, like be in defensive or aggressive mode
            
            // so how do we combine being near the enemy and pursuing,
            // especially when the player wants to turn their units around
            // well how about we keep pursuing the nearest enemy within our "line of sight" box collider

            break;
        
    }
}


void CheckStateRoutines()
{
    // started off in idle set in the Load() method

    // if units are selected we check if there is an additional mouse click to move them there
    // the CheckForMouseClickMovement() will set the action state to MoveToLocation when there is a click
    // important for CheckforMouseClickMovement() to only set the parent action state, and not the animation state,
    // since when acquiring an enemy attacking could be a combination of walking and attacking animations
    // though at this point we will not have an attacking and moving simultaneous action, though this would require an additional action state,
    // and an additional animation state, which would be moving and attacking, so we can't simply use our existing animation states for this

    if (team == Team.Player && selected)
    {
        CheckForMouseClickMovement();
    }

    // we also need to tell if there is an enemy unit there, which we will always attack when close by, unless running away (action state: escape)
    // we can detect an enemy at any time, even if they creep up on us, we would beging to automatically attack
    // however later we can add different pursue / defensive states like in LOTR, but perhaps not immediately for MVP

    // so we are always checking for this,
    // although this is done by OnTriggerEnter, so we will not define, if (DetectedEnemy()), here

    // so the pursue state is set automatically by the is trigger collider
    // now will there be any problems with multiple units? how to handle which ones to acquire
    // and if the player keeps the unit engaged at one unit,
    // and another unit comes in, we don't want to engage this new unit, perhaps need a bool engaged for this
    // so also when are done fighting the unit, we can set the engaged to off, and then allow registering new enemies coming into the collider
    // and pursuing them

    // so a bool engaged of true, prevents auto acquiring new enemies coming into the collider, unless the player points the unit away,
    // to engage the new set of enemies
}


void SetStates()
{
    SetAnimateState();
    SetActionState();
}


void PerformRoutines()
{
    CheckStateRoutines();
    SetStates();
}



void Update()
{
    PerformRoutines();
}

