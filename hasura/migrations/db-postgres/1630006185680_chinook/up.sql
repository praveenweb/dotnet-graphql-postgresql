
-- -- /*******************************************************************************
-- --    Chinook Database - Version 1.4
-- --    Script: Chinook_PostgreSql.sql
-- --    Description: Creates and populates the Chinook database.
-- --    DB Server: PostgreSql
-- --    Author: Luis Rocha
-- --    License: http://www.codeplex.com/ChinookDatabase/license

-- --    Modified By: John Atten
-- --    Modification Date: 3/1/2014
-- --    Summary of Changes:
-- --     - Changed integer PKs to serial (auto-incrementing) Pks
-- --     - Added Transactions table to excercie Biggie code against a bigserial type
-- --     - Imported "Actors", "Cities", "Countries" and "Films" tables from pg dvdrentals Db to excercise 
-- --       Biggie code against Full-Text Search table. 
-- -- ********************************************************************************/


-- ----------------------------
-- Sequence structure for album_album_id_seq
-- ----------------------------
CREATE SEQUENCE "albums_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 347
 CACHE 1;
SELECT setval('"public"."albums_id_seq"', 347, true);


-- ----------------------------
-- Sequence structure for artist_artist_id_seq
-- ----------------------------
CREATE SEQUENCE "artists_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 275
 CACHE 1;
SELECT setval('"public"."artists_id_seq"', 275, true);


-- ----------------------------
-- Sequence structure for customer_customer_id_seq
-- ----------------------------
CREATE SEQUENCE "customers_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 59
 CACHE 1;
SELECT setval('"public"."customers_id_seq"', 59, true);


-- ----------------------------
-- Sequence structure for genre_genre_id_seq
-- ----------------------------
CREATE SEQUENCE "genres_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 25
 CACHE 1;
SELECT setval('"public"."genres_id_seq"', 25, true);


-- ----------------------------
-- Sequence structure for employee_employee_id_seq
-- ----------------------------
CREATE SEQUENCE "employees_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 8
 CACHE 1;
SELECT setval('"public"."employees_id_seq"', 8, true);


-- ----------------------------
-- Sequence structure for invoice_invoice_id_seq
-- ----------------------------
CREATE SEQUENCE "invoices_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 412
 CACHE 1;
SELECT setval('"public"."invoices_id_seq"', 412, true);


-- ----------------------------
-- Sequence structure for invoiceline_invoiceline_id_seq
-- ----------------------------
CREATE SEQUENCE "invoicelines_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 2240
 CACHE 1;
SELECT setval('"public"."invoicelines_id_seq"', 2240, true);


-- ----------------------------
-- Sequence structure for mediatype_mediatype_id_seq
-- ----------------------------
CREATE SEQUENCE "mediatypes_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 5
 CACHE 1;
SELECT setval('"public"."mediatypes_id_seq"', 5, true);


-- ----------------------------
-- Sequence structure for playlist_playlist_id_seq
-- ----------------------------
CREATE SEQUENCE "playlists_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 18
 CACHE 1;
SELECT setval('"public"."playlists_id_seq"', 18, true);


-- ----------------------------
-- Sequence structure for track_track_id_seq
-- ----------------------------
CREATE SEQUENCE "tracks_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 3503
 CACHE 1;
SELECT setval('"public"."tracks_id_seq"', 3503, true);

-- /*******************************************************************************
--    Create Tables
-- ********************************************************************************/
CREATE TABLE albums
(
    id int DEFAULT nextval('albums_id_seq'::regclass) NOT NULL,
    title VARCHAR(160) NOT NULL,
    artist_id INT NOT NULL,
    CONSTRAINT pk_albums PRIMARY KEY  (id)
);

CREATE TABLE artists
(
    id int DEFAULT nextval('artists_id_seq'::regclass) NOT NULL,
    name VARCHAR(120),
    CONSTRAINT pk_artists PRIMARY KEY  (id)
);

