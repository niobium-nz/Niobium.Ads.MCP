@description('Name of the Container App.')
param name string

@description('Location of the Container App.')
param location string

@description('Resource ID of the managed environment.')
param managedEnvironmentId string

@description('Container image to deploy (e.g., docker.io/5he11/adslibrary:sha-<commit>).')
param containerImage string

@description('The external ingress target port for the container.')
param ingressTargetPort int = 80

@secure()
@description('ScrapeCreators API key passed to the container as SCRAPECREATORS_API_KEY.')
param scrapecreatorsApiKey string

@description('Custom domain name to bind to the Container App (host name). Leave empty to skip binding.')
param customDomain string = ''

@description('Custom domain certificate binding name. Defaults to the hostname.')
param customDomainBindingName string = customDomain

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

// Managed certificates require domain validation. With external DNS providers, validation records must be
// created outside of ARM/Bicep. This module creates the binding resources and outputs the expected records.
// Apply after you've created the required DNS records for the domain.

resource customDomainResource 'Microsoft.App/containerApps/customDomains@2024-03-01' = if (!empty(customDomain)) {
  name: customDomainBindingName
  parent: containerApp
  properties: {
    name: customDomain
    bindingType: 'SniEnabled'
    certificateBindingType: 'ManagedCertificate'
  }
}

output id string = containerApp.id
output fqdn string = containerApp.properties.configuration.ingress.fqdn

@description('The custom domain name requested for binding.')
output customDomainName string = customDomain

@description('Guidance for external DNS validation when using managed certificates.')
output customDomainValidationHelp object = !empty(customDomain) ? {
  guidance: 'Create required DNS validation records in your external DNS provider (e.g., Cloudflare), then re-run deployment.'
  records: {
    cname: {
      name: customDomain
      type: 'CNAME'
      value: containerApp.properties.configuration.ingress.fqdn
    }
  }
} : {
  guidance: 'No custom domain provided.'
}
