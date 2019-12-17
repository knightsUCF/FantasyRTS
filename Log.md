# Getting the Basic Unit Movement Correct

So we are going to just focus on getting the units working correctly. This goes along with the John Romero approach of: 

1. no prototypes
2. build on a solid foundation

Here are the things we want to take care of:



- unit selection with a rectangle

- prevent units from bumping into each other

- smooth movement controls, animation blending between the states (https://www.youtube.com/watch?v=YgaLKrSApWM)

- getting the Character.cs class ready to take care of all unit cases, perhaps we will have a switch method based on the unit type, since different types will have different animations, and we can reference a number of different animators for special use cases, this way we can be a little cleaner than inheritance


Once we finish unit movement, then we should focus on a modular way of working on the building structures, especially building requirements.

This way we can use the "divide and conquer" approach.

Modular sections we can apply the divide and conquer approach to: 

- unit movement / combat
- building
- multiplayer


# Dynamic Formations

(probably will not be in MVP)

https://www.youtube.com/watch?v=9hsxjfUqPcE






