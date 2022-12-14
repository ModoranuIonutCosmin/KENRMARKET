resource "azurerm_kubernetes_cluster" "cluster_microservices" {
  name                = var.cluster_name
  location            = var.location
  resource_group_name = var.resource_group_name
  dns_prefix = "store"

  default_node_pool {
    type = "VirtualMachineScaleSets"
    name       = "default"
    max_count = 3
    min_count = 1
    enable_auto_scaling = true

    vm_size    = "Standard_B2s"
  }

  identity {
    type = "SystemAssigned"
  }

  tags = {
    Environment = "Production"
  }
}