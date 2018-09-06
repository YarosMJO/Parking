# Parking API
### ASP.NET Core Web API that allows connect to the parking  emulation program. 

# GET

Show free spaces /api/parking/free_spaces
Show occupied spaces /api/parking/occupied_spaces

Show transaction history for the last minute /api/transactions/current_transactions
Show transaction history for the last minute for certain car api/transactions/car_current_transactions/{id}
Show Transcations.log /api/transactions/transaction_logs

Show car by id /api/cars/{id}
Show all cars /api/cars/

Show parking balances /api/parking/total_balance

# POST
Add car /api/cars/{type}&{balance:int} Where type = 1 Passenger | type = 2 Truck | type = 3 Bus | type = 4 Motorcycle

# PUT
Recharge car balance /api/transactions/put/{id}&{balance:int}

# DELETE
Remove car /api/cars/delete/{id}
