Инструкция по API

По ссылке https://localhost:7202/api/название компонента/создать компонент

Мы создаёт компонент с помощью введённых данных
Для работы необходимо, чтобы страницы не были пустыми
На данных момент можно проверить работу Post запроса для каждого компонента на страницах:


https://localhost:7202/api/ComputerCase/createComputerCase

Создаём корпус компуктера
**Входные данные:**

{
	"name": "computercase name"
	"width": "computercase width"
	"height": "computercase height"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/Processor/createProcessor

Создаём Processor
**Входные данные:**

{
	"name": "processor name"
	"power": "processor power"
	"cores": "processor cores"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/MotherBoard/createMotherBoard

Создаём MotherBoard
**Входные данные:**

{
	"name": "motherboard name"
	"socket": "ram socket"
	"chipset": "ram chipset"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/SSD/createSsd

Создаём SSD
**Входные данные:**

{
	"name": "ssd name"
	"brand": "SSd brand"
	"capacity": "SSD capacity"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/RAM/createRam

Создаём RAM
**Входные данные:**

{
	"name": "ram name"
	"frequecny": "ram frequency"
	"timings": "ram timings"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/PowerUnit/createPowerUnit

Создаём корпус компуктера
**Входные данные:**

{
	"name": "computercase name"
	"width": "computercase width"
	"height": "computercase height"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/VideoCard/createVideoCard

Создаём Видеокарту
**Входные данные:**

{
	"name": "название видеокарты"
	"model": "Модель видеокарты"
	"capacity_db_": "Videocard capacity_db_"

}

Ошибка

{
	"error": "error text"
}


https://localhost:7202/api/Cooler/createCooler

Создаём Куллер
**Входные данные:**

{
	"name": "Название куллера"
	"model": "Модель куллера"
	"power": "Мощность куллера"

}

Ошибка

{
	"error": "error text"
}



 
   
Для проверки Get, Put и Delete

https://localhost:7202/api/ComputerCase/1


https://localhost:7202/api/Processor/1


https://localhost:7202/api/MotherBoard/1


https://localhost:7202/api/SSD/1


https://localhost:7202/api/RAM/1


https://localhost:7202/api/PowerUnit/1


https://localhost:7202/api/VideoCard/1


https://localhost:7202/api/Cooler/1