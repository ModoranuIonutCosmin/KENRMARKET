
Set-Location ($args[0] ?? "../../src/")

# Migrate Carts

dotnet-ef database update --startup-project ./Cart/Cart.API --project ./Cart/Cart.Infrastructure

# Migrate Customers

dotnet-ef database update  --startup-project ./Customers/Customers.API --project ./Customers/Customers.Infrastructure

# Migrate Gateway (Auth)

dotnet-ef database update  --startup-project ./Gateway/Gateway.API --project ./Gateway/Gateway.Infrastructure

# Migrate Order

dotnet-ef database update  --startup-project ./Order/Order.API --project ./Order/Order.Infrastructure

# Migrate Payments

dotnet-ef database update  --startup-project ./Payments/Payments.API --project ./Payments/Payments.Infrastructure

