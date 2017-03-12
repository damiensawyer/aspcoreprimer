CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

create role coreutil with SUPERUSER LOGIN;
alter role coreutil with password 'coreutil';

drop table if exists cycle;
drop table if exists contact;

CREATE TABLE contact
(
	id uuid primary key not null,
	date date not null
);

CREATE TABLE cycle
(
  id uuid not null,
  data json not null,
  CONSTRAINT contactfk FOREIGN KEY (id)
      REFERENCES contact (id) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);

insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());
insert into contact (id, date) values (uuid_generate_v1(), now());


-- insert into cycle (id, data) values (select top 1 id from contact, '{}');

insert into cycle
	select id, '{}' from contact limit 1;

--select * from contact;
select * from cycle;
