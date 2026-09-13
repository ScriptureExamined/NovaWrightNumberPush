---
title: "Number Push Level 9"
layout: post
level: 9
complexity: 9
---

# Number Push Level 9

## Level Information

- **Complexity:** 9
- **Board:** 12 × 14
- **Crates:** 4
- **Interior Walls:** 26
- **Minimum Solution:** 13 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (3, 6), Distance 4
- **Crate 2:** Position (4, 6), Distance 3
- **Crate 3:** Position (11, 4), Distance 2
- **Crate 4:** Position (7, 3), Distance 1

## Goals

- **Goal 1:** (5, 8)
- **Goal 2:** (7, 4)
- **Goal 3:** (4, 3)
- **Goal 4:** (7, 6)

## Solution

**Minimum pushes:** 13

### Push 1

- **Crate:** 2
- **Direction:** Up
- **Distance:** 3
- **Player Start:** (1, 8)
- **Player Push Position:** (4, 7)
- **Crate Start:** (4, 6)
- **Crate End:** (4, 3)
- **Player Walk:** (1, 8) → (1, 7) → (2, 7) → (3, 7) → (4, 7)

### Push 2

- **Crate:** 3
- **Direction:** Down
- **Distance:** 2
- **Player Start:** (4, 6)
- **Player Push Position:** (11, 3)
- **Crate Start:** (11, 4)
- **Crate End:** (11, 6)
- **Player Walk:** (4, 6) → (5, 6) → (6, 6) → (6, 5) → (7, 5) → (7, 4) → (8, 4) → (8, 3) → (9, 3) → (10, 3) → (11, 3)

### Push 3

- **Crate:** 3
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (11, 4)
- **Player Push Position:** (12, 6)
- **Crate Start:** (11, 6)
- **Crate End:** (9, 6)
- **Player Walk:** (11, 4) → (11, 5) → (12, 5) → (12, 6)

### Push 4

- **Crate:** 3
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (11, 6)
- **Player Push Position:** (9, 7)
- **Crate Start:** (9, 6)
- **Crate End:** (9, 4)
- **Player Walk:** (11, 6) → (11, 5) → (11, 4) → (10, 4) → (9, 4) → (8, 4) → (7, 4) → (7, 5) → (7, 6) → (7, 7) → (6, 7) → (6, 8) → (6, 9) → (7, 9) → (7, 10) → (8, 10) → (9, 10) → (9, 9) → (9, 8) → (9, 7)

### Push 5

- **Crate:** 4
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (9, 6)
- **Player Push Position:** (7, 2)
- **Crate Start:** (7, 3)
- **Crate End:** (7, 4)
- **Player Walk:** (9, 6) → (9, 5) → (10, 5) → (10, 4) → (10, 3) → (9, 3) → (9, 2) → (8, 2) → (7, 2)

### Push 6

- **Crate:** 4
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (7, 4)
- **Crate End:** (7, 5)
- **Player Walk:** (7, 3)

### Push 7

- **Crate:** 4
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 4)
- **Player Push Position:** (7, 4)
- **Crate Start:** (7, 5)
- **Crate End:** (7, 6)
- **Player Walk:** (7, 4)

### Push 8

- **Crate:** 3
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (7, 5)
- **Player Push Position:** (10, 4)
- **Crate Start:** (9, 4)
- **Crate End:** (7, 4)
- **Player Walk:** (7, 5) → (7, 4) → (7, 3) → (8, 3) → (9, 3) → (10, 3) → (10, 4)

### Push 9

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 4)
- **Player Push Position:** (8, 6)
- **Crate Start:** (7, 6)
- **Crate End:** (6, 6)
- **Player Walk:** (9, 4) → (9, 5) → (9, 6) → (8, 6)

### Push 10

- **Crate:** 4
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 6)
- **Player Push Position:** (6, 5)
- **Crate Start:** (6, 6)
- **Crate End:** (6, 7)
- **Player Walk:** (7, 6) → (7, 5) → (6, 5)

### Push 11

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (6, 6)
- **Player Push Position:** (7, 7)
- **Crate Start:** (6, 7)
- **Crate End:** (5, 7)
- **Player Walk:** (6, 6) → (7, 6) → (7, 7)

### Push 12

- **Crate:** 1
- **Direction:** Right
- **Distance:** 4
- **Player Start:** (6, 7)
- **Player Push Position:** (2, 6)
- **Crate Start:** (3, 6)
- **Crate End:** (7, 6)
- **Player Walk:** (6, 7) → (6, 6) → (5, 6) → (4, 6) → (4, 7) → (3, 7) → (2, 7) → (2, 6)

### Push 13

- **Crate:** 4
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (3, 6)
- **Player Push Position:** (5, 6)
- **Crate Start:** (5, 7)
- **Crate End:** (5, 8)
- **Player Walk:** (3, 6) → (4, 6) → (5, 6)

## Generation Settings

- **Target Pushes:** 8–19
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–9
- **Interior Wall Range:** 18–40

