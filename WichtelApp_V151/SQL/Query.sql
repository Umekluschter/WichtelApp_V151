﻿CREATE TABLE USERS (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	FIRSTNAME VARCHAR(40),
	LASTNAME VARCHAR(40),
	MAIL VARCHAR(100),
	PASSWORD VARCHAR(100)
);

CREATE TABLE WISHES (
	ID INT IDENTITY(1,1),
	WISHER_ID INT,
	WISH VARCHAR(100),
	COMMENT VARCHAR(256),
	FULFILLED BIT,
	TIME_CREATED DATETIME,
	GRANTED_BY INT,
	HIDE BIT
);

ALTER TABLE WISHES
ADD CONSTRAINT FK_WISHER_ID FOREIGN KEY (WISHER_ID) REFERENCES USERS(ID);