# teleportation-runes
## Abstract

The main idea behind this mod - is to give users a way to quickly travel across the world, without running 10 times thru the same locations.

## Logic

This mod will include 2 items - teleportation beacon and rune stone.

**Teleportation beacon:**

1. To be placed requires block beneath it.
2. Once placed can be removed at any time. It will detach all rune stones.

**Rune stone:**

1. When crafted - rune stone has generic name "Teleportation Rune Stone"
2. Holding rune stone in right hand, and right clicking on beacon should trigger "Name rune" window - which will require user's entry.
3. When the name is entered, the rune becomes attached to the beacon, until the beacon is removed. In this case - rune loses its name or becomes unusable (need to check what is easier to implement)
4. Holding "RMB" with rune equipped will start teleport countdown based on the material used to craft it:
    1. Coper - 5 seconds
    2. Bronze - 4 seconds
    3. Iron - 3 seconds
    4. Steel - 2 second

## Items and Recipes

### 1. Beacon

Can be crafted out of the following materials:

- Stone block (2), allowed variants are: "granite", "andesite", "basalt"
- Temporal gear
- Chisel
- Hammer
- Metal plate, allowed variants are: "copper", "tin bronze", "bismuth bronze", "black bronze", "silver", "gold", "iron", "steel"

Stack size - 8.

Materials used in crafting affect only appearance of the block. Behavior is unchanged. Added variants for decoration purposes. 

### 2. Rune stone nest

Can be crafted out of following metals:

- copper
- tinbronze
- bismuthbronze
- blackbronze
- iron
- steel

Stack size - 32.

Metal wrapper for teleportation rune can be done using **smiting** mechanics:

🔳🔳🔳🔳🔳
🔳🔳🔳🔳🔳
🔳🔳⬜🔳🔳
🔳🔳🔳🔳🔳
🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳
🔳⬜⬜⬜🔳
🔳⬜⬜⬜🔳
🔳⬜⬜⬜🔳
🔳🔳🔳🔳🔳

Another way to create rune stone nest - is to use **clay molds.** 

One version of the mold allows creation of 2 nests out of 100 metal units:

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳


🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜🔳🔳🔳⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜🔳🔳🔳⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜🔳🔳🔳⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜🔳🔳🔳⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜🔳🔳🔳⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜🔳🔳🔳⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳


🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜🔳⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜🔳⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳⬜⬜⬜⬜⬜🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳


Another version allows to create 4 nests out of 200 units of metal.

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳


🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜🔳🔳🔳⬜🔳🔳⬜🔳🔳🔳⬜🔳

🔳⬜🔳🔳🔳⬜🔳🔳⬜🔳🔳🔳⬜🔳

🔳⬜🔳🔳🔳⬜🔳🔳⬜🔳🔳🔳⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜🔳🔳🔳⬜🔳🔳⬜🔳🔳🔳⬜🔳

🔳⬜🔳🔳🔳⬜🔳🔳⬜🔳🔳🔳⬜🔳

🔳⬜🔳🔳🔳⬜🔳🔳⬜🔳🔳🔳⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳


🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜⬜🔳⬜⬜🔳🔳⬜⬜🔳⬜⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜⬜🔳⬜⬜🔳🔳⬜⬜🔳⬜⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳⬜⬜⬜⬜⬜🔳🔳⬜⬜⬜⬜⬜🔳

🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳🔳


### 3. Raw rune stone core

Crafted out of clay - core of the rune stone.

Stack size - 32.

There are 3 recipes for clay forming, first requires 2 layers of clay to be placed in the following shape:

🔳🔳🔳

🔳🔳🔳

🔳🔳🔳


⬜⬜⬜

⬜🔳⬜

⬜⬜⬜

Another recipe allows to craft 2 cores:

🔳🔳🔳⬜🔳🔳🔳

🔳🔳🔳⬜🔳🔳🔳

🔳🔳🔳⬜🔳🔳🔳


⬜⬜⬜⬜⬜⬜⬜

⬜🔳⬜⬜⬜🔳⬜

⬜⬜⬜⬜⬜⬜⬜

And another recipe allows to craft 4 cores:

🔳🔳🔳⬜🔳🔳🔳

🔳🔳🔳⬜🔳🔳🔳

🔳🔳🔳⬜🔳🔳🔳

⬜⬜⬜⬜⬜⬜⬜

🔳🔳🔳⬜🔳🔳🔳

🔳🔳🔳⬜🔳🔳🔳

🔳🔳🔳⬜🔳🔳🔳


⬜⬜⬜⬜⬜⬜⬜

⬜🔳⬜⬜⬜🔳⬜

⬜⬜⬜⬜⬜⬜⬜

⬜⬜⬜⬜⬜⬜⬜

⬜⬜⬜⬜⬜⬜⬜

⬜🔳⬜⬜⬜🔳⬜

⬜⬜⬜⬜⬜⬜⬜

### 4. Raw rune stone

- Raw rune stone core
- Rune stone nest
- Temporal gear
- Knife

### 5. Rune stone

- Fire 950 degrees
- Raw rune stone

 

## Animations and textures

Ideally we can add new animation of user being teleported. But initially we can use slowed down "eating" animation
