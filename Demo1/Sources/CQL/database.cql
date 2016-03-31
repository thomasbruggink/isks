CREATE KEYSPACE isksdemo1 WITH replication = {'class': 'SimpleStrategy', 'replication_factor': '1'}  AND durable_writes = true;

CREATE TABLE isksdemo1.blogs (
    id timeuuid PRIMARY KEY,
    title text,
    content text,
    writer text,
    date timestamp
);

CREATE TABLE isksdemo1.users (
    username text PRIMARY KEY,
    password ascii,
    salt ascii
);