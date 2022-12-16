

./generate_env_map.ps1 secretkeyvaultallkmarket connection-strings.yaml "*"


$connection_strings = Get-Content .\connection-strings.yaml -Raw

$conns_map = ConvertFrom-StringData -StringData $connection_strings -Delimiter '*'



echo $conns_map

Set-Location ($args[0] ?? "../../src/")

# Migrate Carts

dotnet-ef database update --connection $conns_map['SQL-CONNECTIONSTRING-CART'].Trim("""") --startup-project ./Cart/Cart.API --project ./Cart/Cart.Infrastructure

# Migrate Customers

dotnet-ef database update --connection $conns_map['SQL-CONNECTIONSTRING-CUSTOMER'].Trim("""") --startup-project ./Customers/Customers.API --project ./Customers/Customers.Infrastructure

# Migrate Gateway (Auth)

dotnet-ef database update --connection $conns_map['SQL-CONNECTIONSTRING-AUTH'].Trim("""") --startup-project ./Gateway/Gateway.API --project ./Gateway/Gateway.Infrastructure

# Migrate Order

dotnet-ef database update --connection $conns_map['SQL-CONNECTIONSTRING-ORDER'].Trim("""") --startup-project ./Order/Order.API --project ./Order/Order.Infrastructure

# Migrate Payments

dotnet-ef database update --connection $conns_map['SQL-CONNECTIONSTRING-PAYMENT'].Trim("""") --startup-project ./Payments/Payments.API --project ./Payments/Payments.Infrastructure

