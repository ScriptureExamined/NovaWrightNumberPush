---
title: "Number Push Level 4"
layout: post
level: 4
complexity: 4
---

# Number Push Level 4

## Level Information

- **Complexity:** 4
- **Board:** 10 × 12
- **Crates:** 2
- **Interior Walls:** 11
- **Minimum Solution:** 16 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (6, 7), Distance 2
- **Crate 2:** Position (7, 6), Distance 1

## Goals

- **Goal 1:** (10, 1)
- **Goal 2:** (1, 1)

## Solution

**Minimum pushes:** 16

### Push 1

- **Crate:** 1
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (5, 1)
- **Player Push Position:** (6, 8)
- **Crate Start:** (6, 7)
- **Crate End:** (6, 5)
- **Player Walk:** (5, 1) → (6, 1) → (6, 2) → (6, 3) → (6, 4) → (6, 5) → (6, 6) → (5, 6) → (5, 7) → (5, 8) → (6, 8)

### Push 2

- **Crate:** 1
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (6, 7)
- **Player Push Position:** (5, 5)
- **Crate Start:** (6, 5)
- **Crate End:** (8, 5)
- **Player Walk:** (6, 7) → (6, 6) → (5, 6) → (5, 5)

### Push 3

- **Crate:** 1
- **Direction:** Right
- **Distance:** 2
- **Player Start:** (6, 5)
- **Player Push Position:** (7, 5)
- **Crate Start:** (8, 5)
- **Crate End:** (10, 5)
- **Player Walk:** (6, 5) → (7, 5)

### Push 4

- **Crate:** 1
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (8, 5)
- **Player Push Position:** (10, 6)
- **Crate Start:** (10, 5)
- **Crate End:** (10, 3)
- **Player Walk:** (8, 5) → (8, 6) → (9, 6) → (10, 6)

### Push 5

- **Crate:** 1
- **Direction:** Up
- **Distance:** 2
- **Player Start:** (10, 5)
- **Player Push Position:** (10, 4)
- **Crate Start:** (10, 3)
- **Crate End:** (10, 1)
- **Player Walk:** (10, 5) → (10, 4)

### Push 6

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (10, 3)
- **Player Push Position:** (7, 7)
- **Crate Start:** (7, 6)
- **Crate End:** (7, 5)
- **Player Walk:** (10, 3) → (10, 4) → (10, 5) → (10, 6) → (9, 6) → (8, 6) → (8, 7) → (7, 7)

### Push 7

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 6)
- **Player Push Position:** (7, 6)
- **Crate Start:** (7, 5)
- **Crate End:** (7, 4)
- **Player Walk:** (7, 6)

### Push 8

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 5)
- **Player Push Position:** (7, 5)
- **Crate Start:** (7, 4)
- **Crate End:** (7, 3)
- **Player Walk:** (7, 5)

### Push 9

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 4)
- **Player Push Position:** (7, 4)
- **Crate Start:** (7, 3)
- **Crate End:** (7, 2)
- **Player Walk:** (7, 4)

### Push 10

- **Crate:** 2
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (7, 3)
- **Player Push Position:** (7, 3)
- **Crate Start:** (7, 2)
- **Crate End:** (7, 1)
- **Player Walk:** (7, 3)

### Push 11

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 2)
- **Player Push Position:** (8, 1)
- **Crate Start:** (7, 1)
- **Crate End:** (6, 1)
- **Player Walk:** (7, 2) → (8, 2) → (8, 1)

### Push 12

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 1)
- **Player Push Position:** (7, 1)
- **Crate Start:** (6, 1)
- **Crate End:** (5, 1)
- **Player Walk:** (7, 1)

### Push 13

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (6, 1)
- **Player Push Position:** (6, 1)
- **Crate Start:** (5, 1)
- **Crate End:** (4, 1)
- **Player Walk:** (6, 1)

### Push 14

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (5, 1)
- **Player Push Position:** (5, 1)
- **Crate Start:** (4, 1)
- **Crate End:** (3, 1)
- **Player Walk:** (5, 1)

### Push 15

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (4, 1)
- **Player Push Position:** (4, 1)
- **Crate Start:** (3, 1)
- **Crate End:** (2, 1)
- **Player Walk:** (4, 1)

### Push 16

- **Crate:** 2
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (3, 1)
- **Player Push Position:** (3, 1)
- **Crate Start:** (2, 1)
- **Crate End:** (1, 1)
- **Player Walk:** (3, 1)

## Generation Settings

- **Target Pushes:** 16–34
- **Crate Count Range:** 2–3
- **Crate Distance Range:** 1–4
- **Interior Wall Range:** 8–20

