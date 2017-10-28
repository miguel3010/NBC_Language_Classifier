DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `setKnowledge`(IN `patronstr` VARCHAR(200), IN `idcat` INT)
    NO SQL BEGIN 
	SET @idPatron = 0;
	SELECT p.id into @idPatron FROM `patron` p WHERE p.nombre = patronstr;

	IF @idPatron = 0 THEN
		INSERT into patron VALUES (NULL, patronstr);
		SET @idPatron = LAST_INSERT_ID();
	END IF;

	IF (exists(SELECT t.medida FROM tendencia t WHERE t.Patron_id = @idPatron AND t.Categoria_id = idcat )) THEN 
		UPDATE tendencia t SET t.medida = t.medida + 1 WHERE t.Patron_id = @idPatron AND t.Categoria_id = idcat;
	ELSE
		INSERT INTO `tendencia` (`Categoria_id`, `Patron_id`, `medida`) VALUES (idcat, @idPatron, '1');
	END IF;
END