CREATE TABLE customers
(
    id int DEFAULT nextval('customers_id_seq'::regclass) NOT NULL,
    first_name VARCHAR(40) NOT NULL,
    last_name VARCHAR(20) NOT NULL,
    company VARCHAR(80),
    address VARCHAR(70),
    city VARCHAR(40),
    state VARCHAR(40),
    country VARCHAR(40),
    postal_code VARCHAR(10),
    phone VARCHAR(24),
    fax VARCHAR(24),
    email VARCHAR(60) NOT NULL,
    support_rep_id INT,
    CONSTRAINT pk_customers PRIMARY KEY  (id)
);

CREATE TABLE employees
(
    id int DEFAULT nextval('employees_id_seq'::regclass) NOT NULL,
    last_name VARCHAR(20) NOT NULL,
    first_name VARCHAR(20) NOT NULL,
    title VARCHAR(30),
    reports_to INT,
    birth_date TIMESTAMP,
    hire_date TIMESTAMP,
    address VARCHAR(70),
    city VARCHAR(40),
    state VARCHAR(40),
    country VARCHAR(40),
    postal_code VARCHAR(10),
    phone VARCHAR(24),
    fax VARCHAR(24),
    email VARCHAR(60),
    CONSTRAINT pk_employees PRIMARY KEY  (id)
);

CREATE TABLE genres
(
    id int DEFAULT nextval('genres_id_seq'::regclass) NOT NULL,
    name VARCHAR(120),
    CONSTRAINT pk_genres PRIMARY KEY  (id)
);

CREATE TABLE invoices
(
    id int DEFAULT nextval('invoices_id_seq'::regclass) NOT NULL,
    customer_id INT NOT NULL,
    invoice_date TIMESTAMP NOT NULL,
    billing_address VARCHAR(70),
    billing_city VARCHAR(40),
    billing_state VARCHAR(40),
    billing_country VARCHAR(40),
    billing_postal_code VARCHAR(10),
    total NUMERIC(10,2) NOT NULL,
    CONSTRAINT pk_invoices PRIMARY KEY  (id)
);

CREATE TABLE invoice_lines
(
    id int DEFAULT nextval('invoicelines_id_seq'::regclass) NOT NULL,
    invoice_id INT NOT NULL,
    track_id INT NOT NULL,
    unit_price NUMERIC(10,2) NOT NULL,
    quantity INT NOT NULL,
    CONSTRAINT ok_invoice_lines PRIMARY KEY  (id)
);

CREATE TABLE media_types
(
    id int DEFAULT nextval('mediatypes_id_seq'::regclass) NOT NULL,
    name VARCHAR(120),
    CONSTRAINT pk_media_types PRIMARY KEY  (id)
);

CREATE TABLE playlists
(
    id int DEFAULT nextval('playlists_id_seq'::regclass) NOT NULL,
    name VARCHAR(120),
    CONSTRAINT pk_playlists PRIMARY KEY  (id)
);

CREATE TABLE playlist_track
(
    playlist_id INT NOT NULL,
    track_id INT NOT NULL,
    CONSTRAINT pk_playlist_track PRIMARY KEY  (playlist_id, track_id)
);

CREATE TABLE tracks
(
    id int DEFAULT nextval('tracks_id_seq'::regclass) NOT NULL,
    name VARCHAR(200) NOT NULL,
    album_id INT,
    media_type_id INT NOT NULL,
    genre_id INT,
    composer VARCHAR(220),
    milliseconds INT NOT NULL,
    bytes INT,
    unit_price NUMERIC(10,2) NOT NULL,
    CONSTRAINT pk_tracks PRIMARY KEY  (id)
);



/*******************************************************************************
   Create Primary Key Unique Indexes
********************************************************************************/

