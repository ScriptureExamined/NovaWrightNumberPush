---
title: "Number Push Level 14"
layout: post
level: 14
complexity: 14
---

# Number Push Level 14

## Level Information

- **Complexity:** 14
- **Board:** 12 × 14
- **Crates:** 5
- **Interior Walls:** 29
- **Minimum Solution:** 16 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (2, 7), Distance 1
- **Crate 2:** Position (11, 4), Distance 1
- **Crate 3:** Position (7, 7), Distance 1
- **Crate 4:** Position (5, 7), Distance 1
- **Crate 5:** Position (5, 6), Distance 1

## Goals

- **Goal 1:** (9, 9)
- **Goal 2:** (1, 10)
- **Goal 3:** (4, 7)
- **Goal 4:** (10, 5)
- **Goal 5:** (4, 6)

## Solution

**Minimum pushes:** 16

### Push 1

- **Crate:** 2
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (12, 5)
- **Player Push Position:** (11, 3)
- **Crate Start:** (11, 4)
- **Crate End:** (11, 5)
- **Player Walk:** (12, 5) → (12, 4) → (12, 3) → (11, 3)

### Push 2

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (11, 4)
- **Player Push Position:** (12, 5)
- **Crate Start:** (11, 5)
- **Crate End:** (10, 5)
- **Player Walk:** (11, 4) → (12, 4) → (12, 5)

### Push 3

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (11, 5)
- **Player Push Position:** (7, 6)
- **Crate Start:** (7, 7)
- **Crate End:** (7, 8)
- **Player Walk:** (11, 5) → (11, 4) → (11, 3) → (11, 2) → (10, 2) → (9, 2) → (8, 2) → (8, 3) → (8, 4) → (7, 4) → (7, 5) → (7, 6)

### Push 4

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 7)
- **Player Push Position:** (7, 7)
- **Crate Start:** (7, 8)
- **Crate End:** (7, 9)
- **Player Walk:** (7, 7)

### Push 5

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (7, 8)
- **Player Push Position:** (6, 9)
- **Crate Start:** (7, 9)
- **Crate End:** (8, 9)
- **Player Walk:** (7, 8) → (6, 8) → (6, 9)

### Push 6

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (7, 9)
- **Player Push Position:** (7, 9)
- **Crate Start:** (8, 9)
- **Crate End:** (9, 9)
- **Player Walk:** (7, 9)

### Push 7

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (8, 9)
- **Player Push Position:** (6, 7)
- **Crate Start:** (5, 7)
- **Crate End:** (4, 7)
- **Player Walk:** (8, 9) → (8, 8) → (8, 7) → (7, 7) → (6, 7)

### Push 8

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (5, 7)
- **Player Push Position:** (5, 7)
- **Crate Start:** (4, 7)
- **Crate End:** (3, 7)
- **Player Walk:** (5, 7)

### Push 9

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (4, 7)
- **Player Push Position:** (2, 6)
- **Crate Start:** (2, 7)
- **Crate End:** (2, 8)
- **Player Walk:** (4, 7) → (4, 6) → (3, 6) → (2, 6)

### Push 10

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (2, 7)
- **Player Push Position:** (2, 7)
- **Crate Start:** (2, 8)
- **Crate End:** (2, 9)
- **Player Walk:** (2, 7)

### Push 11

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (2, 8)
- **Player Push Position:** (2, 8)
- **Crate Start:** (2, 9)
- **Crate End:** (2, 10)
- **Player Walk:** (2, 8)

### Push 12

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (2, 9)
- **Player Push Position:** (3, 10)
- **Crate Start:** (2, 10)
- **Crate End:** (1, 10)
- **Player Walk:** (2, 9) → (3, 9) → (3, 10)

### Push 13

- **Crate:** 4
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (2, 10)
- **Player Push Position:** (3, 8)
- **Crate Start:** (3, 7)
- **Crate End:** (3, 6)
- **Player Walk:** (2, 10) → (2, 9) → (2, 8) → (3, 8)

### Push 14

- **Crate:** 5
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (3, 7)
- **Player Push Position:** (6, 6)
- **Crate Start:** (5, 6)
- **Crate End:** (4, 6)
- **Player Walk:** (3, 7) → (4, 7) → (5, 7) → (6, 7) → (6, 6)

### Push 15

- **Crate:** 4
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (5, 6)
- **Player Push Position:** (3, 5)
- **Crate Start:** (3, 6)
- **Crate End:** (3, 7)
- **Player Walk:** (5, 6) → (5, 7) → (4, 7) → (3, 7) → (2, 7) → (2, 6) → (2, 5) → (3, 5)

### Push 16

- **Crate:** 4
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (3, 6)
- **Player Push Position:** (2, 7)
- **Crate Start:** (3, 7)
- **Crate End:** (4, 7)
- **Player Walk:** (3, 6) → (2, 6) → (2, 7)

## Generation Settings

- **Target Pushes:** 11–24
- **Crate Count Range:** 5–6
- **Crate Distance Range:** 1–14
- **Interior Wall Range:** 28–60

