---
title: "Number Push Level 10"
layout: post
level: 10
complexity: 10
---

# Number Push Level 10

## Level Information

- **Complexity:** 10
- **Board:** 10 × 12
- **Crates:** 4
- **Interior Walls:** 20
- **Minimum Solution:** 10 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (6, 2), Distance 3
- **Crate 2:** Position (1, 4), Distance 2
- **Crate 3:** Position (8, 2), Distance 1
- **Crate 4:** Position (8, 7), Distance 1

## Goals

- **Goal 1:** (3, 2)
- **Goal 2:** (1, 2)
- **Goal 3:** (10, 5)
- **Goal 4:** (10, 4)

## Solution

**Minimum pushes:** 10

### Push 1

- **Crate:** 1
- **Direction:** Left
- **Distance:** 3
- **Player Start:** (8, 5)
- **Player Push Position:** (7, 2)
- **Crate Start:** (6, 2)
- **Crate End:** (3, 2)
- **Player Walk:** (8, 5) → (8, 4) → (8, 3) → (7, 3) → (7, 2)

### Push 2

- **Crate:** 2
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (6, 2)
- **Player Push Position:** (1, 5)
- **Crate Start:** (1, 4)
- **Crate End:** (1, 2)
- **Player Walk:** (6, 2) → (7, 2) → (7, 3) → (7, 4) → (7, 5) → (7, 6) → (7, 7) → (6, 7) → (5, 7) → (4, 7) → (3, 7) → (2, 7) → (2, 6) → (1, 6) → (1, 5)

### Push 3

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (1, 4)
- **Player Push Position:** (8, 1)
- **Crate Start:** (8, 2)
- **Crate End:** (8, 3)
- **Player Walk:** (1, 4) → (1, 5) → (1, 6) → (1, 7) → (2, 7) → (3, 7) → (4, 7) → (4, 6) → (5, 6) → (6, 6) → (7, 6) → (7, 5) → (7, 4) → (7, 3) → (7, 2) → (7, 1) → (8, 1)

### Push 4

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (8, 2)
- **Player Push Position:** (8, 2)
- **Crate Start:** (8, 3)
- **Crate End:** (8, 4)
- **Player Walk:** (8, 2)

### Push 5

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 3)
- **Player Push Position:** (7, 4)
- **Crate Start:** (8, 4)
- **Crate End:** (9, 4)
- **Player Walk:** (8, 3) → (7, 3) → (7, 4)

### Push 6

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 4)
- **Player Push Position:** (8, 4)
- **Crate Start:** (9, 4)
- **Crate End:** (10, 4)
- **Player Walk:** (8, 4)

### Push 7

- **Crate:** 4
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (9, 4)
- **Player Push Position:** (7, 7)
- **Crate Start:** (8, 7)
- **Crate End:** (9, 7)
- **Player Walk:** (9, 4) → (9, 5) → (8, 5) → (7, 5) → (7, 6) → (7, 7)

### Push 8

- **Crate:** 4
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (8, 7)
- **Player Push Position:** (9, 8)
- **Crate Start:** (9, 7)
- **Crate End:** (9, 6)
- **Player Walk:** (8, 7) → (8, 8) → (9, 8)

### Push 9

- **Crate:** 4
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (9, 7)
- **Player Push Position:** (9, 7)
- **Crate Start:** (9, 6)
- **Crate End:** (9, 5)
- **Player Walk:** (9, 7)

### Push 10

- **Crate:** 4
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (9, 6)
- **Player Push Position:** (8, 5)
- **Crate Start:** (9, 5)
- **Crate End:** (10, 5)
- **Player Walk:** (9, 6) → (9, 7) → (8, 7) → (7, 7) → (7, 6) → (7, 5) → (8, 5)

## Generation Settings

- **Target Pushes:** 9–20
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–10
- **Interior Wall Range:** 20–44

