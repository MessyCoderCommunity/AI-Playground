# AI-Playground
This is a place where The Messy Coder community experiments with building AI Behaviours for use in Unity games. 

Our goal is to create AI Behaviours that can be reused in your chosen AI Framework. Each behaviour is a Scriptable
Object that can be inserted into a suitable container for your framework of choice.

Checkout The Messy Coder on You Tube and Twitch. Join the community and learn with us.

# Project Structure

The project consists of a number of "challenges". You can find these in `Assets/_Challenges/Scenes/Behaviour Tests`
each of the scenes contains a single challenge and a single AI Behaviour to complete that challenge.

In `Assets/_Challenges/Scenes/Behaviours to Build` contains challenges for which we do not yet have a behaviour.

In `Assets/_Challenges/Scenes/Behaviours In Progress` you can find behaviours that are in development. These will
partially work and will either be abandoned, in which case the challenge scene will go back to the 
`Assets/_Challenges/Scenes/Behaviours to Build`.

The `Common` scene should be loaded regardless of the behaviour scene you are loading up.

# Building a Behaviour

The project includes a very basic AI Runniner component. This is not an AI framework. It will only run a single
behaviour, but it is enough to build test characters for the individual behaviour scenes.

Each behaviour is a ScriptableObject that extends `GenericAiBehaviour<T>` where T is the agent type you are
expecting to work with (e.g. a GameObject, a Transfrom, a NaveMeshAgent). 

You will need to make it accessible in the Create Asset menu using something like 
`[CreateAssetMenu(fileName = "...", menuName = "Messy AI/[CATEGORY]/[BEHAVIOUR]")]`

You will need to implement the core of your behaviour in `public override void Tick(Chalkboard chalkboard)`.

Optionally you can override `public override void Initialize(GameObject agent, Chalkboard chalkboard)`. This
method will be called on startup and should be used to configure the behaviour and cache any data needed.

The best place to start looking is `LogBehaviour` which is a very basic behaviour that simply logs to the
console.

## Nested Behaviours

You can nest behaviours to provide more complex behaviours an example of this can be seen in the
`DetectInteractables` behaviour. Using this behaviour your agents have nested behaviours that will be
executed if the character does not detect an interactable within range and another that will be
used when an interactable is within range. For example, in the `SearchForPointOfInterest` challenge
the character will scan for points of interest (marked with an interactable component), if none is found
it will use the `WanderWithIntent` behaviour, if one is found then a different behaviour is used.

## Chalkboard

The chalkboard provides access to shared variables. This will always contain a variable with the name "agent"
if you request this varaible you will recieve a copy of the agent specified as the type in your class
declaration. 

In order to retrieve variables use `GetUnity<T>(int hash)` where T is the `UnityEngine.Object` you want to 
retrieve and hash is the hash of the variable name. You can also retrieve `System.Object` variables using
`GetSystem<T>(int hash)`.

To add variables to the chalkboard use `internal void Add(string name, UnityEngine.Object value)` or
`internal void Add(string name, System.Object value)`.

Variables in the chalkboard will be available to all behaviours, this is useful if you nest behaviours as
described above.

## Testing in Challenge Scenes

Our challenge scenes only use primitive behaviours. Each one will contain the challenge environment and
instructions for the challenge. These instructions will disappear in play mode, you can bring them back
in play mode by hitting the `h` key.

In the `Assets/_Challenges/Prefabs` you can find a useful objects for building test scenes.

To create an AI agent start with one of the `Assets/_Challenges/Prefabs/Characters/Basic [color] Character`
prefabs. These include the `AiBehaviourRunner` and `Chalkboard` components. Create your behaviour using
`Create -> Messy AI -> [CATEGORY] -> [BEHAVIOUR]`. Drag the instance of your behaviour into the
`AI Behaviour` property of the `AI Behaviour Runner` and hit play.

# Integration with a Full AI Frameowrk

At the very least you will need to create a bridge between the `Chalkboard` in this project and the 
equivalent in your chosen frameowrk. The subsections below outline integrations and their current
status.

When developing integrations please ensure you wrap your integration code with an approprate
`#if [DEFINE_SYMBOL] ... #endif` statement since we want everyone to be able to compile the code
even if they do not have the assets you use. If your chosen asset does not provide an appropriate
define symbol please document the one you are using below.

## Node Canvas

Node Canvas is an excellent behaviour tree asset. 

Define symbol: `NODE_CANVAS_PRESENT` (must be manually set, Node Canvas does not provide this).



