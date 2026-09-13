---
title: "Number Push Level 11"
layout: post
level: 11
complexity: 11
---

# Number Push Level 11

## Level Information

- **Complexity:** 11
- **Board:** 12 × 14
- **Crates:** 4
- **Interior Walls:** 22
- **Minimum Solution:** 15 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (7, 5), Distance 3
- **Crate 2:** Position (5, 5), Distance 2
- **Crate 3:** Position (7, 6), Distance 1
- **Crate 4:** Position (8, 10), Distance 1

## Goals

- **Goal 1:** (4, 8)
- **Goal 2:** (9, 5)
- **Goal 3:** (4, 9)
- **Goal 4:** (7, 10)

## Solution

**Minimum pushes:** 15

### Push 1

- **Crate:** 1
- **Direction:** Right
- **Distance:** 3
- **Player Start:** (10, 1)
- **Player Push Position:** (6, 5)
- **Crate Start:** (7, 5)
- **Crate End:** (10, 5)
- **Player Walk:** (10, 1) → (10, 2) → (10, 3) → (10, 4) → (9, 4) → (8, 4) → (7, 4) → (6, 4) → (6, 5)

### Push 2

- **Crate:** 2
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (7, 5)
- **Player Push Position:** (4, 5)
- **Crate Start:** (5, 5)
- **Crate End:** (7, 5)
- **Player Walk:** (7, 5) → (7, 4) → (6, 4) → (5, 4) → (4, 4) → (4, 5)

### Push 3

- **Crate:** 2
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (5, 5)
- **Player Push Position:** (6, 5)
- **Crate Start:** (7, 5)
- **Crate End:** (9, 5)
- **Player Walk:** (5, 5) → (6, 5)

### Push 4

- **Crate:** 2
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (7, 5)
- **Player Push Position:** (9, 6)
- **Crate Start:** (9, 5)
- **Crate End:** (9, 3)
- **Player Walk:** (7, 5) → (8, 5) → (8, 6) → (9, 6)

### Push 5

- **Crate:** 1
- **Direction:** Left
- **Distance:** 3
- **Player Start:** (9, 5)
- **Player Push Position:** (11, 5)
- **Crate Start:** (10, 5)
- **Crate End:** (7, 5)
- **Player Walk:** (9, 5) → (9, 6) → (10, 6) → (11, 6) → (11, 5)

### Push 6

- **Crate:** 1
- **Direction:** Left
- **Distance:** 3
- **Player Start:** (10, 5)
- **Player Push Position:** (8, 5)
- **Crate Start:** (7, 5)
- **Crate End:** (4, 5)
- **Player Walk:** (10, 5) → (9, 5) → (8, 5)

### Push 7

- **Crate:** 2
- **Direction:** Down
- **Distance:** 2
- **Player Start:** (7, 5)
- **Player Push Position:** (9, 2)
- **Crate Start:** (9, 3)
- **Crate End:** (9, 5)
- **Player Walk:** (7, 5) → (7, 4) → (8, 4) → (9, 4) → (10, 4) → (10, 3) → (10, 2) → (9, 2)

### Push 8

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (9, 3)
- **Player Push Position:** (8, 6)
- **Crate Start:** (7, 6)
- **Crate End:** (6, 6)
- **Player Walk:** (9, 3) → (9, 4) → (8, 4) → (8, 5) → (8, 6)

### Push 9

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 6)
- **Player Push Position:** (7, 6)
- **Crate Start:** (6, 6)
- **Crate End:** (5, 6)
- **Player Walk:** (7, 6)

### Push 10

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (6, 6)
- **Player Push Position:** (5, 5)
- **Crate Start:** (5, 6)
- **Crate End:** (5, 7)
- **Player Walk:** (6, 6) → (6, 5) → (5, 5)

### Push 11

- **Crate:** 3
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (5, 6)
- **Player Push Position:** (6, 7)
- **Crate Start:** (5, 7)
- **Crate End:** (4, 7)
- **Player Walk:** (5, 6) → (6, 6) → (6, 7)

### Push 12

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (5, 7)
- **Player Push Position:** (4, 6)
- **Crate Start:** (4, 7)
- **Crate End:** (4, 8)
- **Player Walk:** (5, 7) → (5, 6) → (4, 6)

### Push 13

- **Crate:** 3
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (4, 7)
- **Player Push Position:** (4, 7)
- **Crate Start:** (4, 8)
- **Crate End:** (4, 9)
- **Player Walk:** (4, 7)

### Push 14

- **Crate:** 1
- **Direction:** Down
- **Distance:** 3
- **Player Start:** (4, 8)
- **Player Push Position:** (4, 4)
- **Crate Start:** (4, 5)
- **Crate End:** (4, 8)
- **Player Walk:** (4, 8) → (4, 7) → (4, 6) → (5, 6) → (5, 5) → (5, 4) → (4, 4)

### Push 15

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (4, 5)
- **Player Push Position:** (9, 10)
- **Crate Start:** (8, 10)
- **Crate End:** (7, 10)
- **Player Walk:** (4, 5) → (4, 6) → (4, 7) → (5, 7) → (6, 7) → (6, 8) → (7, 8) → (8, 8) → (9, 8) → (10, 8) → (10, 9) → (10, 10) → (9, 10)

## Generation Settings

- **Target Pushes:** 9–21
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–11
- **Interior Wall Range:** 22–48

