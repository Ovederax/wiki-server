create database wikidb;

use wikidb;

create table if not exists pages (
	pageid int(11) NOT NULL AUTO_INCREMENT,
    title varchar(128) NOT NULL,
    snippet varchar(120) NOT NULL,
    timestamp varchar(20) NOT NULL,

    UNIKUE KEY (title),
    PRIMARY KEY(pageid)
)