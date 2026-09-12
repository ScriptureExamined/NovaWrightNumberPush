---
title: "Number Push Level 3"
layout: post
level: 3
complexity: 3
---

# Number Push Level 3

## Level Information

- **Complexity:** 3
- **Board:** 10 × 12
- **Crates:** 2
- **Interior Walls:** 7
- **Minimum Solution:** 12 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (4, 3), Distance 2
- **Crate 2:** Position (9, 4), Distance 1

## Goals

- **Goal 1:** (4, 4)
- **Goal 2:** (2, 7)

## Solution

**Minimum pushes:** 12

### Push 1

- **Crate:** 1
- **Direction:** Down
- **Distance:** 2
- **Player Start:** (4, 5)
- **Player Push Position:** (4, 2)
- **Crate Start:** (4, 3)
- **Crate End:** (4, 5)
- **Player Walk:** (4, 5) → (4, 4) → (3, 4) → (3, 3) → (3, 2) → (4, 2)

### Push 2

- **Crate:** 1
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (4, 3)
- **Player Push Position:** (3, 5)
- **Crate Start:** (4, 5)
- **Crate End:** (6, 5)
- **Player Walk:** (4, 3) → (4, 4) → (3, 4) → (3, 5)

### Push 3

- **Crate:** 1
- **Direction:** Down
- **Distance:** 2
- **Player Start:** (4, 5)
- **Player Push Position:** (6, 4)
- **Crate Start:** (6, 5)
- **Crate End:** (6, 7)
- **Player Walk:** (4, 5) → (4, 4) → (5, 4) → (6, 4)

### Push 4

- **Crate:** 1
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (6, 5)
- **Player Push Position:** (7, 7)
- **Crate Start:** (6, 7)
- **Crate End:** (4, 7)
- **Player Walk:** (6, 5) → (6, 6) → (7, 6) → (7, 7)

### Push 5

- **Crate:** 1
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (6, 7)
- **Player Push Position:** (5, 7)
- **Crate Start:** (4, 7)
- **Crate End:** (2, 7)
- **Player Walk:** (6, 7) → (5, 7)

### Push 6

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (4, 7)
- **Player Push Position:** (9, 5)
- **Crate Start:** (9, 4)
- **Crate End:** (9, 3)
- **Player Walk:** (4, 7) → (5, 7) → (5, 6) → (5, 5) → (6, 5) → (7, 5) → (8, 5) → (9, 5)

### Push 7

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 4)
- **Player Push Position:** (10, 3)
- **Crate Start:** (9, 3)
- **Crate End:** (8, 3)
- **Player Walk:** (9, 4) → (10, 4) → (10, 3)

### Push 8

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 3)
- **Player Push Position:** (9, 3)
- **Crate Start:** (8, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (9, 3)

### Push 9

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (8, 3)
- **Player Push Position:** (8, 3)
- **Crate Start:** (7, 3)
- **Crate End:** (6, 3)
- **Player Walk:** (8, 3)

### Push 10

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (6, 3)
- **Crate End:** (5, 3)
- **Player Walk:** (7, 3)

### Push 11

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (6, 3)
- **Player Push Position:** (6, 3)
- **Crate Start:** (5, 3)
- **Crate End:** (4, 3)
- **Player Walk:** (6, 3)

### Push 12

- **Crate:** 2
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (5, 3)
- **Player Push Position:** (4, 2)
- **Crate Start:** (4, 3)
- **Crate End:** (4, 4)
- **Player Walk:** (5, 3) → (5, 4) → (4, 4) → (3, 4) → (3, 3) → (3, 2) → (4, 2)

## Generation Settings

- **Target Pushes:** 12–27
- **Crate Count Range:** 2–3
- **Crate Distance Range:** 1–3
- **Interior Wall Range:** 6–16

