terraform {
  backend "azurerm"{
    # storage_account_name = "terraformstoragekmarket"
    # container_name       = "terraform"
    # key                  = "terraform/terraform.tfstate"
    # sas_token = "some_sas_token"
    # access_key = ""
  }
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "3.31.0"
    }
  }
}

provider "azurerm" {
   features {
     resource_group {
       prevent_deletion_if_contains_resources = false
     }
     key_vault {
        purge_soft_delete_on_destroy    = true
        recover_soft_deleted_key_vaults = true
     }
   }
}
