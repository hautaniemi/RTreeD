RTreeD
======

This is in the **very early stages of development** and has not been extensively tested. **Use at your own risk.**

RTreeD is a .NET library for 3D spatial indexing of rectangles using an R-tree data structure. The main use is to accelerate queries that find all items which overlap the query region. It is meant for indexing static data and only supports bulk loading using the OMT: Overlap Minimizing Top-down algorithm.

When completed the library will allow the indexing of 2D spatial datasets in [ECEF](https://en.wikipedia.org/wiki/Earth-centered,_Earth-fixed_coordinate_system) coordinates for use in accelerating spherical (or spheroidal) spatial analysis.

## Things to Do

- Knn queries
- Index persistence
- GDAL intergration
- Lat\Lon to ECEF conversion
- Test. Test. Test.

## Credit

This code was informed by [this](https://github.com/viceroypenguin/RBush) C# port of the [RBush](https://github.com/mourner/rbush) Javascript library. 

## Papers

* [R-trees: a Dynamic Index Structure For Spatial Searching](http://www-db.deis.unibo.it/courses/SI-LS/papers/Gut84.pdf)
* [OMT: Overlap Minimizing Top-down Bulk Loading Algorithm for R-tree](http://ftp.informatik.rwth-aachen.de/Publications/CEUR-WS/Vol-74/files/FORUM_18.pdf)
