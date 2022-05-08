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

- post deck
  - validation

- delete deck

# Web API

- get cards

# Web Application

- [x] Blazor Server

## overview

- [ ] start up
  - [x] first 150 cards
    - [ ] caching
- [x] filtering

  - [x] set
  - [x] artist
  - [x] rarity
  - [x] name
  - [x] text
- [ ] sorting
  - [ ] card name
- [ ] card click > add

## deck

- [ ] card click > remove

- [ ] limit = 60

- [ ] local storage

- [ ] post deck

- [ ] delete deck

# GraphQL API

- [ ] queries

  - [ ] cards
    - [x] ...
    - [x] artist
    - [ ] filters
      - [ ] power (optional)
      - [ ] toughness (optional)

  - [ ] artists
    - [x] id
    - [x] full name
    - [x] cards
    - [ ] filters
      - [ ] limit (optional)

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



# questions

- Minimal API Swagger does not launch.
- Duplicate code Program.cs <=> SwaggerEndPoints.cs.
- get deck: minimal API || web API?