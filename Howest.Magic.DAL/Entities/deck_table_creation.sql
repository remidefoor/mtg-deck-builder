use mtg_v1
go

create table decks(
	id bigint identity not null primary key,
	name nvarchar(255)
)
go

create table deck_cards(
	deck_id bigint not null,
	card_id bigint not null,
	amount int not null,
	primary key(deck_id, card_id),
	constraint fk_decks foreign key(deck_id) references decks(id) on delete cascade,
	constraint fk_cards foreign key(card_id) references cards(id)
)
