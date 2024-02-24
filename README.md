Инструкция по API

По ссылке https://localhost:7202/api/название компонента/создать компонент

Мы создаёт компонент с помощью введённых данных
Для работы необходимо, чтобы страницы не были пустыми
На данных момент можно проверить работу Post запроса для каждого компонента на страницах:


https://localhost:7202/api/ComputerCase/createComputerCase


https://localhost:7202/api/ComputerCase/createProcessor


https://localhost:7202/api/ComputerCase/createMotherBoard


https://localhost:7202/api/ComputerCase/createSsd


https://localhost:7202/api/ComputerCase/createRam


https://localhost:7202/api/ComputerCase/createPowerUnit


https://localhost:7202/api/ComputerCase/createVideoCard


https://localhost:7202/api/ComputerCase/createCooler


 вид json ответа
 
 
 {
 
  "Component": "ComputerCase",
  
  "id": 1, 
  
  "Data": {
  
    // данные о компоненте
    
  }
  
}
   
