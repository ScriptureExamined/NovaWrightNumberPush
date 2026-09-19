---
title: "Number Push Level 12"
layout: post
level: 12
complexity: 11
---

# Number Push Level 12

## Level Information

- **Complexity:** 11
- **Board:** 10 × 12
- **Crates:** 4
- **Interior Walls:** 27
- **Minimum Solution:** 11 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (2, 2), Distance 1
- **Crate 2:** Position (9, 4), Distance 1
- **Crate 3:** Position (8, 3), Distance 1
- **Crate 4:** Position (3, 2), Distance 1

## Goals

- **Goal 1:** (10, 1)
- **Goal 2:** (5, 3)
- **Goal 3:** (3, 1)
- **Goal 4:** (1, 2)

## Solution

**Minimum pushes:** 11

### Push 1

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (6, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (8, 3)
- **Crate End:** (9, 3)
- **Player Walk:** (6, 3) → (7, 3)

### Push 2

- **Crate:** 2
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 3)
- **Player Push Position:** (8, 4)
- **Crate Start:** (9, 4)
- **Crate End:** (10, 4)
- **Player Walk:** (8, 3) → (8, 4)

### Push 3

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (9, 4)
- **Player Push Position:** (10, 5)
- **Crate Start:** (10, 4)
- **Crate End:** (10, 3)
- **Player Walk:** (9, 4) → (9, 5) → (10, 5)

### Push 4

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (10, 4)
- **Player Push Position:** (10, 4)
- **Crate Start:** (10, 3)
- **Crate End:** (10, 2)
- **Player Walk:** (10, 4)

### Push 5

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (10, 3)
- **Player Push Position:** (10, 3)
- **Crate Start:** (10, 2)
- **Crate End:** (10, 1)
- **Player Walk:** (10, 3)

### Push 6

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (10, 2)
- **Player Push Position:** (10, 3)
- **Crate Start:** (9, 3)
- **Crate End:** (8, 3)
- **Player Walk:** (10, 2) → (10, 3)

### Push 7

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 3)
- **Player Push Position:** (9, 3)
- **Crate Start:** (8, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (9, 3)

### Push 8

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (8, 3)
- **Player Push Position:** (8, 3)
- **Crate Start:** (7, 3)
- **Crate End:** (6, 3)
- **Player Walk:** (8, 3)

### Push 9

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (6, 3)
- **Crate End:** (5, 3)
- **Player Walk:** (7, 3)

### Push 10

- **Crate:** 4
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (6, 3)
- **Player Push Position:** (3, 3)
- **Crate Start:** (3, 2)
- **Crate End:** (3, 1)
- **Player Walk:** (6, 3) → (7, 3) → (8, 3) → (8, 4) → (9, 4) → (9, 5) → (10, 5) → (10, 6) → (10, 7) → (9, 7) → (8, 7) → (8, 8) → (7, 8) → (6, 8) → (5, 8) → (4, 8) → (4, 7) → (4, 6) → (3, 6) → (3, 5) → (2, 5) → (1, 5) → (1, 4) → (1, 3) → (2, 3) → (3, 3)

### Push 11

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (3, 2)
- **Player Push Position:** (3, 2)
- **Crate Start:** (2, 2)
- **Crate End:** (1, 2)
- **Player Walk:** (3, 2)

## Generation Settings

- **Target Pushes:** 9–21
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–11
- **Interior Wall Range:** 22–48

