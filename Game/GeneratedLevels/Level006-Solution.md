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
- **Minimum Solution:** 14 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (7, 1), Distance 3
- **Crate 2:** Position (5, 3), Distance 2
- **Crate 3:** Position (4, 3), Distance 1

## Goals

- **Goal 1:** (4, 1)
- **Goal 2:** (9, 8)
- **Goal 3:** (9, 1)

## Solution

**Minimum pushes:** 14

### Push 1

- **Crate:** 1
- **Direction:** Left
- **Distance:** 3
- **Player Start:** (2, 5)
- **Player Push Position:** (8, 1)
- **Crate Start:** (7, 1)
- **Crate End:** (4, 1)
- **Player Walk:** (2, 5) → (3, 5) → (3, 4) → (4, 4) → (5, 4) → (6, 4) → (6, 3) → (7, 3) → (7, 2) → (8, 2) → (8, 1)

### Push 2

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 1)
- **Player Push Position:** (4, 2)
- **Crate Start:** (4, 3)
- **Crate End:** (4, 4)
- **Player Walk:** (7, 1) → (6, 1) → (5, 1) → (5, 2) → (4, 2)

### Push 3

- **Crate:** 2
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (4, 3)
- **Player Push Position:** (4, 3)
- **Crate Start:** (5, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (4, 3)

### Push 4

- **Crate:** 2
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (5, 3)
- **Player Push Position:** (7, 4)
- **Crate Start:** (7, 3)
- **Crate End:** (7, 1)
- **Player Walk:** (5, 3) → (5, 4) → (6, 4) → (7, 4)

### Push 5

- **Crate:** 2
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (7, 3)
- **Player Push Position:** (6, 1)
- **Crate Start:** (7, 1)
- **Crate End:** (9, 1)
- **Player Walk:** (7, 3) → (6, 3) → (5, 3) → (5, 2) → (5, 1) → (6, 1)

### Push 6

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 1)
- **Player Push Position:** (4, 3)
- **Crate Start:** (4, 4)
- **Crate End:** (4, 5)
- **Player Walk:** (7, 1) → (7, 2) → (7, 3) → (6, 3) → (5, 3) → (4, 3)

### Push 7

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (4, 4)
- **Player Push Position:** (4, 4)
- **Crate Start:** (4, 5)
- **Crate End:** (4, 6)
- **Player Walk:** (4, 4)

### Push 8

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (4, 5)
- **Player Push Position:** (4, 5)
- **Crate Start:** (4, 6)
- **Crate End:** (4, 7)
- **Player Walk:** (4, 5)

### Push 9

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (4, 6)
- **Player Push Position:** (3, 7)
- **Crate Start:** (4, 7)
- **Crate End:** (5, 7)
- **Player Walk:** (4, 6) → (3, 6) → (3, 7)

### Push 10

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (4, 7)
- **Player Push Position:** (4, 7)
- **Crate Start:** (5, 7)
- **Crate End:** (6, 7)
- **Player Walk:** (4, 7)

### Push 11

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (5, 7)
- **Player Push Position:** (5, 7)
- **Crate Start:** (6, 7)
- **Crate End:** (7, 7)
- **Player Walk:** (5, 7)

### Push 12

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (6, 7)
- **Player Push Position:** (6, 7)
- **Crate Start:** (7, 7)
- **Crate End:** (8, 7)
- **Player Walk:** (6, 7)

### Push 13

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 7)
- **Player Push Position:** (8, 6)
- **Crate Start:** (8, 7)
- **Crate End:** (8, 8)
- **Player Walk:** (7, 7) → (7, 6) → (8, 6)

### Push 14

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 7)
- **Player Push Position:** (7, 8)
- **Crate Start:** (8, 8)
- **Crate End:** (9, 8)
- **Player Walk:** (8, 7) → (7, 7) → (7, 8)

## Generation Settings

- **Target Pushes:** 7–16
- **Crate Count Range:** 3–4
- **Crate Distance Range:** 1–6
- **Interior Wall Range:** 12–28

