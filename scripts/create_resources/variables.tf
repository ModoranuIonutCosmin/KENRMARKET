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


variable "key_vault_name" {
    type = string
    default = "secretskeyvaultkmarket02"
}

variable "cluster_name" {
    type = string
    default = "kenrmarket-aks"
}
