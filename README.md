# EocPortal2_WebApi

Visual Studio 2017

Tools > Nuget Package Management > Package Manager Console

select default project "SharedBlocks\Data\Hexagon.Data.EF"

execute below command to initialize Sqlite database

Add-Migration init

update-database init

after that, the eoc.db database will be generate under root folder of the project "WebApp\webapi" 
