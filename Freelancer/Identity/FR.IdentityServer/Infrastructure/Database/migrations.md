﻿# Migration instructions for IdentityServer project

* `cd path-to\IdentityServer`
* change `UseInMemoryStores` to `false` in **appsettings.json**

### Create migrations
* Add-Migration Initial -Context ConfigurationDbContext -OutputDir "Infrastructure/Database/Migrations/IdentityServer/Configuration"
* Add-Migration Initial -Context PersistedGrantDbContext -OutputDir "Infrastructure/Database/Migrations/IdentityServer/Persisted"
* Add-Migration Initial -Context ApplicationDbContext -OutputDir "Infrastructure/Database/Migrations/Application"

### Update database
* Update-Database -Context PersistedGrantDbContext
* Update-Database -Context ConfigurationDbContext
* Update-Database -Context ApplicationDbContext