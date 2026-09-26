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
- **Minimum Solution:** 11 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (5, 5), Distance 1
- **Crate 2:** Position (7, 4), Distance 1
- **Crate 3:** Position (7, 2), Distance 1
- **Crate 4:** Position (7, 6), Distance 1

## Goals

- **Goal 1:** (5, 8)
- **Goal 2:** (8, 1)
- **Goal 3:** (5, 6)
- **Goal 4:** (6, 1)

## Solution

**Minimum pushes:** 11

### Push 1

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (3, 7)
- **Player Push Position:** (5, 4)
- **Crate Start:** (5, 5)
- **Crate End:** (5, 6)
- **Player Walk:** (3, 7) → (4, 7) → (4, 6) → (4, 5) → (4, 4) → (5, 4)

### Push 2

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (5, 5)
- **Player Push Position:** (5, 5)
- **Crate Start:** (5, 6)
- **Crate End:** (5, 7)
- **Player Walk:** (5, 5)

### Push 3

- **Crate:** 1
- **Direction:** Down
- **Distance:** 1
- **Player Start:** (5, 6)
- **Player Push Position:** (5, 6)
- **Crate Start:** (5, 7)
- **Crate End:** (5, 8)
- **Player Walk:** (5, 6)

### Push 4

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (5, 7)
- **Player Push Position:** (7, 5)
- **Crate Start:** (7, 4)
- **Crate End:** (7, 3)
- **Player Walk:** (5, 7) → (5, 6) → (5, 5) → (6, 5) → (7, 5)

### Push 5

- **Crate:** 3
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (7, 4)
- **Player Push Position:** (6, 2)
- **Crate Start:** (7, 2)
- **Crate End:** (8, 2)
- **Player Walk:** (7, 4) → (8, 4) → (8, 3) → (8, 2) → (8, 1) → (7, 1) → (6, 1) → (6, 2)

### Push 6

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 2)
- **Player Push Position:** (7, 4)
- **Crate Start:** (7, 3)
- **Crate End:** (7, 2)
- **Player Walk:** (7, 2) → (7, 1) → (8, 1) → (9, 1) → (10, 1) → (10, 2) → (10, 3) → (10, 4) → (9, 4) → (8, 4) → (7, 4)

### Push 7

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (7, 2)
- **Crate End:** (7, 1)
- **Player Walk:** (7, 3)

### Push 8

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 2)
- **Player Push Position:** (8, 1)
- **Crate Start:** (7, 1)
- **Crate End:** (6, 1)
- **Player Walk:** (7, 2) → (7, 3) → (8, 3) → (9, 3) → (10, 3) → (10, 2) → (10, 1) → (9, 1) → (8, 1)

### Push 9

- **Crate:** 3
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 1)
- **Player Push Position:** (8, 3)
- **Crate Start:** (8, 2)
- **Crate End:** (8, 1)
- **Player Walk:** (7, 1) → (7, 2) → (7, 3) → (8, 3)

### Push 10

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (8, 2)
- **Player Push Position:** (8, 6)
- **Crate Start:** (7, 6)
- **Crate End:** (6, 6)
- **Player Walk:** (8, 2) → (8, 3) → (8, 4) → (9, 4) → (10, 4) → (10, 5) → (10, 6) → (9, 6) → (8, 6)

### Push 11

- **Crate:** 4
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 6)
- **Player Push Position:** (7, 6)
- **Crate Start:** (6, 6)
- **Crate End:** (5, 6)
- **Player Walk:** (7, 6)

## Generation Settings

- **Target Pushes:** 9–20
- **Crate Count Range:** 4–5
- **Crate Distance Range:** 1–10
- **Interior Wall Range:** 20–44

