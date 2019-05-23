# dBASEx
Tool for dBASE database files.

This project uses dBASE.NET fork as a dependency, which can be found here: 
https://github.com/lordstanius/dBASE.NET

## Options
```
  -diff  <name> <path to original> <path to modified>    Calculates changes in dBASE table.
  -patch <path to dbf> <path to diff>                    Applies changes to dBASE table. 
  -csv   <path>                                          Exports table data to CSV file.
  -sql   <path>                                          Prints SQL CREATE statement for the table.
```
