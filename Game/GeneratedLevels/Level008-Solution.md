---
title: "Number Push Level 8"
layout: post
level: 8
complexity: 8
---

# Number Push Level 8

## Level Information

- **Complexity:** 8
- **Board:** 10 × 12
- **Crates:** 3
- **Interior Walls:** 17
- **Minimum Solution:** 10 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (6, 7), Distance 2
- **Crate 2:** Position (4, 7), Distance 1
- **Crate 3:** Position (8, 6), Distance 1

## Goals

- **Goal 1:** (4, 3)
- **Goal 2:** (4, 6)
- **Goal 3:** (8, 4)

## Solution

**Minimum pushes:** 10

### Push 1

- **Crate:** 1
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (1, 1)
- **Player Push Position:** (6, 8)
- **Crate Start:** (6, 7)
- **Crate End:** (6, 5)
- **Player Walk:** (1, 1) → (1, 2) → (2, 2) → (3, 2) → (3, 3) → (4, 3) → (5, 3) → (6, 3) → (6, 4) → (7, 4) → (8, 4) → (8, 5) → (9, 5) → (9, 6) → (9, 7) → (9, 8) → (8, 8) → (7, 8) → (6, 8)

### Push 2

- **Crate:** 1
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (6, 7)
- **Player Push Position:** (6, 6)
- **Crate Start:** (6, 5)
- **Crate End:** (6, 3)
- **Player Walk:** (6, 7) → (6, 6)

### Push 3

- **Crate:** 1
- **Direction:** Left
- **Distance:** 2
- **Player Start:** (6, 5)
- **Player Push Position:** (7, 3)
- **Crate Start:** (6, 3)
- **Crate End:** (4, 3)
- **Player Walk:** (6, 5) → (6, 4) → (7, 4) → (7, 3)

### Push 4

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (6, 3)
- **Player Push Position:** (5, 7)
- **Crate Start:** (4, 7)
- **Crate End:** (3, 7)
- **Player Walk:** (6, 3) → (6, 4) → (6, 5) → (6, 6) → (6, 7) → (5, 7)

### Push 5

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (4, 7)
- **Player Push Position:** (4, 7)
- **Crate Start:** (3, 7)
- **Crate End:** (2, 7)
- **Player Walk:** (4, 7)

### Push 6

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (3, 7)
- **Player Push Position:** (2, 8)
- **Crate Start:** (2, 7)
- **Crate End:** (2, 6)
- **Player Walk:** (3, 7) → (3, 6) → (2, 6) → (1, 6) → (1, 7) → (1, 8) → (2, 8)

### Push 7

- **Crate:** 2
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (2, 7)
- **Player Push Position:** (1, 6)
- **Crate Start:** (2, 6)
- **Crate End:** (3, 6)
- **Player Walk:** (2, 7) → (1, 7) → (1, 6)

### Push 8

- **Crate:** 2
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (2, 6)
- **Player Push Position:** (2, 6)
- **Crate Start:** (3, 6)
- **Crate End:** (4, 6)
- **Player Walk:** (2, 6)

### Push 9

- **Crate:** 3
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (3, 6)
- **Player Push Position:** (8, 7)
- **Crate Start:** (8, 6)
- **Crate End:** (8, 5)
- **Player Walk:** (3, 6) → (3, 7) → (4, 7) → (5, 7) → (6, 7) → (6, 8) → (7, 8) → (8, 8) → (8, 7)

### Push 10

- **Crate:** 3
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (8, 6)
- **Player Push Position:** (8, 6)
- **Crate Start:** (8, 5)
- **Crate End:** (8, 4)
- **Player Walk:** (8, 6)

## Generation Settings

- **Target Pushes:** 8–18
- **Crate Count Range:** 3–4
- **Crate Distance Range:** 1–8
- **Interior Wall Range:** 16–36

