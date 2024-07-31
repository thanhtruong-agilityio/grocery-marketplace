variable "location" {
  description = "The Azure Region in which all resources in this example should be created."
  type        = string
  default     = "southeastasia"
}

variable "resource_group_name" {
  description = "The name of the resource group."
  type        = string
  default     = "rg"
}

variable "baseName" {
  description = "This is the base name for each Azure resource name (6-12 chars)"
  type        = string
  default     = "gmaz"
}

variable "tags" {
  description = "(Optional) Specifies tags for all the resources"
  default = {
    createdWith   = "Terraform",
    containerApps = "true"
  }
}

variable "enable_ddos_protection" {
  description = "Specifies if DDoS protection is enabled"
  default     = false
  type        = bool
}

variable "app_name" {
  description = "The name of the application"
  type        = string
  default     = "GroceryMarketplace"
}

variable "log_analytics_workspace_name" {
  description = "Specifies the name of the log analytics workspace"
  default     = "Workspace"
  type        = string
}

variable "log_analytics_retention_days" {
  description = "Specifies the number of days of the retention policy for the log analytics workspace."
  type        = number
  default     = 30
}

variable "environment" {
  description = "Specifies the name of the managed environment."
  type        = string
}

variable "aca_subnet_name" {
  description = "Specifies the name of the subnet"
  default     = "ContainerApps"
  type        = string
}

variable "container_apps" {
  description = "Specifies the container apps in the managed environment."
  type = list(object({
    name          = string
    revision_mode = optional(string)
    ingress = optional(object({
      allow_insecure_connections = optional(bool)
      external_enabled           = optional(bool)
      target_port                = optional(number)
      transport                  = optional(string)
      traffic_weight = optional(list(object({
        label           = optional(string)
        latest_revision = optional(bool)
        revision_suffix = optional(string)
        percentage      = optional(number)
      })))
    }))
    secrets = optional(list(object({
      name  = string
      value = string
    })))
    template = object({
      containers = list(object({
        name    = string
        image   = string
        args    = optional(list(string))
        command = optional(list(string))
        cpu     = optional(number)
        memory  = optional(string)
        env = optional(list(object({
          name        = string
          secret_name = optional(string)
          value       = optional(string)
        })))
      }))
      min_replicas    = optional(number)
      max_replicas    = optional(number)
      revision_suffix = optional(string)
      volume = optional(list(object({
        name         = string
        storage_name = optional(string)
        storage_type = optional(string)
      })))
    })
  }))
}

variable "vnet_name" {
  description = "Specifies the name of the virtual network"
  default     = "VNet"
  type        = string
}

variable "vnet_address_space" {
  description = "Specifies the address prefix of the virtual network"
  default     = ["10.0.0.0/16"]
  type        = list(string)
}

variable "aca_subnet_address_prefix" {
  description = "Specifies the address prefix of the Azure Container Apps environment subnet"
  default     = ["10.0.0.0/20"]
  type        = list(string)
}

variable "private_endpoint_subnet_name" {
  description = "Specifies the name of the subnet"
  default     = "PrivateEndpoints"
  type        = string
}

variable "private_endpoint_subnet_address_prefix" {
  description = "Specifies the address prefix of the private endpoints subnet"
  default     = ["10.0.16.0/24"]
  type        = list(string)
}

variable "application_insights_name" {
  description = "Specifies the name of the application insights resource."
  default     = "ApplicationInsights"
  type        = string
}

variable "application_insights_application_type" {
  description = "(Required) Specifies the type of Application Insights to create. Valid values are ios for iOS, java for Java web, MobileCenter for App Center, Node.JS for Node.js, other for General, phone for Windows Phone, store for Windows Store and web for ASP.NET. Please note these values are case sensitive; unmatched values are treated as ASP.NET by Azure. Changing this forces a new resource to be created."
  type        = string
  default     = "web"
}