/*******************************************************************************
   Create Foreign Keys
********************************************************************************/
ALTER TABLE albums ADD CONSTRAINT fk_album_artist_id
    FOREIGN KEY (artist_id) REFERENCES artists (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_album_artist_id ON albums (artist_id);

ALTER TABLE customers ADD CONSTRAINT fk_customer_support_rep_id
    FOREIGN KEY (support_rep_id) REFERENCES employees (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_customer_support_rep_id ON customers (support_rep_id);

ALTER TABLE employees ADD CONSTRAINT fk_employee_reports_to
    FOREIGN KEY (reports_to) REFERENCES employees (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_employee_reports_to ON employees (reports_to);

ALTER TABLE invoices ADD CONSTRAINT fk_invoice_customer_id
    FOREIGN KEY (customer_id) REFERENCES customers (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_invoice_customer_id ON invoices (customer_id);

ALTER TABLE invoice_lines ADD CONSTRAINT fk_invoice_line_invoice_id
    FOREIGN KEY (invoice_id) REFERENCES invoices (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_invoice_line_invoice_id ON invoice_lines (invoice_id);

ALTER TABLE invoice_lines ADD CONSTRAINT fk_invoice_line_track_id
    FOREIGN KEY (track_id) REFERENCES tracks (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_invoice_line_track_id ON invoice_lines (track_id);

ALTER TABLE playlist_track ADD CONSTRAINT fk_playlist_track_playlist_id
    FOREIGN KEY (playlist_id) REFERENCES playlists (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE playlist_track ADD CONSTRAINT fk_playlist_track_track_id
    FOREIGN KEY (track_id) REFERENCES tracks (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_playlist_track_track_id ON playlist_track (track_id);

ALTER TABLE tracks ADD CONSTRAINT fk_track_album_id
    FOREIGN KEY (album_id) REFERENCES albums (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_track_album_id ON tracks (album_id);

ALTER TABLE tracks ADD CONSTRAINT fk_track_genre_id
    FOREIGN KEY (genre_id) REFERENCES genres (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_track_genre_id ON tracks (genre_id);

ALTER TABLE tracks ADD CONSTRAINT fk_track_media_type_id
    FOREIGN KEY (media_type_id) REFERENCES media_types (id) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE INDEX ifk_track_media_type_id ON tracks (media_type_id);

-- ========================================================================
-- Additional tables from dvd rental db to play with Full-Text Serach and stuff...
-- ========================================================================


-- ----------------------------
-- Sequence structure for actor_actor_id_seq
-- ----------------------------
CREATE SEQUENCE "actors_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 203
 CACHE 1;
SELECT setval('"public"."actors_id_seq"', 203, true);


-- ----------------------------
-- Sequence structure for category_category_id_seq
-- ----------------------------
CREATE SEQUENCE "categories_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 16
 CACHE 1;
SELECT setval('"public"."categories_id_seq"', 16, true);

-- ----------------------------
-- Sequence structure for film_film_id_seq
-- ----------------------------
CREATE SEQUENCE "films_id_seq"
 INCREMENT 1
 MINVALUE 1
 MAXVALUE 2147483647
 START 1000
 CACHE 1;
SELECT setval('"public"."films_id_seq"', 1000, true);


-- ----------------------------
-- Table structure for category
-- ----------------------------
CREATE TABLE "categories" (
"id" int4 DEFAULT nextval('categories_id_seq'::regclass) NOT NULL,
name varchar(25) COLLATE "default" NOT NULL,
"last_update" timestamp(6) DEFAULT now() NOT NULL
)
WITH (OIDS=FALSE)

;




-- ----------------------------
-- Table structure for actor
-- ----------------------------
CREATE TABLE "actors" (
"id" int4 DEFAULT nextval('actors_id_seq'::regclass) NOT NULL,
"first_name" varchar(45) COLLATE "default" NOT NULL,
"last_name" varchar(45) COLLATE "default" NOT NULL,
"last_update" timestamp(6) DEFAULT now() NOT NULL
)
WITH (OIDS=FALSE)

;



-- ----------------------------
-- Table structure for film
-- ----------------------------
CREATE TABLE "films" (
"id" int4 DEFAULT nextval('films_id_seq'::regclass) NOT NULL,
title varchar(255) COLLATE "default" NOT NULL,
"description" text COLLATE "default",
"release_year" int,
"language_id" int2 NOT NULL,
"rental_duration" int2 DEFAULT 3 NOT NULL,
"rental_rate" numeric(4,2) DEFAULT 4.99 NOT NULL,
"length" int2,
"replacement_cost" numeric(5,2) DEFAULT 19.99 NOT NULL,
"rating" text DEFAULT 'G',
"last_update" timestamp(6) DEFAULT now() NOT NULL,
"special_features" text[] COLLATE "default",
"fulltext" tsvector NOT NULL
)
WITH (OIDS=FALSE)

;



-- ----------------------------
-- Table structure for film_actor
-- ----------------------------
CREATE TABLE "film_actor" (
"actor_id" int2 NOT NULL,
"film_id" int2 NOT NULL,
"last_update" timestamp(6) DEFAULT now() NOT NULL
)
WITH (OIDS=FALSE)

;


-- ----------------------------
-- Table structure for film_category
-- ----------------------------
CREATE TABLE "film_category" (
"film_id" int2 NOT NULL,
"category_id" int2 NOT NULL,
"last_update" timestamp(6) DEFAULT now() NOT NULL
)
WITH (OIDS=FALSE)

;

-- --------------------------------------
-- Function Definition for last_updated()
-- --------------------------------------
CREATE OR REPLACE FUNCTION last_updated()
  RETURNS trigger AS
$BODY$
BEGIN
    NEW.last_update = CURRENT_TIMESTAMP;
    RETURN NEW;
END $BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION last_updated()
  OWNER TO postgres;


-- ----------------------------
-- Indexes structure for table actor
-- ----------------------------
CREATE INDEX "idx_actors_last_name" ON "actors" USING btree (last_name);

-- ----------------------------
-- Triggers structure for table actor
-- ----------------------------
CREATE TRIGGER "last_updated" BEFORE UPDATE ON "actors"
FOR EACH ROW
EXECUTE PROCEDURE "last_updated"();

-- ----------------------------
-- Primary Key structure for table actor
-- ----------------------------
ALTER TABLE "actors" ADD PRIMARY KEY ("id");


-- ----------------------------
-- Triggers structure for table category
-- ----------------------------
CREATE TRIGGER "last_updated" BEFORE UPDATE ON "categories"
FOR EACH ROW
EXECUTE PROCEDURE "last_updated"();

-- ----------------------------
-- Primary Key structure for table category
-- ----------------------------
ALTER TABLE "categories" ADD PRIMARY KEY ("id");



-- ----------------------------
-- Indexes structure for table film
-- ----------------------------
CREATE INDEX "films_fulltext_idx" ON "films" USING gist (fulltext);
CREATE INDEX "idx_fk_language_id" ON "films" USING btree (language_id);
CREATE INDEX "idx_title" ON "films" USING btree (title);

-- ----------------------------
-- Triggers structure for table film
-- ----------------------------
CREATE TRIGGER "films_fulltext_trigger" BEFORE INSERT OR UPDATE ON "films"
FOR EACH ROW
EXECUTE PROCEDURE "tsvector_update_trigger"('fulltext', 'pg_catalog.english', 'title', 'description');
CREATE TRIGGER "last_updated" BEFORE UPDATE ON "films"
FOR EACH ROW
EXECUTE PROCEDURE "last_updated"();

-- ----------------------------
-- Primary Key structure for table film
-- ----------------------------
ALTER TABLE "films" ADD PRIMARY KEY ("id");

-- ----------------------------
-- Indexes structure for table film_actor
-- ----------------------------
CREATE INDEX "idx_fk_films_id" ON "film_actor" USING btree (film_id);

-- ----------------------------
-- Triggers structure for table film_actor
-- ----------------------------
CREATE TRIGGER "last_updated" BEFORE UPDATE ON "film_actor"
FOR EACH ROW
EXECUTE PROCEDURE "last_updated"();

-- ----------------------------
-- Primary Key structure for table film_actor
-- ----------------------------
ALTER TABLE "film_actor" ADD PRIMARY KEY ("actor_id", "film_id");
