# Инструкция по API

По ссылке https://localhost:7202/api/название компонента/создать компонент

Мы создаёт компонент с помощью введённых данных.


1) https://localhost:7202/api/сomputerCase/create

## Создаём корпус комьютера
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

2) https://localhost:7202/api/assembly/create

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

   
# Для проверки Get, Put и Delete

3) https://localhost:7202/api/computerCase/get/1

```json
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
```

https://localhost:7202/сomputerCase/update/1

https://localhost:7202/сomputerCase/delete/1

### Get All 

https://localhost:7202/api/сomputerCase/getAll

4) https://localhost:7202/api/assembly/getAll

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

11) https://localhost:7202/api/assembly/getPopular

Получение популярной сборки

12) https://localhost:7202/api/assembly/getLast

Получение последней сборки