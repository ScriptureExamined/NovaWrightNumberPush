---
title: "Number Push Level 13"
layout: post
level: 13
complexity: 13
---

# Number Push Level 13

## Level Information

- **Complexity:** 13
- **Board:** 12 × 14
- **Crates:** 5
- **Interior Walls:** 26
- **Minimum Solution:** 15 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (6, 5), Distance 3
- **Crate 2:** Position (9, 3), Distance 2
- **Crate 3:** Position (8, 1), Distance 1
- **Crate 4:** Position (8, 5), Distance 1
- **Crate 5:** Position (3, 3), Distance 1

## Goals

- **Goal 1:** (9, 2)
- **Goal 2:** (7, 5)
- **Goal 3:** (4, 1)
- **Goal 4:** (6, 3)
- **Goal 5:** (5, 5)

## Solution

**Minimum pushes:** 15

### Push 1

- **Crate:** 1
- **Direction:** Down
- **Distance:** 3
- **Player Start:** (3, 5)
- **Player Push Position:** (6, 4)
- **Crate Start:** (6, 5)
- **Crate End:** (6, 8)
- **Player Walk:** (3, 5) → (3, 4) → (4, 4) → (5, 4) → (6, 4)

### Push 2

- **Crate:** 1
- **Direction:** Right
- **Distance:** 3
- **Player Start:** (6, 5)
- **Player Push Position:** (5, 8)
- **Crate Start:** (6, 8)
- **Crate End:** (9, 8)
- **Player Walk:** (6, 5) → (6, 6) → (6, 7) → (5, 7) → (5, 8)

### Push 3

- **Crate:** 1
- **Direction:** Up
- **Distance:** 3
- **Player Start:** (6, 8)
- **Player Push Position:** (9, 9)
- **Crate Start:** (9, 8)
- **Crate End:** (9, 5)
- **Player Walk:** (6, 8) → (7, 8) → (7, 9) → (8, 9) → (9, 9)

### Push 4

- **Crate:** 2
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (9, 8)
- **Player Push Position:** (10, 3)
- **Crate Start:** (9, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (9, 8) → (9, 7) → (9, 6) → (10, 6) → (11, 6) → (11, 5) → (11, 4) → (10, 4) → (10, 3)

### Push 5

- **Crate:** 2
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (9, 3)
- **Player Push Position:** (8, 3)
- **Crate Start:** (7, 3)
- **Crate End:** (5, 3)
- **Player Walk:** (9, 3) → (8, 3)

### Push 6

- **Crate:** 2
- **Direction:** Down
- **Distance:** 2
- **Player Start:** (7, 3)
- **Player Push Position:** (5, 2)
- **Crate Start:** (5, 3)
- **Crate End:** (5, 5)
- **Player Walk:** (7, 3) → (7, 2) → (7, 1) → (6, 1) → (5, 1) → (5, 2)

### Push 7

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (5, 3)
- **Player Push Position:** (9, 1)
- **Crate Start:** (8, 1)
- **Crate End:** (7, 1)
- **Player Walk:** (5, 3) → (6, 3) → (7, 3) → (7, 2) → (8, 2) → (9, 2) → (9, 1)

### Push 8

- **Crate:** 1
- **Direction:** Up
- **Distance:** 3
- **Player Start:** (8, 1)
- **Player Push Position:** (9, 6)
- **Crate Start:** (9, 5)
- **Crate End:** (9, 2)
- **Player Walk:** (8, 1) → (8, 2) → (8, 3) → (8, 4) → (9, 4) → (10, 4) → (11, 4) → (11, 5) → (11, 6) → (10, 6) → (9, 6)

### Push 9

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 5)
- **Player Push Position:** (8, 1)
- **Crate Start:** (7, 1)
- **Crate End:** (6, 1)
- **Player Walk:** (9, 5) → (9, 4) → (9, 3) → (8, 3) → (8, 2) → (8, 1)

### Push 10

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 1)
- **Player Push Position:** (7, 1)
- **Crate Start:** (6, 1)
- **Crate End:** (5, 1)
- **Player Walk:** (7, 1)

### Push 11

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (6, 1)
- **Player Push Position:** (6, 1)
- **Crate Start:** (5, 1)
- **Crate End:** (4, 1)
- **Player Walk:** (6, 1)

### Push 12

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (5, 1)
- **Player Push Position:** (9, 5)
- **Crate Start:** (8, 5)
- **Crate End:** (7, 5)
- **Player Walk:** (5, 1) → (5, 2) → (5, 3) → (5, 4) → (6, 4) → (7, 4) → (8, 4) → (9, 4) → (9, 5)

### Push 13

- **Crate:** 5
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 5)
- **Player Push Position:** (2, 3)
- **Crate Start:** (3, 3)
- **Crate End:** (4, 3)
- **Player Walk:** (8, 5) → (8, 4) → (8, 3) → (7, 3) → (6, 3) → (5, 3) → (5, 2) → (4, 2) → (3, 2) → (2, 2) → (2, 3)

### Push 14

- **Crate:** 5
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (3, 3)
- **Player Push Position:** (3, 3)
- **Crate Start:** (4, 3)
- **Crate End:** (5, 3)
- **Player Walk:** (3, 3)

### Push 15

- **Crate:** 5
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (4, 3)
- **Player Push Position:** (4, 3)
- **Crate Start:** (5, 3)
- **Crate End:** (6, 3)
- **Player Walk:** (4, 3)

## Generation Settings

- **Target Pushes:** 10–23
- **Crate Count Range:** 5–6
- **Crate Distance Range:** 1–13
- **Interior Wall Range:** 26–56

