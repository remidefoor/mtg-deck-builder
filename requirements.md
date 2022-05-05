# Data Access Library

- ~~Entities~~
- ~~DB context~~
- ~~Repositories~~

# Shared Library

- ~~DTOs~~
- ~~Mappings~~
- ~~Filters~~
- Extension methods

# Minimal API

- post deck
- delete deck

# Web API

- get cards

# Web Application

- [ ] Blazor Server

## overview

- [ ] start up
  - [ ] first 150 cards
    - [ ] caching

- [ ] filtering

  - [ ] set
  - [ ] artist
  - [ ] rarity
  - [ ] name
  - [ ] text
  - [ ] extension method (optional)
  
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

- Wanneer een property van een entiteit van het type string is maar dit eigenlijk een getal voorstelt, welk type gebruik je dan?