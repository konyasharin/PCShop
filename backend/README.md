# Инструкция по API

По ссылке https://localhost:7202/api/название компонента/создать компонент

Мы создаёт компонент с помощью введённых данных.
Для работы необходимо, чтобы страницы не были пустыми.

# На данных момент можно проверить работу Post запроса для каждого компонента на страницах:


1) https://localhost:7202/api/ComputerCase/createComputerCase

## Создаём корпус компуктера
### Входные данные:

```json
{
   
        "id": 1, //int
        "brand": "comptercase brand",  //string
        "model": "computercase model",  //string
        "country": "computercase country", //string
        "material": "computercase material", //string
		"weight": "computercase weight",  //int
		"height": "computercase height",  //int
		"depth": "computercase depth",  //int
		"price": "computercase price",  //int
		"description": "computercase description", //string
		"image": "computercase image"  //byte[]
  
}
```

### Ограничения:

1) Weight от 10 до 100
2) Height от 20 до 150
3) Depth от 20 до 100
4) Цена больше 0

### Пример ошибки

```json

"Height must be between 30 and 150"

```

2) https://localhost:7202/Processor/createProcessor

## Создаём Processor
### Входные данные: 

```json
{
    
        "id": 1,   //int
        "brand": "processor brand",  //string
        "model": "processor model",  //string
        "country": "processor country",  //string
        "cores": 5, //int
        "clock_frequency": "processor clock_frequency",  //int
		"turbo_frequency": "processor turbo_frequency",  //int
		"heat_dissipation": "processor heat_dissipation",  //int
		"price": "processor price",   //int
		"description": "processor description",   //string
		"image": "processor image"   //byte[]
}
```

### Ограничения:

1) Число ядер от 1 до 8
2) Цена больше 0
3) Heat_dissipation от 0 до 10000
4) Turbo_frequency от 0 до 100000 и должна быть больше clock_frequency
5) Clock_frequency от о до 50000 и должна быть меньше turbo_frequency


### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```

"Height must be between 30 and 150"

```

3) https://localhost:7202/MotherBoard/createMotherBoard

## Создаём MotherBoard
### Входные данные:

```json
{
    
        "id": 1,  //int
        "brand": "motherboard brand",   //string
        "model": "motherboard model",   //string
        "country": "motherboard country",  //string
        "frequency": "motherboard frequency",  //int
		"socket": "socket",   //string
		"chipset": "chipset",  //string
		"price": "motherboard price",  //int
		"description": "motherboard description",  //string
		"image": "motherboard image"  //byte[]
    
}
```
### Ограничения

1) Частота от 0 до 100000
2) Цены больше 0

### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```

4) https://localhost:7202/SSD/createSsd

## Создаём SSD
### Входные данные:

```json
{
    
        "id": 1,  //int
        "brand": "ssd brand",  //string
        "model": "ssd model",  //string
        "country": "ssd country", //string
        "capacity": "ssd capacity", //int
		"price": "ssd price", //int
		"description": "ssd description", //string
		"image": "ssd image" //byte[]
    
}
```
### Ограничения

1) Capacity от 0 до 10000
2) Price больше 0

### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```


5) https://localhost:7202/Ram/createRam

Создаём RAM
## Входные данные:

```json
{
    
        "id": 1,  //int
        "brand": "ram brand",  //string
        "model": "ram model",  //string
        "country": "ram country", //string
        "frequency": "ram frequency",  //int
		"timings": "ram timings",  //int
		"capacity_db": "capacity_db",  //int
		"price": "ram price",  //int
		"description": "ram description",  //string
		"image": "ram image"  //byte[]
    
}
```

### Ограничения

1) Цена больше 0
2) Timings и Capacity_db от 0 до 10000
3) Частота от 0 до 100000

### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```


6) https://localhost:7202/PowerUnit/createPowerUnit

## PowerUnit
### Входные данные:

```json
{
   
        "id": 1, //int
        "brand": "powerunit brand", //string
        "model": "powerunit model", //string
        "country": "powerunit country", //string
        "battery": "powerunit battery",  //int
		"voltage": "powerunit voltage", //int
		"price": "powerunit price", //int
		"description": "powerunit description", //string
		"image": "powerunit image" //byte[]
    
}
```
### ограничения

1) Цена больше 0
2) Напряжение от 0 до 50000

### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```

7) 

```json
{
   
        "id": 1, //int            
        "brand": "ram brand",                  //string
        "model": "ram model",                 //string
        "country": "ram country",             //string
        "memory_db": "videocard memory_db",  //int   
		"memory_type": "videocard memory_type",  //string  
		"price": "videocard price",                    //int
		"description": "videocard description",    //text
		"image": "videocard image"                 //byte[]
    
}
```

### Ограничения

1) Частота от 0 до 100000
2) Memory_db от 0 до 10000

### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```


8) https://localhost:7202/api/Cooler/createCooler

## Создаём Куллер
### Входные данные:

```json
{
   
        "id": 1, //int
        "brand": "cooler brand", //string
        "model": "ram model", //string
        "country": "ram country", //string
        "speed": "cooler speed",  //int  
		"power": "cooler power", //int
		"price": "ram price",  //int
		"description": "ram description",   //string
		"image": "ram image"   //byte[]
    
}
```
### Ограничения

