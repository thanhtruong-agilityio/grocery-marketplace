variable "resource_group_name" {
  description = "(Required) Specifies the resource group name"
  type        = string
}

variable "location" {
  description = "(Required) Specifies the location of the resource group"
  type        = string
}

variable "environment" {
  description = "Specifies the name of the environment"
  type        = string
  default     = "dev"
}

variable "app_name" {
  description = "Specifies the name of the application"
  type        = string
  default     = "GroceryMarketplace"
}
