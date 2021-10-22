# StartupElectonicWallet

Este proyecto no se instala, trabaja con datos en memoria según los requerimientos.
Pasos:
1. Descargar el proeycto del repositorio 
2. Se debe extraer y cargar el archivo Colecction de Postman ubicado en la carpeta Postman del proyecto
3. Se debe ejecutar la aplicación desde el Visual Studio
4. Se debe hacer Login en el siguiente servicio 
    POST: api/Authentication
    Datos de prueba: {"Username":"wsUser", "Password":"123456"}
    Este servicio devuelve un token para ser usado en el resto de los servicios mediante autenticación Bearer
5. Se deben agregar los cliente a través del servicio 
    POST: api/v1/Customer/
    Datos de prueba: {
    "CustomerIdentifier": "1234",
    "Name": "Jose",
    "LastName": "Perez",
    "Addreess": "Casa",
    "Birthdate": "2000-10-18"
}
6. Se consulta a los clientes a través del servicio
    GET: api/v1/Customer/
         api/v1/Customer/{id}
         
         Response: {
        "customerID": 1,
        "customerIdentifier": "1235",
        "name": "Jose",
        "lastName": "Perez",
        "addreess": "Casa",
        "birthdate": "2000-10-18T00:00:00",
        "dateCreate": "0001-01-01T00:00:00",
        "statusId": 0
    }
7. Luego se pueden crear cuentas a los clientes por medio del servicio
      POST:api/v1/CustomerAccount/
      Datos de prueba: {"CustomerId":2,"StatusId":1,"AvailableAmount":200}
             Response: {
                "isSuccess": true,
                "message": "Registro guardado.",
                "dateTimeResponse": "2021-10-22T14:12:09.8830936-05:00"
                 }
8. Se pueden consultar las cuentas creadas por medio del servicio
      GET: api/v1/CustomerAccount/{1} 
           api/v1/CustomerAccount/
           Response: {
                      "accountId": 2,
                      "customerId": 2,
                      "statusId": 1,
                      "availableAmount": 600,
                      "dateCreate": "2021-10-22T14:12:09.8813897-05:00",
                      "transactionCollection": [
                          {
                              "transactionId": 2,
                              "accountId": 2,
                              "transactionType": "DP",
                              "accountToId": 0,
                              "amount": 200,
                              "dateCreate": "2021-10-21T00:00:00"
                          },
                          {
                              "transactionId": 3,
                              "accountId": 1,
                              "transactionType": "TF",
                              "accountToId": 2,
                              "amount": 100,
                              "dateCreate": "2021-10-21T00:00:00"
                          },
                          {
                              "transactionId": 4,
                              "accountId": 1,
                              "transactionType": "TF",
                              "accountToId": 2,
                              "amount": 100,
                              "dateCreate": "2021-10-21T00:00:00"
                          }
                      ]
                  }
9. Para realizar transacciones en las cuentas se tiene el servicio
      POST: api/v1/AccountTransaction/
      Tenemos tres tipos de operaciones: (Este código debe indicarse en el atributo TransactionType según la operación que se desee), para el caso de las transferencias debe indicarse la cuenta de destino.
      // TransactionType
      // DP: Depósito
      // RT: Retiro/Pago
      // TF: Transferencia

          Datos de prueba: {
                          "TransactionType":"TF",
                          "AccountId":1,
                          "AccountToId":2,
                          "Amount":100
                          }
10. Los moviemientos pueden observarse por medio del servicio de consulta a las cuentas y sus movimientos (Paso 8)

