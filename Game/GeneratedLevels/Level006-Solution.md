---
title: "Number Push Level 6"
layout: post
level: 6
complexity: 6
---

# Number Push Level 6

## Level Information

- **Complexity:** 6
- **Board:** 10 × 12
- **Crates:** 3
- **Interior Walls:** 12
- **Minimum Solution:** 13 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (2, 5), Distance 3
- **Crate 2:** Position (9, 5), Distance 2
- **Crate 3:** Position (2, 6), Distance 1

## Goals

- **Goal 1:** (6, 8)
- **Goal 2:** (3, 5)
- **Goal 3:** (5, 8)

## Solution

**Minimum pushes:** 13

### Push 1

- **Crate:** 1
- **Direction:** Right
- **Distance:** 3
- **Player Start:** (1, 1)
- **Player Push Position:** (1, 5)
- **Crate Start:** (2, 5)
- **Crate End:** (5, 5)
- **Player Walk:** (1, 1) → (2, 1) → (2, 2) → (2, 3) → (3, 3) → (3, 4) → (3, 5) → (3, 6) → (3, 7) → (2, 7) → (1, 7) → (1, 6) → (1, 5)

### Push 2

- **Crate:** 1
- **Direction:** Down
- **Distance:** 3
- **Player Start:** (2, 5)
- **Player Push Position:** (5, 4)
- **Crate Start:** (5, 5)
- **Crate End:** (5, 8)
- **Player Walk:** (2, 5) → (3, 5) → (3, 4) → (3, 3) → (4, 3) → (5, 3) → (5, 4)

### Push 3

- **Crate:** 2
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (5, 5)
- **Player Push Position:** (9, 6)
- **Crate Start:** (9, 5)
- **Crate End:** (9, 3)
- **Player Walk:** (5, 5) → (5, 6) → (6, 6) → (7, 6) → (8, 6) → (9, 6)

### Push 4

- **Crate:** 2
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (9, 5)
- **Player Push Position:** (10, 3)
- **Crate Start:** (9, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (9, 5) → (9, 4) → (10, 4) → (10, 3)

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
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (7, 3)
- **Player Push Position:** (6, 3)
- **Crate Start:** (5, 3)
- **Crate End:** (3, 3)
- **Player Walk:** (7, 3) → (6, 3)

### Push 7

- **Crate:** 2
- **Direction:** Down
- **Distance:** 2
- **Player Start:** (5, 3)
- **Player Push Position:** (3, 2)
- **Crate Start:** (3, 3)
- **Crate End:** (3, 5)
- **Player Walk:** (5, 3) → (5, 2) → (4, 2) → (3, 2)

### Push 8

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (3, 3)
- **Player Push Position:** (2, 5)
- **Crate Start:** (2, 6)
- **Crate End:** (2, 7)
- **Player Walk:** (3, 3) → (4, 3) → (5, 3) → (5, 4) → (5, 5) → (5, 6) → (5, 7) → (4, 7) → (3, 7) → (2, 7) → (1, 7) → (1, 6) → (1, 5) → (2, 5)

### Push 9

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (2, 6)
- **Player Push Position:** (1, 7)
- **Crate Start:** (2, 7)
- **Crate End:** (3, 7)
- **Player Walk:** (2, 6) → (1, 6) → (1, 7)

### Push 10

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (2, 7)
- **Player Push Position:** (2, 7)
- **Crate Start:** (3, 7)
- **Crate End:** (4, 7)
- **Player Walk:** (2, 7)

### Push 11

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (3, 7)
- **Player Push Position:** (3, 7)
- **Crate Start:** (4, 7)
- **Crate End:** (5, 7)
- **Player Walk:** (3, 7)

### Push 12

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (4, 7)
- **Player Push Position:** (4, 7)
- **Crate Start:** (5, 7)
- **Crate End:** (6, 7)
- **Player Walk:** (4, 7)

### Push 13

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (5, 7)
- **Player Push Position:** (6, 6)
- **Crate Start:** (6, 7)
- **Crate End:** (6, 8)
- **Player Walk:** (5, 7) → (5, 6) → (6, 6)

## Generation Settings

- **Target Pushes:** 7–16
- **Crate Count Range:** 3–4
- **Crate Distance Range:** 1–6
- **Interior Wall Range:** 12–28

