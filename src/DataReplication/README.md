<!-- region Generated -->
# Az.DataReplication
This directory contains the PowerShell module for the DataReplication service.

---
## Status
[![Az.DataReplication](https://img.shields.io/powershellgallery/v/Az.DataReplication.svg?style=flat-square&label=Az.DataReplication "Az.DataReplication")](https://www.powershellgallery.com/packages/Az.DataReplication/)

## Info
- Modifiable: yes
- Generated: all
- Committed: yes
- Packaged: yes

---
## Detail
This module was primarily generated via [AutoRest](https://github.com/Azure/autorest) using the [PowerShell](https://github.com/Azure/autorest.powershell) extension.

## Module Requirements
- [Az.Accounts module](https://www.powershellgallery.com/packages/Az.Accounts/), version 2.7.5 or greater

## Authentication
AutoRest does not generate authentication code for the module. Authentication is handled via Az.Accounts by altering the HTTP payload before it is sent.

## Development
For information on how to develop for `Az.DataReplication`, see [how-to.md](how-to.md).
<!-- endregion -->

### AutoRest Configuration

> see <https://aka.ms/autorest>

``` yaml
skip-semantics-validation: true
require:
  - $(this-folder)/../readme.azure.noprofile.md
# lock the commit
input-file:
  - $(this-folder)/../swagger.json
module-version: 0.1.0
title: DataReplication
subject-prefix: $(service-name)

resourcegroup-append: true
nested-object-to-string: true

directive:
  - where:
      verb: Rename
      subject: ProtectedItemRecoveryPoint
    hide: true

  - where:
      verb: Invoke
    hide: true

  - where:
      verb: Test
    hide: true

  - where:
      verb: ^Get$|^New$
      subject: ProtectedItem
    hide: true  
    
  - where:
      verb: New
      subject: Policy
    hide: true     

  - where:
      subject: (.*)Status$
    remove: true

  - where:
      verb: New
      subject: ^Fabric$|^Dra$|^EmailConfiguration$
    remove: true

  - where:
      verb: Test
      subject: NameAvailability
    remove: true

  # For New-* cmdlets, ViaIdentity is not required, so CreateViaIdentityExpanded is removed as well

  - where:
      verb: Get
      variant: ^GetViaIdentity$|^GetViaIdentityExpanded$
    remove: true

  - where:
      verb: New
      variant: ^CreateViaIdentity$|^CreateViaIdentityExpanded$
    remove: true

  - where:
      verb: Test
      variant: ^TestViaIdentity$|^TestViaIdentityExpanded$|^CheckViaIdentity$|^CheckViaIdentityExpanded$
    remove: true

  - where:
      verb: Invoke
      variant: ^CommitViaIdentity$|^CommitViaIdentityExpanded$|^UnplannedViaIdentity$|^UnplannedViaIdentityExpanded$|^ReprotectViaIdentity$|^ReprotectViaIdentityExpanded$|^ResynchronizeViaIdentity|^ResynchronizeViaIdentityExpanded$|^PlannedViaIdentity$|^PlannedViaIdentityExpanded$|^DeploymentViaIdentity$|^DeploymentViaIdentityExpanded$
    remove: true

  - where:
      verb: Update
      variant: ^UpdateViaIdentity$|^UpdateViaIdentityExpanded$|^RefreshViaIdentity$
    remove: true

  - where:
      verb: Stop
      variant: ^CancelViaIdentity$
    remove: true

  - where:
      verb: Rename
      variant: ^ChangeViaIdentity$|^ChangeViaIdentityExpanded$
    remove: true

  - where:
      verb: remove
      variant: ^DeleteViaIdentity$
    remove: true

  # remove cmdlets

  # Rename cmdlets
  - where:
      verb: Update
    set:
      verb: Set

  - where:
      variant: List1
    set:
      variant: ListByResourceGroup

  - no-inline:
      - PolicyModelCustomProperties
      - ProtectedItemModelCustomProperties
      - VMwareToAvsDefaultDiskInput
      - ReprotectModelCustomProperties
      - TestFailoverModelCustomProperties
      - UnplannedFailoverModelCustomProperties
      - ReplicationExtensionModelCustomProperties
      - ReplicationExtensionModelCustomProperties
      - PlannedFailoverModelCustomProperties
      - EventModelCustomProperties

  - model-cmdlet:
      - VMwareToAvsPolicyModelCustomProperties
      - VMwareToAvsFailbackPolicyModelCustomProperties
      - VMwareToAvsFailbackProtectedItemModelCustomProperties  
       

```
