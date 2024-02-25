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
    "Component": "ComputerCase",
    "id": 1, //long
    "Data": {
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

### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```

2) https://localhost:7202/api/Processor/createProcessor

## Создаём Processor
### Входные данные: 

```json
{
    "Component": "Processor",   //string
    "id": 1,   //long
    "Data": {
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

### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```


3) https://localhost:7202/api/MotherBoard/createMotherBoard

## Создаём MotherBoard
### Входные данные:

```json
{
    "Component": "MotherBoard",  //string
    "id": 1,  //long
    "Data": {
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


### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```

4) https://localhost:7202/api/SSD/createSsd

## Создаём SSD
### Входные данные:

```json
{
    "Component": "SSD",  //string
    "id": 1,  //long
    "Data": {
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


### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```


5) https://localhost:7202/api/RAM/createRam

Создаём RAM
## Входные данные:

```json
{
    "Component": "RAM",  //string
    "id": 1,  //long
    "Data": {
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

### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```


6) https://localhost:7202/api/PowerUnit/createPowerUnit

## PowerUnit
### Входные данные:

```json
{
    "Component": "PowerUnit", //string
    "id": 1, //long
    "Data": {
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

### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```


7) https://localhost:7202/api/VideoCard/createVideoCard

## Создаём Видеокарту
### Входные данные:

```json
{
    "Component": "VideoCard",
    "id": 1,                                    //long
    "Data": {
        "Id": 1,                                //long
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
}
```

### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```


8) https://localhost:7202/api/Cooler/createCooler

## Создаём Куллер
### Входные данные:

```json
{
    "Component": "Cooler",  //long
    "id": 1,
    "Data": {
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

### Ошибка

```json
{
    "statusCode": 500,
    "message": "Internal server error"
}
```



 
   
# Для проверки Get, Put и Delete

1) https://localhost:7202/api/ComputerCase/1

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
		"ComputerCase data with Index {id} updated"
	}

'''

```json
{
	"ComputerCase data with Index {id} deleted"
}
```

2) https://localhost:7202/api/Processor/1

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
		"Processor data with Index {id} updated"
	}

'''

```json
{
	"Processor data with Index {id} deleted"
}
```


3) https://localhost:7202/api/MotherBoard/1

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
		"MotherBoard data with Index {id} updated"
	}

'''

```json
{
	"MotherBoard data with Index {id} deleted"
}
```



4) https://localhost:7202/api/SSD/1

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
		"SSD data with Index {id} updated"
	}

'''

```json
{
	"SSD data with Index {id} deleted"
}
```

5) https://localhost:7202/api/RAM/1

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
		"RAM data with Index {id} updated"
	}

'''

```json
{
	"RAM data with Index {id} deleted"
}
```

6) https://localhost:7202/api/PowerUnit/1

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
		"PowerUnit data with Index {id} updated"
	}

'''

```json
{
	"PowerUnit data with Index {id} deleted"
}
```

7) https://localhost:7202/api/VideoCard/1

```json
{
    
     {
        "Id": 1,                                //long
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
}
```

### Update with Put

```json
	{
		"VideoCard data with Index {id} updated"
	}

'''

```json
{
	"VideoCard data with Index {id} deleted"
}
```

8) https://localhost:7202/api/Cooler/1

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
		"Cooler data with Index {id} updated"
	}

'''

```json
{
	"Cooler data with Index {id} deleted"
}
```