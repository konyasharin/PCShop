Инструкция по API

По ссылке https://localhost:7202/api/название компонента/создать компонент

Мы создаёт компонент с помощью введённых данных
Для работы необходимо, чтобы страницы не были пустыми
На данных момент можно проверить работу Post запроса для каждого компонента на страницах:


https://localhost:7202/api/ComputerCase/createComputerCase


https://localhost:7202/api/Processor/createProcessor


https://localhost:7202/api/MotherBoard/createMotherBoard


https://localhost:7202/api/SSD/createSsd


https://localhost:7202/api/RAM/createRam


https://localhost:7202/api/PowerUnit/createPowerUnit


https://localhost:7202/api/VideoCard/createVideoCard


https://localhost:7202/api/Cooler/createCooler


 вид json ответа
 
 
 {
 
  "Component": "ComputerCase",
  
  "id": 1, 
  
  "Data": {
  
    // данные о компоненте
    
  }
  
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