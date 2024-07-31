resource "azurerm_container_app_environment" "managed_environment" {
  name                           = var.environment
  location                       = var.location
  resource_group_name            = var.resource_group_name
  log_analytics_workspace_id     = var.workspace_id
  infrastructure_subnet_id       = var.infrastructure_subnet_id
  internal_load_balancer_enabled = var.internal_load_balancer_enabled
  tags                           = var.tags

  lifecycle {
    ignore_changes = [
      tags
    ]
  }
}

resource "azurerm_container_app" "container_app" {
  for_each = { for app in var.container_apps : app.name => app }

  name                         = each.key
  resource_group_name          = var.resource_group_name
  container_app_environment_id = azurerm_container_app_environment.managed_environment.id
  tags                         = var.tags
  revision_mode                = each.value.revision_mode

  template {
    dynamic "container" {
      for_each = coalesce(each.value.template.containers, [])
      content {
        name    = container.value.name
        image   = container.value.image
        args    = try(container.value.args, null)
        command = try(container.value.command, null)
        cpu     = container.value.cpu
        memory  = container.value.memory

        dynamic "env" {
          for_each = coalesce(container.value.env, [])
          content {
            name        = env.value.name
            secret_name = try(env.value.secret_name, null)
            value       = try(env.value.value, null)
          }
        }
      }
    }
    min_replicas    = try(each.value.template.min_replicas, null)
    max_replicas    = try(each.value.template.max_replicas, null)
    revision_suffix = try(each.value.template.revision_suffix, null)

    dynamic "volume" {
      for_each = each.value.template.volume != null ? [each.value.template.volume] : []
      content {
        name         = volume.value.name
        storage_name = try(volume.value.storage_name, null)
        storage_type = try(volume.value.storage_type, null)
      }
    }
  }

  dynamic "ingress" {
    for_each = each.value.ingress != null ? [each.value.ingress] : []
    content {
      allow_insecure_connections = try(ingress.value.allow_insecure_connections, null)
      external_enabled           = try(ingress.value.external_enabled, null)
      target_port                = ingress.value.target_port
      transport                  = ingress.value.transport

      dynamic "traffic_weight" {
        for_each = coalesce(ingress.value.traffic_weight, [])
        content {
          label           = traffic_weight.value.label
          latest_revision = traffic_weight.value.latest_revision
          revision_suffix = traffic_weight.value.revision_suffix
          percentage      = traffic_weight.value.percentage
        }
      }
    }
  }

  dynamic "secret" {
    for_each = each.value.secrets != null ? [each.value.secrets] : []
    content {
      name  = secret.value.name
      value = secret.value.value
    }
  }

  lifecycle {
    ignore_changes = [
      tags
    ]
  }
}
