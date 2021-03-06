# RPGCore.View

RPGCore.View is a mechanism for creating game worlds where in every change can be described by a network packet.

Important characteristics of a view:

- A view can be replicated as needed.
- Network packets generated by the view are serializable.
- Every change to the view can be represented by a network packet.

## Implementation

> `View`\
> A representation of the game world.

> `ISyncField`\
> Every item in the view hierarchy inherits from the `ISyncField`.
>
> >`SubscribeDispatcher(ViewDispatcher, EntityPath)`\
> > Subscribes a view dispatcher to this element of the view.
>
> >`UnsubscribeDispatcher(ViewDispatcher, EntityPath)`\
> > Unsubscribes a view dispatcher from this element of the view.
>
> > `Apply(ViewDelta)`\
>> Applies a `ViewDelta` generated by a `ViewDispatcher` to a field.
>
> > `Capture()`\
> > Captures the current state of this element in the view.

> `ViewDispatcher`\
> Responcible for replicating and tracking changes to a view.

### Example

- `ISyncView View`
  - `EventCollection<Entity> Entities`
    - `[0x00000001] = ViewCharacter`
      - `EventField<string> Name`
      - `EventField<Vector3> Position`
      - `TraitCollection Traits`
    - `[0x00000002] = Profile`
      - `EventField<string> Name`
      - `EventField<Vector3> Position`
      - `EventField<EntityRef> Traits`
