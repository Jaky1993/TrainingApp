select * from [User] where [User].DeleteDate IS NULL AND [User].VersionId = (select MAX([User].VersionId) from [User])


-- ROW_NUMBER() is a handy SQL function that assigns a unique sequential integer to rows within a result set,
-- based on the order specified in the ORDER BY clause
SELECT * FROM
(
	SELECT *, ROW_NUMBER() OVER(partition by [User].Name order by [User].Name DESC) as rowNumber FROM [User] WHERE [User].DeleteDate IS NULL
) AS Result
WHERE rowNumber = MAX(rowNumber)




-- SELECT *: Seleziona tutte le colonne dalla tabella [User].
/*
ROW_NUMBER() OVER(partition by [User].Name order by [User].Name DESC) as rowNumber:
Questa parte genera un numero di riga per ogni riga all'interno di ogni partizione del set di risultati.
Le partizioni sono create in base al valore della colonna [User].Name, e le righe all'interno di
ogni partizione sono ordinate in ordine decrescente (DESC) in base al valore [User].Name. Infine,
questo numero di riga viene aliasato come rowNumber

ROW_NUMBER() è una funzione di finestra in SQL che assegna un numero di riga unico per ogni riga,
basato su una partizione e ordinamento specificati. È molto utile quando si vuole avere un identificatore
unico per righe all'interno di un gruppo specifico di risultati. Ecco come funziona in dettaglio

La query esterna in pratica è necessaria per estrarre solo le righe numerate con 1 dalla subquery,
rappresentando così la versione più recente di ogni utente non eliminato. Senza la query esterna,
non potresti facilmente filtrare i risultati numerati.

L'alias è necessario per dare alla subquery un nome temporaneo che può essere riferito nella query esterna.
Senza un alias, la query esterna non avrebbe modo di riferirsi alla subquery e la query SQL risulterebbe non valida.
In questo caso, hai chiamato la subquery ResultPartitionUser, ed è esattamente ciò che permette alla query esterna
di utilizzare i dati della subquery e applicare il filtro WHERE rowNumber = 1
*/

SELECT * FROM
(
	SELECT *, (ROW_NUMBER() OVER(partition by [User].Name order by [User].VersionId DESC)) as rowNumber FROM [User] WHERE [User].DeleteDate IS NULL
) as ResultPartitionUser
WHERE rowNumber = 1



insert into [User] (Name,Description,Email,UserName,Age,Password,VersionId)
values ('Antonio','Descrizone','fff@gmail.com','Jacopo93',34,'testpq',2);

insert into [User] (Name,Description,Email,UserName,Age,Password,VersionId)
values ('Antonio','Descrizone','fff@gmail.com','Jacopo93',34,'testpq',3);

insert into [User] (Name,Description,Email,UserName,Age,Password,VersionId)
values ('Antonio','Descrizone','fff@gmail.com','Jacopo93',34,'testpq',4);

insert into [User] (Name,Description,Email,UserName,Age,Password,VersionId,DeleteDate)
values ('Jacopo','Descrizone','fff@gmail.com','Jacopo93',34,'testpq',1,'12/07/1993');