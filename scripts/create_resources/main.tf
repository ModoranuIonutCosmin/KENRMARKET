data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "rg_kenrmarket" {
  location = var.location
  name     = var.resource_group_name
}


module "clusters" {
  source = "./cluster"
  environment = var.environment
  resource_group_name = azurerm_resource_group.rg_kenrmarket.name
  location = var.location
  cluster_name = var.cluster_name
}

module "data" {
  source = "./data"
  environment = var.environment
  resource_group_name = azurerm_resource_group.rg_kenrmarket.name
  location = var.location
}

module "service_bus" {
  source = "./service buses"
  environment = var.environment
  resource_group_name = azurerm_resource_group.rg_kenrmarket.name
  location = var.location
}