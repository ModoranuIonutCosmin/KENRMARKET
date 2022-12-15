variable "location" {
    type = string
    default = "francecentral"
}

variable "resource_group_name" {
    type = string
    default = "KENRMARKET"
}

variable "environment" {
    type = string
    default = "dev"
}

variable "backend_access_key" {
  type = string
  description = "Access key for a terraform backend"
}

variable "key_vault_name" {
    type = string
    default = "secretskeyvaultkmarket02"
}

variable "cluster_name" {
    type = string
    default = "kenrmarket-aks"
}
