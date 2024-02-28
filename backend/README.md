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



2) https://localhost:7202/Processor/createProcessor

## Создаём Processor
### Входные данные: 

```json
{
    
        "id": 1,   //int
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





5) https://localhost:7202/RAM/createRam

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



6) https://localhost:7202/PowerUnit/createPowerUnit

## PowerUnit
### Входные данные:

```json
{
   
        "id": 1, //long
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




```json
{
   
        "id": 1, //int            
        "brand": "ram brand",                  //string
        "model": "ram model",                 //string
        "country": "ram country",             //string
        "memory_db": "videocard memory_db",  //int   
		"memory_type": "videocard memory_type",  //string
		"capacity_db": "videocard capacity_db",   
		"price": "videocard price",                    //int
		"description": "videocard description",    //text
		"image": "videocard image"                 //byte[]
    
}
```



8) https://localhost:7202/api/Cooler/createCooler

## Создаём Куллер
### Входные данные:

```json
{
   
        "id": 1, //long
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


 
   
# Для проверки Get, Put и Delete

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