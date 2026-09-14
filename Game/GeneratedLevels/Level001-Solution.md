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
- **Minimum Solution:** 4 pushes
- **Generator Seed:** 12345

## Crates

- **Crate 1:** Position (8, 5), Distance 1

## Goals

- **Goal 1:** (10, 3)

## Solution

**Minimum pushes:** 4

### Push 1

- **Crate:** 1
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (8, 4)
- **Player Push Position:** (8, 6)
- **Crate Start:** (8, 5)
- **Crate End:** (8, 4)
- **Player Walk:** (8, 4) → (7, 4) → (7, 5) → (7, 6) → (8, 6)

### Push 2

- **Crate:** 1
- **Direction:** Up
- **Distance:** 1
- **Player Start:** (8, 5)
- **Player Push Position:** (8, 5)
- **Crate Start:** (8, 4)
- **Crate End:** (8, 3)
- **Player Walk:** (8, 5)

### Push 3

- **Crate:** 1
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 4)
- **Player Push Position:** (7, 3)
- **Crate Start:** (8, 3)
- **Crate End:** (9, 3)
- **Player Walk:** (8, 4) → (7, 4) → (7, 3)

### Push 4

- **Crate:** 1
- **Direction:** Right
- **Distance:** 1
- **Player Start:** (8, 3)
- **Player Push Position:** (8, 3)
- **Crate Start:** (9, 3)
- **Crate End:** (10, 3)
- **Player Walk:** (8, 3)

## Generation Settings

- **Target Pushes:** 4–11
- **Crate Count Range:** 1–2
- **Crate Distance Range:** 1–1
- **Interior Wall Range:** 2–8

