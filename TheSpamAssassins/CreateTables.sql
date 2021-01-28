DROP SEQUENCE activity_id_sequence;

DROP TABLE register;
DROP TABLE activity;
DROP TABLE camper;

CREATE TABLE camper
(
	username VARCHAR2(30)
		CONSTRAINT camper_username_nn NOT NULL
		CONSTRAINT camper_pk PRIMARY KEY,
	first_name VARCHAR2(40)
		CONSTRAINT camper_first_name_nn NOT NULL,
	last_name VARCHAR2(40)
		CONSTRAINT camper_last_name_nn NOT NULL
);

CREATE TABLE activity
(
	id NUMBER(4)
		CONSTRAINT activity_id_nn NOT NULL
		CONSTRAINT activity_id_ck CHECK(id >= 1000)
		CONSTRAINT activity_pk PRIMARY KEY,
	name VARCHAR2(60)
		CONSTRAINT activity_name_nn NOT NULL,
	capacity NUMBER(2)
		CONSTRAINT activity_capacity_nn NOT NULL
		CONSTRAINT activity_capacity_ck CHECK (capacity > 0)
);

CREATE TABLE register
(
	camper_username VARCHAR2(30)
		CONSTRAINT register_camper_username_nn NOT NULL
		CONSTRAINT register_camper_username_fk REFERENCES camper(username),
	activity_id NUMBER(4)
		CONSTRAINT register_activity_id_nn NOT NULL
		CONSTRAINT register_activity_id_fk REFERENCES activity(id),
	camp_day NUMBER(1)
		CONSTRAINT register_camp_day_nn NOT NULL
		CONSTRAINT register_camp_day_ck CHECK(camp_day BETWEEN 1 AND 5),
	CONSTRAINT register_pk PRIMARY KEY(camper_username, activity_id),
	CONSTRAINT camper_day_uk UNIQUE(camper_username, camp_day) -- Camper can only register for one activity per day
);

-- SEQUENCES
CREATE SEQUENCE activity_id_sequence
INCREMENT BY 1
START WITH 1000;

-- ACTIVITY TABLE SAMPLE DATA
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Parasailing', 1);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Canoeing', 2);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Swimming', 10);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Archery', 8);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Arts and Crafts', 12);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Hiking', 12);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Fishing', 4);
INSERT INTO activity (id, name, capacity) VALUES (activity_id_sequence.NEXTVAL, 'Bird Watching', 6);
COMMIT;

-- CAMPER TABLE SAMPLE DATA
INSERT INTO camper (username, first_name, last_name) VALUES ('ols655_201a14', 'Foster', 'Hangdaan');
INSERT INTO camper (username, first_name, last_name) VALUES ('ols655_201a24', 'Matthew', 'Percy');
INSERT INTO camper (username, first_name, last_name) VALUES ('ols655_201a11', 'Mark', 'Brierley');
COMMIT;

-- REGISTER TABLE SAMPLE DATA
INSERT INTO register (camper_username, activity_id, camp_day) VALUES ('ols655_201a14', 1005, 1);
INSERT INTO register (camper_username, activity_id, camp_day) VALUES ('ols655_201a14', 1003, 2);
INSERT INTO register (camper_username, activity_id, camp_day) VALUES ('ols655_201a14', 1006, 3);
INSERT INTO register (camper_username, activity_id, camp_day) VALUES ('ols655_201a14', 1001, 4);
INSERT INTO register (camper_username, activity_id, camp_day) VALUES ('ols655_201a14', 1002, 5);
COMMIT;


-- PROCEDURES

-- REGISTER_ACTIVITY
-- Matthew Percy
SET SERVEROUTPUT ON SIZE 4000

CREATE OR REPLACE PROCEDURE REGISTER_ACTIVITY
(
	pactivity_id IN NUMBER,
	pcamp_day IN NUMBER,
	psuccess OUT NUMBER
)
IS
  registerCount NUMBER;
  capacity NUMBER;
  userName Camper.UserName%TYPE;
BEGIN
  SELECT USER INTO userName FROM DUAL;

  SELECT COUNT(*) INTO registerCount FROM REGISTER WHERE activity_id = pactivity_id AND camp_day = pcamp_day;
  SELECT Capacity INTO capacity FROM ACTIVITY WHERE ID = pactivity_id;
  IF registerCount >= capacity THEN 
    psuccess := 0;
  ELSE
    INSERT INTO REGISTER (CAMPER_USERNAME,ACTIVITY_ID,CAMP_DAY)  VALUES (LOWER(userName),pactivity_id,pcamp_day);
    psuccess := 1;
  END IF;
  EXCEPTION
    WHEN OTHERS THEN psuccess := 0;
END;
/

-- IS_AVAILABLE 
-- Mark Brierley
SET SERVEROUTPUT ON SIZE 4000

CREATE OR REPLACE FUNCTION IS_AVAILABLE
(
  pcamp_day NUMBER,
  pactivity_id NUMBER
)
RETURN NUMBER
IS
  matches NUMBER;
  user_name register.camper_username%TYPE;
BEGIN
  SELECT USER INTO user_name FROM dual;
  SELECT COUNT(*) INTO matches FROM register WHERE (camper_username = LOWER(user_name) AND camp_day = pcamp_day) OR (camper_username = LOWER(user_name) AND activity_id = pactivity_id);
  
  IF matches > 0 THEN
    RETURN 0;
  ELSE
    RETURN 1;
  END IF;
END;
/
COMMIT;