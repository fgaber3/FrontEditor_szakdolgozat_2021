CREATE TABLE `__EFMigrationsHistory` (
	`MigrationId` VARCHAR(150) NOT NULL,
	`ProductVersion` VARCHAR(32) NOT NULL,
	PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE=InnoDB;