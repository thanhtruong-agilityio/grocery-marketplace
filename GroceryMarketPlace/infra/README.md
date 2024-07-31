# Deploy Grocery Marketplace on Azure using Terraform

This repo contains terraform code for deploying Grocery Marketplace on Azure

* [Getting started](#getting-started)
  * [Prerequisites](#prerequisites)
* [Provision environment](#provision-environment)
* [Build](#build)
* [Deploy](#deploy)
  * [Push images to Azure Container Registry](#push-images-to-azure-container-registry)
  * [Deploy Container Apps](#deploy-container-apps)

## Getting started

### Prerequisites

* Azure account (with at least Contributor role on a resource group)
* ASP.NET core 8
* Terraform >=1.9.2
* Azure CLI >=2.61.0

Download the project:

```sh
git clone git@gitlab.asoft-python.com:thanh.truong/dotnet-training.git

cd GroceryMarketplace
```

## Provision environment

Create a variables file for the environments (dev/prod/staging) like so:

```sh
touch infra/env/dev.tfvars
```

Add the following variables to the file:

```hcl
resource_group_name = "<name-of-resource-group>"
location            = "<location>"
...
```

Run Terraform:

```sh
cd infra
terraform plan -out=tfplan

# Verify the output and apply.
terraform apply tfplan
```

## Build

BDD

## Deploy

### Push images to Azure Container Registry

BDD

### Deploy Container Apps

#### Terraform

BDD

## Authors

- ThanhTruong (<thanh.truong@asnet.com.vn>)
