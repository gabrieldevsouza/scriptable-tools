# Scriptable Tools

A lightweight, modular **ScriptableObject-oriented architecture** toolkit for Unity.

Scriptable Tools provides a small set of building blocks to keep systems **decoupled**, **designer-friendly**, and **reusable**, without resorting to singletons or large “god managers”.

Key modules:
- **Scriptable Variables** (with Constant-or-Variable references)
- **Scriptable Events** (Inspector-wired broadcast with payloads)
- **Runtime Sets** (live collections of active objects)
- **Relays & Listeners** (bridge state changes to UnityEvents or GameEvents)

Inspired by Ryan Hipple’s ScriptableObject architecture patterns and Unity’s guidance for ScriptableObjects.


## Table of Contents
- [Getting started](#getting-started)
  - [Requirements](#requirements)
  - [Install (UPM)](#install-upm)
- [Concepts](#concepts)
  - [1) Scriptable Variables](#1-scriptable-variables)
  - [2) Scriptable Events](#2-scriptable-events)
  - [3) Runtime Sets](#3-runtime-sets)
  - [4) Relays & Change Listeners](#4-relays--change-listeners)
- [Quick recipes](#quick-recipes)
- [Best practices](#best-practices)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)
- [Credits](#credits)

Getting started
---

### Requirements
- Unity version: **(unknown)**. This project uses standard ScriptableObjects + generics.
- Works with both Designer-centric workflows (Inspector wiring) and Programmer-centric workflows (typed APIs).

### Install (UPM)
#### Install via git URL
Requires a version of unity that supports path query parameter for git packages (Unity >= 2019.3.4f1, Unity >= 2020.1a21). You can add https://github.com/gabrieldevsouza/scriptable-tools.git?path=Packages/com.gabisou.scriptabletools to Package Manager.

###### Path to copy:
```
https://github.com/gabrieldevsouza/scriptable-tools.git?path=Packages/com.gabisou.scriptabletools
```


## Concepts

## 1) Scriptable Variables

Scriptable Variables are ScriptableObject assets that hold a value (`int`, `float`, `bool`, `string`, `Vector2/3`, `GameObject`, `Transform`, etc.).

A **Reference wrapper** lets designers choose **Constant** or **Variable** mode per-field in the Inspector.

### Core types (generic)

```csharp
// ScriptableVariable<T>
public abstract class ScriptableVariable<T> : ScriptableObject
{
    [SerializeField] private T initialValue;
    [NonSerialized] private T _value;

    public event System.Action<T> OnValueChanged;

    public T Value
    {
        get => _value;
        set { _value = value; OnValueChanged?.Invoke(_value); }
    }

    protected virtual void OnEnable() => _value = initialValue; // reset on play
}

// ScriptableVariableReference<T>
[System.Serializable]
public sealed class ScriptableVariableReference<T>
{
    [SerializeField] private bool useConstant = true;
    [SerializeField] private T constantValue;
    [SerializeField] private ScriptableVariable<T> variable;

    public T Value
    {
        get => (useConstant || variable == null) ? constantValue : variable.Value;
        set { if (useConstant || variable == null) constantValue = value; else variable.Value = value; }
    }
}
```

### Concrete example

```csharp
[CreateAssetMenu(fileName="SOBJ_FloatVariable", menuName="Scriptable Tools/Variables/Float")]
public sealed class FloatVariable : ScriptableVariable<float> { }

[System.Serializable]
public sealed class FloatReference : ScriptableVariableReference<float> { }
```

### Quick usage

```csharp
public sealed class ScoreSystem : MonoBehaviour
{
    [SerializeField] private FloatVariable score;
    public void AddPoints(float amount) => score.Value += amount;
}

public sealed class ScoreLabel : MonoBehaviour
{
    [SerializeField] private FloatVariable score;
    [SerializeField] private TMPro.TextMeshProUGUI label;

    void OnEnable()  => score.OnValueChanged += HandleScore;
    void OnDisable() => score.OnValueChanged -= HandleScore;

    void HandleScore(float v) => label.text = v.ToString("0");
}
```

## 2) Scriptable Events

Scriptable Events are ScriptableObject assets that broadcast events (optionally with payloads) to any number of listeners.

Listeners are MonoBehaviours that invoke **UnityEvents**, so designers can wire responses in the Inspector.

### Core types (generic)

```csharp
public class GameEvent<T> : ScriptableObject
{
    private readonly System.Collections.Generic.List<GameEventListener<T>> listeners = new();

    public void Raise(T value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(value);
    }

    public void RegisterListener(GameEventListener<T> l)
    { if (!listeners.Contains(l)) listeners.Add(l); }

    public void UnregisterListener(GameEventListener<T> l)
    { listeners.Remove(l); }
}

public class GameEventListener<T> : MonoBehaviour
{
    [SerializeField] private GameEvent<T> gameEvent;
    [SerializeField] private UnityEngine.Events.UnityEvent<T> response;

    void OnEnable()
    {
        if (gameEvent == null) return;
        gameEvent.RegisterListener(this);
    }

    void OnDisable()
    {
        if (gameEvent == null) return;
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T value) => response?.Invoke(value);
}
```

### Void events (no payload)

```csharp
public readonly struct Vazio { } // acts like void for generics

[CreateAssetMenu(fileName="SOBJ_VazioGameEvent", menuName="Scriptable Tools/Events/Vazio")]
public sealed class VazioGameEvent : GameEvent<Vazio>
{
    public void Raise() => Raise(default);
}
```

### Quick usage

```csharp
public sealed class Goal : MonoBehaviour
{
    [SerializeField] private VazioGameEvent onGoalScored;
    void Score() => onGoalScored.Raise();
}
```

## 3) Runtime Sets

Runtime Sets are ScriptableObjects that track a **live collection** of items.  
Objects self-register on `OnEnable` and unregister on `OnDisable`.

### Core type (List-backed)

```csharp
public abstract class RuntimeSet<T> : ScriptableObject
{
    [System.NonSerialized] private readonly System.Collections.Generic.List<T> items = new();

    public System.Collections.Generic.IReadOnlyList<T> Items => items;
    public int Count => items.Count;

    protected virtual void OnEnable() => items.Clear();

    public bool Add(T item)    { if (items.Contains(item)) return false; items.Add(item); return true; }
    public bool Remove(T item) => items.Remove(item);
    public void Clear()        => items.Clear();
}
```

### Concrete example

```csharp
[CreateAssetMenu(fileName="SOBJ_GameObjectRuntimeSet", menuName="Scriptable Tools/Runtime Sets/GameObject Set")]
public sealed class GameObjectRuntimeSet : RuntimeSet<GameObject> { }

[AddComponentMenu("Scriptable Tools/Runtime Sets/Register Self To GameObject Set")]
public sealed class RegisterSelfToGameObjectSet : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet set;
    void OnEnable()  { if (set != null) set.Add(gameObject); }
    void OnDisable() { if (set != null) set.Remove(gameObject); }
}
```

## 4) Relays & Change Listeners

Relays and listeners reduce glue code:

- **VariableChangeListener**: subscribe to a variable and invoke a UnityEvent.
    
- **VariableToEventRelay**: subscribe to a variable and raise a GameEvent on change.
    

```csharp
public class VariableChangeListener<T> : MonoBehaviour
{
    [SerializeField] private ScriptableVariable<T> variable;
    [SerializeField] private UnityEngine.Events.UnityEvent<T> onValueChanged;
    [SerializeField] private bool invokeOnEnableWithCurrent = true;

    void OnEnable()
    {
        if (variable == null) return;
        variable.OnValueChanged += Handle;
        if (invokeOnEnableWithCurrent) Handle(variable.Value);
    }

    void OnDisable()
    {
        if (variable == null) return;
        variable.OnValueChanged -= Handle;
    }

    void Handle(T v) => onValueChanged?.Invoke(v);
}

public class VariableToEventRelay<T> : MonoBehaviour
{
    [SerializeField] private ScriptableVariable<T> variable;
    [SerializeField] private GameEvent<T> gameEvent;
    [SerializeField] private bool raiseOnEnableWithCurrent = false;

    void OnEnable()
    {
        if (variable == null || gameEvent == null) return;
        variable.OnValueChanged += gameEvent.Raise;
        if (raiseOnEnableWithCurrent) gameEvent.Raise(variable.Value);
    }

    void OnDisable()
    {
        if (variable == null || gameEvent == null) return;
        variable.OnValueChanged -= gameEvent.Raise;
    }
}
```

## Quick recipes

- “I need a timer that designers can tweak and UI updates automatically.”
    
    - Use a `FloatVariable` for duration/current time.
        
    - Use `VariableChangeListener<float>` to update UI without UI code.
        
- “I want to notify multiple systems when a hidden object is found.”
    
    - Use a `VazioGameEvent`, call `Raise()` when found, wire multiple listeners (UI, audio, VFX, score).
        
- “I need to highlight all remaining hidden objects.”
    
    - Register objects into a `GameObjectRuntimeSet` and iterate `Items` from a hint system.
        

## Best practices

- Do not turn every field into a ScriptableVariable.
    
    - Keep local-only values as normal serialized fields.
        
- Do not raise events every frame.
    
    - Events are for gameplay signals, not continuous streams.
        
- Use Runtime Sets for global live collections.
    
    - Avoid them for tiny private lists inside one component.
        
- If you have very large sets and heavy `Contains` usage, consider a HashSet-backed runtime set variant.
    
    - Otherwise, List-backed sets are usually perfect.
        

## Roadmap

- Additional built-in variable types and event types.
    
- Editor utilities (optional): inspectors, runtime debugging views, create menus, validation helpers.
    
- Documentation pages with common patterns and anti-patterns.
    

## Contributing

Issues and PRs are welcome.

- Keep APIs small and composable.
    
- Prefer clear naming and minimal magic.
    
- Include a usage snippet when adding a new core feature.
    

## License

Licensed under the Apache License 2.0.

See LICENSE for the full text and NOTICE for required attribution.

## Credits

- Ryan Hipple: ScriptableObject architecture patterns (Unite 2017).
    
- Unity: ScriptableObject guidance and best practices.
    