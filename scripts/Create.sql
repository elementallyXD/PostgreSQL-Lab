CREATE TABLE IF NOT EXISTS customer (
  _id SERIAL PRIMARY KEY,
  fullname VARCHAR(255) NOT NULL,
  city VARCHAR(255) NOT NULL,
  contacts VARCHAR(255) NOT NULL,
  buyDate DATE NOT NULL
);

CREATE TABLE IF NOT EXISTS supplier (
  _id SERIAL PRIMARY KEY,
  title VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS products (
  _id SERIAL PRIMARY KEY,
  designation VARCHAR(255) NOT NULL,
  description TEXT,
  avilable boolean NOT NULL
);

CREATE TABLE IF NOT EXISTS buyers (
  c_id INTEGER REFERENCES customer ON UPDATE CASCADE ON DELETE CASCADE,
  s_id INTEGER NOT NULL REFERENCES supplier ON UPDATE CASCADE ON DELETE CASCADE
)