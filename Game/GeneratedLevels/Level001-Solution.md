---
title: "Number Push Level 1"
layout: post
level: 1
complexity: 1
---

# Number Push Level 1

## Level Information

- **Complexity:** 1
- **Board:** 10 × 12
- **Crates:** 1
- **Interior Walls:** 2
- **Minimum Solution:** 5 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (8, 5), Distance 1

## Goals

- **Goal 1:** (4, 4)

## Solution

**Minimum pushes:** 5

### Push 1

- **Crate:** 1
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (2, 8)
- **Player Push Position:** (8, 6)
- **Crate Start:** (8, 5)
- **Crate End:** (8, 4)
- **Player Walk:** (2, 8) → (3, 8) → (4, 8) → (5, 8) → (5, 7) → (5, 6) → (6, 6) → (7, 6) → (8, 6)

### Push 2

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (8, 5)
- **Player Push Position:** (9, 4)
- **Crate Start:** (8, 4)
- **Crate End:** (7, 4)
- **Player Walk:** (8, 5) → (9, 5) → (9, 4)

### Push 3

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (8, 4)
- **Player Push Position:** (8, 4)
- **Crate Start:** (7, 4)
- **Crate End:** (6, 4)
- **Player Walk:** (8, 4)

### Push 4

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (7, 4)
- **Player Push Position:** (7, 4)
- **Crate Start:** (6, 4)
- **Crate End:** (5, 4)
- **Player Walk:** (7, 4)

### Push 5

- **Crate:** 1
- **Direction:** Left
- **Distance:** 1
- **Player Start:** (6, 4)
- **Player Push Position:** (6, 4)
- **Crate Start:** (5, 4)
- **Crate End:** (4, 4)
- **Player Walk:** (6, 4)

## Generation Settings

- **Target Pushes:** 4–11
- **Crate Count Range:** 1–2
- **Crate Distance Range:** 1–1
- **Interior Wall Range:** 2–8

