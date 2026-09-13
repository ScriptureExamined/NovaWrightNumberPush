---
title: "Number Push Level 9"
layout: post
level: 9
complexity: 9
---

# Number Push Level 9

## Level Information

- **Complexity:** 9
- **Board:** 10 × 12
- **Crates:** 4
- **Interior Walls:** 18
- **Minimum Solution:** 10 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (7, 8), Distance 2
- **Crate 2:** Position (1, 5), Distance 1
- **Crate 3:** Position (6, 4), Distance 1
- **Crate 4:** Position (6, 3), Distance 1

## Goals

- **Goal 1:** (5, 8)
- **Goal 2:** (7, 5)
- **Goal 3:** (1, 1)
- **Goal 4:** (8, 2)

## Solution

**Minimum pushes:** 10

### Push 1

- **Crate:** 1
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (9, 3)
- **Player Push Position:** (8, 8)
- **Crate Start:** (7, 8)
- **Crate End:** (5, 8)
- **Player Walk:** (9, 3) → (9, 4) → (9, 5) → (9, 6) → (9, 7) → (8, 7) → (8, 8)

### Push 2

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 8)
- **Player Push Position:** (1, 6)
- **Crate Start:** (1, 5)
- **Crate End:** (1, 4)
- **Player Walk:** (7, 8) → (8, 8) → (8, 7) → (9, 7) → (9, 6) → (9, 5) → (9, 4) → (8, 4) → (7, 4) → (7, 5) → (6, 5) → (5, 5) → (4, 5) → (4, 6) → (3, 6) → (2, 6) → (1, 6)

### Push 3

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (1, 5)
- **Player Push Position:** (1, 5)
- **Crate Start:** (1, 4)
- **Crate End:** (1, 3)
- **Player Walk:** (1, 5)

### Push 4

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (1, 4)
- **Player Push Position:** (1, 4)
- **Crate Start:** (1, 3)
- **Crate End:** (1, 2)
- **Player Walk:** (1, 4)

### Push 5

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (1, 3)
- **Player Push Position:** (1, 3)
- **Crate Start:** (1, 2)
- **Crate End:** (1, 1)
- **Player Walk:** (1, 3)

### Push 6

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (1, 2)
- **Player Push Position:** (5, 4)
- **Crate Start:** (6, 4)
- **Crate End:** (7, 4)
- **Player Walk:** (1, 2) → (1, 3) → (1, 4) → (2, 4) → (3, 4) → (4, 4) → (5, 4)

### Push 7

- **Crate:** 4
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (6, 4)
- **Player Push Position:** (5, 3)
- **Crate Start:** (6, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (6, 4) → (5, 4) → (5, 3)

### Push 8

- **Crate:** 4
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (6, 3)
- **Player Push Position:** (6, 3)
- **Crate Start:** (7, 3)
- **Crate End:** (8, 3)
- **Player Walk:** (6, 3)

### Push 9

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (7, 4)
- **Crate End:** (7, 5)
- **Player Walk:** (7, 3)

### Push 10

- **Crate:** 4
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 4)
- **Player Push Position:** (8, 4)
- **Crate Start:** (8, 3)
- **Crate End:** (8, 2)
- **Player Walk:** (7, 4) → (8, 4)

## Generation Settings

- **Target Pushes:** 8–19
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–9
- **Interior Wall Range:** 18–40

