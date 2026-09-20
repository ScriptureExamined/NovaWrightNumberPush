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
- **Interior Walls:** 26
- **Minimum Solution:** 12 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (9, 6), Distance 1
- **Crate 2:** Position (3, 5), Distance 1
- **Crate 3:** Position (6, 6), Distance 1
- **Crate 4:** Position (9, 3), Distance 1

## Goals

- **Goal 1:** (3, 7)
- **Goal 2:** (9, 2)
- **Goal 3:** (7, 8)
- **Goal 4:** (8, 3)

## Solution

**Minimum pushes:** 12

### Push 1

- **Crate:** 2
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (7, 2)
- **Player Push Position:** (3, 4)
- **Crate Start:** (3, 5)
- **Crate End:** (3, 6)
- **Player Walk:** (7, 2) → (7, 1) → (6, 1) → (5, 1) → (4, 1) → (3, 1) → (3, 2) → (3, 3) → (3, 4)

### Push 2

- **Crate:** 2
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (3, 5)
- **Player Push Position:** (3, 5)
- **Crate Start:** (3, 6)
- **Crate End:** (3, 7)
- **Player Walk:** (3, 5)

### Push 3

- **Crate:** 3
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (3, 6)
- **Player Push Position:** (6, 7)
- **Crate Start:** (6, 6)
- **Crate End:** (6, 5)
- **Player Walk:** (3, 6) → (3, 5) → (4, 5) → (5, 5) → (5, 6) → (5, 7) → (6, 7)

### Push 4

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (6, 6)
- **Player Push Position:** (9, 5)
- **Crate Start:** (9, 6)
- **Crate End:** (9, 7)
- **Player Walk:** (6, 6) → (7, 6) → (8, 6) → (8, 5) → (9, 5)

### Push 5

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (9, 6)
- **Player Push Position:** (9, 6)
- **Crate Start:** (9, 7)
- **Crate End:** (9, 8)
- **Player Walk:** (9, 6)

### Push 6

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 7)
- **Player Push Position:** (10, 8)
- **Crate Start:** (9, 8)
- **Crate End:** (8, 8)
- **Player Walk:** (9, 7) → (10, 7) → (10, 8)

### Push 7

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 8)
- **Player Push Position:** (9, 8)
- **Crate Start:** (8, 8)
- **Crate End:** (7, 8)
- **Player Walk:** (9, 8)

### Push 8

- **Crate:** 3
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (8, 8)
- **Player Push Position:** (6, 6)
- **Crate Start:** (6, 5)
- **Crate End:** (6, 4)
- **Player Walk:** (8, 8) → (8, 7) → (8, 6) → (7, 6) → (6, 6)

### Push 9

- **Crate:** 3
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (6, 5)
- **Player Push Position:** (6, 5)
- **Crate Start:** (6, 4)
- **Crate End:** (6, 3)
- **Player Walk:** (6, 5)

### Push 10

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (6, 4)
- **Player Push Position:** (5, 3)
- **Crate Start:** (6, 3)
- **Crate End:** (7, 3)
- **Player Walk:** (6, 4) → (6, 5) → (5, 5) → (4, 5) → (3, 5) → (3, 4) → (3, 3) → (3, 2) → (3, 1) → (4, 1) → (5, 1) → (5, 2) → (5, 3)

### Push 11

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (6, 3)
- **Player Push Position:** (6, 3)
- **Crate Start:** (7, 3)
- **Crate End:** (8, 3)
- **Player Walk:** (6, 3)

### Push 12

- **Crate:** 4
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (9, 4)
- **Crate Start:** (9, 3)
- **Crate End:** (9, 2)
- **Player Walk:** (7, 3) → (6, 3) → (6, 4) → (6, 5) → (6, 6) → (7, 6) → (8, 6) → (8, 5) → (9, 5) → (9, 4)

## Generation Settings

- **Target Pushes:** 9–21
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–11
- **Interior Wall Range:** 22–48

