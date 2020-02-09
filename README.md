# OpenStreetMap AddressParser
OpenStreetMap Address parser helps you to fetch out all addresses from OSM files that you can generate via [OpenStreetMap Export tool](https://www.openstreetmap.org/export#map=13/44.8078/-68.7567) that generates a single XML file. The file contains nodes, relations, ways and so on but this tool only goes through nodes.

## Warning
As it seems there is no solid data structure for addresses in OpenStreetMap. For example in Czech republic there is a tag for country and conscriptionnumber but when you look at to London there is no country tag and the same goes for US addresses. *** CHECK YOUR OSM DATA AND AMEND THIS TOOL ACCORDINGLY***

## Motivation
Our motivation was to have a compact set of real addresses for [RentToday database](https://github.com/keenmate/renttoday-database) that would be coherent and compact yet small enough for testing purposes. The original Pagila database contained dummy, random addresses without Lat/Lng coordinates and those could not be used for real world testing purposes.

## Processing
- Parse XML file generated to data models
- Filter out only those nodes that contain addr:city tag, presumably all address nodes contains addr:city but it might not be true in your case
- Convert address tags and lat/lng coordinates to a single compact object
- Export to CSV/Json

## Side note
OpenStreetMap Export tool link leads to Bangor, Maine also know as Derry, Maine, city of many stories of Stephen King
