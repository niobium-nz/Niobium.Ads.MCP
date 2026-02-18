targetScope = 'resourceGroup'

@description('Location for deployed resources. If empty, uses the resource group location.')
param location string = resourceGroup().location

@description('Short name used as a prefix for Azure resources. Keep it globally unique where required.')
param appName string = 'adslibrary'

@description('Name of the Container Apps managed environment.')
param containerAppsEnvironmentName string = '${appName}-cae'

@description('Name of the Container App.')
param containerAppName string = '${appName}-ca'

@description('Container image to deploy to Azure Container Apps (e.g., docker.io/5he11/adslibrary:sha-<commit>).')
param containerImage string

@secure()
@description('ScrapeCreators API key passed to the container as SCRAPECREATORS_API_KEY.')
param scrapecreatorsApiKey string

@description('The external ingress target port for the container.')
param ingressTargetPort int = 8080

@description('Environment variables for the container app (array of {name, value} objects).')
param envVars array = []

var logAnalyticsName = '${appName}-law'
var environmentName = containerAppsEnvironmentName
var containerAppResourceName = containerAppName

module logAnalytics './modules/log-analytics.bicep' = {
  name: 'logAnalytics'
  params: {
    name: logAnalyticsName
    location: location
  }
}

module managedEnvironment './modules/managed-environment.bicep' = {
  name: 'managedEnvironment'
  params: {
    name: environmentName
    location: location
    logAnalyticsWorkspaceId: logAnalytics.outputs.id
    logAnalyticsCustomerId: logAnalytics.outputs.customerId
  }
}

module containerApp './modules/container-app.bicep' = {
  name: 'containerApp'
  params: {
    name: containerAppResourceName
    location: location
    managedEnvironmentId: managedEnvironment.outputs.id
    containerImage: containerImage
    ingressTargetPort: ingressTargetPort
    scrapecreatorsApiKey: scrapecreatorsApiKey
    containerAppEnv: envVars
  }
}

output containerAppId string = containerApp.outputs.id
output containerAppFqdn string = containerApp.outputs.fqdn