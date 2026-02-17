@description('Name of the Container App.')
param name string

@description('Location of the Container App.')
param location string

@description('Resource ID of the managed environment.')
param managedEnvironmentId string

@description('Container image to deploy (e.g., docker.io/5he11/adslibrary:sha-<commit>).')
param containerImage string

@description('The external ingress target port for the container.')
param ingressTargetPort int = 443

@secure()
@description('ScrapeCreators API key passed to the container as SCRAPECREATORS_API_KEY.')
param scrapecreatorsApiKey string

resource containerApp 'Microsoft.App/containerApps@2024-03-01' = {
  name: name
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: managedEnvironmentId
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: true
        targetPort: ingressTargetPort
        transport: 'auto'
      }
      secrets: [
        {
          name: 'scrapecreators-api-key'
          value: scrapecreatorsApiKey
        }
      ]
    }
    template: {
      containers: [
        {
          name: 'app'
          image: containerImage
          env: [
            {
              name: 'SCRAPECREATORS_API_KEY'
              secretRef: 'scrapecreators-api-key'
            }
          ]
          resources: {
            cpu: any('0.25')
            memory: '0.5Gi'
          }
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 1
      }
    }
  }
}

output id string = containerApp.id
output fqdn string = containerApp.properties.configuration.ingress.fqdn