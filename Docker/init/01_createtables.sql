create database fronteditordb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
create user 'fronteditoruser'@'127.0.0.1' identified by 'fronteditorpass';
grant all privileges on fronteditordb.* to 'fronteditoruser'@'127.0.0.1';
create user 'fronteditoruser'@'%' identified by 'fronteditorpass';
grant all privileges on fronteditordb.* to 'fronteditoruser'@'%';

USE fronteditordb;

CREATE TABLE `__EFMigrationsHistory` (
	`MigrationId` VARCHAR(150) NOT NULL,
	`ProductVersion` VARCHAR(32) NOT NULL,
	PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE=InnoDB;