1) Цена больше 0
2) Speed и power должны быть от 0 до 10000

### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```


9) https://localhost:7202/api/Assembly/createAssembly

## Создаём сборку
### Входные данные

```json
{
   
        "id": 1, //int
        "name": "cooler brand", //string
        "price": 566, //int
        "computerCaseId": 1,
        "coolerId": 1,
        "motherBoardId": 1,
        "processorId": 1,
        "ramId": 1,
        "ssdId": 1,
        "videoCardId": 1,
        "powerUnitId": 1,
		"likes": 56,
        "creation_time": "2024-02-28"
    
}
```

### Ограничения

1) Цена больше 0
2) Цена сборки складывается из цен компонентов + цена за сборку 3000

 ### Пример ошибки при несоблюдении диапазона численных данных

```json

"Height must be between 30 and 150"

```

   
# Для проверки Get, Put и Delete

### Везде выдаёт ошибку если price < 0 и все численные значения должны быть больше 0, но некоторые со своими особенностями

1) https://localhost:7202/api/ComputerCase/GetComputerCase/1

```json
{
    {
        "Id": 1,  //long
        "brand": "comptercase brand",  //string
        "model": "computercase model",  //string
        "country": "computercase country", //string
        "material": "computercase material", //string
		"weight": "computercase weight",  //int
		"height": "computercase height",  //int
		"depth": "computercase depth",  //int
		"price": "computercase price",  //int
		"description": "computercase description", //string
		"image": "computercase image"  //byte[]
    }
}
```

### Update with Put

```json
	{
		"ComputerCase data updated in the database"
	}


    {
        "ComputerCase data deleted from the database"
    }
```

https://localhost:7202/ComputerCase/UpdateComputerCase/1

https://localhost:7202/ComputerCase/DeleteComputerCase/1

### Get All 

https://localhost:7202/api/ComputerCase/GetAllComputerCases

## Ошибки

### width, height и depth не могут быть больше 100, 150 и 150 соответственно

2) https://localhost:7202/api/Processor/GetProcessor/1

```json
{
     {
        "Id": 1,  //long
        "brand": "processor brand",  //string
        "model": "processor model",  //string
        "country": "processor country",  //string
        "clock_frequency": "processor clock_frequency",  //int
		"turbo_frequency": "processor turbo_frequency",  //int
		"heat_dissipation": "processor heat_dissipation",  //int
		"price": "processor price",   //int
		"description": "processor description",   //string
		"image": "processor image"   //byte[]
    }
}
```

### Update with Put

```json
	{
		"Processor data updated in the database"
	}


    {
        "Processor data deleted from the database"
    }
```

https://localhost:7202/ComputerCase/Updateprocessor/1

https://localhost:7202/ComputerCase/DeleteProcessor/1

### Get All 

https://localhost:7202/api/Processor/GetAllProcessors

## Ошибки
### clock_frequency должен быть от 0 до 50000 и меньше турбоfrequency

3) https://localhost:7202/api/MotherBoard/GetMotherBoard/1

```json
{
    {
        "Id": 1,  //long
        "brand": "motherboard brand",   //string
        "model": "motherboard model",   //string
        "country": "motherboard country",  //string
        "frequency": "motherboard frequency",  //int
		"socket": "socket",   //string
		"chipset": "chipset",  //string
		"price": "motherboard price",  //int
		"description": "motherboard description",  //string
		"image": "motherboard image"  //byte[]
    }
}
```

### Update with Put

```json
	{
		"MotherBoard data updated in the database"
	}


    {
        "MotherBoard data deleted from the database"
    }
```

https://localhost:7202/ComputerCase/UpdateMotherBoard/1

https://localhost:7202/ComputerCase/DeleteMotherBoard/1

### Get All 

https://localhost:7202/api/MotherBoard/GetAllMotherBoards



4) https://localhost:7202/api/SSD/GetSsd/1

```json
{
    
    {
        "Id": 1, //long
        "brand": "ssd brand",  //string
        "model": "ssd model",  //string
        "country": "ssd country", //string
        "capacity": "ssd capacity", //int
		"price": "ssd price", //int
		"description": "ssd description", //string
		"image": "ssd image" //byte[]
    }
}
```
### Update with Put

```json
	{
		"SSD data updated in the database"
	}


    {
        "SSD data deleted from the database"
    }
```

https://localhost:7202/Ssd/UpdateSsd/1

https://localhost:7202/Ssd/DeleteSsd/1

### Get All 

https://localhost:7202/api/Ssd/GetAllSsd

5) https://localhost:7202/api/RAM/GetRam/1

```json
{
    
    {
        "Id": 1,  //long
        "brand": "ram brand",  //string
        "model": "ram model",  //string
        "country": "ram country", //string
        "frequency": "ram frequency",  //int
		"timings": "ram timings",  //int
		"capacity_db": "capacity_db",  //int
		"price": "ram price",  //int
		"description": "ram description",  //string
		"image": "ram image"  //byte[]
    }
}
```

