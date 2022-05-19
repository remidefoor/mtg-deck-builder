# Data Access Library

- ~~Entities~~
- ~~DB context~~
- ~~Repositories~~

# Shared Library

- ~~DTOs~~
- ~~Mappings~~
- ~~Filters~~
- ~~Extension methods~~

# Minimal API

- ~~post deck~~
  - ~~validation~~

- ~~delete deck~~

# Web API

- ~~get cards~~

# Web Application

- [x] Blazor Server

## overview

- [x] start up
  - [x] first 150 cards
    - [x] caching
- [x] filtering

  - [x] set
  - [x] artist
  - [x] rarity
  - [x] name
  - [x] text
- [x] sorting
  - [x] card name
- [x] card click > add

## deck

- [x] card click > remove

- [x] limit = 60

- [x] local storage

- [x] post deck

- [x] delete deck

# GraphQL API

- [x] queries

  - [x] cards
    - [x] ...
    - [x] artist
    - [x] filters
      - [x] power (optional)
      - [x] toughness (optional)

  - [x] artists
    - [x] id
    - [x] full name
    - [x] cards
    - [x] filters
      - [x] limit (optional)

  - [x] artist
    - [x] ...
    - [x] filters
      - [x] id

# video

- [ ] Web Application
- [ ] Swagger UI
  - [ ] Minimal API
  - [ ] Web API
- [ ] GraphQL API
