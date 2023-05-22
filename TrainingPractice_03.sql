USE master

--CREATE DATABASE DB_CompAvailability
USE DB_CompAvailability




--CREATE TABLE admins 
--(
--	adm_admin nvarchar(25),
--	adm_password nvarchar(25) NOT NULL,
--    PRIMARY KEY(adm_admin)
--)

--CREATE TABLE Users
--(
--	id_user INT IDENTITY(1,1) NOT NULL,
--	user_name nvarchar(25),
--	user_password nvarchar(25) NOT NULL,
--	PRIMARY KEY(id_user)
--)


--CREATE TABLE Availability
--(
--	prod_id INT IDENTITY(1,1) NOT NULL,
--	prod_name nvarchar(50) NOT NULL,
--	prod_status nvarchar(25) NOT NULL,
-- 	prod_price INT NOT NULL,
--	prod_column INT NOT NULL,
--  PRIMARY KEY(prod_id)
--)


--CREATE TABLE Orders
--(
--	order_id INT IDENTITY(1,1) NOT NULL,
--	id_prod INT NOT NULL,
--	users_id INT NOT NULL,
--	order_count INT NOT NULL,
--	order_price INT NOT NULL,
--	FOREIGN KEY (id_prod) REFERENCES Availability(prod_id),
--	FOREIGN KEY (users_id) REFERENCES Users(id_user),
--  PRIMARY KEY(order_id)
--)

--INSERT INTO admins(adm_admin, adm_password) VALUES
--('admin', '123'),
--('ADMS', 'paswd'),
--('adm123','adm123')

--INSERT INTO users(user_name, user_password) VALUES
--('user', '123'),
--('Leks', '2000'),
--('use','use123')












/*

CREATE TABLE supplierdir
(
	sup_id int IDENTITY(1,1) NOT NULL,
	sup_name nvarchar(50),
	PRIMARY KEY(sup_id)
)
CREATE TABLE fuel
(
	fuel_id int IDENTITY(1,1) NOT NULL,
	fuel_name nvarchar(50),
	price DECIMAL(10,2),
	sup_id INT NOT NULL,
	FOREIGN KEY (sup_id) REFERENCES supplierdir(sup_id),
	PRIMARY KEY(fuel_id)
)
CREATE TABLE remains
(
	remain_id int IDENTITY(1,1) NOT NULL,
	fuel_id INT NOT NULL,
	FOREIGN KEY (fuel_id) REFERENCES fuel(fuel_id),
	remain_date date,
	vol_init DECIMAL(10,2),
	vol_sold DECIMAL(10,2),
	PRIMARY KEY(remain_id)
)

INSERT INTO supplierdir(sup_name) VALUES
('УлГРаф'),
('РосНефть'),
('Газпром')

INSERT INTO fuel(fuel_name,price,sup_id) VALUES
('95',39.95,2),
('Дизель',42.90,1),
('92',49.20,2),
('98',61.5,3)

INSERT INTO remains(fuel_id,remain_date,vol_init,vol_sold) VALUES
(1,'16-09-2016',50000,20000),
(2,'05-01-2017',45000,15000),
(3,'21-04-2049',60000,30000),
(4,'09-03-2077',20000,5000),
(2,'01-001-2001',30000,12000)

*/