### Update with Put

```json
	{
		"RAM data updated in the database"
	}


    {
        "RAM data deleted from the database"
    }
```

https://localhost:7202/Ram/UpdateRam/1

https://localhost:7202/Ram/DeleteRam/1

### Get All 

https://localhost:7202/api/Ram/GetAllRam

6) https://localhost:7202/api/PowerUnit/GetPowerUnit/1

```json
{
     {
        "Id": 1,  //string
        "brand": "powerunit brand", //string
        "model": "powerunit model", //string
        "country": "powerunit country", //string
        "battery": "powerunit battery",  //int
		"voltage": "powerunit voltage", //int
		"price": "powerunit price", //int
		"description": "powerunit description", //string
		"image": "powerunit image" //byte[]
    }
}
```
### Выдаёт ошибку если price < 0 and voltage must be in range from 0 to 50000

### Update with Put

```json
	{
		"PowerUnit data updated in the database"
	}


    {
        "PowerUnit data deleted from the database"
    }
```

https://localhost:7202/PowerUnit/UpdatePowerUnit/1

https://localhost:7202/PowerUnit/DeletePowerUnit/1

### Get All 

https://localhost:7202/api/PowerUnit/GetAllPowerUnits

7) https://localhost:7202/api/VideoCard/GetVideoCard/1

```json
{
    
     {
        "Id": 1,                                //long
        "brand": "brand",                  //string
        "model": "model",                 //string
        "country": " country",             //string
        "memory_db": "videocard memory_db",  //int   
		"memory_type": "videocard memory_type",  //string
		"capacity_db": "videocard capacity_db",   
		"price": "videocard price",                    //int
		"description": "videocard description",    //text
		"image": "videocard image"                 //byte[]
    }
}
```

### Update with Put

```json
	{
		"VideoCard data updated in the database"
	}


    {
        "VideoCard data deleted from the database"
    }
```

https://localhost:7202/VideoCard/UpdateVideoCard/1

https://localhost:7202/VideoCard/DeleteVideCard/1

### Get All 

https://localhost:7202/api/VideoCard/GetAllVideoCards

8) https://localhost:7202/api/Cooler/GetCooler/1

```json
{
    
     {
        "Id": 1,
        "brand": "cooler brand", //string
        "model": "ram model", //string
        "country": "ram country", //string
        "speed": "cooler speed",  //int  
		"power": "cooler power", //int
		"price": "ram price",  //int
		"description": "ram description",   //string
		"image": "ram image"   //byte[]
    }
}
```

### Update with Put

```json
	{
		"Cooler data updated in the database"
	}


    {
        "Cooler data deleted from the database"
    }
```

https://localhost:7202/Cooler/UpdateCooler/1

https://localhost:7202/Cooler/DeleteCooler/1

### Get All 

https://localhost:7202/api/Cooler/GetAllCoolers

9) https://localhost:7202/api/Assembly/getAssembly/1

### Errors

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-02b585e18cfb3c1206ecc5b3e4603a00-4425c4a7f2e4991e-00"

    "Internal server error" //При неверном индексе
}
```
10) https://localhost:7202/api/Assembly/getAllAssemblies

Получение всех сборок
# Это также формат для вывода всех данных!!

```json

    [
    {
        "id": 1,
        "name": "cooler brand",
        "price": 400,
        "likes": 58,
        "creation_time": "2024-02-28T00:00:00",
        "computerCaseId": 1,
        "coolerId": 1,
        "motherBoardId": 1,
        "powerUnitId": 1,
        "processorId": 1,
        "ramId": 1,
        "ssdId": 1,
        "videoCardId": 1
    },
    {
        "id": 2,
        "name": "cooler brand",
        "price": 4310,
        "likes": 0,
        "creation_time": "2024-02-28T00:00:00",
        "computerCaseId": 1,
        "coolerId": 1,
        "motherBoardId": 1,
        "powerUnitId": 1,
        "processorId": 1,
        "ramId": 1,
        "ssdId": 1,
        "videoCardId": 1
    },
    {
        "id": 3,
        "name": "cooler brand",
        "price": 4310,
        "likes": 0,
        "creation_time": "2024-02-29T03:02:15.333766",
        "computerCaseId": 1,
        "coolerId": 1,
        "motherBoardId": 1,
        "powerUnitId": 1,
        "processorId": 1,
        "ramId": 1,
        "ssdId": 1,
        "videoCardId": 1
    }
]
```

11) https://localhost:7202/api/Assembly/getAssembly/2

Получение сборки по индексу

12) https://localhost:7202/api/Assembly/deleteAssembly/3

Удаление сборки по индексу

13) https://localhost:7202/api/Assembly/updateAssembly/3

обновление по индексу

14) https://localhost:7202/api/Assembly/getAllAssemblies/desc

Вывод сборок по времени убывания

15) https://localhost:7202/api/Assembly/getAllAssemblies/asc

Вывод сборок по времени возрастания

16) https://localhost:7202/api/Assembly/likeAssembly/1

Лайк сборки

17)  https://localhost:7202/api/Assembly/getPopularAssemblies

Вывод популярных сборок